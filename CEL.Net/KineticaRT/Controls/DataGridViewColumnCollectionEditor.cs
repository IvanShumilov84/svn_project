using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CEL.KineticaRT.Controls
{
#if _TESTEDITOR_
    class DataGridViewColumnCollectionEditor : UITypeEditor
    {
        Form m_dlgColumnEditor;

        DataGridViewColumnCollectionEditor()
        {
        }

        public Form CreateColumnEditor(ITypeDescriptorContext context)
        {
            Type columnEditorType = Assembly.Load("System.Design").GetType("System.Windows.Forms.Design.DataGridViewColumnCollectionDialog");
            Type[] types = new Type[1];
            types[0] = typeof(IServiceProvider);
            // var constructor = columnEditorType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            var constructor = columnEditorType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
                null, types, null);
            Object[] parameters = new Object[1];
            parameters[0] = ((DataGridView)context.Instance).Site;
            var form = (Form)constructor.Invoke(parameters);

            return form;
        }

        public static void SetGrid(Form form, DataGridView grid)
        {
            form.GetType().GetMethod("SetLiveDataGridView", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(form, new object[] { grid });
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                var service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (service == null || context.Instance == null)
                    return value;

                var host = (IDesignerHost)provider.GetService(typeof(IDesignerHost));

                if (m_dlgColumnEditor == null)
                    m_dlgColumnEditor = CreateColumnEditor(context);

                SetGrid(m_dlgColumnEditor, (DataGridView)context.Instance);

                if (null != host)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("DataGridViewColumnCollectionTransaction"))
                    {
                        if (service.ShowDialog(m_dlgColumnEditor) == DialogResult.OK)
                            transaction.Commit();
                        else
                            transaction.Cancel();
                    }
                }
                else
                {
                    service.ShowDialog(m_dlgColumnEditor);
                }
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
#else
    class DataGridViewColumnCollectionEditor : CollectionEditor
    {
        public DataGridViewColumnCollectionEditor(Type type) : base(type) { }

        protected override Type CreateCollectionItemType()
        {
            return typeof(DataGridViewColumn);
        }

        protected override Type[] CreateNewItemTypes()
        {
            Type[] types = new Type[] {
                typeof(DataGridViewTextBoxColumn),
                typeof(DataGridViewComboBoxColumn),
                typeof(DataGridViewCheckBoxColumn),
                typeof(DataGridViewButtonColumn),
                typeof(DataGridViewImageColumn),
                typeof(DataGridViewLinkColumn)
            };

            return types;
        }

        protected override object CreateInstance(Type itemType)
        {
            DataGridViewColumn column =
                itemType.GetConstructor(Type.EmptyTypes).Invoke(null) as DataGridViewColumn;

            return column;
        }

        protected override string GetDisplayText(object value)
        {
            if (null != value &&
                value is DataGridViewColumn &&
                !string.IsNullOrEmpty(((DataGridViewColumn)(value)).HeaderText))
            {
                return ((DataGridViewColumn)(value)).HeaderText;
            }
            else
            {
                return base.GetDisplayText(value);
            }
        }
    }
#endif
}
