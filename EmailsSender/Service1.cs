﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EmailsSender
{
    public partial class ServiceSendEmail : ServiceBase
    {
        public ServiceSendEmail()
        {
            InitializeComponent();
            eventLog = new EventLog();
            if (!EventLog.SourceExists("EmailSending"))
            {
                EventLog.CreateEventSource(
                    "EmailSending", "LogEmailSending");
            }
            eventLog.Source = "EmailSending";
            eventLog.Log = "LogEmailSending";
        }

        public ServiceSendEmail(int teste)
        {
            eventLog = new EventLog();
            if (!EventLog.SourceExists("EmailSending"))
            {
                EventLog.CreateEventSource(
                    "EmailSending", "LogEmailSending");
            }
            eventLog.Source = "EmailSending";
            eventLog.Log = "LogEmailSending";

            while (teste-- > 0)
            {
                OnTimer(null, null);
            }
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog.WriteEntry("Service started");

            Timer timer = new Timer();
            timer.Interval = 10000; // 10 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            eventLog.WriteEntry("Executando");
            DataBase.Connection.OpenConection("Server=tcp:sunsale.database.windows.net,1433;Initial Catalog=devtools;Persist Security Info=False;User ID=rodrigo;Password=qbj1ACjd**;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            try
            {
                Model.MD_Email.ReenviaEmailsComFalha(new Model.MD_Configuracaoemail(0), eventLog);
            }
            catch(Exception ex)
            {
                eventLog.WriteEntry("Error: " + ex.Message);
            }

            DataBase.Connection.CloseConnection();
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Service finshed");
        }

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
    }
}
