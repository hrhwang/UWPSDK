﻿using System;
using Windows.Foundation;
using Windows.Storage;

namespace Neosmartpen.Net
{
    public class PenController : IPenController, IPenControllerEvent
    {
        private PenClientParserV1 mClientV1;
        private PenClientParserV2 mClientV2;

		internal int Protocol { get; set; }

        public PenController()
        {
            mClientV1 = new PenClientParserV1(this);
            mClientV2 = new PenClientParserV2(this);

            Protocol = -1;
        }

        public IPenClient PenClient
        {
            get;set;
        }

        void IPenController.OnDataReceived( byte[] buff )
        {
            if ( Protocol == Protocols.N2 )
            {
                mClientV1.ProtocolParse(buff, buff.Length);
            }
            else
            {
                mClientV2.ProtocolParse(buff, buff.Length);
            }
        }

        #region Event Property
        /// <summary>
        /// Occurs when a connection is made
        /// </summary>
        public event TypedEventHandler<IPenClient, ConnectedEventArgs> Connected;
		internal void onConnected(ConnectedEventArgs args)
		{
			Connected?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when a connection is closed
        /// </summary>
		public event TypedEventHandler<IPenClient, object> Disconnected;
		internal void onDisconnected()
		{
			Disconnected?.Invoke(PenClient, new object());
		}

        /// <summary>
        /// Occurs when finished offline data downloading
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> OfflineDownloadFinished;
		internal void onFinishedOfflineDownload(SimpleResultEventArgs args)
		{
			OfflineDownloadFinished?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when authentication is complete, the password entered has been verified.
        /// </summary>
		public event TypedEventHandler<IPenClient, object> Authenticated;
		internal void onPenAuthenticated()
		{
			Authenticated?.Invoke(PenClient, new object());
		}

        /// <summary>
        /// Occurs when the note information to be used is added
        /// </summary>
        public event TypedEventHandler<IPenClient, object> AvailableNoteAdded;
        internal void onAvailableNoteAdded()
        {
            AvailableNoteAdded?.Invoke(PenClient, new object());
        }

        /// <summary>
        /// Occurs when the power-on setting is applied when the pen tip is pressed
        /// </summary>
        public event TypedEventHandler<IPenClient, SimpleResultEventArgs> AutoPowerOnChanged;
		internal void onPenAutoPowerOnSetupResponse(SimpleResultEventArgs args)
		{
            AutoPowerOnChanged?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when the power-off setting is applied when there is no input for a certain period of time
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> AutoPowerOffTimeChanged;
		internal void onPenAutoShutdownTimeSetupResponse(SimpleResultEventArgs args)
		{
			AutoPowerOffTimeChanged?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when the beep setting is applied
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> BeepSoundChanged;
		internal void onPenBeepSetupResponse(SimpleResultEventArgs args)
		{
			BeepSoundChanged?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when the cap is closed and the power-on and power-off setting is applied
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> PenCapPowerOnOffChanged;
		internal void onPenCapPowerOnOffSetupResponse(SimpleResultEventArgs args)
		{
			PenCapPowerOnOffChanged?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when the pen's new LED color value is applied
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> PenColorChanged;
		internal void onPenColorSetupResponse(SimpleResultEventArgs args)
		{
			PenColorChanged?.Invoke(PenClient, args);
		}

		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> HoverChanged;
		internal void onPenHoverSetupResponse(SimpleResultEventArgs args)
		{
			HoverChanged?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when settings to store offline data are applied
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> OfflineDataChanged;
		internal void onPenOfflineDataSetupResponse(SimpleResultEventArgs args)
		{
			OfflineDataChanged?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when requesting a password when the pen is locked with a password
        /// </summary>
		public event TypedEventHandler<IPenClient, PasswordRequestedEventArgs> PasswordRequested;
		internal void onPenPasswordRequest(PasswordRequestedEventArgs args)
		{
            PasswordRequested?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when the pen's new password is applied
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> PasswordChanged;
		internal void onPenPasswordSetupResponse(SimpleResultEventArgs args)
		{
			PasswordChanged?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when the pen's new sensitivity setting is applied
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> SensitivityChanged;
		internal void onPenSensitivitySetupResponse(SimpleResultEventArgs args)
		{
			SensitivityChanged?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when pen's RTC time is applied
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> RtcTimeChanged;
		internal void onPenTimestampSetupResponse(SimpleResultEventArgs args)
		{
			RtcTimeChanged?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when the pen's battery status changes
        /// </summary>
		public event TypedEventHandler<IPenClient, BatteryAlarmReceivedEventArgs> BatteryAlarmReceived;
		internal void onReceiveBatteryAlarm(BatteryAlarmReceivedEventArgs args)
		{
			BatteryAlarmReceived?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when new coordinate data is received
        /// </summary>
		public event TypedEventHandler<IPenClient, DotReceivedEventArgs> DotReceived;
		internal void onReceiveDot(DotReceivedEventArgs args)
		{
			DotReceived?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when firmware installation is complete
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> FirmwareInstallationFinished;
		internal void onReceiveFirmwareUpdateResult(SimpleResultEventArgs args)
		{
			FirmwareInstallationFinished?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when firmware installation is started
        /// </summary>
        public event TypedEventHandler<IPenClient, object> FirmwareInstallationStarted;
        internal void onStartFirmwareInstallation()
        {
            FirmwareInstallationStarted?.Invoke(PenClient, new object());
        }

        /// <summary>
        /// Notice the progress while the firmware installation is in progress
        /// </summary>
        public event TypedEventHandler<IPenClient, ProgressChangeEventArgs> FirmwareInstallationStatusUpdated;
		internal void onReceiveFirmwareUpdateStatus(ProgressChangeEventArgs args)
		{
            FirmwareInstallationStatusUpdated?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when a list of offline data is received
        /// </summary>
		public event TypedEventHandler<IPenClient, OfflineDataListReceivedEventArgs> OfflineDataListReceived;
		internal void onReceiveOfflineDataList(OfflineDataListReceivedEventArgs args)
		{
			OfflineDataListReceived?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when an offline stroke is received
        /// </summary>
		public event TypedEventHandler<IPenClient, OfflineStrokeReceivedEventArgs> OfflineStrokeReceived;
		internal void onReceiveOfflineStrokes(OfflineStrokeReceivedEventArgs args)
		{
			OfflineStrokeReceived?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when a status of pen is received
        /// </summary>
		public event TypedEventHandler<IPenClient, PenStatusReceivedEventArgs> PenStatusReceived;
		internal void onReceivePenStatus(PenStatusReceivedEventArgs args)
		{
			PenStatusReceived?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when an offline data is removed
        /// </summary>
		public event TypedEventHandler<IPenClient, SimpleResultEventArgs> OfflineDataRemoved;
		internal void onRemovedOfflineData(SimpleResultEventArgs args)
		{
			OfflineDataRemoved?.Invoke(PenClient, args);
		}

        /// <summary>
        /// Occurs when offline downloading starts
        /// </summary>
		public event TypedEventHandler<IPenClient, object> OfflineDataDownloadStarted;
		internal void onStartOfflineDownload()
		{
			OfflineDataDownloadStarted?.Invoke(PenClient, new object());
		}
        #endregion

        #region Request
        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="oldone">old password</param>
        /// <param name="newone">new password</param>
        public void SetPassword(string oldone, string newone = "")
        {
            Request(() => mClientV1.ReqSetUpPassword(oldone, newone), () => mClientV2.ReqSetUpPassword(oldone, newone));
        }

        /// <summary>
        /// If you request a password when the pen is locked, enter it
        /// </summary>
        /// <param name="password">Specifies the password for authentication. password is a string, maximum length is 16 bytes</param>
        public void InputPassword(string password)
        {
            Request(() => mClientV1.ReqInputPassword(password), () => mClientV2.ReqInputPassword(password));
        }

        /// <summary>
        /// Request the status of the pen
        /// </summary>
        public void RequestPenStatus()
        {
            Request(() => mClientV1.ReqPenStatus(), () => mClientV2.ReqPenStatus());
        }

        /// <summary>
        /// Sets the pen's RTC timestamp
        /// </summary>
        /// <param name="timetick">milisecond timestamp tick (from 1970-01-01)</param>
        public void SetRtcTime(long timetick)
        {
            Request(null, () => mClientV2.ReqSetupTime(timetick));
        }

        /// <summary>
        /// Sets the power-off setting when there is no input for a certain period of time
        /// </summary>
        /// <param name="minute">minute of maximum idle time, staying power on (0~)</param>
        public void SetAutoPowerOffTime(short minute)
        {
            Request(() => mClientV1.ReqSetupPenAutoShutdownTime(minute), () => mClientV2.ReqSetupPenAutoShutdownTime(minute));
        }

        /// <summary>
        /// Sets the property that can be control by cap of pen
        /// </summary>
        /// <param name="enable">true if you want enable setting; otherwise, false</param>
        public void SetPenCapPowerOnOffEnable(bool enable)
        {
            Request(null, () => mClientV2.ReqSetupPenCapPower(enable));
        }

        /// <summary>
        /// Sets the power-on setting when the pen tip is pressed
        /// </summary>
        /// <param name="enable">true if you want enable setting; otherwise, false</param>
        public void SetAutoPowerOnEnable(bool enable)
        {
            Request(() => mClientV1.ReqSetupPenAutoPowerOn(enable), () => mClientV2.ReqSetupPenAutoPowerOn(enable));
        }

        /// <summary>
        /// Sets the status of the beep sound property
        /// </summary>
        /// <param name="enable">true if you want enable setting; otherwise, false</param>
        public void SetBeepSoundEnable(bool enable)
        {
            Request(() => mClientV1.ReqSetupPenBeep(enable), () => mClientV2.ReqSetupPenBeep(enable));
        }

        public void SetHoverEnable(bool enable)
        {
            Request(() => mClientV1.ReqPenStatus(), () => mClientV2.ReqPenStatus());
        }

        /// <summary>
        /// Sets the usage of offline data
        /// </summary>
        /// <param name="enable">true if you want enable setting; otherwise, false</param>
        public void SetOfflineDataEnable(bool enable)
        {
            Request(null, () => mClientV2.ReqSetupOfflineData(enable));
        }

        /// <summary>
        /// Sets the color of LED
        /// </summary>
        /// <param name="color">integer type color formatted 0xAARRGGBB</param>
        public void SetColor(int color)
        {
            Request(() => mClientV1.ReqSetupPenColor(color), () => mClientV2.ReqSetupPenColor(color));
        }

        /// <summary>
        /// Sets the value of the pen's sensitivity property that controls the force sensor of pen
        /// </summary>
        /// <param name="step">the value of sensitivity. (0~4, 0 means maximum sensitivity)</param>
        public void SetSensitivity(short step)
        {
            Request(() => mClientV1.ReqSetupPenSensitivity(step), () => mClientV2.ReqSetupPenSensitivity(step));
        }

        /// <summary>
        /// Sets the available notebook type
        /// </summary>
        public void AddAvailableNote()
        {
            Request(() => mClientV1.ReqAddUsingNote(), () => mClientV2.ReqAddUsingNote());
        }

        /// <summary>
        /// Sets the available notebook type 
        /// </summary>
        /// <param name="section">The section Id of the paper</param>
        /// <param name="owner">The owner Id of the paper</param>
        /// <param name="notes">The array of notebook Id list</param>
        public void AddAvailableNote(int section, int owner, int[] notes = null)
        {
            Request(() => mClientV1.ReqAddUsingNote(section, owner, notes), () => mClientV2.ReqAddUsingNote( section, owner, notes));
        }

        //public bool RequestOfflineDataList(int section, int owner, int note);

        /// <summary>
        /// Requests the list of Offline data
        /// </summary>
        public void RequestOfflineDataList()
        {
            Request(() => mClientV1.ReqOfflineDataList(), () => mClientV2.ReqOfflineDataList());
        }

        /// <summary>
        /// Requests the transmission of offline data 
        /// </summary>
        /// <param name="section">The section Id of the paper</param>
        /// <param name="owner">The owner Id of the paper</param>
        /// <param name="notes">The array of notebook Id list</param>
        /// <param name="deleteOnFinished">delete offline data when transmission is finished</param>
        /// <param name="pages">The array of page's number</param>
        public void RequestOfflineData(int section, int owner, int note, bool deleteOnFinished = true, int[] pages = null)
        {
            Request(() => mClientV1.ReqOfflineData(new OfflineDataInfo(section, owner, note, pages)), () => mClientV2.ReqOfflineData(section, owner, note, deleteOnFinished, pages));
        }

        /// <summary>
        /// Requests the transmission of offline data 
        /// </summary>
        /// <param name="section">The section Id of the paper</param>
        /// <param name="owner">The owner Id of the paper</param>
        /// <param name="notes">The array of notebook Id list</param>
        public void RequestOfflineData(int section, int owner, int[] notes)
        {
            Request(() => mClientV1.ReqOfflineData(new OfflineDataInfo(section, owner, notes[0])), () => mClientV2.ReqOfflineData(section, owner, notes[0]));
        }

        /// <summary>
        /// Requests the transmission of offline data 
        /// </summary>
        /// <param name="section">The section Id of the paper</param>
        /// <param name="owner">The owner Id of the paper</param>
        public void RequestOfflineData(int section, int owner)
        {
            Request(() => mClientV1.ReqPenStatus(), () => mClientV2.ReqPenStatus());
        }

        /// <summary>
        /// Requests the firmware installation 
        /// </summary>
        /// <param name="file">Represents a binary file of firmware</param>
        /// <param name="version">Version of firmware typed string</param>
        public void RequestFirmwareInstallation(StorageFile file, string version = null)
        {
            Request(() => mClientV1.ReqPenSwUpgrade(file), () => { mClientV2.ReqPenSwUpgrade(file, version);  });
        }

        /// <summary>
        /// Request to suspend firmware installation
        /// </summary>
        public void SuspendFirmwareInstallation()
        {
            Request(() => mClientV1.SuspendSwUpgrade(), () => mClientV2.SuspendSwUpgrade());
        }

        public void OnConnected()
        {
            if (Protocol != Protocols.N2)
            {
                mClientV2.ReqVersion();
            }
        }

        public void OnDisconnected()
        {
			if (Protocol == Protocols.N2)
				mClientV1.OnDisconnected();
            onDisconnected();
        }

        public delegate void RequestDele();

        private void Request(RequestDele requestToV1, RequestDele requestToV2)
        {
            if ( PenClient == null || !PenClient.Alive || Protocol == -1 )
            {
                throw new RequestIsUnreached();
            }

            if ( Protocol == Protocols.N2 )
            {
                if (requestToV1 == null) throw new UnavailableRequest();

                requestToV1();
            }
            else
            {
                if (requestToV2 == null) throw new UnavailableRequest();

                requestToV2();
            }
        }



        #endregion
    }

    /// <summary>
    /// This exception is thrown when a request is submitted to the PenController and the request fails.
    /// </summary>
    public class PenRequestException : Exception
    {
    }

    /// <summary>
    /// This exception is thrown when the connection is abnormal when requesting the pen.
    /// </summary>
    public class RequestIsUnreached : PenRequestException
    {
    }

    /// <summary>
    /// This exception is thrown when requesting a function that is not in the pen
    /// </summary>
    public class UnavailableRequest : PenRequestException
    {
    }
}