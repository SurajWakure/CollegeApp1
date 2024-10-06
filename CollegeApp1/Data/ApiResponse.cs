using System.Net;

namespace Collegeapp1.Data
{
    public class ApiResponse
    {
        public bool status { get; set; }
        public HttpStatusCode Statuscode { get; set; }
        public dynamic data { get; set; }
        public List<string> Errors  { get; set; }
    }
}
