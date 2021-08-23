using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SystemCommands.Commands;
using SystemManager.Shared;
using SystemRepository;
using Common;
using Extensions;

namespace SystemManager.Services
{
    public class CommonService: ICommonService
    {
        private readonly ICommonRepository _commonRepository;

        public CommonService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }
        
        public async Task<string> GetNextCode(GetNextCodeQuery query)
        {
            string code = string.Empty;
            int numberFormat = 6;
            if (query.Number > 0)
            {
                numberFormat = query.Number;
            }

            try
            {
                long nextValue = await _commonRepository.GetNextValueForSequence(query.TypeName);
                code = query.IsDigit
                    ? nextValue.ToString().PadLeft(numberFormat, '0')
                    : CommonUtility.GenerateCodeFromId(nextValue, numberFormat);
            }
            catch (SqlException e)
            {
                if (e.Message.StartsWith("Invalid object name 'Sequence"))
                {
                    await _commonRepository.CreateSequence(query.TypeName);
                    long nextValue = await _commonRepository.GetNextValueForSequence(query.TypeName);
                    code = query.IsDigit
                        ? nextValue.ToString().PadLeft(numberFormat, '0')
                        : CommonUtility.GenerateCodeFromId(nextValue, numberFormat);
                }
            }
            catch (Exception e)
            {
                e.ExceptionAddParam("CommonService.GetNextId", query.TypeName);
                throw;
            }
            if (!string.IsNullOrEmpty(query.Prefix))
            {
                code = $"{query.Prefix}{code}";
            }

            return code;
        }
    }
}