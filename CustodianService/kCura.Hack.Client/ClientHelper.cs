namespace kCura.Hack.Client
{
    using kCura.Hack.Logging;

    public class ClientHelper
    {
        private static ILogService logService = null;

        public static ILogService LogService
        {
            get
            {
                if (logService == null)
                {
                    logService = LogServiceFactory.CreateLogService("Client");
                }

                return logService;
            }
        }
    }
}