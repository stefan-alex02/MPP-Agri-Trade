namespace Domain.Details;

public class Address : Entity<int> {
    public int Number { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public int ZipCode { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    
    public Address() : base(default) {
    }
    
    public Address(int id, int number, string street, string city, string county, int zipCode, float latitude, float longitude) : 
        base(id) {
        Number = number;
        Street = street;
        City = city;
        County = county;
        ZipCode = zipCode;
        Latitude = latitude;
        Longitude = longitude;
    }
}