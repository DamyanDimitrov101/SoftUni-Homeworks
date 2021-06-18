using System.Threading.Tasks;

namespace DWAsync.Models.Contracts
{
    public interface IReader
    {
        Task<string> ReadFileAsync(string filename);
    }
}
