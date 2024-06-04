namespace WebApp.Models.DTO;

public class AddressDto {
    public int Id { get; set; }
    public int Number { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public int ZipCode { get; set; }
}