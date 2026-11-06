using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.Serialization ;
using System.Reflection;
using System.Windows.Forms;
using System.CodeDom;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Collection of Panel objects and associated information 
	/// designed for use with a KineticaRT Tab control
	/// </summary>
	[TypeConverter(typeof(PanelCollection.PanelCollConverter))]
	public class PanelCollection : SubObjectCollection
	{
		#region Variables and Declarations
		/// <summary>Event Handler for Panel added / removed events</summary>
		/// <param name="sender">The source of the event.</param>
		public delegate void PanelChangedEventHandler(object sender) ;//, string PanelName);
		/// <summary>
		/// Event generated when a panel is added to the collection
		/// </summary>
		public event PanelChangedEventHandler		PanelAdded ;
		/// <summary>
		/// Event generated when a panel is removed from the collection
		/// </summary>
		public event PanelChangedEventHandler		PanelRemoved ;

//		/// <summary>
//		/// Event generated when a panel is renamed
//		/// </summary>
//		public event PanelInfo.PanelRenamedEventHandler		PanelRenamed ;
//
//		private PanelInfo.PanelRenamedEventHandler	renamedHdlr ;
		#endregion

		#region Type Converter
		internal class PanelCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					PanelCollection		coll		= (PanelCollection) value;
					PanelInfo []		info		= new PanelInfo [coll.Count] ;
					ConstructorInfo		ctor_info ;
					
					ctor_info = typeof(PanelCollection).GetConstructor(new Type[]{typeof(PanelInfo[])});

						//copy info into array
						for (int index = 0; index < coll.Count; index++)
						{
							info[index] = coll[index] ;
						}

					return new InstanceDescriptor(ctor_info, new object[]{info}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the PanelCollection class
		/// </summary>
		public PanelCollection()
		{
//			renamedHdlr = new PanelInfo.PanelRenamedEventHandler(OnPanelRenamed) ;
		}

		/// <summary>
		/// Initialises a new instance of the PanelCollection class containing the specified PanelInfo objects
		/// </summary>
		/// <param name="panelInfo">Array of PanelInfo objects to add to collection</param>
		public PanelCollection(PanelInfo [] panelInfo)
		{
//			renamedHdlr = new PanelInfo.PanelRenamedEventHandler(OnPanelRenamed) ;
			AddRange(panelInfo) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the PanelInfo object currently at the given index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public PanelInfo this[int Index]
		{
			get
			{
				return (PanelInfo) List[Index] ;
			}
			set
			{
				if (value != List[Index])
				{
					List[Index] = value ;
				}
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new PanelInfo object with the specified Panel to the collection
		/// </summary>
		/// <param name="Text">Text of new Panel</param>
		public void AddPanel(string Text) //, string Name)
		{
			this.List.Add(new PanelInfo(Text)) ;//, Name)) ;
		}

		/// <summary>
		/// Adds an array of PanelInfo objects to the collection
		/// </summary>
		/// <param name="newInfo">Array of PanelInfo objects to add</param>
		public void AddRange(PanelInfo [] newInfo)
		{
			for (int index = 0; index < newInfo.Length; index++)
			{
				this.List.Add(newInfo[index]) ;
			}
		}

		#endregion

		#region Private methods
		/// <summary>
		/// List InsertComplete Event
		/// </summary>
		/// <param name="Index">Index of inserted object</param>
		/// <param name="Value">Inserted object</param>
		protected override void OnInsertComplete(int Index, object Value)
		{
			((PanelInfo)Value).PropertyChanged	+= propertyHdlr ;
			((PanelInfo)Value).ObjectSearch		+= searchHdlr ;

			if (PanelAdded != null)
			{
				PanelAdded(Value) ;//, ((PanelInfo)Value).Name) ;
			}

//			((PanelInfo)Value).PanelRenamed += renamedHdlr ;

			base.OnInsertComplete(Index, Value) ;
		}

		/// <summary>
		/// Clear Event
		/// </summary>
		protected override void OnClear()
		{
			//this gets called from collection editor if anything added
			//or removed. If I fire a Removed event, the panel objects themselves
			//go, along with any objects on them.
			//			foreach (PanelInfo info in this)
			//			{
			//				if (PanelRemoved != null)		PanelRemoved(this, info.Name) ;
			//				info.PanelRenamed -= renamedHdlr ;
			//			}
			base.OnClear() ;
		}

		/// <summary>
		/// List RemoveComplete Event
		/// </summary>
		/// <param name="Index">Index of removed object</param>
		/// <param name="Value">Removed object</param>
		protected override void OnRemoveComplete(int Index, object Value)
		{
			((PanelInfo)Value).PropertyChanged	-= propertyHdlr ;
			((PanelInfo)Value).ObjectSearch		-= searchHdlr ;

			if (PanelRemoved != null)
			{
				PanelRemoved(Value) ; //, ((PanelInfo)Value).Name) ;
			}
			
//			((PanelInfo)Value).PanelRenamed -= renamedHdlr ;

			base.OnRemoveComplete(Index, Value) ;
		}

//		private void OnPanelRenamed(object sender, string OldName, string NewName)
//		{
//			if (PanelRenamed != null)
//			{
//				PanelRenamed(this, OldName, NewName) ;
//			}
//		}

		/// <summary>
		/// Looks for the named object
		/// </summary>
		/// <param name="objectName">Name of Object to find</param>
		/// <returns>Always returns null</returns>
		public override object FindObject(string objectName)
		{
			foreach (SubObjectBase obj in this)
			{
				object found_obj = obj.FindObject (objectName) ;
				if (found_obj != null) return found_obj ;
			}

			return null ;
		}

		#endregion
	}


	/// <summary>
	/// PanelInfo object containing information about Panel objects.
	/// Designed for use with a KineticaRT Tab control
	/// </summary>
	[TypeConverter(typeof(ObjectTypeConverter))]
	[DefaultProperty("Panel")]
	//[DesignerSerializer(typeof(PISerializer), typeof(CodeDomSerializer))]
	public class PanelInfo : KineticaRT.Controls.SubObjectBase
	{
		#region SubObjectBase stuff
//		#region Property Changed handling
//		/// <summary>
//		/// Event generated whenever a property (including Value) changes
//		/// </summary>
//		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;   
//		/// <summary>
//		/// The named property has changed: generate the PropertyChanged event
//		/// </summary>
//		/// <param name="propertyName">Name of property that has changed</param>
//		protected virtual void OnPropertyChanged(string propertyName)
//		{
//			if (PropertyChanged != null)
//			{
//				PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));  //Invokes the delegates.
//			}
//		}
//		#endregion
//
//		#region IsDefaultState
//		/// <summary>
//		/// Determines if the object is in its default state (and therefore does not require saving)
//		/// </summary>
//		/// <returns>True if default state</returns>
//		public virtual bool IsDefaultState()
//		{
//			// - always save!
//			return false ;
////			foreach (PropertyDescriptor desc in TypeDescriptor.GetProperties(this))
////			{
////				if (desc.ShouldSerializeValue(this))
////				{
////					//not in default state
////					return false ;
////				}
////			}
////
////			//nothing needs serializing => it is in default state
////			return true ;
//		}
//		#endregion
//
//		#region IConnectable
//		/// <summary>
//		/// Event for requests to find an object by name.
//		/// </summary>
//		public event KineticaRT.ObjectSearchEventHandler ObjectSearch;   
//		/// <summary>
//		/// Fires an ObjectSearch event for the named object
//		/// </summary>
//		protected object OnObjectSearch(string objectName)
//		{
//			if (ObjectSearch == null) return null ;
//			return ObjectSearch(objectName);  
//		}
//
		/// <summary>
		/// Looks for the named object
		/// </summary>
		/// <param name="objectName">Name of Object to find</param>
		/// <returns>Object which matches objectName or null if not found.</returns>
		public override object FindObject(string objectName)
		{
			return this.Panel.FindObject(objectName) ;
		}

//		/// <summary>
//		/// Connects this object to others.
//		/// </summary>
//		public virtual void Connect ()
//		{
//		}
//
//		/// <summary>
//		/// Disconnects this object from others.
//		/// </summary>
//		public virtual void Disconnect ()
//		{
//		}
//		#endregion
		#endregion

		#region Variables and Declarations
//		/// <summary>Event Handler for Panel renamed</summary>
//		/// <param name="sender">The source of the event (PanelInfo).</param>
//		/// <param name="OldName">The old name of the Panel.</param>
//		/// <param name="NewName">The new name of the Panel.</param>
//		public delegate void PanelRenamedEventHandler(object sender, string OldName, string NewName);
//		/// <summary>
//		/// Event generated when a panel is renamed
//		/// </summary>
//		public event PanelRenamedEventHandler		PanelRenamed ;

		//property variables
		internal	Rectangle			rect ;

		private		string				text ;
//		private		string				name ;
		private		string				dllName ;
		private		PanelBase			myPanel ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the PanelInfo class
		/// </summary>
		public PanelInfo()
		{
			Initialise("New Tab") ;//, "panel" + Environment.TickCount) ;
		}

		/// <summary>
		/// Initialises a new instance of the PanelInfo class with the specified name and Text
		/// </summary>
		/// <param name="Text">Associated Text</param>
		public PanelInfo(string Text) //, string Name)
		{
			Initialise(Text) ;//, Name) ;
		}

		private void Initialise(string Text) //, string Name)
		{
			rect = Rectangle.Empty ;

			text = Text ;
			//dllName = "" ;
            dllName = System.IO.Path.ChangeExtension(Text.Replace(" ", ""), "dll");
            //name = Name ;

			//##this should load the correct panel dll?
//			myPanel = new PanelBase(Name) ;
		}

		#endregion

		#region Properties
		///<summary>
		/// Gets or sets the Text associated with the Panel
		///</summary>
		[Category("Tab")]
		[Description("Text")]
		[DefaultValue("New Tab")]
		public string Text
		{
			get
			{
				return text ;
			}
			set
			{
				if (text != value)
				{
					text = value ;
					OnPropertyChanged("Text") ;
				}
			}
		}

		///<summary>
		/// Gets or sets the DLL Name from which the Panel is loaded
		///</summary>
		/// <remarks>
		/// Extensions other than '.dll' will be changed to '.dll'.
		/// If path is not specified, the file is relative to the path specified in the SystemDirectory property.
		/// If the specified file exists it will be loaded, and current controls
		/// will be unloaded. If the file does not exist the current controls will
		/// remain unchanged. When the frame is saved, current controls will be saved
		/// to the file specified here, and the object saved within the dll will be
		/// case sensitively named after the dll. Note that save will not be available if no 
		/// DllName is specified.
		/// </remarks>
		[Category("Tab")]
		[Description("DLL Name")]
		[DefaultValue("")]
		public string DllName
		{
			get
			{
				return dllName ;
			}
			set
			{
				value = System.IO.Path.ChangeExtension(value, ".dll") ;

				//if (dllName != value)
				{
					dllName = value ;
					OnPropertyChanged("DllName") ;
				}
			}
		}

//		///<summary>
//		/// Gets or sets the Name of the Panel object
//		///</summary>
//		[Category("Tab")]
//		[Description("Name")]
//		[DefaultValue("")]
//		public string Name
//		{
//			get
//			{
//				return name ;
//			}
//			set
//			{
//				if (name != value)
//				{
//					OnRenamed(name, value) ;
//					name = value ;
//					OnPropertyChanged("Name") ;
//				}
//			}
//		}

		/// <summary>
		/// Gets or sets the PanelBase object associated with this page
		/// </summary>
		[Category("Tab")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public PanelBase Panel
		{
			get
			{
				return myPanel ;
			}
			set
			{
				if (value != null)
				{
					myPanel = value ;
//					myPanel.Name = name ;
					OnPropertyChanged("Panel") ;
				}
			}
		}
		private bool ShouldSerializePanel()
		{
			return (dllName == "") ;
		}
		#endregion

		#region Other Methods (none)
//		private void OnRenamed(string oldName, string newName)
//		{
//			if (PanelRenamed != null)
//			{
//				PanelRenamed(this, oldName, newName) ;
//			}
//		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
        public override void Connect()
        {
            base.Connect();

            if (myPanel != null) myPanel.Connect();
        }

        /// <summary>
        /// Connects this object to others, stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        public override void Connect2()
        {
            base.Connect2();

            if (myPanel != null) myPanel.Connect2();
        }

        /// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect() ;

			if (myPanel != null)	myPanel.Disconnect() ;
		}
		#endregion (connection)
	}
}
