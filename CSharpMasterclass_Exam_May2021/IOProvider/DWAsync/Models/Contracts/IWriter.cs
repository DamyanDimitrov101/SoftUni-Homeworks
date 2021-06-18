using System.Threading.Tasks;

namespace DWAsync.Models.Contracts
{
    public interface IWriter
    {
        Task SaveFile(string filepath, string data);
    }
}
