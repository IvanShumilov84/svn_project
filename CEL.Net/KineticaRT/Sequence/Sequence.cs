/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 5/7/2008
 * Time: 1:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using KTC = KineticaRT.Channels;
using KTS = KineticaRT.Sequence;
using CEL.Library;

	
namespace CEL.KineticaRT.Sequence
{
	#region Enum
	public enum eTestMgrCmd
	{
		Open			= 1,
		Load			= 2,
		Initialize		= 3,
		Run				= 4,
		Close			= 5,
		Idle			= -99
	}
	
	public enum eTestMgrStatus
	{
		NotConnected    = 0,//ft Ajout pour regler un progbleme d'arithmetic overflow
        Opening			= 1,
		Opened			= 2,
		Loading			= 3,
		Loaded			= 4,
		Initializing	= 5,
		Initialized		= 6,
		Running			= 7,
		Closing			= 8,
		Closed			= 9,
		Idling			= -999
	}
	#endregion

	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public abstract class Sequence : KTS.Sequence
	{
		#region Fields

		private TestSessionStateChangedEventHandler testSessionClosingHdlr;
		private TestSessionStateChangedEventHandler testSessionClosedHdlr;
		private TestSessionStateChangedEventHandler testSessionIdlingHdlr;
		private TestSessionStateChangedEventHandler testSessionInitializingHdlr;
		private TestSessionStateChangedEventHandler testSessionInitializedHdlr;
		private TestSessionStateChangedEventHandler testSessionLoadingHdlr;
		private TestSessionStateChangedEventHandler testSessionLoadedHdlr;
		private TestSessionStateChangedEventHandler	testSessionOpeningHdlr;
		private TestSessionStateChangedEventHandler testSessionOpenedHdlr;
		private TestSessionStateChangedEventHandler testSessionRunningHdlr;

        private bool ExitThread;
        protected Thread EngineWorkerThread;

         
		#endregion

		#region Constructor

		public Sequence()
		{
			this.testSessionClosingHdlr			= new TestSessionStateChangedEventHandler( this.OnTestSessionClosing );
			this.testSessionClosedHdlr			= new TestSessionStateChangedEventHandler( this.OnTestSessionClosed );
			this.testSessionIdlingHdlr			= new TestSessionStateChangedEventHandler( this.OnTestSessionIdling );
			this.testSessionInitializingHdlr	= new TestSessionStateChangedEventHandler( this.OnTestSessionInitializing );
			this.testSessionInitializedHdlr		= new TestSessionStateChangedEventHandler( this.OnTestSessionInitialized ); 
			this.testSessionLoadingHdlr			= new TestSessionStateChangedEventHandler( this.OnTestSessionLoading );
			this.testSessionLoadedHdlr			= new TestSessionStateChangedEventHandler( this.OnTestSessionLoaded );
			this.testSessionOpeningHdlr			= new TestSessionStateChangedEventHandler( this.OnTestSessionOpening );
			this.testSessionOpenedHdlr			= new TestSessionStateChangedEventHandler( this.OnTestSessionOpened );
			this.testSessionRunningHdlr			= new TestSessionStateChangedEventHandler( this.OnTestSessionRunning );
		}

		#endregion

		#region Methods

		public override void Connect()
		{
			if ( !this.Connected )
			{
				TestMgrStatusDispatcher.Instance.TestSessionClosing			+= this.testSessionClosingHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionClosed			+= this.testSessionClosedHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionIdling			+= this.testSessionIdlingHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionInitializing	+= this.testSessionInitializingHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionInitialized		+= this.testSessionInitializedHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionLoading			+= this.testSessionLoadingHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionLoaded			+= this.testSessionLoadedHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionOpening			+= this.testSessionOpeningHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionOpened			+= this.testSessionOpenedHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionRunning			+= this.testSessionRunningHdlr;
                base.Connect();
				
			}
		}
			
		public override void Disconnect()
		{
			if ( this.Connected )
			{
                ThreadHelper.StopOrKillThread(ref EngineWorkerThread, ref ExitThread);
                base.Disconnect();
				TestMgrStatusDispatcher.Instance.TestSessionClosing			-= this.testSessionClosingHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionClosed			-= this.testSessionClosedHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionIdling			-= this.testSessionIdlingHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionInitializing	-= this.testSessionInitializingHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionInitialized		-= this.testSessionInitializedHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionLoading			-= this.testSessionLoadingHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionLoaded			-= this.testSessionLoadedHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionOpening			-= this.testSessionOpeningHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionOpened			-= this.testSessionOpenedHdlr;
				TestMgrStatusDispatcher.Instance.TestSessionRunning			-= this.testSessionRunningHdlr;
			}
		}

		protected virtual void OnTestSessionClosed( eTestMgrStatus newStatus )
		{
		}

		protected virtual void OnTestSessionClosing( eTestMgrStatus newStatus )
		{
            StopWorkingThread();
		}

		protected virtual void OnTestSessionIdling( eTestMgrStatus newStatus )
		{
            StopWorkingThread();
		}

		protected virtual void OnTestSessionInitializing( eTestMgrStatus newStatus )
		{
		}

		protected virtual void OnTestSessionInitialized( eTestMgrStatus newStatus )
		{
		}

		protected virtual void OnTestSessionLoaded( eTestMgrStatus newStatus )
		{
		}

		protected virtual void OnTestSessionLoading( eTestMgrStatus newStatus )
		{

		}

		protected virtual void OnTestSessionOpened( eTestMgrStatus newStatus )
		{
		}

		protected virtual void OnTestSessionOpening( eTestMgrStatus newStatus )
		{
		}

		protected virtual void OnTestSessionRunning( eTestMgrStatus newStatus )
		{
          
		}
        public bool InitializeChannel(ref KTC.IChannel Channel, string ChannelName)
        { 
            return InitializeChannel(ref Channel,  ChannelName,  true);
        }
        public bool InitializeChannel(ref KTC.IChannel Channel, string ChannelName, bool DisplayErrorMessage) 
		{
			bool ret = false;
			try
			{	
				Channel = base.GetChannel(ChannelName);
		
				if( null == Channel )
				{
					Console.WriteLine(this.GetType().Assembly.FullName + "." + MethodBase.GetCurrentMethod()+ ": Cannot initialize " + ChannelName);
				}
				else
				{
					ret = true;
				}
			}
			catch( Exception ex)
			{
				Console.WriteLine(this.GetType().Assembly.FullName + "." + MethodBase.GetCurrentMethod()+ ": Exception when initializing " + ChannelName + " " + ex.Message.ToString());
			}
			finally
			{
				if(false == ret)
				{
					string msg = this.GetType().Assembly.GetName() + "." + MethodBase.GetCurrentMethod()+ ": Cannot initialize " + ChannelName;
					Console.WriteLine(msg);
                    if (DisplayErrorMessage)
					    System.Windows.Forms.MessageBox.Show(msg);
				}
			}
			return ret;
		}
        public bool InitializeObject(ref object Channel, string ChannelName)
        {
            return InitializeObject( ref Channel, ChannelName, true);
        }
		public bool InitializeObject(ref object Channel, string ChannelName, bool DisplayErrorMessage) 
		{
			bool ret = false;
			try
			{	
				Channel = base.GetObject(ChannelName);
		
				if( null == Channel )
				{
					Console.WriteLine(this.GetType().Assembly.FullName + "." + MethodBase.GetCurrentMethod()+ ": Cannot initialize " + ChannelName);
				}
				else
				{
					ret = true;
				}
			}
			catch( Exception ex)
			{
				Console.WriteLine(this.GetType().Assembly.FullName + "." + MethodBase.GetCurrentMethod()+ ": Exception when initializing " + ChannelName + " " + ex.Message.ToString());
			}
			finally
			{
				if(false == ret)
				{
					string msg = this.GetType().Assembly.GetName() + "." + MethodBase.GetCurrentMethod()+ ": Cannot initialize " + ChannelName;
					Console.WriteLine(msg);
                    if (DisplayErrorMessage)
					    System.Windows.Forms.MessageBox.Show(msg);
				}
			}
			return ret;
		}

        protected void EngineWorkingThread()
        {
            try
            {
                while (!ExitThread)
                {
                    Thread.Sleep(250);
                    OnEngineWorkingThread();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Sequence.EngineWorkingThread: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        protected abstract void OnEngineWorkingThread();
        
        public void StartWorkingThread()
        {
            ExitThread = false;
            EngineWorkerThread = new Thread(EngineWorkingThread);
            EngineWorkerThread.Priority = ThreadPriority.Normal;
            EngineWorkerThread.Name = "EngineWorkerThread";
            EngineWorkerThread.Start();
        }
        public void StopWorkingThread()
        {
            ExitThread = true;
            ThreadHelper.StopOrKillThread(ref EngineWorkerThread, ref ExitThread);
        }

		#endregion
	}
}
