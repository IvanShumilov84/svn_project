using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Diagnostics ;
using System.Windows.Forms;
using KineticaRT;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
	/// <summary>
	/// ButtonInterlock Class. Links a collection of Button controls,
	/// only allowing a maximum of one to be 'on' at once.
	/// </summary>
#if KRT_license
	[LicenseProviderAttribute(typeof(Button.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(ButtonInterlock))]
	public class ButtonInterlock : System.ComponentModel.Component, IConnectable
	{
		#region Variables
		private KineticaRT.Controls.ValueChangedEventHandler	handler	;
		//all buttons
		private ArrayList					buttonList	= new ArrayList() ;
		//buttons added by name only
		private ArrayList					nameList	= new ArrayList() ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the ButtonInterlock class
		/// </summary>
		public ButtonInterlock()
		{
#if KRT_license
			GetLicense() ;
#endif

			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();
				
			handler = new ValueChangedEventHandler(OnChanged) ;
		}

		/// <summary>
		/// Initialises a new instance of the ButtonInterlock class with the specified Container
		/// </summary>
		/// <param name="container">Container</param>
		public ButtonInterlock(System.ComponentModel.IContainer container)
		{
#if KRT_license
			GetLicense() ;
#endif

			container.Add(this);
			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();

			handler = new ValueChangedEventHandler(OnChanged) ;
		}

#if KRT_license
		private void GetLicense()
		{
			//exception if no license
			License		license			= null;
			bool		unlicensed ;
			unlicensed = !LicenseManager.IsValid (typeof(Button), this, out license) || (license.LicenseKey != "Run");
			if (license != null) {license.Dispose();license = null;}

			if (unlicensed)		throw new LicenseException(typeof(Button), this, typeof(Button).FullName+": Invalid Licence Key - Please contact www.KineticaRT.co.uk") ;	
		}
#endif
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets an array of text names of buttons for this object to interlock
		/// </summary>
		public string[] NamedButtons
		{
			get
			{
				string []		array = new string[nameList.Count] ;

				nameList.CopyTo(array) ;
				return array ;
			}
			set
			{
				//##not sure about this - seems drastic
				//? just remove any in current list that aren't in new list?
				// - not easy - atm leave like this, which works fine
				Clear() ;

				foreach (string str in value)
				{
					Add(str) ;
				}

				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("NamedButtons")) ;
			}
		}
			#endregion

		#region Public methods
		/// <summary>
		/// Add a button
		/// </summary>
		/// <param name="newButton">Button to be added</param>
		public void Add (IButton newButton)
		{
			if (!buttonList.Contains(newButton))
			{
				buttonList.Add(newButton) ;
				//sink its on changed events
				newButton.ValueChanged += handler ;
			}
		}

		/// <summary>
		/// Add a button by name
		/// </summary>
		/// <param name="buttonName">Name of Button to add</param>
		/// <remarks>If a button of the given name can be found it is connected immediately</remarks>
		public void Add (string buttonName)
		{
			if (!nameList.Contains(buttonName))
			{
				IButton		newButton ;

				//add it to the name list
				nameList.Add(buttonName) ;
					
				//find the actual object...
				newButton = OnObjectSearch(buttonName) as IButton ;
				if (newButton != null)
				{
					//...and add it
					Add(newButton) ;
				}
			}
		}

		/// <summary>
		/// Remove all buttons from the interlock.
		/// </summary>
		public void Clear ()
		{
			foreach (IButton btn in buttonList)
			{
				btn.ValueChanged -= handler ;
			}
			buttonList.Clear() ;
			nameList.Clear() ;
		}

		/// <summary>
		/// Tests to see if the given button is in the List
		/// </summary>
		/// <param name="testButton">Button to check</param>
		/// <returns>True if Button is in the list</returns>
		public bool Contains (IButton testButton)
		{
			return buttonList.Contains(testButton) ;
		}

		/// <summary>
		/// Tests to see if the named button is in the List.
		/// Only checks buttons set by name
		/// </summary>
		/// <param name="testName">Button name to test</param>
		/// <returns>True if the name is in the list</returns>
		public bool Contains (string testName)
		{
			return nameList.Contains(testName) ;
		}

		/// <summary>
		/// Removes the given button from the List
		/// </summary>
		/// <param name="byeByeButton">Button to remove</param>
		public void Remove (IButton byeByeButton)
		{
			if (buttonList.Contains(byeByeButton))
			{
				//remove handler
				byeByeButton.ValueChanged -= handler ;
				buttonList.Remove(byeByeButton) ;
			}
		}

		/// <summary>
		/// Removes the named button from the List
		/// </summary>
		/// <param name="byeByeName">Name of Button to remove</param>
		public void Remove (string byeByeName)
		{
			if (nameList.Contains(byeByeName))
			{
				nameList.Remove(byeByeName) ;

				//find the actual object in the main list
				for (int index = 0; index < buttonList.Count; index++)
				{
					if (((IButton)buttonList[index]).Name == byeByeName)
					{
						//remove handler
						((IButton)buttonList[index]).ValueChanged -= handler ;
						buttonList.RemoveAt(index) ;
						break ;
					}
				}
			}
		}
		#endregion

		#region Other methods
		private void OnChanged(object sender, KineticaRT.Values.Value newVal)
		{
			if (newVal.ToDouble() != 0)
			{
				//set all buttons to unpressed, except the one just pressed
				foreach (IButton btn in buttonList)
				{
					if (!sender.Equals(btn))
					{
						btn.SetButtonState(false) ;
					}
				}
			}
		}

		#endregion
	
		#region IConnectable
		/// <summary>
		/// Event which fires when the value of a property has changed.
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged ;   

		/// <summary>
		/// Event for requests to find an object by name.
		/// </summary>
		public event KineticaRT.ObjectSearchEventHandler ObjectSearch;   
		/// <summary>
		/// Fires an ObjectSearch event for the named object
		/// </summary>
		protected object OnObjectSearch(string objectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(objectName);  
		}

		/// <summary>
		/// Looks for the named object
		/// </summary>
		/// <param name="objectName">Name of Object to find</param>
		/// <returns>Always returns null</returns>
		public virtual object FindObject(string objectName)
		{
			return null ;
		}

		/// <summary>
		/// Returns the named sibling object or null if it does not exist.
		/// </summary>
		public virtual object SearchObject (string objectName)
		{
			return OnObjectSearch (objectName) ;
		}

		/// <summary>
		/// Connects this object to others.
		/// </summary>
		/// <remarks>Any buttons added by name are sought and connected to if possible</remarks>
		public virtual void Connect ()
		{
			//should any old (invalid) connections be cleared?

			for (int index = 0; index < nameList.Count; index++)
			{
				//find the actual object...
				IButton		newButton = OnObjectSearch((string)nameList[index]) as IButton ;
				if (newButton != null)
				{
					//...and add it
					Add(newButton) ;
				}
			}
            this.Connected = true; //ft
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
			//clears all objects, but no names

			foreach (IButton btn in buttonList)
			{
				btn.ValueChanged -= handler ;
			}
			buttonList.Clear() ;
            this.Connected = false; //ft
		}
        
        private bool connected = false;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool Connected //ft
        {
            get { return connected; }
            set { connected = value; }
        }
		#endregion
	}
}