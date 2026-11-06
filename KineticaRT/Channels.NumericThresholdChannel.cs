using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Security.Permissions;

namespace KineticaRT.Channels
{
	/// <summary>
	/// Base class for KineticaRT Channel objects containing numeric values.
	/// </summary>
	[ChangeRecord ("1.0", "04 11 2004", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="CEL Q09/010", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2004 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
	public class NumericThresholdChannel:NumericChannel, INumericChannel, ISerializable, INumericThresholdChannel
	{
		struct Threshold
		{
			public double				Value ;
			public System.Drawing.Color Color ;
		}
		private Threshold	lowLowThreshold ;
		private Threshold	lowThreshold ;
		private Threshold	normalThreshold ;
		private Threshold	highThreshold ;
		private Threshold	highHighThreshold ;
		private System.Drawing.Color current_color ;
        private string thresholdValuesMessage; //ft Utilisé pour afficher les valeurs limites des threshold dans un display pour tooltip

		/// <summary>Initializes a new instance of the class.</summary>
		public NumericThresholdChannel()
		{
			lowLowThreshold.Value = System.Double.NegativeInfinity ;
			lowThreshold.Value = System.Double.NegativeInfinity ;
			highThreshold.Value = System.Double.PositiveInfinity ;
			highHighThreshold.Value = System.Double.PositiveInfinity ;

			lowLowThreshold.Color = System.Drawing.Color.LightSteelBlue ;
			lowThreshold.Color = System.Drawing.Color.PaleTurquoise;
			highThreshold.Color = System.Drawing.Color.NavajoWhite ;
			highHighThreshold.Color = System.Drawing.Color.Salmon ;
			//Default normalColor = System.Drawing.Color.FromArgb(192, 192, 192) ;

		}
		
		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException("info") ;

			base.GetObjectData (info, context) ;
			info.AddValue ("LowLow", this.lowLowThreshold.Value) ;
			info.AddValue ("Low", this.lowThreshold.Value) ;
			info.AddValue ("Normal", this.normalThreshold.Value) ;
			info.AddValue ("High", this.highThreshold.Value) ;
			info.AddValue ("HighHigh", this.highHighThreshold.Value) ;
			info.AddValue ("LowLowColor", this.lowLowThreshold.Color) ;
			info.AddValue ("LowColor", this.lowThreshold.Color) ;
			info.AddValue ("HighColor", this.highThreshold.Color) ;
			info.AddValue ("HighHighColor", this.highHighThreshold.Color) ;
			info.AddValue ("NormalColor", this.normalThreshold.Color) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected NumericThresholdChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			try		
			{
				//lowLowThreshold.Value 	= info.GetDouble ("LowLow"); 
                lowLowThreshold.Value = KineticaRT.Convert.StringToDouble(info.GetString("LowLow"));
                lowThreshold.Value = KineticaRT.Convert.StringToDouble(info.GetString("Low"));
                normalThreshold.Value = KineticaRT.Convert.StringToDouble(info.GetString("Normal"));
                highThreshold.Value = KineticaRT.Convert.StringToDouble(info.GetString("High"));
                highHighThreshold.Value = KineticaRT.Convert.StringToDouble(info.GetString("HighHigh")); 
				lowLowThreshold.Color 	= (System.Drawing.Color)info.GetValue("LowLowColor", typeof(System.Drawing.Color)); 
				lowThreshold.Color 		= (System.Drawing.Color)info.GetValue("LowColor", typeof(System.Drawing.Color)); 
				highThreshold.Color 	= (System.Drawing.Color)info.GetValue("HighColor", typeof(System.Drawing.Color)); 
				highHighThreshold.Color = (System.Drawing.Color)info.GetValue("HighHighColor", typeof(System.Drawing.Color)); 
				normalThreshold.Color 	= (System.Drawing.Color)info.GetValue("NormalColor", typeof(System.Drawing.Color)); 
			}
			catch (System.Runtime.Serialization.SerializationException e)
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, e.Message, this.GetType().ToString()) ;
			}
		}
		#endregion


		#region Properties
		/// <summary>
		/// Gets or sets the LowLow threshold value of the channel.
		/// </summary>
		/// <value>The LowLow threshold value of the channel.</value>
		/// <remarks>
		/// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
		/// <para>Set to System.Double.NegativeInfinity if not required.</para>
		/// </remarks>
		[System.ComponentModel.Description("The LowLow threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")] 
		[DefaultValue(System.Double.NegativeInfinity)]
		public double LowLow
		{
			get
			{
				return lowLowThreshold.Value ;
			}
			set
			{
				lowLowThreshold.Value = value ;
                UpdateThresholdValuesMessage(); //ft
				OnPropertyChanged(LowLowPropertyChangedEventArgs) ;
				CheckColor () ;
			}
		}
		/// <summary>
		/// Gets or sets the Low threshold value of the channel.
		/// </summary>
		/// <value>The Low threshold value of the channel.</value>
		/// <remarks>
		/// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
		/// <para>Set to System.Double.NegativeInfinity if not required.</para>
		/// </remarks>
		[System.ComponentModel.Description("The Low threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")] 
		[DefaultValue(System.Double.NegativeInfinity)]
		public double Low
		{
			get
			{
				return lowThreshold.Value ;
			}
			set
			{
				lowThreshold.Value = value ;
                UpdateThresholdValuesMessage(); //ft
				OnPropertyChanged(LowPropertyChangedEventArgs) ;
				CheckColor () ;
			}
		}

		/// <summary>
		/// Gets or sets the Normal threshold value of the channel (Center Point).
		/// </summary>
		/// <value>The Normal threshold value of the channel.</value>
		/// <remarks>
		/// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
		/// <para>Set to System.Double.NegativeInfinity if not required.</para>
		/// </remarks>
		[System.ComponentModel.Description("The Normal threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")] 
		[DefaultValue(System.Double.NegativeInfinity)]
		public double Normal
		{
			get
			{
				return normalThreshold.Value ;
			}
			set
			{
				normalThreshold.Value = value ;
                UpdateThresholdValuesMessage(); //ft
				OnPropertyChanged(NormalPropertyChangedEventArgs) ;
				CheckColor () ;
			}
		}

		/// <summary>
		/// Gets or sets the LowLow threshold value of the channel.
		/// </summary>
		/// <value>The High threshold value of the channel.</value>
		/// <remarks>
		/// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
		/// <para>Set to System.Double.PositiveInfinity if not required.</para>
		/// </remarks>
		[System.ComponentModel.Description("The High threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")] 
		[DefaultValue(System.Double.PositiveInfinity)]
		public double High
		{
			get
			{
				return highThreshold.Value ;
			}
			set
			{
				highThreshold.Value = value ;
                UpdateThresholdValuesMessage(); //ft
				OnPropertyChanged(HighPropertyChangedEventArgs) ;
				CheckColor () ;
			}
		}

		/// <summary>
		/// Gets or sets the HighHigh threshold value of the channel.
		/// </summary>
		/// <value>The HighHigh threshold value of the channel.</value>
		/// <remarks>
		/// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
		/// <para>Set to System.Double.PositiveInfinity if not required.</para>
		/// </remarks>
		[System.ComponentModel.Description("The HighHigh threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")] 
		[DefaultValue(System.Double.PositiveInfinity)]
		public double HighHigh
		{
			get
			{
				return highHighThreshold.Value ;
			}
			set
			{
				highHighThreshold.Value = value ;
                UpdateThresholdValuesMessage(); //ft
				OnPropertyChanged(HighHighPropertyChangedEventArgs) ;
				CheckColor () ;
			}
		}
		/// <summary>
		/// Gets or sets the background color of the normal value
		/// </summary>
		[Category("Threshold")]
		[Description("Gets or sets the color of the normal value")]
		public System.Drawing.Color NormalColor
		{
			get
			{
				return normalThreshold.Color ;
			}
			set
			{
				if (normalThreshold.Color != value)
				{
					normalThreshold.Color = value ;
					OnPropertyChanged(NormalColorPropertyChangedEventArgs) ;
					CheckColor () ;
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the background color of the value when below the LowLow Threshold.
		/// </summary>
		[Category("Threshold")]
		[Description("Gets or sets the background color of the value when below the LowLow Threshold.")]
		public System.Drawing.Color LowLowColor
		{
			get
			{
				return lowLowThreshold.Color ;
			}
			set
			{
				if (lowLowThreshold.Color != value)
				{
					lowLowThreshold.Color = value ;
					OnPropertyChanged(LowLowColorPropertyChangedEventArgs) ;
					CheckColor () ;
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the background color of the value when below the Low Threshold.
		/// </summary>
		[Category("Threshold")]
		[Description("Gets or sets the background color of the value when below the Low Threshold.")]
		public System.Drawing.Color LowColor
		{
			get
			{
				return lowThreshold.Color ;
			}
			set
			{
				if (lowThreshold.Color != value)
				{
					lowThreshold.Color = value ;
					OnPropertyChanged(LowColorPropertyChangedEventArgs) ;
					CheckColor () ;
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the background color of the value when above the High Threshold.
		/// </summary>
		[Category("Threshold")]
		[Description("Gets or sets the background color of the value when above the High Threshold.")]
		public System.Drawing.Color HighColor
		{
			get
			{
				return highThreshold.Color ;
			}
			set
			{
				if (highThreshold.Color != value)
				{
					highThreshold.Color = value ;
					OnPropertyChanged(HighColorPropertyChangedEventArgs) ;
					CheckColor () ;
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the background color of the value when above the HighHigh Threshold.
		/// </summary>
		[Category("Threshold")]
		[Description("Gets or sets the background color of the value when above the HighHigh Threshold.")]
		public System.Drawing.Color HighHighColor
		{
			get
			{
				return highHighThreshold.Color ;
			}
			set
			{
				if (highHighThreshold.Color != value)
				{
					highHighThreshold.Color = value ;
					OnPropertyChanged(HighHighColorPropertyChangedEventArgs) ;
					CheckColor () ;
				}
			}
		}
		
		/// <summary>
		/// Gets the current background color for the value.
		/// </summary>
		[Browsable(false)]
		[Description("Gets the current color.")]
		public System.Drawing.Color Color
		{
			get
			{
				return current_color ;
			}
		}


        /// <summary>
        /// Gets the different values of each Threshold in a string format
        /// </summary>
        [Browsable(false)]
        [Description("Gets the different values of each Threshold in a string format to display in tooltip over display controls")]
        public string ThresholdValuesMessage
        {
            get
            {
                return thresholdValuesMessage;
            }
        }
		/// <summary>Gets or sets the value of this channel.</summary>
		/// <value>A Value object representing the value of the channel.</value>
		[System.ComponentModel.Description("Value of channel."),System.ComponentModel.Category("Channel")] 
		public override KineticaRT.Values.Value Value
		{
			get
			{
				return base.Value ;
			}
			set
			{
				base.Value = value ;
				CheckColor () ;
			}
		}

		private void CheckColor ()
		{
			if (base.RawValue== null) return ;
			double val = base.RawValue.ToDouble() ;
			System.Drawing.Color new_color ;
			
			if (val < lowLowThreshold.Value)
			{
				new_color = lowLowThreshold.Color ;
			}
			else if (val < lowThreshold.Value)
			{
				new_color = lowThreshold.Color ;
			}
			else if (val > highHighThreshold.Value)
			{
				new_color = highHighThreshold.Color ;
			}
			else if (val > highThreshold.Value)
			{
				new_color = highThreshold.Color ;
			}
			else 
			{
				new_color = normalThreshold.Color ;
			}

			if (new_color != current_color)
			{
				current_color = new_color ;
				OnPropertyChanged (ColorPropertyChangedEventArgs) ;
			}
		}
        private void UpdateThresholdValuesMessage() //ft
        {
            int count = 0;
            thresholdValuesMessage = "";
            if (highHighThreshold.Value != System.Double.PositiveInfinity)
            {
                thresholdValuesMessage += "HIHI   " + highHighThreshold.Value.ToString(base.Format, base.Info);
                count++;
            }
            if (highThreshold.Value != System.Double.PositiveInfinity)
            {
                if (count > 0) thresholdValuesMessage += "\n";
                thresholdValuesMessage += "HI       " + highThreshold.Value.ToString(base.Format, base.Info);
                count++;
            }            
            if (lowThreshold.Value != System.Double.NegativeInfinity)
            {
                if (count > 0) thresholdValuesMessage += "\n";
                thresholdValuesMessage += "LO      " + lowThreshold.Value.ToString(base.Format, base.Info);
                count++;
            }            
            if (lowLowThreshold.Value != System.Double.NegativeInfinity)
            {
                if (count > 0) thresholdValuesMessage += "\n";
                thresholdValuesMessage += "LOLO " + lowLowThreshold.Value.ToString(base.Format, base.Info);
                count++;
            }            
        }
        
		#endregion

	}
}
