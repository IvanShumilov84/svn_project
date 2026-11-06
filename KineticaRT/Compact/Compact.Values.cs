using System;

namespace KineticaRT.Compact.Values
{
	/// <summary>
	/// Standard values for common values and error conditions.
	/// </summary>
	/// <remarks>Using standard values avoids having to create new objects each time you use one of these values.<br/>
	/// Using standard error values also allows specific errors to be recognised and used correctly by various objects.</remarks>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	public sealed class Values 
	{
		static Text		emptyString	= new Text ("") ;
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	EmptyString { get { return emptyString ;}}
		
		static Text		operationNotSupported	= new Text ("Operation Not Supported") ;
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	OperationNotSupported { get { return operationNotSupported ;}}
		
		static Text		typeMismatch			= new Text ("Type Mismatch") ;
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	TypeMismatch			{ get { return typeMismatch			;}}

		static Text		notConnected			= new Text ("Not Connected") ;
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	NotConnected			{ get { return notConnected			;}}

		static Text		outOfRange				= new Text ("Out of Range") ;
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	OutOfRange				{ get { return outOfRange				;}}

		static Text		underRange				= new Text ("Under Range") ;
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	UnderRange				{ get { return underRange				;}}

		static Text		overRange				= new Text ("Over Range") ;
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	OverRange				{ get { return overRange				;}}

		static Double	pi						= new Double (System.Math.PI) ; 
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	Pi						{ get { return pi						;}}

		static Double	positiveInfinity		= new Double (System.Double.PositiveInfinity) ; 
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	PositiveInfinity		{ get { return positiveInfinity		;}}

		static Double	negativeInfinity		= new Double (System.Double.NegativeInfinity) ; 
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	NegativeInfinity		{ get { return negativeInfinity		;}}

		static Boolean	_true					= new Boolean (true) ; 
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Boolean True					{ get { return _true					;}}

		static Boolean	_false					= new Boolean (false) ; 
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Boolean False					{ get { return _false					;}}

		static Double	zero					= new Double (0.0) ; 
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	Zero					{ get { return zero					;}}

		static Double	one						= new Double (1.0) ; 
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	One						{ get { return one						;}}

		private static Double	minusOne				= new Double (-1.0) ; 
		/// <summary>Gets a KineticaRT system-defined value.</summary>
		/// <value>A Value object representing a system-defined value</value>
		public static Value	MinusOne				{ get { return minusOne				;}}

	}

}
