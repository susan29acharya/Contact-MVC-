namespace Contact__MVC_.Models
{
    public class ContactData
    {   public string? ContactId {  get; set; }
        public string? ContactNumber { get;set; }
        public string? ContactName { get;set; }
        public IFormFile? formFile { get; set; }
        public string? ImagePath { get; set; }
    }
}
