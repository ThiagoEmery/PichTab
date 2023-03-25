using System.Net;

namespace PichTabs_API.Modelos
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;

        public List<string> Errors { get; set; }

        public object Resultado { get; set; }
    }
}
