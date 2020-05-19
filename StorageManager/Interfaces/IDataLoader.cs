using System.Threading.Tasks;

namespace StorageManager.Interfaces
{
    public interface IDataLoader
    {
        Task LoadDataFromFileAsync();
    }
}