using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using KineticaRT;

namespace KineticaRT.Studio
{
	/// <summary>
	/// Loads objects from XML files in a specific directory
	/// </summary>
	public class DirectoryLoader
	{
		#region Variables
		/// <summary>
		/// Progress event
		/// </summary>
		public event Loader.ProgressEventHandler		Progress ;

		private	string						directoryPath ;
		private	ArrayList					fileList ;
		private Loader.ProgressEventHandler	progressHdlr ;

		private int							dirIndex ;			//set on initialisation
		private int							nextFileIndex ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the DirectoryLoader class
		/// </summary>
		/// <param name="Index">Integer that uniquely identifies this object</param>
		public DirectoryLoader(int Index)
		{
			directoryPath	= "" ;
			dirIndex		= Index ;

			nextFileIndex	= 1 ;

			fileList = new ArrayList() ;

			progressHdlr = new Loader.ProgressEventHandler(OnChildProgress) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the path of the Directory that this object loads
		/// </summary>
		public string DirectoryPath
		{
			get
			{
				return directoryPath ;
			}
			set
			{
				directoryPath = value ;
			}
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Loads the directory
		/// </summary>
		internal void Load(Collection Collection, Loader.Translation[] Translations, XmlElement PersistenceRoot)
		{
			FireProgress("Loading directory " + directoryPath) ;

			//don't bother if the directory doesn't exist
			if (Directory.Exists(directoryPath))
			{
				//Find all '*.xml' files in the given directory
				string[]		files			= Directory.GetFileSystemEntries(directoryPath, "*.xml") ;
				ArrayList		new_list		= new ArrayList() ;
				FileLoader		file_loader ;
				string			filename ;

				//nb: 'file' is full path
				foreach (string file in files)
				{
					//ignore copied files
					filename = Path.GetFileName(file).ToLower() ;
					if (!filename.StartsWith("copy "))
					{
						//look for processing instruction saying it is a loader file
						if ( IsLoaderFile(file) )
						{
							file_loader = null ;

							//find the loader for this filename, if possible
							//don't check the path, just the name
							foreach (FileLoader loader in fileList)
							{
								if (loader.FileName == filename)
								{
									file_loader = loader ;
									break ;
								}
							}

							if (file_loader == null)
							{
								//new file
								file_loader = new FileLoader(filename, dirIndex, nextFileIndex) ;
								file_loader.Progress += progressHdlr ;

								nextFileIndex++ ;
							}
							else
							{
								fileList.Remove(file_loader) ;
							}

							//add the file to the new list
							new_list.Add(file_loader) ;

							//load or reload as appropriate
							file_loader.Load(Collection, Translations, directoryPath) ;
						}
//						else
//						{
//							Debug.WriteLine("Ignoring file with no KRC_Loader instruction " + filename) ;
//						}
					}
//					else
//					{
//						Debug.WriteLine("Ignoring copy file " + filename) ;
//					}
				}

				if (fileList != null)
				{
					foreach (FileLoader loader in fileList)
					{
						loader.Progress -= progressHdlr ;
					}

					//clear any files still in list as no longer exist
					Clear(Collection) ;
				}
				fileList = new_list ;

				//now all files have been cleared that need to be, actually create new channels
				foreach (FileLoader loader in fileList)
				{
					loader.CreateObjects(Collection, Translations, PersistenceRoot) ;
				}
			}
		}

		// Returns true if anything was saved
		internal bool Persist(XmlElement Root, Collection Collection)
		{
			bool		persisted = false ;

			FireProgress("Persisting Directory " + directoryPath) ;

			foreach (FileLoader loader in fileList)
			{
				if (loader.Persist(Root, Collection))
				{
					persisted = true ;
				}
			}
			return persisted ;
		}

		internal void Clear(Collection Collection)
		{
			//FireProgress("Clearing Directory " + directoryPath) ;

			foreach (FileLoader loader in fileList)
			{
				loader.Clear(Collection) ;
			}
		}
		#endregion

		#region Private methods
		private bool IsLoaderFile(string Filepath)
		{
			bool				loader_file		= false ;
			StreamReader		reader			= File.OpenText(Filepath) ;
			char []				buffer			= new char[0x500] ;
			string				str ;

			reader.Read(buffer, 0, 0x500) ;
			str = new String(buffer) ;

			if (str.IndexOf("<?KRC_Loader") >= 0)
			{
				loader_file = true ;
			}

			reader.Close() ;

			return loader_file ;
		}
		#endregion

		#region Progress
		private void OnChildProgress(string Info)
		{
			FireProgress(Info) ;
		}

		private void FireProgress(string Info)
		{
			if (Progress != null)		Progress(Info) ;
		}
		#endregion
	}
}
