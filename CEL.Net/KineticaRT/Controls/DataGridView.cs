using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CEL.KineticaRT.Controls
{
    public class DataGridView : global::System.Windows.Forms.DataGridView, global::KineticaRT.IConnectable
    {
        public DataGridView() : base() { }

        [Editor(typeof(DataGridViewColumnCollectionEditor), typeof(UITypeEditor))]
        public new DataGridViewColumnCollection Columns
        {
            get
            {
                return base.Columns;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new ControlCollection Controls
        {
            get
            {
                return base.Controls;
            }
        }

        public void Connect()
        {
            this.Connected = true;//ft
        }

        public void Disconnect()
        {
            this.Connected = false;//ft
        }
        
        private bool connected;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool Connected //ft
        {
            get { return connected; }
            set { connected = value; }
        }

        /// <summary>
        /// Looks for the named object
        /// </summary>
        /// <param name="objectName">Name of Object to find</param>
        /// <returns>Always returns null</returns>
        public virtual object FindObject(string objectName)
        {
            return null;
        }

        public event global::KineticaRT.ObjectSearchEventHandler ObjectSearch;
        /// <summary>
        /// Fires an ObjectSearch event for the named object
        /// </summary>
        protected object OnObjectSearch(string objectName)
        {
            if (string.IsNullOrEmpty(objectName)) return null;
            if (ObjectSearch == null) return null;
            return ObjectSearch(objectName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The named property has changed: generate the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of property that has changed</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));  //Invokes the delegates.
        }

        /// <summary>
        /// Returns the named sibling object or null if it does not exist.
        /// </summary>
        public virtual object SearchObject(string objectName)
        {
            return OnObjectSearch(objectName);
        }
    }
}
