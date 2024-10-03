using Recipie_Manager_App.MVVM.Models;
using SQLite;

namespace Recipie_Manager_App.Services;

public class DatabaseService
{
    public SQLiteAsyncConnection Connection { get; set; }
    public SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite|
        SQLiteOpenFlags.SharedCache| 
        SQLiteOpenFlags.Create;
    public DatabaseService(string path)
    {   SQLiteConnection connection = new SQLiteConnection(path, Flags);
        Connection = new SQLiteAsyncConnection(path);
        Connection.CreateTableAsync<RecipieModel>().Wait();
    }

    public Task<List<RecipieModel>> GetRecipieAsync()
    {
        return Connection.Table<RecipieModel>().ToListAsync();
    }        

    public Task<int> SaveRecipieAsync(RecipieModel model)
    {
        return model.Id != 0 ? Connection.UpdateAsync(model) : Connection.InsertAsync(model);
    }

    public Task<int> DeleteRecipieAsync(RecipieModel model)
    {
        return Connection.DeleteAsync(model);
    }
}
