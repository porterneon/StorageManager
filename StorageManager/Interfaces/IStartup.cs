using System.Threading.Tasks;

namespace StorageManager.Interfaces
{
    public interface IStartup
    {
        Task RunAsync();
    }
}