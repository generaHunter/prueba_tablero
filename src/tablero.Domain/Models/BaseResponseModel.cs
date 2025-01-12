namespace tablero.Domain.Models
{
    public class BaseResponseModel
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public dynamic Data { get; set; }
    }
}
