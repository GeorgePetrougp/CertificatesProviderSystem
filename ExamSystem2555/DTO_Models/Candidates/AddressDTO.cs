namespace WebApp.DTO_Models.Candidates
{
    public class AddressDTO
    {
        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public string? State { get; set; }
        public string? Province { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
    }
}