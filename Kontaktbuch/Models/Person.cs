namespace Kontaktbuch.Models;

public class Person
{
    public string Firstname { get; set; } = "";
    public string Lastname { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime? Birthdate { get; set; }
}