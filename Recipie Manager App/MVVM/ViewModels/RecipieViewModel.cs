using Recipie_Manager_App.MVVM.Models;
using Recipie_Manager_App.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Recipie_Manager_App.MVVM.ViewModels;

public class RecipieViewModel : INotifyPropertyChanged
{
    public DatabaseService DatabaseService { get; set; }
    public ObservableCollection<RecipieModel> Recipies { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

    public RecipieViewModel(DatabaseService databaseService)
    {
        DatabaseService = databaseService;
        Recipies = new ObservableCollection<RecipieModel>();
        LoadRecipies();
    }

    public RecipieViewModel()
    {

    }

    private async void LoadRecipies()
    {
        var recipies = await DatabaseService.GetRecipieAsync();
        Recipies.Clear();
        foreach(var recipie in recipies)
        {
            Recipies.Add(recipie);
        }
    }

    public async void AddRecipieAsync(string name, string ingredients, string instructions)
    {
        var recipe = new RecipieModel()
        {
            Name = name,
            Ingredients = ingredients,
            Instructions = instructions
        };
        await DatabaseService.SaveRecipieAsync(recipe);
        Recipies.Add(recipe);
    }

    public async void RemoveRecipieAsync(RecipieModel recipie)
    {
        await DatabaseService.DeleteRecipieAsync (recipie);
        Recipies.Remove(recipie);
    }
    protected void OnPropertyChanged([CallerMemberName] string propName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
