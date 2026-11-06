using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace KineticaRT.Controls
{
    /// <summary>
    /// Collection of StaticTrace objects designed for use with an XYGraph
    /// </summary>
    [ChangeRecord("2.1.2.1", "19 04 2007", "N. Savidge", "Fixed deadlock when adding a trace (continued).")]
    [ChangeRecord("2.1.2.0", "18 04 2007", "N. Savidge", "Fixed deadlock when adding a trace.")]
    [ChangeRecord("1.0", "01 01 2003", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2003-2007 KineticaRT Ltd.")]
    [TypeConverter(typeof(XYStaticTraceCollection.TraceCollConverter))]
    public class XYStaticTraceCollection : SubObjectCollection 
    {
		#region Type Converter
		internal class TraceCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					XYStaticTraceCollection	coll		= (XYStaticTraceCollection) value;
					XYStaticTrace []			traces		= new XYStaticTrace [coll.Count] ;
					ConstructorInfo		ctor_info ;
					
					ctor_info = typeof(XYStaticTraceCollection).GetConstructor(new Type[]{typeof(XYStaticTrace[])});

						//copy traces into array
						for (int index = 0; index < coll.Count; index++)
						{
							traces[index] = coll[index] ;
						}

					return new InstanceDescriptor(ctor_info, new object[]{traces}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the StaticTraceCollection class with one trace
		/// </summary>
		public XYStaticTraceCollection()
		{
			AddTrace(Color.Red) ;
		}

		/// <summary>
		/// Initialises a new instance of the StaticTraceCollection class containing the specified trace objects
		/// </summary>
		/// <param name="traces">Array of StaticTrace objects to add to collection</param>
        public XYStaticTraceCollection(XYStaticTrace[] traces)
		{
			AddRange(traces) ;
		}
		#endregion

        internal event DisplayUpdateEventHandler DisplayUpdate;
        private void OnDisplayUpdate(DisplayUpdateMode type)
        {
            if (DisplayUpdate == null) return ;
            DisplayUpdate(this, type);
        }

		#region Properties
		/// <summary>
        /// Gets or sets the StaticTrace object currently at the given index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public XYStaticTrace this[int Index]
		{
			get
			{
                return (XYStaticTrace)List[Index];
			}
			set
			{
				if (value != List[Index])
				{
                    //((StaticTrace)List[Index]).PropertyChanged -= hdlr ;	//?? needed?
					List[Index] = value ;
                    //((StaticTrace)List[Index]).PropertyChanged += hdlr ;	//?? needed?
				}
			}
		}

		#endregion

		#region Public Methods
		/// <summary>
        /// Adds a new StaticTrace of the specified color
		/// </summary>
        /// <param name="LineColor">Color of new StaticTrace</param>
		public void AddTrace(Color LineColor)
		{

            XYStaticTrace new_trace = new XYStaticTrace();
            new_trace.DisplayUpdate +=new DisplayUpdateEventHandler(new_trace_DisplayUpdate);
            
            this.List.Add(new_trace);
		}

        void new_trace_DisplayUpdate(object source, DisplayUpdateMode type)
        {
            OnDisplayUpdate(type);
        }

		/// <summary>
        /// Adds an array of StaticTrace objects to the collection
		/// </summary>
        /// <param name="newStaticTraces">Array of StaticTraces to add</param>
        public void AddRange(XYStaticTrace[] newStaticTraces)
		{
            for (int index = 0; index < newStaticTraces.Length; index++)
			{
                newStaticTraces[index].DisplayUpdate += new DisplayUpdateEventHandler(new_trace_DisplayUpdate);
                this.List.Add(newStaticTraces[index]);
			}
		}

        internal void ScaleData(int oldLeft, int oldBottom, int newLeft, int newBottom, float scaleX, float scaleY)
        {
            foreach (XYStaticTrace trace in this)
            {
                trace.ScaleData(oldLeft, oldBottom, newLeft, newBottom, scaleX, scaleY);
            }
        }


        #endregion

		#region Protected/Internal methods (drawing etc)
		/// <summary>
        /// Draw all the StaticTraces in the collection
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="BackColor">XYGraph background color</param>
		/// <returns>True if some drawing done</returns>
		protected internal bool Draw(Graphics g, Color BackColor)
		{
			bool		drawn		= false ;

			//...and then normal traces
            foreach (XYStaticTrace trace in this)
			{
				if (trace.Draw(g, BackColor))
				{
					drawn = true ;
				}
			}

			return drawn ;
		}






		/// <summary>
		/// Recalculates the traces
		/// </summary>
        protected internal void Recalculate(Scale xScale, Scale yScale, int xStart, int xEnd, int yStart, int yEnd)
		{
            foreach (XYStaticTrace trace in this)
			{
                trace.Recalculate(xScale, yScale, xStart, xEnd, yStart, yEnd);
			}
		}

        ///// <summary>
        ///// Reset the display info in the traces
        ///// </summary>
        ///// <param name="MakeNextCurrent">True to move 'next' to 'current'. False to move 'current' to 'last'</param>
        //protected internal void ProgressDisplayInfo(bool MakeNextCurrent)
        //{
        //    foreach (StaticTrace trace in this)
        //    {
        //        trace.ProgressDisplayInfo(MakeNextCurrent) ;
        //    }
        //}
		#endregion
    }
}
///// <summary>
///// Draws the pens on the traces
///// </summary>
///// <param name="g">Graphics object with which to draw</param>
//protected internal void DrawPens(Graphics g)
//{
//    foreach (StaticTrace trace in this)
//    {
//        trace.DrawPen(g) ;
//    }
//}

///// <summary>
///// Causes the traces to update their values and recalculate points etc
///// </summary>
///// <param name="CurrentTime">Now</param>
///// <param name="PaperRect">Where drawing is allowed</param>
///// <param name="Scales">Array of parent control's scales used for range calculation</param>
///// <param name="EraseTraces">returned as True to force trace lines to be erased</param>
///// <param name="DrawTraces">returned as True to force trace lines to be drawn</param>
///// <param name="ErasePens">returned as True to force pens to be erased</param>
///// <param name="DrawPens">returned as True to force pens to be drawn</param>
///// <param name="RefreshGraph">returned as True to force the whole graph to be redrawn</param>
//protected internal void Update(int CurrentTime, Rectangle PaperRect, Scale[] Scales, ref bool EraseTraces, ref bool DrawTraces, ref bool ErasePens, ref bool DrawPens, ref bool RefreshGraph)
//{
//    foreach (StaticTrace trace in this)
//    {
//        trace.Update(CurrentTime, PaperRect, Scales, ref EraseTraces, ref DrawTraces, ref ErasePens, ref DrawPens, ref RefreshGraph) ;
//    }
//}

///// <summary>
///// Erase all the out of date StaticTraces in the collection
///// </summary>
///// <param name="g">Graphics object with which to draw</param>
///// <param name="ErasePens">True to erase out of date Pens</param>
///// <param name="EraseTraces">True to erase out of date line segments</param>
///// <param name="BackColor">XYGraph background color</param>
//protected internal bool Erase(Graphics g, bool ErasePens, bool EraseTraces, Color BackColor)
//{
//    bool		erased		= false ;

//    foreach (StaticTrace trace in this)
//    {
//        if (trace.Erase(g, ErasePens, EraseTraces, BackColor))
//        {
//            erased = true ;
//        }
//    }

//    return erased ;
//}
