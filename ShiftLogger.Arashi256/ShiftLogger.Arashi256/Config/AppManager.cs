using System.Collections.Specialized;

namespace ShiftLogger_Frontend.Arashi256.Config
{
    internal class AppManager
    {
        public string? ApiBaseURL { get; private set; }
        public string? PreferredDateTimeFormat { get; private set; }
        private NameValueCollection? _appConfig;

        public AppManager()
        {
            try
            {
                _appConfig = System.Configuration.ConfigurationManager.AppSettings;
                if (_appConfig.Count == 0)
                {
                    Console.WriteLine("\nERROR: AppSettings is empty or cannot be read.\n");
                }
                else
                {
                    ApiBaseURL = _appConfig.Get("APIBaseURL");
                    PreferredDateTimeFormat = _appConfig.Get("PreferredDateTimeFormat");
                }
            }
            catch (System.Configuration.ConfigurationErrorsException)
            {
                Console.WriteLine("\nERROR: Could not read app settings\n");
            }
        }
    }
}
