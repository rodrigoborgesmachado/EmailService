using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EmailsSender
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ServiceSendEmail()
            };
            ServiceBase.Run(ServicesToRun);
            //ServiceSendEmail temp = new ServiceSendEmail(1);
        }
    }
}
