using Kontaktbuch.Models;
using Kontaktbuch.Views;

namespace Kontaktbuch.Controllers;

public class Controller
{
    private readonly View _view;
    private readonly Model _model;

    public Controller(View view, Model model)
    {
        _view = view;
        _model = model;
    }
    
    public ConsoleKeyInfo ReadUserInput()
    {
        return Console.ReadKey();
    }

    public void ProcessMenuInput(ConsoleKeyInfo consoleKeyInfo)
    {
        try
        {
            var key = consoleKeyInfo.Key;
            switch (key)
            {
                case ConsoleKey.D1: ListContacts();
                    break;
                case ConsoleKey.D2: CreateContact();
                    break;
                case ConsoleKey.D3: SearchContacts();
                    break;
                case ConsoleKey.D4: EditContact();
                    break;
                case ConsoleKey.D5: DeleteContact();
                    break;
                case ConsoleKey.D6: ExportContacts();
                    break;
                case ConsoleKey.D7: ExitProgram();
                    break;
            }
        }
        catch (Exception e)
        {
            _view.PrintErrorHappened();
            Console.WriteLine(e);
            _view.PrintPressAnyKeyToContinue();
            AskForCharacterInput();
        }
    }

    private void ListContacts()
    {
        _view.ClearTerminal();
        var contacts = _model.GetContacts();
        _view.PrintContacts(contacts);

        if (contacts.Count == 0)
        {
            _view.PrintNoContactsExistYet();
        }
        
        _view.PrintPressAnyKeyToContinue();
        AskForCharacterInput();
    }

    private void CreateContact()
    {
        _view.ClearTerminal();
        _view.PrintCreateOrEditContactInformation();
        var contact = AskForLineInput();
        var details = contact.Split(", ");

        var newContact = CreateContactFromDetailsArray(details);

        if (_model.DuplicateExists(newContact))
        {
            _view.PrintDuplicateExists();
            AskForCharacterInput();
            return;
        }
        
        _model.CreateContact(CreateContactFromDetailsArray(details));
        
        _view.PrintPressAnyKeyToContinue();
        AskForCharacterInput();
    }

    private void SearchContacts()
    {
        _view.ClearTerminal();
        _view.PrintSearchPrompt();
        var searchCommand = AskForLineInput();
        var contacts = _model.SearchContacts(searchCommand);

        if (contacts.Count == 0)
        {
            _view.PrintNoContactsInSearchResult();            
        }
        else
        {
            _view.PrintContacts(contacts);
        }
        

        _view.PrintPressAnyKeyToContinue();
        AskForCharacterInput();
    }

    private void EditContact()
    {
        _view.ClearTerminal();
        _view.PrintEditContactPrompt();

        var id = AskForIntegerCharacterInput();
        
        _view.PrintCreateOrEditContactInformation();

        if (!_model.ContactExists(id))
        {
            _view.PromptContactWithIdDoesntExist(id);
            return;
        }
        
        var contact = AskForLineInput();
        var details = contact.Split(", ");
        
        var contactToEdit = CreateContactFromDetailsArray(details);

        if (_model.DuplicateExists(contactToEdit))
        {
            _view.PrintDuplicateExists();
            AskForCharacterInput();
            return;
        }
        
        _model.EditContact(id, contactToEdit);
        
        _view.PrintPressAnyKeyToContinue();
        AskForCharacterInput();
    }

    private void DeleteContact()
    {
        _view.ClearTerminal();
        _view.PrintDeleteContactPrompt();
        var id = AskForIntegerCharacterInput();
        _model.DeleteContact(id);
        _view.PromptContactHasBeenDeleted();
        
        _view.PrintPressAnyKeyToContinue();
        AskForCharacterInput();
    }

    private void ExportContacts()
    {
        _view.ClearTerminal();
        _view.PrintDeleteContactPrompt();
        var path = AskForLineInput();

        _model.Export(path);
        _view.PrintContactSuccessfullyExported();

        _view.PrintPressAnyKeyToContinue();
        AskForCharacterInput();
    }

    private void ExitProgram()
    {
        Environment.Exit(0);
    }

    private string AskForLineInput()
    {
        return Console.ReadLine() ?? "";
    }

    private int AskForIntegerCharacterInput()
    {
        var input = Console.ReadKey().KeyChar;
        
        while (!Int32.TryParse(input.ToString(), out _))
        {
            _view.PrintEnterValidInteger();
            input = Console.ReadKey().KeyChar;
        }
        
        return Int32.Parse(input.ToString());
    }

    private string AskForCharacterInput()
    {
        return Console.ReadKey().KeyChar.ToString();
    }

    private Contact CreateContactFromDetailsArray(string[] details)
    {
        if (details.Length != 8)
        {
            throw new Exception(
                "There must be exactly 8 comma seperated values provided. If you want to have an empty value, " +
                "you still must write down the comma.");
        }
        
        return new Contact
        {
            CreatedDate = DateTime.Now,
            Person =
            {
                Firstname = details[0],
                Lastname = details[1],
                Email = details[2],
                Birthdate = !string.IsNullOrEmpty(details[3]) ? DateTime.Parse(details[3]) : null,
            },
            Address =
            {
                Street = details[4],
                Postalcode = details[5],
                City = details[6],
                Country = details[7],
            }
        };
    }
}