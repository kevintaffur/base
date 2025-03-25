using front.Models;

namespace front.Utils.Responses
{
    public class ProductoListResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<Producto> Content { get; set; }
    }
}
