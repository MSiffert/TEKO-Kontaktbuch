using Kontaktbuch.Models;
using Kontaktbuch.Repositories;

namespace Kontaktbuch.Tests;

public class Tests
{
    [Test]
    public void WhenNoContactsExist_CreateContact_ShouldCreateContact()
    {
        var repository = new InMemoryRepository();
        
        repository.CreateContact(new Contact
        {
            Id = 0,
            CreatedDate = DateTime.Now,
            Person =
            {
                Firstname = "Mike",
                Lastname = "Muster",
                Birthdate = new DateTime(2020, 2, 20),
                Email = "mike.muster@gmail.com"
            },
            Address =
            {
                City = "Testcity",
                Country = "Testcountry",
                Postalcode = "4000",
                Street = "Musterstrasse"
            }
        });

        var result = repository.GetContacts();

        Assert.AreEqual(result.Count, 1);
    }
}