namespace ZvadoHacks.Models.Shared
{
    public class BaseResponse<T>
    {
        public T Data { get; set; } = default!;

        public string RequestId { get; set; } = Guid.NewGuid().ToString();

        public DateTime Time { get; set; } = DateTime.UtcNow;

        public string ErrorMessage { get; set; } = default!;

        public bool IsSuccess { get; set; } 
        
        public void SetData(T data)
        {
            Data = data;
        }
    }
}
