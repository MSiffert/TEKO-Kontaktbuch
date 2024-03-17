namespace Kontaktbuch.Models;

public class Contact
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Person Person { get; set; } = new();
    public Address Address { get; set; } = new();
}