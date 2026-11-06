using System;
using KineticaRT;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace KineticaRT.Logic.Filters
{
	/// <summary>
	/// Performs a low pass first order linear filter on values it is set to or on values from a Source channel.
	/// </summary>
	[DefaultProperty("Value")]
	////[LicenseProviderAttribute(typeof(LowPass.LicenseProvider))]
	public class LowPass:KineticaRT.Channels.Channel, ISerializable
	{
		#region License
//		// Do not move this class or its contents into another namespace - it would not be secure.
//	
//		////[LicenseProviderAttribute(typeof(Class.LicenseProvider))]
//		//private License license = null;
//		//license = LicenseManager.Validate(this.GetType(), this);
//		////demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
//		//if (license != null) {license.Dispose();license = null;}
//		/// <summary>
//		/// Implements a license provider for the class.
//		/// </summary>
//		public class LicenseProvider: System.ComponentModel.LicenseProvider
//		{
//			/// <summary>
//			/// Gets a license for an instance or type of component, when given a context and whether the denial of a license throws an exception
//			/// </summary>
//			/// <param name="context">A LicenseContext that specifies where you can use the licensed object.</param>
//			/// <param name="type">A Type that represents the component requesting the license.</param>
//			/// <param name="instance">An object that is requesting the license. </param>
//			/// <param name="allowExceptions">true if a LicenseException should be thrown when the component cannot be granted a license; otherwise, false.</param>
//			/// <returns>A valid License.</returns>
//			public override System.ComponentModel.License GetLicense(LicenseContext context,Type type,object instance,bool allowExceptions)
//			{
//				License licence = null ;
//
//				if (context != null)
//				{
//					string key = null ;
//
//					if (context.UsageMode == LicenseUsageMode.Designtime)
//					{
//						Microsoft.Win32.RegistryKey registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+"KineticaRT.Logic.Filters") ; 
//						if (registry != null) key = registry.GetValue ("RegKey") as string ;
//						if ((key == null) || (key != "D2441394053D" && key != "4CD0571EE68D"))
//						{
//							new KineticaRT.LicenceForm ("KineticaRT.Logic.Filters").ShowDialog () ;
//							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+"KineticaRT.Logic.Filters") ; 
//							if (registry != null) key = registry2.GetValue ("RegKey") as string ;
//						}
//						if ((key != null) && (key == "D2441394053D" || key == "4CD0571EE68D")) key = "{D2441394-053D-4926-9A80-8AF59524F237}" ; 
//						context.SetSavedLicenseKey (typeof(LowPass), key) ;
//					}
//					if  (key == null) key = context.GetSavedLicenseKey (typeof(LowPass), null) ;
//					if (key == "{D2441394-053D-4926-9A80-8AF59524F237}") licence = new License ("Run") ;
//					else if (allowExceptions && (context.UsageMode != LicenseUsageMode.Designtime)) throw new LicenseException (type, instance, type.FullName+": Invalid Licence Key - Please contact www.KineticaRT.co.uk") ;
//					else licence = new License ("None") ;
//				}
//
//				return licence ;
//			}
//
//			private class License:System.ComponentModel.License 
//			{
//				string key ;
//
//				public License (string key)
//				{
//					this.key = key ;
//				}
//
//				public override string LicenseKey 
//				{
//					get
//					{
//						return key ;
//					}
//				}
//		
//				public override void Dispose()
//				{
//				}
//			}
//		}
		#endregion
		
		bool connected ;
		bool update_connected ;
		string source ;
		string destination ;
		KineticaRT.Channels.Channel sourceChannel ;
		KineticaRT.Channels.Channel destinationChannel ;

		private KineticaRT.Updater.UpdateEventHandler hdlr ;
		double old_value = 0 ;
		bool failed = true ;
		/// <summary>Current value of Source channel.</summary>
		protected double currentValue = 0 ;

		double t = 1.0 ;
		double rate = KineticaRT.Services.FastUpdater.UpdateRate ;
		double ratio ; 

		/// <summary>Initializes a new instance of the class.</summary>
		public LowPass()
		{
//			License license = null;
//			license = LicenseManager.Validate(this.GetType(), this);
//			if (license != null) {license.Dispose();license = null;}
			
			if (hdlr == null) hdlr = new KineticaRT.Updater.UpdateEventHandler(update_proc);
			ratio = Math.Exp (-rate / t) ;
		}

		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			GetObjectData (info, context) ;
		}
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		protected new void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected LowPass (SerializationInfo info, StreamingContext context):base(info, context)
		{
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
			
			if (hdlr == null) hdlr = new KineticaRT.Updater.UpdateEventHandler(update_proc);

			try
			{
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				System.Diagnostics.Trace.WriteLine ("Configuration is missing a property for " + GetType()) ;
			}

			ratio = Math.Exp (-rate / t) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of the source channel.
		/// </summary>
		/// <value>The name of the source channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a source channel.
		/// <para>The channel will not connect to the source until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of source channel"),System.ComponentModel.Category("Filter")] 
		public string Source
		{
			get
			{
				return source ;
			}
			set
			{
				if (source != value)
				{
					source = value ;
					OnPropertyChanged("Source") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the name of the destination channel.
		/// </summary>
		/// <value>The name of the destination channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a destination channel.
		/// <para>The channel will not connect to the destination until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of destination channel"),System.ComponentModel.Category("Filter")] 
		public string Destination
		{
			get
			{
				return destination ;
			}
			set
			{
				if (destination != value)
				{
					destination = value ;
					OnPropertyChanged("Destination") ;
				}
			}
		}

		/// <summary>Gets or sets the value of this channel.</summary>
		/// <value>A Value object representing the value of the channel.</value>
		[System.ComponentModel.Description("The value of this channel. "), System.ComponentModel.Category("Channel")] 
		public override KineticaRT.Values.Value Value
		{
			get
			{
				return base.Value ;
			}
			set
			{
				currentValue = value.ToDouble() ;
			}
		}
		
		/// <summary>
		/// Gets or sets the time constant T.
		/// </summary>
		/// <value>The value of T.</value>
		/// <remarks>A typical value for T is 1.0 seconds.
		/// </remarks>
		[System.ComponentModel.Description("Value of T"),System.ComponentModel.Category("Filter")] 
		public double T
		{
			get
			{
				return t ;
			}
			set
			{
				if (t != value)
				{
					t = value ;
					OnPropertyChanged("T") ;

					ratio = Math.Exp (-rate / t) ;
					if (System.Double.IsInfinity(ratio)) ratio = 1 ;
				}
			}
		}

		#endregion

		#region Logic
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			lock (this)
			{
				failed = true ;
				if (!update_connected)
				{
					KineticaRT.Services.FastUpdater.Update += hdlr ;
					update_connected = true ;
				}

				if (connected && sourceChannel != null)sourceChannel.ValueChanged -= new KineticaRT.Channels.ValueChangedEventHandler(This_OnValueChanged);
				
				if (destination != null && destination.Length > 0)
				{
					destinationChannel = OnObjectSearch (destination) as KineticaRT.Channels.Channel  ;
				}

				if (source != null && source.Length > 0)
				{
					sourceChannel = OnObjectSearch (source) as KineticaRT.Channels.Channel  ;
			
					if (sourceChannel != null)
					{
						Value = sourceChannel.Value ;
						sourceChannel.ValueChanged += new KineticaRT.Channels.ValueChangedEventHandler(This_OnValueChanged);
					}
					else
					{
						Value = KineticaRT.Values.Values.NotConnected ;
					}
				}

				connected = true ;
			}
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect () ;
			
			lock (this)
			{
				if (update_connected)
				{
					KineticaRT.Services.FastUpdater.Update -= hdlr ;
					update_connected = false ;
				}

				if (connected)
				{
					sourceChannel.ValueChanged -= new KineticaRT.Channels.ValueChangedEventHandler(This_OnValueChanged);
					Value = KineticaRT.Values.Values.NotConnected ;
					sourceChannel = null ;
					destinationChannel = null ;
					connected = false ;
				}
			}
		}

		private void This_OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			currentValue = newVal.ToDouble() ;
		}

		private void update_proc()
		{
			if (!failed)
			{
				if (System.Double.IsNaN(currentValue) || System.Double.IsNaN(t))
				{
					failed = true ;
					Value = KineticaRT.Values.Values.TypeMismatch ;
				}
				old_value = old_value * ratio + currentValue * (1 - ratio) ;
			}
			else
			{
				old_value = currentValue ;
				if (System.Double.IsNaN(old_value)) Value = KineticaRT.Values.Values.TypeMismatch ;
			}
			
			System.Diagnostics.Trace.WriteLine("Calc:"+old_value.ToString()) ;
			SetValue (old_value) ;
			if (destinationChannel != null) destinationChannel.Value = base.Value ;
			failed = false ;
		}

		/// <summary>
		/// Called to set the current value of the object to a new value.
		/// </summary>
		/// <param name="newVal">The new value of the object.</param>
		protected virtual void SetValue (double newVal)
		{
			base.Value = new KineticaRT.Values.Double(newVal) ;
		}
		#endregion
	}
}
