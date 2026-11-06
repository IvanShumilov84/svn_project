using System;

namespace KineticaRT
{
	/// <summary>
	/// Attribute for recording the change history of anything.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
	public class ChangeRecordAttribute : Attribute
	{
		/// <summary>
		/// A change record.
		/// </summary>
		/// <param name="version">The new version number</param>
		/// <param name="date">Date of change</param>
		/// <param name="author">Name of author</param>
		/// <param name="description">Description of change</param>
		public ChangeRecordAttribute(string version, string date, string author, string description) { this.author = author; this.date = date; this.version = version; this.description=description; }
		/// <summary>
		/// Date of change.
		/// </summary>
		public string date;
		/// <summary>
		/// New version number.
		/// </summary>
		public string version;
		/// <summary>
		/// Description of change
		/// </summary>
		public string description;
		/// <summary>
		/// Author of change
		/// </summary>
		public string author;
	}

	/// <summary>
	/// Attribute for recording the copyright of anything.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
	public class CopyrightAttribute : Attribute
	{
		/// <summary>
		/// A change record.
		/// </summary>
		/// <param name="message">Copyright message</param>
		public CopyrightAttribute(string message) { this.message = message; }
		/// <summary>
		/// Copyright message.
		/// </summary>
		public string message;
	}

	/// <summary>
	/// Attribute for recording a reference.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
	public class ReferenceAttribute : Attribute
	{
		/// <summary>
		/// A reference.
		/// </summary>
		public ReferenceAttribute() {}

		/// <summary>
		/// Project Number.
		/// </summary>
		public string ProjectNumber;

		/// <summary>
		/// Project Name.
		/// </summary>
		public string ProjectName;

		/// <summary>
		/// Document reference number.
		/// </summary>
		public string DocumentReference;

		/// <summary>
		/// Document Name.
		/// </summary>
		public string DocumentName;
	}
}