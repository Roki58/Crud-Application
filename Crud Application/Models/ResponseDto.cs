
namespace Crud_Application.Models
{
    public class ResponseDto
    {
        public string StatusCode {  get; set; }
        public string Msg { get; set; }
        public object? Data { get; set; }
      
    }
}
