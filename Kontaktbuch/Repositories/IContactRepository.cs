using Kontaktbuch.Models;

namespace Kontaktbuch.Repositories;

public interface IContactRepository
{
    public List<Contact> GetContacts();
    public void CreateContact(Contact contact);
    public void EditContact(Contact contact);
    public void DeleteContact(Contact contact);
    public void Export(string path, string content);
}