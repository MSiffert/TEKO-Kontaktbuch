using Kontaktbuch.Models;

namespace Kontaktbuch.Repositories;

public class InMemoryRepository : IContactRepository
{
    private readonly List<Contact> _contacts = new();
    
    public List<Contact> GetContacts()
    {
        return _contacts;
    }

    public void CreateContact(Contact contact)
    {
        _contacts.Add(contact);
    }

    public void EditContact(Contact contact)
    {
        var toEditIndex = _contacts.IndexOf(_contacts.First(e => e.Id == contact.Id));
        _contacts[toEditIndex] = contact;
    }

    public void DeleteContact(Contact contact)
    {
        var toDeleteIndex = _contacts.IndexOf(_contacts.First(e => e.Id == contact.Id));
        _contacts.RemoveAt(toDeleteIndex);
    }

    public void Export(string path, string content)
    {
        throw new NotSupportedException("Cannot export contacts when using an InMemory Repository.");
    }
}