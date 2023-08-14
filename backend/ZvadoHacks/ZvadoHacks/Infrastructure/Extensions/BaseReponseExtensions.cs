using Newtonsoft.Json;
using ZvadoHacks.Models.Shared;

namespace ZvadoHacks.Infrastructure.Extensions
{
    public static class BaseReponseExtensions
    {
        public static string Error<TData>(this BaseResponse<TData> response, string errorMessage)
        {
            response.ErrorMessage = errorMessage;
            response.IsSuccess = false;

            var responseParsed = JsonConvert.SerializeObject(response);

            return responseParsed;
        }

        public static string Success<TData>(this BaseResponse<TData> response)
        {
            response.IsSuccess = true;

            var responseParsed = JsonConvert.SerializeObject(response);

            return responseParsed;
        }
    }
}
