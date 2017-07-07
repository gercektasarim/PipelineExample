namespace Parantez.Core.Configuration
{
    public interface IConfigReader
    {
        bool HelpEnabled();
        T GetConfigEntry<T>(string entryName);
    }
}