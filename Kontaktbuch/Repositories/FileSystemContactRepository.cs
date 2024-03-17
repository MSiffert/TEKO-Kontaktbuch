using System.Text;
using System.Text.Json;
using Kontaktbuch.Models;

namespace Kontaktbuch.Repositories;

public class FileSystemContactRepository : IContactRepository
{
    public List<Contact> GetContacts()
    {
        if (!File.Exists(GetContactsFilePath()))
        {
            return new List<Contact>();
        }
        
        var path = GetContactsFilePath();
        var contactsJson = File.ReadAllText(path);
        var contacts = JsonSerializer.Deserialize<List<Contact>>(contactsJson);

        return contacts ?? new List<Contact>();
    }

    public void CreateContact(Contact contact)
    {
        var contacts = GetContacts();
        contacts.Add(contact);
        
        var contactsJson = JsonSerializer.Serialize(contacts);
        File.WriteAllText(GetContactsFilePath(), contactsJson, Encoding.UTF8);
    }

    public void EditContact(Contact contact)
    {
        var contacts = GetContacts();
        var toEdit = contacts.First(e => e.Id == contact.Id);

        contacts[contacts.IndexOf(toEdit)] = contact;
        
        var contactsJson = JsonSerializer.Serialize(contacts);
        File.WriteAllText(GetContactsFilePath(), contactsJson, Encoding.UTF8);
    }

    public void DeleteContact(Contact contact)
    {
        var contacts = GetContacts();
        var toEdit = contacts.First(e => e.Id == contact.Id);

        contacts.RemoveAt(contacts.IndexOf(toEdit));
        
        var contactsJson = JsonSerializer.Serialize(contacts);
        File.WriteAllText(GetContactsFilePath(), contactsJson);
    }

    public void Export(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    private string GetContactsFilePath()
    {
        return Path.Combine("data.json");
    }
}