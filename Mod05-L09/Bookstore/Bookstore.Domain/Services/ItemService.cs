
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bookstore.Domain.Interfaces;

namespace Bookstore.Domain.Services
{
  public class ItemService<T> : IItemService<T> where T : IBaseItem
  {
    private IRepository<T> _repoGeneric;

    public ItemService(IRepository<T> r)
    {
      _repoGeneric = r;
    }

    public void Add(T t)
    {
      _repoGeneric.Add(t);
    }

    public T Read(int Id)
    {
      return _repoGeneric.Read(Id);
    }

    public List<T> ReadAll()
    {
      return _repoGeneric.ReadAll();
    }

    public void Update(int Id, T t)
    {
      _repoGeneric.Update(Id, t);
    }

    public void Delete(int Id)
    {
      _repoGeneric.Delete(Id);
    }

    public List<int> GetIdList()
    {
      return _repoGeneric.GetIdList();
    }

    public bool ItemListIsEmpty()
    {
      return _repoGeneric.ItemListIsEmpty();
    }
  }
}
