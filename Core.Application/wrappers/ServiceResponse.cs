namespace Core.Application.Wrappers
{
    public class ServiceResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ServiceResponse(T data)
        {
            Status = true;
            Message = "İşlem başarılı.";
            Data = data;
        }
        public ServiceResponse()
        {
            Status = true;
        }
    }
}