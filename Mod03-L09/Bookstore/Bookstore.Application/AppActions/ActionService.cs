using Bookstore.Application.Interfaces;
using Bookstore.Domain.Models;
using Bookstore.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.AppActions
{
  public class ActionService : IActionService
  {
    ItemService<Book> _itemServiceBook;
    ItemService<JournalIssue> _itemServiceJournal;

    VMActionsBook _vmBookActions;
    VMActionsJournal _vmJournalActions;

    public ActionService(
    ItemService<Book> itemServiceB,
    ItemService<JournalIssue> itemServiceJ)
    {
      _itemServiceBook = itemServiceB;
      _itemServiceJournal = itemServiceJ;

      _vmBookActions = new VMActionsBook();
      _vmJournalActions = new VMActionsJournal();
    }

    // Book
    public void OnAddBook()
    {
      Book newBook = _vmBookActions.Create();
      _itemServiceBook.Add(newBook);
    }
    public void OnReadBookDetails()
    {
      if (_itemServiceBook.ItemListIsEmpty())
      {
        Console.WriteLine("[Backend list is empty]");
        return;
      }

      List<int> validIds = _itemServiceBook.GetIdList();
      int id = _vmBookActions.SelectId(validIds);
      Book book = _itemServiceBook.Read(id);
      _vmBookActions.Show(book);
    }
    public void OnEditBook()
    {
      List<int> validIds = _itemServiceBook.GetIdList();
      int id = _vmBookActions.SelectId(validIds);
      Book book = _itemServiceBook.Read(id);

      Book bookUpdate = _vmBookActions.Edit(book);
      _itemServiceBook.Update(id, bookUpdate);
    }
    public void OnDeleteBook()
    {
      if (_itemServiceBook.ItemListIsEmpty())
      {
        Console.WriteLine("[Backend list is empty]");
        return;
      }

      List<int> validIds = _itemServiceBook.GetIdList();
      int id = _vmBookActions.SelectId(validIds);
      _itemServiceBook.Delete(id);

      Console.WriteLine("[DONE]");
    }
    public void OnShowBooksList()
    {
      List<Book> list = _itemServiceBook.ReadAll();
      _vmBookActions.ShowList(list);
    }
    public void OnAutogenBooksList()
    {
      Console.WriteLine("[Adding 3 books...]");

      Book book;

      book = new Book();
      book.Title = "The C# Programming Language";
      book.Authors = "Anders Hejlsberg";
      book.Publisher = "Wiley";
      book.Year = 2014;
      _itemServiceBook.Add(book);

      book = new Book();
      book.Title = "Beginning gRPC with ASP.NET Core 6";
      book.Authors = "Anthony Giretti";
      book.Publisher = "Apress";
      book.Year = 2022;
      _itemServiceBook.Add(book);

      book = new Book();
      book.Title = "Learn PHP 8";
      book.Authors = "Steve Prettyman";
      book.Publisher = "Apress";
      book.Year = 2020;
      _itemServiceBook.Add(book);
    }

    // Journal
    public void OnAddJournal()
    {
      JournalIssue journalIssue = _vmJournalActions.Create();
      _itemServiceJournal.Add(journalIssue);
    }
    public void OnReadJournalDetails()
    {
      if (_itemServiceJournal.ItemListIsEmpty())
      {
        Console.WriteLine("[Backend list is empty]");
        return;
      }

      List<int> validIDs = _itemServiceJournal.GetIdList();
      int id = _vmJournalActions.SelectId(validIDs);
      JournalIssue issue = _itemServiceJournal.Read(id);
      _vmJournalActions.Show(issue);
    }
    public void OnEditJournal()
    {
      if (_itemServiceJournal.ItemListIsEmpty())
      {
        Console.WriteLine("[Backend list is empty]");
        return;
      }

      List<int> validIDs = _itemServiceJournal.GetIdList();
      int id = _vmJournalActions.SelectId(validIDs);
      JournalIssue issue = _itemServiceJournal.Read(id);

      JournalIssue editedIssue = _vmJournalActions.Edit(issue);
      _itemServiceJournal.Update(id, editedIssue);
    }
    public void OnDeleteJournal()
    {
      if (_itemServiceJournal.ItemListIsEmpty())
      {
        Console.WriteLine("[Backend list is empty]");
        return;
      }

      List<int> validIDs = _itemServiceJournal.GetIdList();
      int id = _vmJournalActions.SelectId(validIDs);
      _itemServiceJournal.Delete(id);

      Console.WriteLine("[DONE]");
    }
    public void OnShowJournalList()
    {
      List<JournalIssue> issues = _itemServiceJournal.ReadAll();
      _vmJournalActions.ShowList(issues);
    }
    public void OnAutogenJournalList()
    {
      JournalIssue issue = null;

      issue = new JournalIssue();
      issue.Name = "MSDN Magazine";
      issue.Year = 2019;
      issue.Vol = 34;
      issue.Nr = 1;
      _itemServiceJournal.Add(issue);

      issue = new JournalIssue();
      issue.Name = "MSDN Magazine";
      issue.Year = 2019;
      issue.Vol = 34;
      issue.Nr = 2;
      _itemServiceJournal.Add(issue);

      issue = new JournalIssue();
      issue.Name = "MSDN Magazine";
      issue.Year = 2019;
      issue.Vol = 34;
      issue.Nr = 3;
      _itemServiceJournal.Add(issue);

      Console.WriteLine("[DONE]");
    }

  }
}
