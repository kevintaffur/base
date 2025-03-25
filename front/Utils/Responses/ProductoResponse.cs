using front.Models;

namespace front.Utils.Responses
{
    public class ProductoResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Producto Content { get; set; }
    }
}
