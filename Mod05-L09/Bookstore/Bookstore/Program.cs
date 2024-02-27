// Bookstore version: v.5.2.6.25

using System.Reflection;

using Bookstore.Application;
using Bookstore.Domain.Services;
using Bookstore.Domain.Models;
using Bookstore.Application.AppActions;
using Bookstore.Application.Interfaces;
using Bookstore.Infrastructure.Repository;

namespace Bookstore
{
  internal class Program
  {
    static void Main(string[] args)
    {
      try
      {
        string appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        Console.Write($"App: M05-L09 Homework, v.{appVersion}, {DateTime.Now}");
        Console.WriteLine("");
        Console.WriteLine("Welcome to Bookstore application");
        Console.WriteLine("---------------------------------------");

        // configure persitent repositories for Book and JournalIssue   
        XMLFileRepository<Book> bookRepository = new XMLFileRepository<Book>();
        XMLFileRepository<JournalIssue> journalIssueRepository = new XMLFileRepository<JournalIssue>();
        bookRepository.Configure("BookRepo.xml");
        journalIssueRepository.Configure("JournalRepo.xml");

        // configure domain model services 
        ItemService<Book> itemServiceBook = new ItemService<Book>(bookRepository);
        ItemService<JournalIssue> itemServiceJournal = new ItemService<JournalIssue>(journalIssueRepository);

        // configure app menu 
        MenuService mainMenuService = new MenuService();
        mainMenuService.Configure();
        MenuManager mainMenu = new MenuManager(mainMenuService);

        // configure handling of user commands (use case requests received from menu)
        // via ActionDispatcher and via ActionService injection  
        IActionService actionService = new ActionService(itemServiceBook, itemServiceJournal);
        ActionDispatcher actionDispatcher = new ActionDispatcher(actionService);

        // handle user requests
        while (true)
        {
          MenuActionType userAction = mainMenu.SelectMenuItem();
          if (userAction == MenuActionType.Exit)
          {
            break;
          }
          actionDispatcher.OnUserAction(userAction);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Application error occurred {ex.ToString()}");
      }
      finally
      {
        Console.WriteLine();
        Console.WriteLine("STOPPING: Press any key to exit ...");
        string s = Console.ReadLine();
      }
    }
  }
}
