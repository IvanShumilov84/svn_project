using System;

namespace KineticaRT.Compact
{
	/// <summary>
	/// Attribute for recording the change history of anything.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
	public sealed class ChangeRecordAttribute : Attribute
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
		public string Date
		{
			get 
			{
				return date;
			}
		}
		private string date;
		/// <summary>
		/// New version number.
		/// </summary>
		public string Version
		{
			get 
			{
				return version;
			}
		}
		private string version;
		/// <summary>
		/// Description of change
		/// </summary>
		public string Description
		{
			get 
			{
				return description;
			}
		}
		private string description;
		/// <summary>
		/// Author of change
		/// </summary>
		public string Author
		{
			get 
			{
				return author;
			}
		}
		private string author;
	}

	/// <summary>
	/// Attribute for recording the copyright of anything.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
	public sealed class CopyrightAttribute : Attribute
	{
		/// <summary>
		/// A change record.
		/// </summary>
		/// <param name="message">Copyright message</param>
		public CopyrightAttribute(string message) { this.message = message; }

		/// <summary>
		/// Copyright message.
		/// </summary>
		public string Message
		{
			get 
			{
				return message;
			}
		}
		private string message;
	}

	/// <summary>
	/// Attribute for recording a reference.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
	public sealed class ReferenceAttribute : Attribute
	{
		/// <summary>
		/// A reference.
		/// </summary>
		public ReferenceAttribute() {}

		/// <summary>
		/// Project Number.
		/// </summary>
		public string ProjectNumber
		{
			get 
			{
				return projectNumber;
			}
			set
			{
				projectNumber = value ;
			}
		}
		private string projectNumber;

		/// <summary>
		/// Project Name.
		/// </summary>
		public string ProjectName
		{
			get 
			{
				return projectName;
			}
			set
			{
				projectName = value ;
			}
		}
		private string projectName;

		/// <summary>
		/// Document reference number.
		/// </summary>
		public string DocumentReference
		{
			get 
			{
				return documentReference;
			}
			set
			{
				documentReference = value ;
			}
		}
		private string documentReference;

		/// <summary>
		/// Document Name.
		/// </summary>
		public string DocumentName
		{
			get 
			{
				return documentName;
			}
			set
			{
				documentName = value ;
			}
		}
		private string documentName;
	}
}