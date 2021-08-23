using System.Threading.Tasks;
using SystemCommands.Commands;

namespace SystemManager.Shared
{
    public interface ICommonService
    {
        Task<string> GetNextCode(GetNextCodeQuery query);
    }
}