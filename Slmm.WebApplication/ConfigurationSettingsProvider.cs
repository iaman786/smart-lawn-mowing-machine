namespace Slmm.WebApplication
{
    using Microsoft.Extensions.Configuration;
    using slmm.LawnMowing.Api;

    public class ConfigurationSettingsProvider : IAppSettingsProvider
    {
        IConfiguration configuration;

        public ConfigurationSettingsProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public T AppSettingWithFallBack<T>(string sectionName, string key, T fallBack) 
        {
            var section = this.configuration.GetSection(sectionName);
            if (section == null)
            {
                return fallBack;
            }

            return section.GetValue(key, fallBack);
        }
    }
}
