using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace EmailManager
{
    public class Log4Net
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Test()
        {
            log.Info("First Log");
            
        }
    }
}
