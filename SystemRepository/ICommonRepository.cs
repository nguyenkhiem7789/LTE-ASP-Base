using System.Threading.Tasks;

namespace SystemRepository
{
    public interface ICommonRepository
    {
        Task<long> GetNextValueForSequence(string pathName);
        Task CreateSequence(string pathName);
    }
}