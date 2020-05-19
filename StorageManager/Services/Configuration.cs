using StorageManager.Interfaces;

namespace StorageManager.Services
{
    public class Configuration : IConfiguration
    {
        public string FilePath { get; set; }
    }
}
