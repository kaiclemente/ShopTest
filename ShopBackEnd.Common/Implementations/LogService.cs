
using log4net;

namespace ShopBackEnd.Common
{
    public class LogService : ILogService
    {
        private ILog logger;
        private bool isConfigured = false;

        public LogService()
        {
            if (!isConfigured)
            {
                logger = LogManager.GetLogger(typeof (LogService));
                log4net.Config.XmlConfigurator.Configure();
                // Move to external config file
                //string l4netPath = Server.MapPath("~/log4net.config");
                //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(l4netPath));
            }
        }

        public ILog Logger()
        {
            return logger;
        }
    }
}
