using SQLite;

namespace Recipie_Manager_App.MVVM.Models;

public class RecipieModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ingredients   { get; set; }
    public string Instructions {  get; set; }
}
