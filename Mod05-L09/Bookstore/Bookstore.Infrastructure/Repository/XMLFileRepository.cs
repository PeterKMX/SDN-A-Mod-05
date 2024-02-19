
using Bookstore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bookstore.Infrastructure.Repository
{
  public class XMLFileRepository<T> : IRepository<T> where T : IBaseItem
  {
    private List<T> dataList;

    private String BackendDataFolder = "";
    private String DataFileName = "";
    private String DataFilePathName = "";

    public void Configure(String dataFileName)
    {
      DataFileName = dataFileName;

      String dataDir = Directory.GetCurrentDirectory();
      BackendDataFolder = dataDir;
      BackendDataFolder = Path.Combine(BackendDataFolder, "Repo");
      if (!Directory.Exists(BackendDataFolder)) 
      { 
        Directory.CreateDirectory(BackendDataFolder); 
      }

      DataFilePathName = Path.Combine(BackendDataFolder, DataFileName);
      if (!File.Exists(DataFilePathName)) 
      { 
        SaveToDisk();      
      }
    }
    public void ReadFromDisk()
    {
      if (BackendDataFolder == string.Empty)
        throw new Exception("backend folder not found");
      if (!File.Exists(DataFilePathName))
        throw new Exception("backend file not found: \n\n"
          + DataFilePathName);

      dataList = null;
      FileStream fileStream = new FileStream(DataFilePathName, FileMode.Open);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
      dataList = (List<T>)xmlSerializer.Deserialize(fileStream);

      fileStream.Close();
    }
    public void SaveToDisk()
    {
      StreamWriter sw = new StreamWriter(DataFilePathName, false, Encoding.Unicode);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
      xmlSerializer.Serialize(sw, dataList);
      sw.Close();
    }
    public XMLFileRepository()
    {
      dataList = new List<T>();
    }
    public void Add(T t)
    {
      ReadFromDisk(); 
      int id = GenerateNewId();
      t.Id = id;
      dataList.Add(t);
      SaveToDisk(); 
    }
    public T Read(int id)
    {
      ReadFromDisk();
      T t = FindItemById(id);
      return t;
    }
    public List<T> ReadAll()
    {
      ReadFromDisk();
      return dataList;
    }
    public void Update(int Id, T t)
    {
      ReadFromDisk();
      int index = FindItemIndexById(Id);
      if (index != -1)
      {
        dataList[index] = t;
      }
      else
      {
        throw new Exception($"Error: Update: {Id} could not be found");
      }
      SaveToDisk() ;  
    }
    public void Delete(int id)
    {
      ReadFromDisk() ;  
      int index = FindItemIndexById(id);
      dataList.RemoveAt(index);
      SaveToDisk() ;
    }
    public List<int> GetIdList()
    {
      List<int> idList = new List<int>();
      for (int i = 0; i < dataList.Count; i++)
      {
        idList.Add(dataList[i].Id);
      }

      return idList;
    }
    public bool ItemListIsEmpty()
    {
      ReadFromDisk();
      return dataList.Count == 0;
    }
    //-----------------------------------------
    // generic helper functions
    //-----------------------------------------
    public T FindItemById(int id)
    {
      T itemFound = default;
      bool found = false;
      foreach (T t in dataList)
      {
        if (t.Id == id)
        {
          itemFound = t;
          found = true;
          break;
        }
      }

      if (!found)
      {
        throw new Exception($"Error: could not find item by Id {id}");
      }

      return itemFound;
    }
    private int FindItemIndexById(int id)
    {
      int index = -1;
      for (int i = 0; i < dataList.Count; i++)
      {
        if (dataList[i].Id == id)
        {
          index = i;
          break;
        }
      }

      return index;
    }

    //====================================================================
    // internal repo specific actions
    //----------------------------------------------------
    private int GenerateNewId()
    {
      int highestCurrendtId = 0;

      List<int> idList = new List<int>();
      foreach (T t in dataList)
      {
        idList.Add(t.Id);
      }

      if (idList.Count > 0)
      {
        // sorts ascending
        idList.Sort();
        highestCurrendtId = idList[idList.Count - 1];
      }

      int newId = highestCurrendtId + 1;
      return newId;
    }

  }
}
