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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using RecipeOrganizerDatabase;
using System.Xml.Linq;

namespace Reciepe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static RecipesContext recipesContext = null;
        public MainWindow()
        {
            InitializeComponent();
            displayLabel.Content = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.SetInitializer<RecipesContext>(new RecipesContextInitializer());
                using (recipesContext = new RecipesContext())
                {
                    if (recipesContext.Recipes.Count() == 0)
                    {
                        throw new Exception();
                    }
                    TitleListBox.DataContext = (from a in recipesContext.Recipes
                                                orderby a.Title
                                                select a).ToArray();


                }
            }
            catch (Exception ex)
            {
                displayMessage(RecipesContextInitializer.PrintAllInnerException(ex));
            }

        }

        public void displayMessage(string message)
        {
            displayLabel.Content = message;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            using (recipesContext = new RecipesContext())
            {
  
            List<Recipe> recipes = (from a in recipesContext.Recipes
                                    orderby a.RecipeID
                                    select a).ToList();

                XDocument document = new XDocument(
                  new XDeclaration("1.0", "utf-8", "yes"),
                  new XComment("Contents of authors table in Pubs database"),
                  new XElement("Recipes",
                      from r in recipes  
                      select new XElement("Recipe",
                                 new XElement("RecipeID", r.RecipeID),
                                 new XElement("Title", r.Title),
                                 new XElement("Yield", r.Yield),
                                 new XElement("ServingSize", r.ServingSize),
                                 new XElement("Directions", r.Directions),
                                 new XElement("Comment", r.Comment),
                                 new XElement("RecipeType", r.RecipeType))
                  )
                );

                document.Save(@"..\..\..\Recipes.xml");

                List<Ingredient> ingredients = (from i in recipesContext.Ingredients
                                        orderby i.IngredientID
                                        select i).ToList();

                document = new XDocument(
                  new XDeclaration("1.0", "utf-8", "yes"),
                  new XComment("Contents of authors table in Pubs database"),
                  new XElement("Ingredients",
                      from i in ingredients 
                      select new XElement("Ingredient",
                                 new XElement("IngredientID", i.IngredientID),
                                 new XElement("Description", i.Description),
                                 new XElement("RecipeID", i.RecipeID))
                  )
                );

                document.Save(@"..\..\..\Ingredients.xml");


            }
        }

        private void TitleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (recipesContext = new RecipesContext())
            {


                Recipe recipe = (Recipe)TitleListBox.SelectedItem;
                Ingredient ingredient = (from i in recipesContext.Ingredients where i.RecipeID == recipe.RecipeID select i).First();


                TitleTB.Text = recipe.Title;
                YieldTB.Text = recipe.Yield;
                ServignSizeTB.Text = recipe.ServingSize;
                DirectionsTB.Text = recipe.Directions;
                CommentsTB.Text = recipe.Comment;
                IngredientsTB.Text = ingredient.Description;
                RecipeTypeTB.Text = recipe.RecipeType;

            }
        }

        //private void Label_Initialized(object sender, EventArgs e)
        //{

        //}
    }
}