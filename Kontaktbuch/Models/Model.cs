using System.Text;
using Kontaktbuch.Repositories;

namespace Kontaktbuch.Models;

public class Model
{
    private readonly IContactRepository _contactRepository;

    public Model(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public List<Contact> GetContacts()
    {
        var contacts = _contactRepository.GetContacts();

        return contacts;
    }

    public void CreateContact(Contact contact)
    {
        var contacts = _contactRepository.GetContacts();
        var highestIdContact = contacts.MaxBy(e => e.Id);
        contact.Id = (highestIdContact?.Id ?? 0) + 1;
        _contactRepository.CreateContact(contact);
    }

    public bool ContactExists(int id)
    {
        var contacts = _contactRepository.GetContacts();

        return contacts.Any(e => e.Id == id);
    }

    public void EditContact(int id, Contact newContactInfo)
    {
        var contacts = _contactRepository.GetContacts();

        var toEdit = contacts.First(e => e.Id == id);
        newContactInfo.Id = id;
        
        _contactRepository.EditContact(toEdit);
    }

    public void DeleteContact(int id)
    {
        var contacts = _contactRepository.GetContacts();
        var toDelete = contacts.First(e => e.Id == id);
        
        _contactRepository.DeleteContact(toDelete);
    }

    public bool DuplicateExists(Contact contact)
    {
        var contacts = _contactRepository.GetContacts();

        return contacts.Any(e => e.Person.Email == contact.Person.Email);
    }

    public List<Contact> SearchContacts(string searchString)
    {
        var contacts = _contactRepository.GetContacts();

        return contacts.Where(e => e.Person.Firstname.Contains(searchString) ||
                                   e.Person.Lastname.Contains(searchString) ||
                                   e.Person.Email.Contains(searchString) ||
                                   (e.Person.Birthdate.HasValue && e.Person.Birthdate.Value.ToString("dd.MM.yyyy").Contains(searchString)) ||
                                    e.Address.Street.Contains(searchString) ||
                                    e.Address.Postalcode.Contains(searchString) ||
                                    e.Address.City.Contains(searchString) || e.Address.Country.Contains(searchString))
            .ToList();
    }

    public void Export(string path)
    {
        var contacts = _contactRepository.GetContacts();
        var writeText = new StringBuilder("ID;Firstname;Lastname;Email;Birthdate;Street;Postalcode;City;Country" + Environment.NewLine);
        
        foreach (var contact in contacts)
        {
            writeText.Append($"{contact.Id};{contact.Person.Firstname};{contact.Person.Lastname};" +
                             $"{contact.Person.Email};{contact.Person.Birthdate?.ToString("dd.MM.yyyy")};" +
                             $"{contact.Address.Street};{contact.Address.Postalcode};{contact.Address.City};" +
                             $"{contact.Address.Country}");

            writeText.Append(Environment.NewLine);
        }
        
        _contactRepository.Export(path, writeText.ToString());
    }
}