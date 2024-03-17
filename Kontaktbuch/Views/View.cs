using Kontaktbuch.Models;

namespace Kontaktbuch.Views;

public class View
{
    public void PrintMenu()
    {
        Console.WriteLine("#####################################");
        Console.WriteLine("##### Willkommen im Kontaktbuch #####");
        Console.WriteLine("#####################################");
        Console.WriteLine();
        Console.WriteLine("# 1. KONTAKTE ANZEIGEN ");
        Console.WriteLine("# 2. KONTAKTE HINZUFÜGEN");
        Console.WriteLine("# 3. KONTAKTE SUCHEN");
        Console.WriteLine("# 4. KONTAKTE BEARBEITEN");
        Console.WriteLine("# 5. KONTAKTE LÖSCHEN");
        Console.WriteLine("# 6. KONTAKTE EXPORTIEREN");
        Console.WriteLine("# 7. PROGRAMM BEENDEN");
    } 
    
    public void ClearTerminal()
    {
        Console.Clear();
    } 
    
    public void PrintPressAnyKeyToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
    }
    
    public void PrintDuplicateExists()
    {
        Console.WriteLine();
        Console.WriteLine("A contact with the same email already exists. Aborting...");
    }
    
    public void PrintErrorHappened()
    {
        Console.WriteLine();
        Console.WriteLine("Woops. Look like something went wrong here. See the exception below and try again...");
        Console.WriteLine();
    }
    
    public void PrintEnterValidInteger()
    {
        Console.WriteLine();
        Console.Write("Please enter a valid number: ");
    }
    
    public void PromptContactWithIdDoesntExist(int id)
    {
        Console.WriteLine();
        Console.Write($"Contact with id {id} doesn't exist.");
    }
    
    public void PromptContactHasBeenDeleted()
    {
        Console.WriteLine();
        Console.WriteLine($"Contact successfully deleted.");
    }

    public void PrintContacts(IEnumerable<Contact> contacts)
    {
        foreach (var contact in contacts.OrderBy(e => e.Person.Lastname))
        {
            Console.WriteLine($"{contact.Id}:  Personal Details: {contact.Person.Firstname} {contact.Person.Lastname}, {contact.Person.Email}, {contact.Person.Birthdate?.ToString("dd.MM.YYYY")}");
            Console.WriteLine($"    Address: {contact.Address.Street}, {contact.Address.Postalcode} {contact.Address.City}, {contact.Address.Country}");
            Console.WriteLine();
        }
    }
    
    public void PrintNoContactsExistYet()
    {
        Console.WriteLine("There are no contacts yet. Create a contact first.");
    }
    
    public void PrintNoContactsInSearchResult()
    {
        Console.WriteLine("Your search query didn't match any existing contacts.");
    }
    
    public void PrintEscToExit(Contact contact)
    {
        Console.WriteLine($"(ESC) to exit");
    }
    
    public void PrintContactDetails(Contact contact)
    {
        Console.WriteLine($"{contact.Id}: {contact.Person.Firstname} {contact.Person.Lastname}, {contact.Person.Email}, {contact.Person.Birthdate?.ToString("dd.MM.YYYY")}");
        Console.WriteLine($"  Address: {contact.Address.Street}, {contact.Address.Postalcode} {contact.Address.City}, {contact.Address.Country}");
    }
    
    public void PrintCreateOrEditContactInformation()
    {
        Console.WriteLine();
        Console.WriteLine($"Please enter all information based on this pattern: ");
        Console.WriteLine($"Firstname, Lastname, Email, Birthdate (DD.MM.YYYY), Street, Postalcode, City, Country");
    }
    
    public void PrintSearchPrompt()
    {
        Console.Write($"Type any string to search for any contact: ");
    }
    
    public void PrintContactSuccessfullyExported()
    {
        Console.Write($"Contacts successfully exported.");
    }
    
    public void PrintDeleteContactPrompt()
    {
        Console.Write($"Type the ID of the contact you want to delete: ");
    }
    
    public void PrintExportPrompt()
    {
        Console.Write($"Type the path to the CSV file where you want to export your data to: ");
    }
    
    public void PrintEditContactPrompt()
    {
        Console.Write($"Type the ID of the contact you want to edit: ");
    }
}