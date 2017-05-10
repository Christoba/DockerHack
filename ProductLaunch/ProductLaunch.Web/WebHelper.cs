namespace ProductLaunch.Web
{
    using kCura.Hack.Logging;

    public class WebHelper
    {
        private static ILogService logService = null;

        public static ILogService LogService
        {
            get
            {
                if (logService == null)
                {
                    logService = LogServiceFactory.CreateLogService("Web");
                }

                return logService;
            }
        }
    }
}