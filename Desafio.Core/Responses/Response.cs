using System.Text.Json.Serialization;

namespace Desafio.Core.Responses
{
    public class Response<TData>
    {
        private int _statusCode;

        [JsonConstructor]
        public Response() => _statusCode = Configuration.DEFAULT_STATUS_CODE;

        public Response(TData? data, int statusCode = Configuration.DEFAULT_STATUS_CODE , string? message = null)
        {
            this.data = data;
            _statusCode = statusCode;
            this.message = message;
        }

        public TData? data { get; set; }
        public string? message { get; set; }

        [JsonIgnore]
        public bool IsSucess => _statusCode is >= 200 and <= 299;
    }
}
