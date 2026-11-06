/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 5/7/2008
 * Time: 1:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Windows.Forms;
using KTC = KineticaRT.Channels;
using KTS = KineticaRT.Sequence;
using KTV = KineticaRT.Values;
	
namespace CEL.KineticaRT.Sequence
{
	public sealed class TestMgrStatusDispatcher
	{
		internal interface ITestMgrState
		{
			void EnterState( TestMgrStatusDispatcher dispatcher );
			ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus );
		}
		
		private sealed class IdlingState : ITestMgrState
		{
			#region Fields
			private static readonly IdlingState instance = new IdlingState();
			#endregion

			#region Properties

			public static IdlingState Instance
			{
				get
				{
					return instance;
				}
			}
			#endregion

			#region Constructors

			private IdlingState() {}

			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionIdling();
			}
			
			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch ( newStatus )
				{
					case eTestMgrStatus.Closing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Closed:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opening:
						nextState = OpeningState.Instance;
						break;
					case eTestMgrStatus.Opened:
						nextState = OpeningState.Instance;
						break;
					case eTestMgrStatus.Loading:
						nextState = OpeningState.Instance;
						break;
					case eTestMgrStatus.Loaded:
						nextState = OpeningState.Instance;
						break;
					case eTestMgrStatus.Initializing:
						nextState = OpeningState.Instance;
						break;
					case eTestMgrStatus.Initialized:
						nextState = OpeningState.Instance;
						break;
					case eTestMgrStatus.Running:
						nextState = OpeningState.Instance;
						break;
				}
				return nextState;
			}

			#endregion
		}
		
		private sealed class OpeningState : ITestMgrState
		{
			#region Fields
			private static readonly OpeningState instance = new OpeningState();
			#endregion
			
			#region Properties

			public static OpeningState Instance
			{
				get
				{
					return instance;
				}
			}

			#endregion

			#region Constructors
			private OpeningState() {}
			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionOpening();
			}
			
			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch ( newStatus )
				{
					case eTestMgrStatus.Closing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Closed:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Idling:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opened:
						nextState = OpenedState.Instance;
						break;
					case eTestMgrStatus.Loading:
						nextState = OpenedState.Instance;
						break;
					case eTestMgrStatus.Loaded:
						nextState = OpenedState.Instance;
						break;
					case eTestMgrStatus.Initializing:
						nextState = OpenedState.Instance;
						break;
					case eTestMgrStatus.Initialized:
						nextState = OpenedState.Instance;
						break;
					case eTestMgrStatus.Running:
						nextState = OpenedState.Instance;
						break;
				}
				return nextState;
			}
			#endregion
		}
		
		private class OpenedState : ITestMgrState
		{
			#region Fields
			private static readonly OpenedState instance = new OpenedState();
			#endregion
			
			#region Properties

			public static OpenedState Instance
			{
				get
				{
					return instance;
				}
			}

			#endregion

			#region Constructors
			private OpenedState() {}
			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionOpened();
			}
			
			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch ( newStatus )
				{
					case eTestMgrStatus.Closing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Closed:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Idling:
						nextState = ClosingState.Instance;
						break; 
					case eTestMgrStatus.Opening:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Loading:
						nextState = LoadingState.Instance;
						break;
					case eTestMgrStatus.Loaded:
						nextState = LoadingState.Instance;
						break;
					case eTestMgrStatus.Initializing:
						nextState = LoadingState.Instance;
						break;
					case eTestMgrStatus.Initialized:
						nextState = LoadingState.Instance;
						break;
					case eTestMgrStatus.Running:
						nextState = LoadingState.Instance;
						break;
				}
				return nextState;
			}
			#endregion
		}
		
		private class LoadingState : ITestMgrState
		{
			#region Fields
			private static readonly LoadingState instance = new LoadingState();
			#endregion
			

			#region Properties

			public static LoadingState Instance
			{
				get
				{
					return instance;
				}
			}

			#endregion

			#region Constructors
			private LoadingState() {}
			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionLoading();
			}
			
			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch ( newStatus )
				{
					case eTestMgrStatus.Closing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Closed:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Idling:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opening:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opened:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Loaded:
						nextState = LoadedState.Instance;
						break;
					case eTestMgrStatus.Initializing:
						nextState = LoadedState.Instance;
						break;
					case eTestMgrStatus.Initialized:
						nextState = LoadedState.Instance;
						break;
					case eTestMgrStatus.Running:
						nextState = LoadedState.Instance;
						break;
				}
				return nextState;
			}
			#endregion
		}

		private class LoadedState : ITestMgrState
		{
			#region Fields
			private static readonly LoadedState instance = new LoadedState();
			#endregion
			
			#region Properties

			public static LoadedState Instance
			{
				get
				{
					return instance;
				}
			}


			#endregion

			#region Constructors
			private LoadedState() {}
			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionLoaded();
			}
			
			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch( newStatus )
				{
					case eTestMgrStatus.Closing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Closed:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Idling:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opening:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opened:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Loading:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Initializing:
						nextState = InitializingState.Instance;
						break;
					case eTestMgrStatus.Initialized:
						nextState = InitializingState.Instance;
						break;
					case eTestMgrStatus.Running:
						nextState = InitializingState.Instance;
						break;
				}
				return nextState;
			}
			#endregion
		}

		private class InitializingState : ITestMgrState
		{
			#region Fields
			private static readonly InitializingState instance = new InitializingState();
			#endregion
			
			#region Properties

			public static InitializingState Instance
			{
				get
				{
					return instance;
				}
			}

			#endregion

			#region Constructors
			private InitializingState() {}
			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionInitializing();
			}
			
			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch ( newStatus )
				{
					case eTestMgrStatus.Closing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Closed:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Idling:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opening:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opened:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Loading:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Loaded:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Initialized:
						nextState = InitializedState.Instance;
						break;
					case eTestMgrStatus.Running:
						nextState = InitializedState.Instance;
						break;
				}
				return nextState;
			}
			#endregion
		}

		private class InitializedState : ITestMgrState
		{
			#region Fields
			private static readonly InitializedState instance = new InitializedState();
			#endregion
			
			#region Properties

			public static InitializedState Instance
			{
				get
				{
					return instance;
				}
			}

			#endregion

			#region Constructors
			private InitializedState() {}
			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionInitialized();
			}

			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch ( newStatus )
				{
					case eTestMgrStatus.Closing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Closed:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Idling:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opening:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opened:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Loading:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Loaded:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Initializing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Running:
						nextState = RunningState.Instance;
						break;
				}
				return nextState;
			}
			#endregion
		}

		private class RunningState : ITestMgrState
		{
			#region Fields
			private static readonly RunningState instance = new RunningState();
			#endregion
			
			#region Properties

			public static RunningState Instance
			{
				get
				{
					return instance;
				}
			}

			#endregion

			#region Constructors
			private RunningState() {}
			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionRunning();
			}
			
			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch ( newStatus )
				{
					case eTestMgrStatus.Closing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Closed:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Idling:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opening:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Opened:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Loading:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Loaded:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Initializing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Initialized:
						nextState = ClosingState.Instance;
						break;
				}
				return nextState;
			}
			#endregion
		}

		private class ClosingState : ITestMgrState
		{
			#region Fields
			private static readonly ClosingState instance = new ClosingState();
			#endregion
			
			#region Properties

			public static ClosingState Instance
			{
				get
				{
					return instance;
				}
			}

			#endregion

			#region Constructors
			private ClosingState() {}
			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionClosing();
			}
			
			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch ( newStatus )
				{
					case eTestMgrStatus.Closed:
						nextState = ClosedState.Instance;
						break;
					case eTestMgrStatus.Idling:
						nextState = ClosedState.Instance;
						break;
					case eTestMgrStatus.Opening:
						nextState = ClosedState.Instance;
						break;
					case eTestMgrStatus.Opened:
						nextState = ClosedState.Instance;
						break;
					case eTestMgrStatus.Loading:
						nextState = ClosedState.Instance;
						break;
					case eTestMgrStatus.Loaded:
						nextState = ClosedState.Instance;
						break;
					case eTestMgrStatus.Initializing:
						nextState = ClosedState.Instance;
						break;
					case eTestMgrStatus.Initialized:
						nextState = ClosedState.Instance;
						break;
					case eTestMgrStatus.Running:
						nextState = ClosedState.Instance;
						break;
				}
				return nextState;
			}
			#endregion
		}

		private class ClosedState : ITestMgrState
		{
			#region Fields
			private static readonly ClosedState instance = new ClosedState();
			#endregion
			
			#region Properties

			public static ClosedState Instance
			{
				get
				{
					return instance;
				}
			}

			#endregion

			#region Constructors
			private ClosedState() {}
			#endregion
			
			#region Methods

			public void EnterState( TestMgrStatusDispatcher dispatcher )
			{
				dispatcher.OnTestSessionClosed();
			}
			
			public ITestMgrState OnTestMgrStatusChanged( eTestMgrStatus newStatus )
			{
				ITestMgrState nextState = this;
				
				switch ( newStatus )
				{
					case eTestMgrStatus.Closing:
						nextState = ClosingState.Instance;
						break;
					case eTestMgrStatus.Idling:
						nextState = IdlingState.Instance;
						break;
					case eTestMgrStatus.Opening:
						nextState = IdlingState.Instance;
						break;
					case eTestMgrStatus.Opened:
						nextState = IdlingState.Instance;
						break;
					case eTestMgrStatus.Loading:
						nextState = IdlingState.Instance;
						break;
					case eTestMgrStatus.Loaded:
						nextState = IdlingState.Instance;
						break;
					case eTestMgrStatus.Initializing:
						nextState = IdlingState.Instance;
						break;
					case eTestMgrStatus.Initialized:
						nextState = IdlingState.Instance;
						break;
					case eTestMgrStatus.Running:
						nextState = IdlingState.Instance;
						break;
				}
				return nextState;
			}
			#endregion
		}
		
		#region Fields
		private static readonly TestMgrStatusDispatcher instance	= new TestMgrStatusDispatcher();
		private ITestMgrState localTestMgrState						= IdlingState.Instance;
		#endregion

		#region Properties

		public static TestMgrStatusDispatcher Instance
		{
			get
			{
				return instance;
			}
		}
		
		private ITestMgrState TestMgrState
		{
			get
			{
				return this.localTestMgrState;
			}
			set
			{
				if ( value != this.localTestMgrState )
				{
					this.localTestMgrState = value;
					this.localTestMgrState.EnterState( this );
				}
			}
		}

		#endregion

		#region Events

		public event TestSessionStateChangedEventHandler TestSessionClosed;
		public event TestSessionStateChangedEventHandler TestSessionClosing;
        public event TestSessionStateChangedEventHandler TestSessionPostClosing; //ft Pour regler des probleme de synchronisation entre les sequence moteur et la sequence OpenTest
		public event TestSessionStateChangedEventHandler TestSessionIdling;
		public event TestSessionStateChangedEventHandler TestSessionInitialized;
		public event TestSessionStateChangedEventHandler TestSessionInitializing;
		public event TestSessionStateChangedEventHandler TestSessionLoading;
		public event TestSessionStateChangedEventHandler TestSessionLoaded;
        public event TestSessionStateChangedEventHandler TestSessionPreOpening;//ft Pour regler des probleme de synchronisation entre les sequence moteur et la sequence OpenTest
		public event TestSessionStateChangedEventHandler TestSessionOpening;
		public event TestSessionStateChangedEventHandler TestSessionOpened;
		public event TestSessionStateChangedEventHandler TestSessionRunning;

		#endregion

		#region Constructors

		private TestMgrStatusDispatcher() {}

		#endregion

		#region Methods

		private void OnTestSessionClosed()
		{
			if (this.TestSessionClosed != null)
			{
                this.TestSessionClosed( eTestMgrStatus.Closed );
			}
		}
		
		private void OnTestSessionClosing()
		{
			if (this.TestSessionClosing != null)
			{
				this.TestSessionClosing( eTestMgrStatus.Closing );
			}
            if (this.TestSessionPostClosing != null)//ft Pour regler des probleme de synchronisation entre les sequence moteur et la sequence OpenTest
            {
                this.TestSessionPostClosing(eTestMgrStatus.Closing);
            }
		}
		
		private void OnTestSessionIdling()
		{
			if (this.TestSessionIdling != null)
			{
				this.TestSessionIdling( eTestMgrStatus.Idling );
			}
		}

		private void OnTestSessionInitializing()
		{
			if (this.TestSessionInitializing != null)
			{
				this.TestSessionInitializing( eTestMgrStatus.Initializing );
			}
		}

		private void OnTestSessionInitialized()
		{
			if (this.TestSessionInitialized != null)
			{
				this.TestSessionInitialized( eTestMgrStatus.Initialized );
			}
		}
		
		private void OnTestSessionLoaded()
		{
			if (this.TestSessionLoaded != null)
			{
				this.TestSessionLoaded( eTestMgrStatus.Loaded );
			}
		}
		
		private void OnTestSessionLoading()
		{
			if (this.TestSessionLoading != null)
			{
				this.TestSessionLoading( eTestMgrStatus.Loading );
			}
		}

		private void OnTestSessionOpening()
		{
            if (this.TestSessionPreOpening != null)//ft Pour regler des probleme de synchronisation entre les sequence moteur et la sequence OpenTest
            {
                this.TestSessionPreOpening(eTestMgrStatus.Opening);
            }
			if (this.TestSessionOpening != null)
			{
				this.TestSessionOpening( eTestMgrStatus.Opening );
			}
		}

		private void OnTestSessionOpened()
		{
			if (this.TestSessionOpened != null)
			{
				this.TestSessionOpened( eTestMgrStatus.Opened );
			}
		}

		private void OnTestSessionRunning()
		{
			if (this.TestSessionRunning != null)
			{
				this.TestSessionRunning( eTestMgrStatus.Running );
			}
		}

		internal void UpdateStatus( eTestMgrStatus newStatus )
		{
			this.TestMgrState = this.TestMgrState.OnTestMgrStatusChanged( newStatus );
		}
		
		#endregion
	}
	
	/// <summary>
	/// Description of OpenTest.
	/// </summary>
	public class OpenTest : Sequence
	{
		#region Fields
		
		private bool								connected				= false;
		private eTestMgrStatus						wbTestMgrStatus			= eTestMgrStatus.Idling;
		private EventHandler						timerTickHdlr;
		private KTC.Channel							testMgrStatus;
		private KTC.ValueChangedEventHandler		testMgrStatusHdlr;
		private UnhandledExceptionEventHandler		unHandledExceptionHdlr;
		private static System.Windows.Forms.Timer	timer					= new System.Windows.Forms.Timer();
		private AppDomain							curDomain				= AppDomain.CurrentDomain;

        private TestSessionStateChangedEventHandler testSessionPostClosingHdlr;//ft
        private TestSessionStateChangedEventHandler testSessionPreOpeningHdlr;//ft

		#endregion
	
		#region Constructor
		
		public OpenTest()
		{
			this.unHandledExceptionHdlr	= new UnhandledExceptionEventHandler( OnUnHandledException );
			this.testMgrStatusHdlr		= new KTC.ValueChangedEventHandler( this.OnTestMgrStatusValueChanged );
			this.timerTickHdlr			= new EventHandler( this.TimerEventProcessor );

            this.testSessionPreOpeningHdlr = new TestSessionStateChangedEventHandler(this.OnTestSessionPreOpening); //ft
            this.testSessionPostClosingHdlr = new TestSessionStateChangedEventHandler(this.OnTestSessionPostClosing); //ft
		}
		
		#endregion
		
		#region Methods

		public override void Connect()
		{
			if ( !this.connected )
			{
				base.Connect();
				
				this.testMgrStatus = this.GetChannel( "Test_Mgr_Status" );

                curDomain.UnhandledException	+= this.unHandledExceptionHdlr;

                if ( this.testMgrStatus.Value.ToString() != "Not Connected")//ft Permet de regler un bug d'arythmetic overtflow lorsque le channel est converti de NotConnected==> Double.Nan==>eTestManagerStatus pouf!
                {
                    this.wbTestMgrStatus = (eTestMgrStatus)this.testMgrStatus.Value.ToDouble();//ft Il arrive certaine fois que le wbTestMgrStatus est a Idling, et que le TestMgrStatus est a Running, et les sequences ne bouge pas  (monte pas jusqua running)
                }
                else
                {
                    this.wbTestMgrStatus = eTestMgrStatus.NotConnected; //ft En théorie il faudrait ajouter un statut dans eTestMgrStatus i.e. Not Connected
                }
				this.testMgrStatus.ValueChanged	+= this.testMgrStatusHdlr;
				timer.Tick						+= this.timerTickHdlr;
				
				timer.Interval = 100;
				timer.Start();

                TestMgrStatusDispatcher.Instance.TestSessionPostClosing += this.testSessionPostClosingHdlr;//ft
                TestMgrStatusDispatcher.Instance.TestSessionPreOpening += this.testSessionPreOpeningHdlr;//ft

				this.connected = true;
			}
		}
		
		public override void Disconnect()
		{
			if ( this.connected )
			{
				timer.Stop();

				timer.Tick -= this.timerTickHdlr;

				if ( curDomain != null )
				{
					curDomain.UnhandledException	-= this.unHandledExceptionHdlr;
				}
				
				if ( this.testMgrStatus != null )
				{
					this.testMgrStatus.ValueChanged -= this.testMgrStatusHdlr;
				}

                TestMgrStatusDispatcher.Instance.TestSessionPostClosing -= this.testSessionPostClosingHdlr;//ft
                TestMgrStatusDispatcher.Instance.TestSessionPreOpening -= this.testSessionPreOpeningHdlr;//ft

				this.connected = false;
				base.Disconnect();

               
			}
		}

		private void OnTestMgrStatusValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			if(newVal != KTV.Values.Empty && newVal != KTV.Values.NotConnected)//ft Sometimes we were receiving a Empty value that might cause an overflow exception when converting to double
			{
				if ( newVal.ToDouble() != 0.0 )
				{
					this.wbTestMgrStatus = (eTestMgrStatus) newVal.ToDouble();
				}
			}
		}

        protected virtual void OnTestSessionPostClosing(eTestMgrStatus newStatus)//ft
        {
        }
        protected virtual void OnTestSessionPreOpening(eTestMgrStatus newStatus)//ft
        {
        }

		private void OnUnHandledException( object sender, UnhandledExceptionEventArgs ex )
		{
			StringWriter errorMsg	= new StringWriter();
			String fileName;
			Exception e 			= (Exception)ex.ExceptionObject;
			
			if ( e.GetType() != typeof(System.Threading.ThreadAbortException) )
			{
				errorMsg.WriteLine("An error occurred please contact the adminstrator with the following information:");
				errorMsg.WriteLine("");
				errorMsg.WriteLine(e.Message);
				errorMsg.WriteLine("");
				errorMsg.WriteLine("Stack Trace:");
				errorMsg.WriteLine(e.StackTrace);
				try
				{
					StreamWriter sw;
					
					fileName = Application.StartupPath + "\\Logs\\UnHandledException " + DateTime.Now.ToString("yyyy-MM-dd HH'h'mm'm'ss's'") + ".log";
					sw = File.CreateText( fileName );
					sw.Write( errorMsg.ToString());
					sw.Close();
				}
				catch
				{
				}
				MessageBox.Show(errorMsg.ToString(), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}
		
		private void TimerEventProcessor( Object myObject, EventArgs myEventArgs )
		{
			timer.Stop();
			
			TestMgrStatusDispatcher.Instance.UpdateStatus( this.wbTestMgrStatus );
			
			timer.Start();
		}

        protected override void OnEngineWorkingThread()
        { 
        }
        
		#endregion
	}
	
	public delegate void TestSessionStateChangedEventHandler( eTestMgrStatus newStatus );
}
