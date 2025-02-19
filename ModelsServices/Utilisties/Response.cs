namespace ModelsServices.Utilisties
{
    public class Response
    {
        public Guid Id { get; set; }
        public string? Message { get; set; }
        public object? Content { get; set; }
        public int TypeResponse { get; set; }
    }
}
