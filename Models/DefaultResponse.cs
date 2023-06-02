using System.Net;

namespace SistemAdminProducts.Models
{
    public class DefaultResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Ok { get; set; } = true;
        public List<string>? ErrorMessage { get; set; }
        public int TotalPages { get; set; } = 0;
        public int TotalRecords { get; set; } = 0;
        public object? Data { get; set; }
    }
}
