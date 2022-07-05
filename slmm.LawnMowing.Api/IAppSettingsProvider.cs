namespace slmm.LawnMowing.Api
{
    public interface IAppSettingsProvider
    {
        T AppSettingWithFallBack<T>(string sectionName, string key, T defaultValue);
    }
}
