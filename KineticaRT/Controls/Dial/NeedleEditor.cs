using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;

namespace KRT_Dial
{
	public class NeedleEditor : System.Drawing.Design.UITypeEditor 
	{
		private IWindowsFormsEditorService edSvc = null;

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{

			if (context != null && context.Instance != null && provider != null) 
			{
				edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if (edSvc != null) 
				{
					Needle needle = new Needle(null);
					//needle.ValueChanged += new EventHandler(this.ValueChanged);
					bool asInt = true;
					if (value is int) 
					{
						needle.Value = (int)value;
					}
					else if (value is byte) 
					{
						asInt = false;
						needle.Value = (byte)value;
					}
					edSvc.DropDownControl(new Dial());
					//*********    edSvc.ShowDialog (
					if (asInt) 
					{
						value = needle.Value;
					}
					else 
					{
						value = (byte)needle.Value;
					}
				}
			}

			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			if (context != null && context.Instance != null) 
			{
				return UITypeEditorEditStyle.Modal ;//.DropDown;
			}
			return base.GetEditStyle(context);
		}

//		private void ValueChanged(object sender, EventArgs e) 
//		{
//			if (edSvc != null) 
//			{
//				edSvc.CloseDropDown();
//			}
//		}
	}
}
