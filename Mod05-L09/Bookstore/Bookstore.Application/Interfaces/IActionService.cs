using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Interfaces
{
  public interface IActionService
  {
    // these actions are a mapping from the use cases 
    // supported by the application 

    // - Book actions
    void OnAddBook();
    void OnReadBookDetails();
    void OnEditBook();
    void OnDeleteBook();
    void OnShowBooksList();
    void OnAutogenBooksList();

    // - Journal actions
    void OnAddJournal();
    void OnReadJournalDetails();
    void OnEditJournal();
    void OnDeleteJournal();
    void OnShowJournalList();
    void OnAutogenJournalList();
  }
}
