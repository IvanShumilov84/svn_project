using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using KineticaRT;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Basic features of a SubObject.
	/// </summary>
	public class SubObjectBase : System.Object, IConnectable
	{
		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the SubObjectBase class
		/// </summary>
		public SubObjectBase()
		{
			//nothing to do
		}
		#endregion

		#region Property Changed handling
		/// <summary>
		/// Event generated whenever a property (including Value) changes
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;   
		/// <summary>
		/// The named property has changed: generate the PropertyChanged event
		/// </summary>
		/// <param name="propertyName">Name of property that has changed</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));  //Invokes the delegates.
			}
		}
		#endregion

		#region IsDefaultState
		/// <summary>
		/// Determines if the object is in its default state (and therefore does not require saving)
		/// </summary>
		/// <returns>True if default state</returns>
		public virtual bool IsDefaultState()
		{
			foreach (PropertyDescriptor desc in TypeDescriptor.GetProperties(this))
			{
				if (desc.ShouldSerializeValue(this))
				{
					//not in default state
					return false ;
				}
			}

			//nothing needs serializing => it is in default state
			return true ;
		}
		#endregion

		#region IConnectable
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
        public virtual void Connect()
        {
            this.Connected = true; //ft
        }

        /// <summary>
        /// Connects this object to others, stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        public virtual void Connect2()
        {
        }

        /// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
            this.Connected = false; //ft
		}
        
        private bool connected = false;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool Connected //ft //ft
        {
            get { return connected; }
            set { connected = value; }
        }
		#endregion
	}


	/// <summary>
	/// Basic features of a scaleable SubObject, such as a needle.
	/// </summary>
	public class SubObject : SubObjectBase
	{
		#region Variables
		/// <summary>
		/// Width used by ScaleX
		/// </summary>
		protected int					xScale ;
		/// <summary>
		/// Offset used by ScaleX
		/// </summary>
		protected int					xOffset ;
		/// <summary>
		/// Height used be ScaleY
		/// </summary>
		protected int					yScale ;
		/// <summary>
		/// Offset used by ScaleY
		/// </summary>
		protected int					yOffset ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the SubObject class
		/// </summary>
		public SubObject()
		{
			//nothing to do
		}
		#endregion

		#region Methods
		/// <summary>
		/// Converts a Fraction into a horizontal pixel position using a previously defined rectangle
		/// </summary>
		/// <param name="Fraction">Fraction of width</param>
		/// <returns>Pixel position</returns>
		protected int ScaleX(double Fraction)
		{
			return xOffset + (int)(xScale * Fraction) ;
		}

		/// <summary>
		/// Converts a Fraction into a vertical pixel position using a previously defined rectangle
		/// </summary>
		/// <param name="Fraction">Fraction of height</param>
		/// <returns>Pixel position</returns>
		protected int ScaleY(double Fraction)
		{
			return yOffset + (int)(yScale * Fraction) ;
		}

		/// <summary>
		/// Defines the Rectangle to be used by ScaleX and ScaleY and ScaleAngles
		/// </summary>
		/// <param name="DrawRect">Rectangle to use</param>
		protected void SetUpScaling(Rectangle DrawRect)
		{
			xScale	= DrawRect.Width ;
			xOffset = DrawRect.Left ;
			yScale	= DrawRect.Height ;
			yOffset = DrawRect.Top ;
		}

		//converts angles for non-square objects
		/// <summary>
		/// Converts angles for non-square controls.
		/// </summary>
		/// <param name="Acw">True if the controlis anti-clockwise</param>
		/// <param name="StartAngle">Angle in square that is returned converted to an angle in the current Rectangle</param>
		/// <param name="SweepAngle">Angle in square that is returned converted to an angle in the current Rectangle</param>
		protected void ScaleAngles(bool Acw, ref double StartAngle, ref double SweepAngle)
		{
			double			end_angle ;
			double			scaled_start ;
			double			scaled_end ;
			double			scaled_sweep ;
			double			rad_angle ;
			PointF			pt				= new PointF() ;

			//find start point...
			rad_angle = StartAngle * Math.PI / 180 ;
			pt.X = (float)(xScale * 0.5 * Math.Sin(rad_angle)) ;
			pt.Y = (float)(- yScale * 0.5 * Math.Cos(rad_angle)) ;

			//and convert that back to an angle
			scaled_start = Math.Atan((double)(pt.Y / pt.X)) * 180 / Math.PI ;
			//period of Tan is 180 deg, so may need to manually offset it
			if (StartAngle > 180)			scaled_start += 180 ;
			if (scaled_start < 0)			scaled_start += 360 ;

			//find end angle...
			if (SweepAngle == 0 || SweepAngle == 360)
			{
				//nothing to do - both 0 and 360 scale to themselves
				scaled_sweep = SweepAngle ;
			}
			else
			{
				//calculate the scaled sweep angle
				end_angle = (!Acw) ? (StartAngle + SweepAngle) : (StartAngle - SweepAngle) ;
				if (end_angle < 0)			end_angle += 360 ;
				if (end_angle >= 360)		end_angle -= 360 ;

				//...and end point...
				rad_angle	= end_angle * Math.PI / 180 ;
				pt.X = (float)(xScale * 0.5 * Math.Sin(rad_angle)) ;
				pt.Y = (float)(- yScale * 0.5 * Math.Cos(rad_angle)) ;

				//...convert that back to an angle...
				scaled_end = Math.Atan((double)(pt.Y / pt.X)) * 180 / Math.PI ;
				//period of Tan is 180 deg, so may need to manually offset it
				if (end_angle > 180)		scaled_end += 180 ;
				if (scaled_end < 0)			scaled_end += 360 ;

				//and convert that to a sweep angle
				scaled_sweep = (!Acw) ? (360 - scaled_start + scaled_end) : (360 - scaled_end + scaled_start) ;
				if (scaled_sweep < 0)			scaled_sweep += 360 ;
				if (scaled_sweep >= 360)		scaled_sweep -= 360 ;
			}

			//return the values
			StartAngle = scaled_start ;
			SweepAngle = scaled_sweep ;
		}
		#endregion
	}


	/// <summary>
	/// Basic functionality of a Collection of sub-objects
	/// </summary>
	public class SubObjectCollection : CollectionBase, IConnectable
	{
		/// <summary>
		/// Event handler for PropertyChanged events
		/// </summary>
		protected PropertyChangedEventHandler		propertyHdlr ;
		/// <summary>
		/// Event Handler for ObjectSearch events
		/// </summary>
		protected ObjectSearchEventHandler			searchHdlr ;

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the SubObjectCollection class
		/// </summary>
		public SubObjectCollection()
		{
			propertyHdlr = new PropertyChangedEventHandler(OnChildPropertyChanged);
			searchHdlr = new ObjectSearchEventHandler(OnObjectSearch);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Removes all objects from the collection
		/// </summary>
		public new void Clear()
		{
			while (Count > 0)
			{
				this.List.RemoveAt(Count - 1) ;
			}
			base.Clear() ;
		}

		/// <summary>
		/// Determines if the object is in its default state (and therefore does not require saving)
		/// </summary>
		/// <remarks>Assumes default state is with one default object in collection. Override this method if this is not the case</remarks>
		/// <returns>True if default state</returns>
		public virtual bool IsDefaultState()
		{
			if (Count != 1)		return false ;

			foreach (SubObjectBase obj in this)
			{
				//only one object to check, so can simply return this
				return obj.IsDefaultState() ;
			}

			return false ;
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
			((SubObjectBase)Value).PropertyChanged	+= propertyHdlr ;
			((SubObjectBase)Value).ObjectSearch		+= searchHdlr ;

			//and report that there is a new item
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("Item"));  //Invokes the delegates.
			}

			base.OnInsertComplete(Index, Value) ;
		}

		/// <summary>
		/// List RemoveComplete Event
		/// </summary>
		/// <param name="Index">Index of removed object</param>
		/// <param name="Value">Removed object</param>
		protected override void OnRemoveComplete(int Index, object Value)
		{
			((SubObjectBase)Value).PropertyChanged	-= propertyHdlr ;
			((SubObjectBase)Value).ObjectSearch		-= searchHdlr ;

			//report change
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("Item"));  //Invokes the delegates.
			}

			base.OnRemoveComplete(Index, Value) ;
		}
		#endregion

		#region Property Changed event handling (in and out)
		/// <summary>
		/// Event generated whenever a property changes, including on objects within the collection
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;   

		private void OnChildPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			//pass on that a property has changed
			if (PropertyChanged != null)	PropertyChanged(sender, e);  //Invokes the delegates.
		}
		#endregion

		#region IConnectable
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
		public virtual void Connect ()
		{
			foreach (SubObjectBase obj in this)
			{
				obj.Connect() ;
			}
            this.Connected = true; //ft
		}

        /// <summary>
        /// Connects this object to others, stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        public virtual void Connect2()
        {
            foreach (SubObjectBase obj in this)
            {
                obj.Connect2();
            }
        }

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
			foreach (SubObjectBase obj in this)
			{
				obj.Disconnect() ;
			}
            this.Connected = false; //ft
		}
        
        private bool connected = false;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool Connected //ft //ft
        {
            get { return connected; }
            set { connected = value; }
        }
		#endregion
	}
}
