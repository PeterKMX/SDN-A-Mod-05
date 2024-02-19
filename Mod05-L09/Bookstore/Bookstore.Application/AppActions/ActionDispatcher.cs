using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bookstore.Application.Interfaces;
using Bookstore.Domain.Models;
using Bookstore.Domain.Services;

namespace Bookstore.Application
{
  public class ActionDispatcher
  {
    IActionService _iActionService;
    public ActionDispatcher(IActionService iActionService)
    {
      _iActionService = iActionService;
    }
    public void OnUserAction(MenuActionType actionType)
    {
      // action types are mapped from use cases which can executed from app menu
      // _iActionService is the interface for the implementation of 
      // these use cases
      switch (actionType)
      {
        case MenuActionType.AddBook:
          _iActionService.OnAddBook();
          break;
        case MenuActionType.ReadBookDetails:
          _iActionService.OnReadBookDetails();
          break;
        case MenuActionType.EditBook:
          _iActionService.OnEditBook();
          break;
        case MenuActionType.RemoveBook:
          _iActionService.OnDeleteBook();
          break;
        case MenuActionType.ReadBooksList:
          _iActionService.OnShowBooksList();
          break;
        case MenuActionType.AutogenBooksList:
          _iActionService.OnAutogenBooksList();
          break;
        case MenuActionType.AddJournal:
          _iActionService.OnAddJournal();
          break;
        case MenuActionType.ReadDetailsJournal:
          _iActionService.OnReadJournalDetails();
          break;
        case MenuActionType.EditJournal:
          _iActionService.OnEditJournal();
          break;
        case MenuActionType.RemoveJournal:
          _iActionService.OnDeleteJournal();
          break;
        case MenuActionType.ReadJournalList:
          _iActionService.OnShowJournalList();
          break;
        case MenuActionType.AutogenJournalList:
          _iActionService.OnAutogenJournalList();
          break;

        default:
          throw new Exception("Error: unknown action");
      }
    }
  }
}
