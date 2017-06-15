using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EmailNotificationService
{
    public partial class EmailNotificationService : ServiceBase
    {
        //Daily report time for yesterday's events
        private DateTime yesterdayTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
        //Default SMTP
        private string smtpServ = "email.websdepot.com";
        //Daily report time for next x hour events
        private DateTime futureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

        Timer yesterdayTimer;
        Timer futureTimer;


        public EmailNotificationService()
        {
            InitializeComponent();
            eventLog1 = new EventLog();
	        if (!EventLog.SourceExists("MySource")) 
	        {         
			        EventLog.CreateEventSource(
				        "MySource","MyNewLog");
	        }
	        eventLog1.Source = "MySource";
	        eventLog1.Log = "MyNewLog";

        }

        protected override void OnStart(string[] args)
        {
            if(args.Length != 0)
            {
                yesterdayTime = Convert.ToDateTime(args[0]);
                futureTime = Convert.ToDateTime(args[1]);
                smtpServ = args[2];
            }

            yesterdayTimer = new Timer(1000 * 60);
            yesterdayTimer.Elapsed += previousDayReport;
            yesterdayTimer.Start();

            futureTimer = new Timer(1000 * 60);
            futureTimer.Elapsed += futureReport;
            futureTimer.Start();

        }

        private void futureReport(object sender, ElapsedEventArgs e)
        {
            if(DateTime.Now.TimeOfDay == futureTime.TimeOfDay)
            {
                // TODO: Send report for future reboots here
            }
        }

        private void previousDayReport(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.TimeOfDay == yesterdayTime.TimeOfDay)
            {
                // TODO: Send report for yesterday reboots here
            }
        }

        protected override void OnStop()
        {

        }
    }
}
