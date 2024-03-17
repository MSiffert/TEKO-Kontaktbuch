// See https://aka.ms/new-console-template for more information

using Kontaktbuch.Controllers;
using Kontaktbuch.Models;
using Kontaktbuch.Repositories;
using Kontaktbuch.Views;

namespace Kontaktbuch;

public class Program
{
    public static void Main(string[] arg)
    {
        var view = new View();
        var repository = new FileSystemContactRepository();
        var model = new Model(repository);
        var controller = new Controller(view, model);

        while (true)
        {
            view.ClearTerminal();
            view.PrintMenu();
            var userInput = controller.ReadUserInput();
            controller.ProcessMenuInput(userInput);
        }
    } 
}