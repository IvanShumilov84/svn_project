using KineticaRT;
using KineticaRT.Channels;
using KSL = KineticaRT.Servers.Logging;
using KV = KineticaRT.Values;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CEL.KineticaRT.Servers.Logger
{
    [Serializable, Description("CSV File logger"), DefaultProperty("FileName")]
    public class CsvFile : KSL.Logger, ISerializable, IConnectable, IDisposable
    {
        static CsvFile()
        {
            CsvFile.traceSwitch = new TraceSwitch("KineticaRT.Servers.Logger", "Logger Messages");
        }

        public CsvFile()
        {
            this.comment = "";
            this.currentPlaySpeed = 1f;
            this.currentRecordSpeed = 10f;
            this.recordList = "";
            this.fileName = "";
            this.recordSpeed = "10";
            this.record = "False";
            this.play = "False";
            this.pause = "False";
            this.comment = "";
            this.step = "False";
            this.stop = "True";
            this.playSpeed = "1";
            this.playTime = "";
            this.Initialise();
        }

        protected CsvFile(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.comment = "";
            this.currentPlaySpeed = 1f;
            this.currentRecordSpeed = 10f;
            this.recordList = "";
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            this.Initialise();
            try
            {
                this.fileName = info.GetString("FileName");
                this.recordSpeed = info.GetString("RecordSpeed");
                this.playSpeed = info.GetString("PlaySpeed");
                this.maxFileSize = info.GetInt64("MaxFileSize");
                this.record = info.GetString("Record");
                this.play = info.GetString("Play");
                this.pause = info.GetString("Pause");
                this.step = info.GetString("Step");
                this.stop = info.GetString("Stop");
                this.playTime = info.GetString("Time");
                this.comment = info.GetString("Comment");
                this.recordList = info.GetString("RecordList");
            }
            catch (SerializationException)
            {
            }
        }

        public override void AddChannel(KSL.ILoggerChannel channel)
        {
            if (channel == null)
            {
                throw new ArgumentNullException("channel");
            }
            this.EnsureStopped();
            int num1 = 0;
            while (num1 < this.channelList.Count)
            {
                KSL.ILoggerChannel channel1 = (KSL.ILoggerChannel) this.channelList[num1];
                if (channel1.Index >= channel.Index)
                {
                    break;
                }
                num1++;
            }
            this.channelList.Insert(num1, channel);
        }

        private void CloseFileRead()
        {
            Thread thread1 = this.updateThread;
            if (thread1 != null)
            {
                lock (thread1)
                {
                    thread1.Abort();
                }
                thread1.Join();
            }
            if (this.reader != null)
            {
                Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - Close reader.", base.Name);
                this.reader.Close();
                this.reader = null;
            }
        }

        private void CloseFileWrite()
        {
            Thread thread1 = this.updateThread;
            if (thread1 != null)
            {
                lock (thread1)
                {
                    thread1.Abort();
                }
                thread1.Join();
            }
        }

        private void commentChannel_ValueChanged(IChannel sender, KV.Value newVal)
        {
            if ((this.currentMode == KSL.Logger.Mode.Record) && (this.updateThread != null))
            {
                lock (this.updateThread)
                {
                    StringBuilder builder1 = new StringBuilder();
                    builder1.Append(System.DateTime.Now.ToString(DateTimeFormatInfo.InvariantInfo));
                    builder1.Append(".");
                    builder1.Append(System.DateTime.Now.Millisecond.ToString("000", NumberFormatInfo.InvariantInfo));
                    if (this.firstChannel < this.channelList.Count)
                    {
                        bool flag1 = true;
                        for (int num1 = this.firstChannel; num1 < this.channelList.Count; num1++)
                        {
                            KSL.ILoggerChannel channel1 = (KSL.ILoggerChannel) this.channelList[num1];
                            if (channel1.Enabled)
                            {
                                builder1.Append(",");
                                if (flag1)
                                {
                                    builder1.Append("\"Comment:");
                                    builder1.Append(this.commentChannel.Value.ToString());
                                    builder1.Append("\"");
                                    flag1 = false;
                                }
                            }
                        }
                    }
                    this.writer.WriteLine(builder1.ToString());
                    this.writer.Flush();
                }
            }
        }

        public override void Connect()
        {
           
            lock (this)
            {
                if (!this.Connected)
                {
                    if (this.recordChannel != null)
                    {
                        this.recordChannel.ValueChanged -= this.recordChannelValueChangedHandler;
                        this.recordChannel = null;
                    }
                    if (this.record.Length > 0)
                    {
                        this.recordChannel = base.OnObjectSearch(this.record) as IChannel;
                        if (this.recordChannel == null)
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Channel not found-" + this.record, "Connect");
                        }
                        else
                        {
                            this.recordChannel.ValueChanged += this.recordChannelValueChangedHandler;
                        }
                    }
                    if (this.playChannel != null)
                    {
                        this.playChannel.ValueChanged -= this.playChannelValueChangedHandler;
                        this.playChannel = null;
                    }
                    if (this.play.Length > 0)
                    {
                        this.playChannel = base.OnObjectSearch(this.play) as IChannel;
                        if (this.playChannel == null)
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Channel not found-" + this.play, "Connect");
                        }
                        else
                        {
                            this.playChannel.ValueChanged += this.playChannelValueChangedHandler;
                        }
                    }
                    this.SetCurrentRecordSpeed();
                    this.SetCurrentPlaySpeed();
                    if (this.pauseChannel != null)
                    {
                        this.pauseChannel.ValueChanged -= this.pauseChannelValueChangedHandler;
                        this.pauseChannel = null;
                    }
                    if (this.pause.Length > 0)
                    {
                        this.pauseChannel = base.OnObjectSearch(this.pause) as IChannel;
                        if (this.pauseChannel == null)
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Channel not found-" + this.pause, "Connect");
                        }
                        else
                        {
                            this.pauseChannel.ValueChanged += this.pauseChannelValueChangedHandler;
                        }
                    }
                    if (this.commentChannel != null)
                    {
                        this.commentChannel.ValueChanged -= this.commentChannelValueChangedHandler;
                        this.commentChannel = null;
                    }
                    if (this.comment.Length > 0)
                    {
                        this.commentChannel = base.OnObjectSearch(this.comment) as IChannel;
                        if (this.commentChannel == null)
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Channel not found-" + this.comment, "Connect");
                        }
                        else
                        {
                            this.commentChannel.ValueChanged += this.commentChannelValueChangedHandler;
                        }
                    }
                    if (this.stepChannel != null)
                    {
                        this.stepChannel.ValueChanged -= this.stepChannelValueChangedHandler;
                        this.stepChannel = null;
                    }
                    if (this.step.Length > 0)
                    {
                        this.stepChannel = base.OnObjectSearch(this.step) as IChannel;
                        if (this.stepChannel == null)
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Channel not found-" + this.step, "Connect");
                        }
                        else
                        {
                            this.stepChannel.ValueChanged += this.stepChannelValueChangedHandler;
                        }
                    }
                    if (this.stopChannel != null)
                    {
                        this.stopChannel.ValueChanged -= this.stopChannelValueChangedHandler;
                        this.stopChannel = null;
                    }
                    if (this.stop.Length > 0)
                    {
                        this.stopChannel = base.OnObjectSearch(this.stop) as IChannel;
                        if (this.stopChannel == null)
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Channel not found-" + this.stop, "Connect");
                        }
                        else
                        {
                            this.stopChannel.ValueChanged += this.stopChannelValueChangedHandler;
                        }
                    }
                    this.fileNameChannel = null;
                    if (!CsvFile.QuotedString(this.fileName))
                    {
                        this.fileNameChannel = base.OnObjectSearch(this.fileName) as IChannel;
                        if (this.fileNameChannel == null)
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Channel not found-" + this.fileName, "Connect");
                        }
                    }
                    this.playTimeChannel = null;
                    if (!CsvFile.QuotedString(this.playTime))
                    {
                        this.playTimeChannel = base.OnObjectSearch(this.playTime) as IChannel;
                        if (this.playTimeChannel == null)
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Time channel not found-" + this.playTime, "Connect");
                        }
                    }
                    if (this.stopChannel != null)
                    {
                        this.stopChannel.Value = KV.Values.True;
                    }
                    if (this.recordList.Length > 0)
                    {
                        string[] textArray1 = this.recordList.Split(new char[] { ',' });
                        this.recordChannels = new IChannel[textArray1.Length];
                        int num1 = 0;
                        foreach (string text1 in textArray1)
                        {
                            string text2 = text1.Trim();
                            IChannel channel1 = base.OnObjectSearch(text2) as IChannel;
                            if ((channel1 != null) && (channel1 is INumericThresholdChannel))
                            {
                                KSL.ILoggerChannel channel3 = new KSL.NumericThresholdChannel();
                                channel3.Source = text2;
                                channel3.Logger = base.Name;
                                channel3.ObjectSearch += this.channelObjectSearchHandler;
                                this.recordChannels[num1++] = channel3;
                                channel3.Connect();
                            }
                        }
                    }
                    base.Connect();
                }
            }
        }

        public override void Disconnect()
        {
            this.EnsureStopped();
            lock (this)
            {
                if (this.Connected)
                {
                    if (this.recordChannels != null)
                    {
                        foreach (KSL.ILoggerChannel channel1 in this.recordChannels)
                        {
                            if (channel1 != null)
                            {
                                channel1.Disconnect();
                            }
                        }
                    }
                    if (this.playSpeedChannel != null)
                    {
                        this.playSpeedChannel.ValueChanged -= this.playSpeedChannelValueChangedHandler;
                        this.playSpeedChannel = null;
                    }
                    if (this.recordSpeedChannel != null)
                    {
                        this.recordSpeedChannel.ValueChanged -= this.playSpeedChannelValueChangedHandler;
                        this.recordSpeedChannel = null;
                    }
                    this.recordChannel = null;
                    this.playChannel = null;
                    this.playSpeedChannel = null;
                    this.pauseChannel = null;
                    this.commentChannel = null;
                    this.stepChannel = null;
                    this.stopChannel = null;
                    this.playTimeChannel = null;
                    base.Disconnect();
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.channelObjectSearchHandler = null;
                    if (this.writer != null)
                    {
                        this.writer.Close();
                        this.writer = null;
                    }
                    if (this.reader != null)
                    {
                        this.reader.Close();
                        this.reader = null;
                    }
                    if (this.updateThread != null)
                    {
                        lock (this.updateThread)
                        {
                            this.updateThread.Abort();
                        }
                        this.updateThread.Join();
                        this.updateThread = null;
                    }
                }
                this.disposed = true;
            }
        }

        private void EnsureStopped()
        {
            if (this.currentMode != KSL.Logger.Mode.Stop)
            {
                Thread thread1 = this.updateThread;
                if (thread1 != null)
                {
                    lock (thread1)
                    {
                        thread1.Abort();
                    }
                    thread1.Join();
                }
                if (this.playList != null)
                {
                    this.playList.Clear();
                }
                if (this.stopChannel != null)
                {
                    this.stopChannel.Value = KV.Values.True;
                }
            }
            if (this.doStep)
            {
                this.doStep = false;
                this.stepChannel.Value = KV.Values.False;
            }
        }

        private KSL.ILoggerChannel FindChannel(string name)
        {
        	KSL.ILoggerChannel tmpChannel = null;
            for (int num1 = 0; num1 < this.channelList.Count; num1++)
            {
                KSL.ILoggerChannel channel1 = (KSL.ILoggerChannel) this.channelList[num1];
                if (channel1.Description == name)
                {
                	if (channel1.Enabled)
                	{
                    	return channel1;
                	}
                	else
                	{
                		tmpChannel = channel1;
                	}
                }
            }
            return tmpChannel;
        }

        private void FindFirstChannel()
        {
            if (this.channelList.Count <= 0)
            {
                this.firstChannel = 0;
            }
            else
            {
                int num1 = this.channelList.Count - 1;
                while (num1 >= 0)
                {
                    KSL.ILoggerChannel channel1 = (KSL.ILoggerChannel) this.channelList[num1];
                    if (channel1.Index <= 0)
                    {
                        break;
                    }
                    num1--;
                }
                this.firstChannel = num1 + 1;
            }
        }

        public override object FindObject(string objectName)
        {
            return null;
        }

        [SecurityPermission(SecurityAction.Demand)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            base.GetObjectData(info, context);
            info.AddValue("FileName", this.fileName);
            info.AddValue("RecordSpeed", this.recordSpeed);
            info.AddValue("PlaySpeed", this.playSpeed);
            info.AddValue("MaxFileSize", this.maxFileSize);
            info.AddValue("Record", this.record);
            info.AddValue("Play", this.play);
            info.AddValue("Pause", this.pause);
            info.AddValue("Step", this.step);
            info.AddValue("Stop", this.stop);
            info.AddValue("Time", this.playTime);
            info.AddValue("Comment", this.comment);
            info.AddValue("RecordList", this.recordList);
        }

        private void HeaderRead()
        {
            this.currentReadLine = this.reader.ReadLine();
            if (this.currentReadLine != null)
            {
                while (this.currentReadLine[0] == '"')
                {
                    int num1 = this.currentReadLine.IndexOf("\"", 1);
                    if (num1 > 0)
                    {
                        string text1 = this.currentReadLine.Substring(1, num1 - 1);
                        KSL.ILoggerChannel channel1 = this.FindChannel(text1);
                        if (channel1 != null)
                        {
                            if (channel1.Enabled)
                            {
                                int num2 = this.currentReadLine.IndexOf(",") + 1;
                                if (num2 != 0)
                                {
                                    int num3;
                                    if (this.currentReadLine[num2] == '"')
                                    {
                                        num3 = this.currentReadLine.IndexOf("\"", num2 + 1);
                                        if (num3 != -1)
                                        {
                                            num3 = this.currentReadLine.IndexOf(",", num3);
                                        }
                                    }
                                    else
                                    {
                                        num3 = this.currentReadLine.IndexOf(",", num2);
                                    }
                                    if (num3 == -1)
                                    {
                                        num3 = this.currentReadLine.Length;
                                    }
                                    string text2 = this.currentReadLine.Substring(num2, num3 - num2);
                                    if (text2[0] == '"')
                                    {
                                        channel1.Value = new KV.Text(text2.Substring(1, text2.Length - 2));
                                    }
                                    else
                                    {
                                        channel1.Value = new KV.Double(text2);
                                    }
                                }
                            }
                            else
                            {
                                Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "CsvFile - ReadHeader - Channel not enabled: " + text1, base.Name);
                            }
                        }
                        else
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "CsvFile - ReadHeader - Channel not found: " + text1, base.Name);
                        }
                        this.currentReadLine = this.reader.ReadLine();
                        if (this.currentReadLine == null)
                        {
                            return;
                        }
                    }
                }
                int num4 = this.currentReadLine.IndexOf(",");
                if (num4 < 0)
                {
                    num4 = this.currentReadLine.Length;
                }
                string text3 = this.currentReadLine.Substring(0, num4);
                if (text3 != "Date Time")
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "CsvFile - Invalid file format. (Date Time field incorrect.)", base.Name);
                    this.currentReadLine = null;
                }
                else if (num4 == this.currentReadLine.Length)
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "CsvFile - Empty File.", base.Name);
                    this.currentReadLine = null;
                }
                else
                {
                    do
                    {
                        int num5 = num4 + 1;
                        num4 = this.currentReadLine.IndexOf(",", num5);
                        if (num4 == -1)
                        {
                            num4 = this.currentReadLine.Length;
                        }
                        text3 = this.currentReadLine.Substring(num5 + 1, (num4 - num5) - 2);
                        KSL.ILoggerChannel channel2 = this.FindChannel(text3);
                        if (channel2 != null)
                        {
                            if (channel2.Enabled)
                            {
                                this.playList.Add(channel2);
                            }
                            else
                            {
                                Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "CsvFile - Channel not enabled: " + text3, base.Name);
                            }
                        }
                        else
                        {
                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "CsvFile - Channel not found: " + text3, base.Name);
                        }
                    }
                    while (num4 != this.currentReadLine.Length);
                    this.startReadTime = System.DateTime.Now;
                    this.currentReadTime = System.DateTime.MinValue;
                    if (this.playTimeChannel != null)
                    {
                        this.playTimeChannel.Value = new KV.DateTime(this.currentReadTime);
                    }
                    this.ReadLine();
                }
            }
        }

        private void HeaderWrite()
        {
            StringBuilder builder1 = new StringBuilder();
            for (int num1 = 0; num1 < this.firstChannel; num1++)
            {
                KSL.ILoggerChannel channel1 = (KSL.ILoggerChannel) this.channelList[num1];
                if ((channel1.Index < 0) && channel1.Enabled)
                {
                    if (channel1.Description != null)
                    {
                        builder1.Append("\"");
                        builder1.Append(channel1.Description.ToString());
                        builder1.Append("\"");
                    }
                    builder1.Append(",");
                    if (channel1.Value != null)
                    {
                        if (channel1.Value is KV.Text)
                        {
                            builder1.Append("\"");
                            builder1.Append(channel1.Value.ToString());
                            builder1.Append("\"");
                        }
                        else
                        {
                            builder1.Append(channel1.Value.ToString());
                        }
                    }
                    for (int num2 = 1; num2 < (this.channelList.Count - this.firstChannel); num2++)
                    {
                        builder1.Append(",");
                    }
                    builder1.Append("\r\n");
                }
            }
            builder1.Append("Date Time");
            if (this.firstChannel < this.channelList.Count)
            {
                for (int num3 = this.firstChannel; num3 < this.channelList.Count; num3++)
                {
                	KSL.ILoggerChannel channel2 = (KSL.ILoggerChannel) this.channelList[num3];
                    if (channel2.Enabled)
                    {
                        builder1.Append(",");
                        if (channel2.Description != null)
                        {
                            builder1.Append("\"");
                            builder1.Append(channel2.Description.ToString());
                            builder1.Append("\"");
                        }
                    }
                }
            }
            this.writer.WriteLine(builder1.ToString());
            this.writer.Flush();
        }

        private void Initialise()
        {
            this.recordChannelValueChangedHandler = new ValueChangedEventHandler(this.recordChannel_ValueChanged);
            this.recordSpeedChannelValueChangedHandler = new ValueChangedEventHandler(this.recordSpeedChannel_ValueChanged);
            this.playSpeedChannelValueChangedHandler = new ValueChangedEventHandler(this.playSpeedChannel_ValueChanged);
            this.playChannelValueChangedHandler = new ValueChangedEventHandler(this.playChannel_ValueChanged);
            this.pauseChannelValueChangedHandler = new ValueChangedEventHandler(this.pauseChannel_ValueChanged);
            this.commentChannelValueChangedHandler = new ValueChangedEventHandler(this.commentChannel_ValueChanged);
            this.stepChannelValueChangedHandler = new ValueChangedEventHandler(this.stepChannel_ValueChanged);
            this.stopChannelValueChangedHandler = new ValueChangedEventHandler(this.stopChannel_ValueChanged);
            this.channelObjectSearchHandler = new ObjectSearchEventHandler(this.OnChannelObjectSearch);
            this.channelList = new ArrayList();
            this.playList = new ArrayList();
            this.time = System.DateTime.MinValue;
            base.RawValue = new KV.Text("Initialised");
        }

        private object OnChannelObjectSearch(string objectName)
        {
            if (objectName == base.Name)
            {
                return this;
            }
            return base.OnObjectSearch(objectName);
        }

        private void OpenFileRead()
        {
            string text1;
            if (!CsvFile.QuotedString(this.fileName))
            {
                if (this.fileNameChannel != null)
                {
                    text1 = Path.Combine(Application.StartupPath, this.fileNameChannel.Value.ToString());
                    goto Label_0074;
                }
                Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "Open File failed: Not connected to " + this.fileName.Substring(1), base.Name);
                return;
            }
            text1 = Path.Combine(Application.StartupPath, CsvFile.UnQuotedString(this.fileName));
        Label_0074:
            try
            {
                this.reader = new StreamReader(text1, true);
                if (this.reader == null)
                {
                    return;
                }
                this.FindFirstChannel();
                lock (this)
                {
                    this.updateThread = new Thread(new ThreadStart(this.ReadThread));
                    this.updateThread.Name = base.Name + ": Player";
                    this.updateThread.Start();
                    return;
                }
            }
            catch (IOException exception1)
            {
                Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "Read: " + exception1.Message, base.Name);
                this.Value = new KV.Text(exception1.Message);
                this.currentMode = KSL.Logger.Mode.Stop;
            }
        }

        private void OpenFileWrite()
        {
            string text1;
            if (!CsvFile.QuotedString(this.fileName))
            {
                if (this.fileNameChannel != null)
                {
                    text1 = Path.Combine(Application.StartupPath, this.fileNameChannel.Value.ToString());
                    goto Label_0074;
                }
                Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "Open File failed: Not connected to " + this.fileName.Substring(1), base.Name);
                return;
            }
            text1 = Path.Combine(Application.StartupPath, CsvFile.UnQuotedString(this.fileName));
        Label_0074:
            try
            {
                //this.writer = new StreamWriter(text1, false, Encoding.ASCII);
                this.writer = new StreamWriter(text1, false);
                if (this.writer == null)
                {
                    return;
                }
                this.FindFirstChannel();
                this.HeaderWrite();
                lock (this)
                {
                    this.updateThread = new Thread(new ThreadStart(this.WriteThread));
                    this.updateThread.Name = base.Name + ": Writer";
                    this.updateThread.Start();
                    return;
                }
            }
            catch (IOException exception1)
            {
                Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "Write: " + exception1.Message, base.Name);
                this.Value = new KV.Text(exception1.Message);
                this.currentMode = KSL.Logger.Mode.Stop;
            }
        }

        private void pauseChannel_ValueChanged(IChannel sender, KV.Value newVal)
        {
            if (newVal == KV.Values.True)
            {
                this.paused = true;
            }
            else
            {
                this.paused = false;
            }
            this.SetOutputs();
        }

        private void playChannel_ValueChanged(IChannel sender, KV.Value newVal)
        {
            if ((newVal == KV.Values.True) && (this.currentMode != KSL.Logger.Mode.Play))
            {
                this.EnsureStopped();
                this.currentMode = KSL.Logger.Mode.Play;
                this.OpenFileRead();
                this.SetOutputs();
            }
        }

        private void playSpeedChannel_ValueChanged(IChannel sender, KV.Value newVal)
        {
            float single1 = Math.Abs((float) (((float) newVal.ToDouble()) - this.currentPlaySpeed));
            if (single1 > 0.01)
            {
                this.currentPlaySpeed = (float) newVal.ToDouble();
            }
        }

        private static bool QuotedString(string str)
        {
            if (str == null)
            {
                return false;
            }
            if (str.Length < 2)
            {
                return false;
            }
            if (str[0] != '"')
            {
                return false;
            }
            if (str[str.Length - 1] != '"')
            {
                return false;
            }
            return true;
        }

        private void ReadLine()
        {
            this.currentReadLine = this.reader.ReadLine();
            if (this.currentReadLine != null)
            {
                this.commaPosition = this.currentReadLine.IndexOf(",");
                if (this.commaPosition >= 0)
                {
                    System.DateTime time1 = System.DateTime.Parse(this.currentReadLine.Substring(0, this.commaPosition), DateTimeFormatInfo.InvariantInfo);
                    if (this.currentReadTime == System.DateTime.MinValue)
                    {
                        this.currentReadTime = time1;
                    }
                    TimeSpan span1 = (TimeSpan) (time1 - this.currentReadTime);
                    this.sleepTime = ((long) (((float) span1.Ticks) / this.currentPlaySpeed)) / ((long) 0x2710);
                    this.currentReadTime = time1;
                    if (this.playTimeChannel != null)
                    {
                        this.playTimeChannel.Value = new KV.DateTime(this.currentReadTime);
                    }
                }
            }
        }

        private void ReadThread()
        {
            try
            {
                try
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - ReadThread Start.", base.Name);
                    Thread.CurrentThread.Priority = ThreadPriority.Highest;
                    this.HeaderRead();
                    while (this.currentReadLine != null)
                    {
                        if (this.sleepTime < 0)
                        {
                            this.sleepTime = 0;
                        }
                        Thread.Sleep((int) this.sleepTime);
                        if (this.doStep)
                        {
                            this.doStep = false;
                            this.stepChannel.Value = KV.Values.False;
                        }
                        else if (this.paused)
                        {
                            this.sleepTime = 0x14d;
                            continue;
                        }
                        lock (this.updateThread)
                        {
                            int num1 = this.commaPosition + 1;
                            foreach (KSL.ILoggerChannel channel1 in this.playList)
                            {
                                this.commaPosition = this.currentReadLine.IndexOf(",", num1);
                                if (this.commaPosition == -1)
                                {
                                    this.commaPosition = this.currentReadLine.Length;
                                }
                                if ((this.commaPosition - num1) > 0)
                                {
                                    string text1 = this.currentReadLine.Substring(num1, this.commaPosition - num1);
                                    if (text1[0] == '"')
                                    {
                                        if (text1.StartsWith("\"Comment:"))
                                        {
                                            Trace.WriteLineIf(CsvFile.traceSwitch.TraceInfo, "Playback: Skiping comment.");
                                        }
                                        else
                                        {
                                            channel1.Value = new KV.Text(text1.Substring(1, text1.Length - 2));
                                        }
                                    }
                                    else if (text1 == "True")
                                    {
                                        channel1.Value = KV.Values.True;
                                    }
                                    else if (text1 == "False")
                                    {
                                        channel1.Value = KV.Values.False;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            double num2 = double.Parse(text1, NumberFormatInfo.InvariantInfo);
                                            channel1.Value = new KV.Double(num2);
                                        }
                                        catch (FormatException)
                                        {
                                            channel1.Value = KV.Values.TypeMismatch;
                                        }
                                        catch (OverflowException)
                                        {
                                            channel1.Value = KV.Values.OutOfRange;
                                        }
                                    }
                                }
                                else
                                {
                                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceInfo, "Playback: Empty value");
                                }
                                num1 = this.commaPosition + 1;
                            }
                            this.ReadLine();
                        }
                        Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - ReadThread Update.", base.Name);
                    }
                }
                catch (ThreadAbortException exception1)
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - ReadThread Abort. " + exception1.Message, base.Name);
                    return;
                }
                catch (ArgumentOutOfRangeException exception2)
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, "CsvFile - Invalid file format. " + exception2.Message, base.Name);
                    return;
                }
                catch
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceError, "CsvFile - Reading: Unhandled Exception (2).", base.Name);
                    return;
                }
                finally
                {
                    lock (this)
                    {
                        this.updateThread = null;
                    }
                    if (this.reader != null)
                    {
                        Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - Close reader.", base.Name);
                        this.reader.Close();
                        this.reader = null;
                    }
                    this.playList.Clear();
                    this.currentMode = KSL.Logger.Mode.Stop;
                    this.SetOutputs();
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - Read Thread Stop.", base.Name);
                }
            }
            catch
            {
                Trace.WriteLineIf(CsvFile.traceSwitch.TraceError, "CsvFile - Reading: Unhandled Exception.", base.Name);
            }
        }

        private void recordChannel_ValueChanged(IChannel sender, KV.Value newVal)
        {
            if (newVal == KV.Values.True)
            {
                if (this.currentMode != KSL.Logger.Mode.Record)
                {
                    this.EnsureStopped();
                    this.OpenFileWrite();
                    if (this.writer != null)
                    {
                        this.currentMode = KSL.Logger.Mode.Record;
                    }
                }
                this.SetOutputs();
            }
        }

        private void recordSpeedChannel_ValueChanged(IChannel sender, KV.Value newVal)
        {
            float single1 = Math.Abs((float) (((float) newVal.ToDouble()) - this.currentRecordSpeed));
            if (single1 > 0.001)
            {
                this.currentRecordSpeed = (float) newVal.ToDouble();
            }
        }

        public override void RemoveChannel(KSL.ILoggerChannel channel)
        {
            this.EnsureStopped();
            this.channelList.Remove(channel);
        }

        private void SetCurrentPlaySpeed()
        {
            if (this.playSpeedChannel != null)
            {
                this.playSpeedChannel.ValueChanged -= this.playSpeedChannelValueChangedHandler;
                this.playSpeedChannel = null;
            }
            try
            {
                float single1 = System.Convert.ToSingle(this.playSpeed, NumberFormatInfo.CurrentInfo);
                this.currentPlaySpeed = single1;
            }
            catch (FormatException)
            {
                if (!this.Connected || (this.play.Length <= 0))
                {
                    return;
                }
                this.playSpeedChannel = base.OnObjectSearch(this.playSpeed) as IChannel;
                if (this.playSpeedChannel == null)
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Channel not found-" + this.playSpeed, "Connect");
                    return;
                }
                this.playSpeedChannel.ValueChanged += new ValueChangedEventHandler(this.playSpeedChannel_ValueChanged);
                this.currentPlaySpeed = (float) this.playSpeedChannel.Value.ToDouble();
            }
        }

        private void SetCurrentRecordSpeed()
        {
            if (this.recordSpeedChannel != null)
            {
                this.recordSpeedChannel.ValueChanged -= this.recordSpeedChannelValueChangedHandler;
                this.recordSpeedChannel = null;
            }
            try
            {
                float single1 = System.Convert.ToSingle(this.recordSpeed, NumberFormatInfo.CurrentInfo);
                this.currentRecordSpeed = single1;
            }
            catch (FormatException)
            {
                if (!this.Connected || (this.record.Length <= 0))
                {
                    return;
                }
                this.recordSpeedChannel = base.OnObjectSearch(this.recordSpeed) as IChannel;
                if (this.recordSpeedChannel == null)
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceWarning, base.Name + ": Channel not found-" + this.recordSpeed, "Connect");
                    return;
                }
                this.recordSpeedChannel.ValueChanged += new ValueChangedEventHandler(this.recordSpeedChannel_ValueChanged);
                this.currentRecordSpeed = (float) this.recordSpeedChannel.Value.ToDouble();
            }
        }

        private void SetOutputs()
        {
            if (this.recordChannel != null)
            {
                this.recordChannel.Value = (this.currentMode == KSL.Logger.Mode.Record) ? KV.Values.True : KV.Values.False;
            }
            else if (!CsvFile.QuotedString(this.record))
            {
                this.record = (this.currentMode == KSL.Logger.Mode.Record) ? "True" : "False";
                this.OnPropertyChanged("Record");
            }
            if (this.playChannel != null)
            {
                this.playChannel.Value = (this.currentMode == KSL.Logger.Mode.Play) ? KV.Values.True : KV.Values.False;
            }
            else if (!CsvFile.QuotedString(this.play))
            {
                this.play = (this.currentMode == KSL.Logger.Mode.Play) ? "True" : "False";
                this.OnPropertyChanged("Play");
            }
            if (this.pauseChannel != null)
            {
                this.pauseChannel.Value = this.paused ? KV.Values.True : KV.Values.False;
            }
            else if (!CsvFile.QuotedString(this.pause))
            {
                this.pause = this.paused ? "True" : "False";
                this.OnPropertyChanged("Pause");
            }
            if (this.stopChannel != null)
            {
                this.stopChannel.Value = (this.currentMode == KSL.Logger.Mode.Stop) ? KV.Values.True : KV.Values.False;
            }
            else if (!CsvFile.QuotedString(this.stop))
            {
                this.stop = (this.currentMode == KSL.Logger.Mode.Stop) ? "True" : "False";
                this.OnPropertyChanged("Stop");
            }
            switch (this.currentMode)
            {
                case KSL.Logger.Mode.Stop:
                    this.Value = new KV.Text("Stopped");
                    return;

                case KSL.Logger.Mode.Play:
                    this.Value = new KV.Text(this.paused ? "Paused" : "Playing");
                    return;

                case KSL.Logger.Mode.Record:
                    this.Value = new KV.Text(this.paused ? "Paused" : "Recording");
                    return;
            }
        }

        private void stepChannel_ValueChanged(IChannel sender, KV.Value newVal)
        {
            if (newVal == KV.Values.True)
            {
                this.doStep = true;
            }
        }

        private void stopChannel_ValueChanged(IChannel sender, KV.Value newVal)
        {
            if (newVal == KV.Values.True)
            {
                switch (this.currentMode)
                {
                	case KSL.Logger.Mode.Play:
                        this.CloseFileRead();
                        break;

                    case KSL.Logger.Mode.Record:
                        this.CloseFileWrite();
                        break;
                }
                this.currentMode = KSL.Logger.Mode.Stop;
                this.SetOutputs();
                this.EnsureStopped();
            }
        }

        private static string UnQuotedString(string str)
        {
            return str.Substring(1, str.Length - 2);
        }

        private void WriteThread()
        {
            try
            {
                try
                {
                    int num1 = Environment.TickCount;
                    int num2 = (int) ((1 / ((double) this.currentRecordSpeed)) * 1000);
                    bool flag1 = false;
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - WriteThread Start.", base.Name);
                    Thread.CurrentThread.Priority = ThreadPriority.Highest;
                    while (!flag1)
                    {
                        if (!this.paused || this.doStep)
                        {
                            if (this.doStep)
                            {
                                this.doStep = false;
                                this.stepChannel.Value = KV.Values.False;
                            }
                            System.DateTime time1 = System.DateTime.Now;
                            lock (this.updateThread)
                            {
                                StringBuilder builder1 = new StringBuilder();
                                builder1.Append(System.DateTime.Now.ToString(DateTimeFormatInfo.InvariantInfo));
                                builder1.Append(".");
                                builder1.Append(System.DateTime.Now.Millisecond.ToString("000", NumberFormatInfo.InvariantInfo));
                                if (this.firstChannel < this.channelList.Count)
                                {
                                    for (int num3 = this.firstChannel; num3 < this.channelList.Count; num3++)
                                    {
                                        KSL.ILoggerChannel channel1 = (KSL.ILoggerChannel) this.channelList[num3];
                                        if (channel1.Enabled)
                                        {
                                            builder1.Append(",");
                                            if (channel1.Value is KV.Text)
                                            {
                                                builder1.Append("\"");
                                                builder1.Append(channel1.Value.ToString());
                                                builder1.Append("\"");
                                            }
                                            else if (channel1.Value is KV.Double)
                                            {
                                                builder1.Append((channel1.Value as KV.Double).ToDouble().ToString(NumberFormatInfo.InvariantInfo));
                                            }
                                            else
                                            {
                                                builder1.Append(channel1.Value.ToString());
                                            }
                                        }
                                    }
                                }
                                this.writer.WriteLine(builder1.ToString());
                                this.writer.Flush();
                                if ((this.maxFileSize != 0) && ((this.writer.BaseStream.Position / ((long) 0xf4240)) >= this.maxFileSize))
                                {
                                    flag1 = true;
                                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceInfo, "CsvFile - Write terminated due to reaching MaxFileSize.", base.Name);
                                }
                            }
                            if (this.playTimeChannel != null)
                            {
                                this.playTimeChannel.Value = new KV.DateTime(time1);
                            }
                        }
                        int num5 = num1 - Environment.TickCount;
                        if (num5 > 2)
                        {
                            Thread.Sleep(num5);
                            num1 += num2;
                        }
                        else if (num5 > -10)
                        {
                            num1 += num2;
                        }
                        else
                        {
                            Thread.Sleep(1);
                            num1 = Environment.TickCount + num2;
                        }
                        Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - WriteThread Update.", base.Name);
                    }
                }
                catch (ThreadAbortException)
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - WriteThread Abort.", base.Name);
                    return;
                }
                catch
                {
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceError, "CsvFile - Write Thread: Unhandled Exception (2).", base.Name);
                    return;
                }
                finally
                {
                    lock (this)
                    {
                        this.updateThread = null;
                    }
                    this.currentMode = KSL.Logger.Mode.Stop;
                    this.SetOutputs();
                    Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - WriteThread Stop.", base.Name);
                    if (this.writer != null)
                    {
                        Trace.WriteLineIf(CsvFile.traceSwitch.TraceVerbose, "CsvFile - Close writer.", base.Name);
                        this.writer.Close();
                        this.writer = null;
                    }
                }
            }
            catch
            {
                Trace.WriteLineIf(CsvFile.traceSwitch.TraceError, "CsvFile - Write Thread: Unhandled Exception.", base.Name);
            }
        }


        [Description("Optional - Name of channel for comments, the logger will write new values in this channel to the file if logging."), Category("Logger"), DefaultValue("")]
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                if (this.comment != value)
                {
                    this.comment = value;
                    this.OnPropertyChanged("Comment");
                }
            }
        }

        [DefaultValue(0), Description("The current Mode of the logger."), Category("Logger")]
        public override KSL.Logger.Mode CurrentMode
        {
            get
            {
                return this.currentMode;
            }
        }

        [DefaultValue(""), Description("The name, and optional path, of the file to record to or read from."), Category("Logger")]
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                if (this.fileName != value)
                {
                    this.fileName = value;
                    this.OnPropertyChanged("FileName");
                }
            }
        }

        [Description("Recording will stop just before the file reaches this size.  Units - MBytes.  0 = No limit.  This is for protection against accidentally using excessive disk space."), DefaultValue("0"), Category("Logger")]
        public long MaxFileSize
        {
            get
            {
                return this.maxFileSize;
            }
            set
            {
                if (this.maxFileSize != value)
                {
                    this.maxFileSize = value;
                    this.OnPropertyChanged("MaxFileSize");
                }
            }
        }

        [DefaultValue(""), Description("Optional - Name of Boolean channel, the logger will pause recording/playback while this is set true."), Category("Logger")]
        public string Pause
        {
            get
            {
                return this.pause;
            }
            set
            {
                if (this.pause != value)
                {
                    this.pause = value;
                    this.OnPropertyChanged("Pause");
                }
                try
                {
                    bool flag1 = System.Convert.ToBoolean(value, NumberFormatInfo.CurrentInfo);
                    if (flag1)
                    {
                        this.pauseChannel_ValueChanged(this, flag1 ? KV.Values.True : KV.Values.False);
                    }
                }
                catch (FormatException)
                {
                }
            }
        }

        [Category("Logger"), DefaultValue(""), Description("Name of Boolean channel, the logger will play while this is set true.")]
        public string Play
        {
            get
            {
                return this.play;
            }
            set
            {
                if (this.play != value)
                {
                    this.play = value;
                    this.OnPropertyChanged("Play");
                }
                try
                {
                    if (System.Convert.ToBoolean(value, NumberFormatInfo.CurrentInfo))
                    {
                        this.playChannel_ValueChanged(this, KV.Values.True);
                    }
                }
                catch (FormatException)
                {
                }
            }
        }

        [Category("Logger"), Description("Name of a numeric channel, the logger will replay data at the speed indicated by this channel."), DefaultValue("1")]
        public string PlaySpeed
        {
            get
            {
                return this.playSpeed;
            }
            set
            {
                if (this.playSpeed != value)
                {
                    this.playSpeed = value;
                    this.OnPropertyChanged("PlaySpeed");
                    this.SetCurrentPlaySpeed();
                }
            }
        }

        [Description("Name of Boolean channel, the logger will record while this is set true."), DefaultValue(""), Category("Logger")]
        public string Record
        {
            get
            {
                return this.record;
            }
            set
            {
                if (this.record != value)
                {
                    this.record = value;
                    this.OnPropertyChanged("Record");
                }
                try
                {
                    if (System.Convert.ToBoolean(value, NumberFormatInfo.CurrentInfo))
                    {
                        this.recordChannel_ValueChanged(this, KV.Values.True);
                    }
                }
                catch (FormatException)
                {
                }
            }
        }

        [Browsable(false), Category("Logger"), DefaultValue(""), Description("Names of channels to be recorded. Use comma ',' to seperate names.")]
        public string RecordList
        {
            get
            {
                return this.recordList;
            }
            set
            {
                if (this.recordList != value)
                {
                    this.recordList = value;
                    this.OnPropertyChanged("RecordList");
                }
            }
        }

        [Description("Name of a numeric channel, indicates how fast to record the data in Hz."), DefaultValue("10"), Category("Logger")]
        public string RecordSpeed
        {
            get
            {
                return this.recordSpeed;
            }
            set
            {
                if (this.recordSpeed != value)
                {
                    this.recordSpeed = value;
                    this.OnPropertyChanged("RecordSpeed");
                    this.SetCurrentRecordSpeed();
                }
            }
        }

        [DefaultValue(""), Description("Optional - Name of Boolean channel, the logger will record/play a single record when this is set true and RecordSpeed is set to 0."), Category("Logger")]
        public string Step
        {
            get
            {
                return this.step;
            }
            set
            {
                if (this.step != value)
                {
                    this.step = value;
                    this.OnPropertyChanged("Step");
                }
                try
                {
                    if (System.Convert.ToBoolean(value, NumberFormatInfo.CurrentInfo))
                    {
                        this.stepChannel_ValueChanged(this, KV.Values.True);
                    }
                }
                catch (FormatException)
                {
                }
            }
        }

        [Category("Logger"), DefaultValue(""), Description("Name of Boolean channel, the logger will stop playing or recording when this is set true.")]
        public string Stop
        {
            get
            {
                return this.stop;
            }
            set
            {
                if (this.stop != value)
                {
                    this.stop = value;
                    this.OnPropertyChanged("Stop");
                }
                try
                {
                    if (System.Convert.ToBoolean(value, NumberFormatInfo.CurrentInfo))
                    {
                        this.stopChannel_ValueChanged(this, KV.Values.True);
                    }
                }
                catch (FormatException)
                {
                }
            }
        }

        [DefaultValue(""), Category("Logger"), Description("During playback this contains the record time of the current data.")]
        public string Time
        {
            get
            {
                return this.playTime;
            }
            set
            {
                if (this.playTime != value)
                {
                    this.playTime = value;
                    this.OnPropertyChanged("Time");
                }
            }
        }


        private ArrayList channelList;
        private ObjectSearchEventHandler channelObjectSearchHandler;
        private int commaPosition;
        private string comment;
        private IChannel commentChannel;
        private ValueChangedEventHandler commentChannelValueChangedHandler;
        private KSL.Logger.Mode currentMode;
        private float currentPlaySpeed;
        private string currentReadLine;
        private System.DateTime currentReadTime;
        private float currentRecordSpeed;
        private bool disposed;
        private bool doStep;
        private string fileName;
        private IChannel fileNameChannel;
        private int firstChannel;
        private long maxFileSize;
        private string pause;
        private IChannel pauseChannel;
        private ValueChangedEventHandler pauseChannelValueChangedHandler;
        private bool paused;
        private string play;
        private IChannel playChannel;
        private ValueChangedEventHandler playChannelValueChangedHandler;
        private ArrayList playList;
        private string playSpeed;
        private IChannel playSpeedChannel;
        private ValueChangedEventHandler playSpeedChannelValueChangedHandler;
        private string playTime;
        private IChannel playTimeChannel;
        private StreamReader reader;
        private string record;
        private IChannel recordChannel;
        private IChannel[] recordChannels;
        private ValueChangedEventHandler recordChannelValueChangedHandler;
        private string recordList;
        private string recordSpeed;
        private IChannel recordSpeedChannel;
        private ValueChangedEventHandler recordSpeedChannelValueChangedHandler;
        private long sleepTime;
        private System.DateTime startReadTime;
        private string step;
        private IChannel stepChannel;
        private ValueChangedEventHandler stepChannelValueChangedHandler;
        private string stop;
        private IChannel stopChannel;
        private ValueChangedEventHandler stopChannelValueChangedHandler;
        private System.DateTime time;
        private static TraceSwitch traceSwitch;
        private Thread updateThread;
        private StreamWriter writer;
    }
}

