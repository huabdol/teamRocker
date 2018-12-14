using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RecipeOrganizerDatabase;

namespace Reciepe
{
    /// <summary>
    /// Interaction logic for AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public AddEditWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (RecipesContext context = new RecipesContext())
            {
                Recipe r = new Recipe();
                r.Yield = YieldText.Text;
                r.Comment = CommentText.Text;
                if (TitleText.Text != null && ServingSizeText.Text != null && DirectionsText.Text != null && IngredientsText.Text != null && (MealRadio.IsChecked == true || DessertRadio.IsChecked == true))
                {
                    r.Title = TitleText.Text;
                    r.ServingSize = ServingSizeText.Text;
                    r.Directions = DirectionsText.Text;
                    if (MealRadio.IsChecked == true)
                        r.RecipeType = "Meal Item";
                    else if (DessertRadio.IsChecked == true)
                        r.RecipeType = "Dessert Item";

                    var IngredientList = (IngredientsText.Text).Trim().Split(';');

                    foreach (var i in IngredientList)
                    {
                        Ingredient newIngredient = new Ingredient();
                        newIngredient.Description = i;
                        newIngredient.RecipeID = r.RecipeID;
                        context.Ingredients.Add(newIngredient);
                       
                    }

                    context.Recipes.Add(r);
                    context.SaveChanges();
                    this.Close();
                }

                else
                    ErrorLabel.Content = "Please fill all the mandatory fields";
            }
        }

        



        private void MealRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
