using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using KineticaRT;
using KineticaRT.Channels;

namespace KineticaRT.Studio
{
	/// <summary>
	/// Loads objects from XML files
	/// </summary>
	public class Loader : KineticaRT.Object, ISupportInitialize
	{
		#region CLASS: Translation
		internal class PropertyInfo
		{
			public string		newName ;
			public Type			type ;

			public PropertyInfo(string NewName, string TypeName)
			{
				newName = NewName ;
				if (TypeName != "")
				{
					type = Type.GetType(TypeName) ;
				}
				else
				{
					type = null ;
				}
			}
		}

		internal class Translation
		{
			public string			progId ;
			public string			typename ;
			public ListDictionary	properties ;

			public Translation(string ProgId, string Typename)
			{
				progId = ProgId ;
				typename = Typename ;

				properties = new ListDictionary() ;
			}
		}
		#endregion

		#region Variables
		/// <summary>
		/// Handler for Progress event
		/// </summary>
		public delegate void ProgressEventHandler(string Info);
		/// <summary>
		/// Progress event
		/// </summary>
		public event ProgressEventHandler		Progress ;

		//properties
		private	string			baseDirectory ;
		private	string[]		directories ;			//appended to baseDirectory
		private	Collection		collection ;
		private	string			translatorFile ;
		private	string			persistenceFile ;

		//internal
		private	Translation[]			translations ;
		private	ArrayList				dirLoaders ;
		private	XmlElement				persistenceRoot ;
		private	ProgressEventHandler	progressHdlr ;
		private int						nextDirIndex ;
		private bool					initialising ;

		private	Updater						updater ;
		private	Updater.UpdateEventHandler	updateHdlr ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Loader class
		/// </summary>
		public Loader()
		{
			updater = new Updater ("LoaderUpdate", 100) ;

			baseDirectory	= "" ;
			directories		= null ;
			translatorFile	= "" ;
			persistenceFile	= "" ;

			translations	= null ;
			dirLoaders		= new ArrayList() ;
			persistenceRoot	= null ;

			nextDirIndex	= 1 ;
			initialising	= false ;

			updateHdlr = new Updater.UpdateEventHandler(OnUpdate) ;
			progressHdlr = new ProgressEventHandler(OnChildProgress) ;
		}
		#endregion

		#region ISupportInitialize
		/// <summary>
		/// Initialization is beginning
		/// </summary>
		public void BeginInit()
		{
			initialising = true ;
		}

		/// <summary>
		/// Initialization is complete
		/// </summary>
		public virtual void EndInit()
		{
			initialising = false ;

			if (directories != null)
			{
				LoadAvailable(true) ;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Base Directory Path, to which all other Directories are appended
		/// </summary>
		[Category("Loader")]
		[DefaultValue("")]
		[Description("Base Directory Path, to which all other Directories are appended")]
		[Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string BaseDirectory
		{
			get
			{
				return baseDirectory ;
			}
			set
			{
				if (baseDirectory != value)
				{
					baseDirectory = value ;
					OnPropertyChanged("BaseDirectory") ;

					if (!initialising)	LoadAvailable(true) ;
				}
			}
		}

		/// <summary>
		/// Array of Directories to load. Names or partial paths get appended to the BaseDirectory.
		/// </summary>
		/// <remarks>
		/// Ensure Collection, TranslatorFile and PersistenceFile are set before this
		/// property, as first time it is set loading is initiated automatically. Subsequently
		/// the 'LoadAvailable' channel is set, and loading occurs when the 'LoadNow' channel
		/// becomes true.
		/// To replace a directory (eg Run2 instead of Run1) without causing
		/// a complete reload, ensure that equivalent directories are in the same
		/// order as for the original load.
		/// When this property is set, the 'LoadAvailable' channel in the Collection
		/// becomes true. Note that simply changing the value of one of the strings
		/// without calling the property's 'set' method will not cause 'LoadAvailable'
		/// to be updated
		/// </remarks>
		[Category("Loader")]
		[DefaultValue(null)]
		[Description("Array of Directories to load. Names or partial paths get appended to the BaseDirectory.")]
		public string [] Directories
		{
			get
			{
				return directories ;
			}
			set
			{
				directories = value ;
				OnPropertyChanged("Directories") ;

				if (!initialising)	LoadAvailable(true) ;
			}
		}

		/// <summary>
		/// Collection object in which to put loaded objects
		/// </summary>
		[Category("Loader")]
		[DefaultValue(null)]
		[Description("Collection object in which to put loaded objects")]
		public Collection Collection
		{
			get
			{
				return collection ;
			}
			set
			{
				if (collection != value)
				{
					collection = value ;
					SetCollection() ;
					OnPropertyChanged("Collection") ;
				}
			}
		}

		/// <summary>
		/// Path and name of Translator file
		/// </summary>
		[Category("Loader")]
		[DefaultValue("")]
		[Description("Path and name of Translator file")]
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string TranslatorFile
		{
			get
			{
				return translatorFile ;
			}
			set
			{
				if (translatorFile != value)
				{
					translatorFile = value ;
					ReadTranslator() ;
					OnPropertyChanged("TranslatorFile") ;
				}
			}
		}

		/// <summary>
		/// Path and name of Persistence file
		/// </summary>
		[Category("Loader")]
		[DefaultValue("")]
		[Description("Path and name of Persistence file")]
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string PersistenceFile
		{
			get
			{
				return persistenceFile ;
			}
			set
			{
				if (persistenceFile != value)
				{
					persistenceFile = value ;
					
					//find the file's root node
					persistenceRoot = null ;

					if (File.Exists(persistenceFile))
					{
						XmlDocument			doc				= new XmlDocument() ;

						try
						{
							doc.Load(persistenceFile) ;

							if (doc.DocumentElement == null || doc.DocumentElement.Name != "Persistence")
							{
								Debug.WriteLine("Invalid Persistence file " + persistenceFile) ;
							}
							else
							{
								persistenceRoot = doc.DocumentElement ;
							}
						}
						catch
						{
							Debug.WriteLine("Invalid xml in persistence file " + persistenceFile) ;
						}
					}
					else
					{
						Debug.WriteLine("Persistence file does not exist " + persistenceFile) ;
					}

					OnPropertyChanged("PersistenceFile") ;
				}
			}
		}

		#endregion

		#region Methods
		//got a new Collection object - add the required channels to it and
		//watch events from those channels
		private void SetCollection()
		{
			BooleanChannel		complete		= new BooleanChannel() ;
			BooleanChannel		load_now		= new BooleanChannel() ;
			BooleanChannel		available		= new BooleanChannel() ;

			//add a 'LoadComplete' Boolean Variable channel
			complete.Name = "LoadComplete" ;
			complete.Value = Values.Values.True ;
			collection.Children.Add(complete.Name, complete) ;

			//add LoadAvailable
			available.Name = "LoadAvailable" ;
			available.Value = Values.Values.False ;
			collection.Children.Add(available.Name, available) ;

			//add LoadNextRun
			load_now.Name = "LoadNow" ;
			load_now.Value = Values.Values.False ;
			collection.Children.Add(load_now.Name, load_now) ;
			//connect to its value change event
			load_now.ValueChanged += new Channels.ValueChangedEventHandler(OnLoadNow) ;

			//collection.Connect() ;
		}

		private void Load()
		{
			BooleanChannel		load			= collection.FindObject("LoadNow") as BooleanChannel ;
			BooleanChannel		done			= collection.FindObject("LoadComplete") as BooleanChannel ;
			string				full_path ;
			int					dir_count		= (directories == null) ? 0 : directories.Length ;
			DirectoryLoader		dir_loader ;
			int					index			= 0 ;

			FireProgress("Loading") ;

			if (load != null)			load.Value = Values.Values.False ;
			if (done != null)			done.Value = Values.Values.False ;

			//persist current values
			Persist() ;

			//make sure we have the correct number of directory loaders
			if (dir_count > dirLoaders.Count)
			{
				//need to add more loaders - got extra directories
				while (dirLoaders.Count < dir_count)
				{
					//create, link and add a new loader
					dir_loader = new DirectoryLoader(nextDirIndex) ;
					dir_loader.Progress += progressHdlr ;
					dirLoaders.Add(dir_loader) ;
					
					nextDirIndex++ ;
				}
			}
			else if (dir_count < dirLoaders.Count)
			{
				//need to remove some loaders - got fewer directories
				while (dirLoaders.Count > dir_count)
				{
					//clear, unlink and remove the directory
					dir_loader = (DirectoryLoader)dirLoaders[dir_count] ;
					dir_loader.Progress -= progressHdlr ;
					dir_loader.Clear(collection) ;
					dirLoaders.Remove(dir_loader) ;
				}
			}
			//else correct number of loaders already

			foreach (DirectoryLoader loader in dirLoaders)
			{
				full_path = Path.Combine(baseDirectory, (string)directories.GetValue(index)) ;

				//(re)set the directory path and load the directory
				loader.DirectoryPath = full_path ;
				loader.Load(collection, translations, persistenceRoot) ;

				index++ ;
			}

			FireProgress("Load complete. Connecting...") ;
			Connect() ;
			FireProgress("Connected") ;

			LoadAvailable(false) ;
			if (done != null)			done.Value = Values.Values.True ;
		}

		private void ReadTranslator()
		{
			//clear old translations
			if (translations != null)		translations = null ;

			//read new ones
			if (File.Exists(translatorFile))
			{
				XmlDocument			doc				= new XmlDocument() ;
				XmlNodeList			object_list ;

				FireProgress("Parsing Translator file") ;

				try
				{
					doc.Load(translatorFile) ;
				}
				catch
				{
					Debug.WriteLine("Invalid xml in translator file " + translatorFile) ;
					return ;
				}

				if (doc.DocumentElement == null || doc.DocumentElement.Name != "Translations")
				{
					Debug.WriteLine("Missing or invalid root node in translator file " + translatorFile) ;
					return ;
				}

				object_list = doc.DocumentElement.SelectNodes("Object[@OldProgID and @NewType]") ;

				if (object_list.Count > 0)
				{
					int					obj_index			= 0 ;
					XmlNodeList			property_list ;

					translations = new Translation[object_list.Count] ;

					foreach (XmlElement elt in object_list)
					{
						translations[obj_index] = new Translation
													( elt.GetAttribute("OldProgID"),
													  elt.GetAttribute("NewType")	) ;

						//what about properties that need translation?
						property_list = elt.SelectNodes("Property[@OldName and @NewName]") ;

						foreach (XmlElement property in property_list)
						{
							PropertyInfo		info ;

							info = new PropertyInfo(property.GetAttribute("NewName"), property.GetAttribute("Type")) ;
							translations[obj_index].properties.Add(property.GetAttribute("OldName"), info) ;
						}

						obj_index++ ;
					}
				}
				FireProgress("Translator file parsing complete") ;
			}
		}

		private void Persist()
		{
			FireProgress("Persisting") ;

			//no point bothering if no valid persistence file or collection
			if (persistenceRoot == null || collection == null)		return ;

			bool		persisted		= false ;

			foreach (DirectoryLoader loader in dirLoaders)
			{
				if (loader.Persist(persistenceRoot, collection))
				{
					persisted = true ;
				}
			}

			if (persisted)
			{
				//something has changed - save the file
				persistenceRoot.OwnerDocument.Save(persistenceFile) ;
			}
			FireProgress("Persist complete") ;
		}

		//sets the value of the LoadAvailable channel
		private void LoadAvailable(bool Available)
		{
			if (collection != null)
			{
				BooleanChannel		channel		= collection.FindObject("LoadAvailable") as BooleanChannel ;

				if (channel != null)
				{
					channel.Value = Available ? Values.Values.True : Values.Values.False ;
				}

				if (Available && nextDirIndex == 1)
				{
					//as soon as loaded a directory this index will increase
					//so can only reach here if not done a load yet
					
					//Automate the first load
					updater.Update += updateHdlr ;
				}
			}
		}
		#endregion

		#region Events (in and out)
		private void OnLoadNow(Channels.IChannel Sender, Values.Value Value)
		{
			if (Value != null && Value.ToDouble() != 0)
			{
				updater.Update += updateHdlr ;
			}
			//else not interested
		}

		private void OnUpdate()
		{
			updater.Update -= updateHdlr ;

			Load() ;
		}

		private void OnChildProgress(string Info)
		{
			FireProgress(Info) ;
		}

		private void FireProgress(string Info)
		{
			if (Progress != null)		Progress(Info) ;
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect() ;

			//connect collection
			if (collection != null)		collection.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect() ;

			//disconnect collection
			if (collection != null)		collection.Disconnect() ;
		}
		#endregion
	}
}
