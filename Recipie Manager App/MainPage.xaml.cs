using Recipie_Manager_App.MVVM.Models;
using Recipie_Manager_App.MVVM.ViewModels;
using Recipie_Manager_App.Services;

namespace Recipie_Manager_App
{
    public partial class MainPage : ContentPage
    {
        private RecipieViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "SQLite.db3");
            var database = new DatabaseService(dbPath);
            viewModel = new RecipieViewModel(database);
            BindingContext = viewModel;
        }

        private void OnAddRecipieClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RecipieNameEntry.Text) &&
                !string.IsNullOrEmpty(IngredeintsEditor.Text) &&
                 !string.IsNullOrEmpty(InstructionsEditor.Text))
            {
                var name = RecipieNameEntry.Text;
                var ingredients = IngredeintsEditor.Text;
                var instructions = InstructionsEditor.Text;
                viewModel.AddRecipieAsync(name, ingredients, instructions);
                RecipieNameEntry.Text = string.Empty;
                IngredeintsEditor.Text = string.Empty;
                InstructionsEditor.Text = string.Empty;
            }
        }

        private void OnDeleteRecipieClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var recipie = (RecipieModel)button.CommandParameter;
            viewModel.RemoveRecipieAsync(recipie);
        }
    }

}
