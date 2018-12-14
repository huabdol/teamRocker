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
using SearchLibrary;


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
                    RefreshTitleBox();
                }
            }
            catch (Exception ex)
            {
                displayMessage(RecipesContextInitializer.PrintAllInnerException(ex));
            }

        }

        public void RefreshTitleBox()
        {
            if (recipesContext.Recipes.Count() == 0)
            {
                throw new Exception();
            }



            //TitleListBox.DataContext 
            var recipeArray = (from a in recipesContext.Recipes
                               orderby a.RecipeType, a.Title
                               select a).ToArray();

            TitleListBox.DataContext = FilterRecipeType(recipeArray);


            displayLabel.Content = null;

        }
        private List<Recipe> FilterRecipeType(Recipe[] recipesArray)
        {
            List<Recipe> reciepes = new List<Recipe>();
            foreach (var recipe in recipesArray)
            {
                if (recipe.RecipeType.Trim().Equals("Meal Item"))
                {
                    MealItem mealItem = new MealItem(recipe.RecipeID, recipe.Title, recipe.Yield, recipe.ServingSize, recipe.Directions, recipe.Comment, recipe.RecipeType);
                    reciepes.Add(mealItem);
                }
                else
                {
                    Dessert dessert = new Dessert(recipe.RecipeID, recipe.Title, recipe.Yield, recipe.ServingSize, recipe.Directions, recipe.Comment, recipe.RecipeType);
                    reciepes.Add(dessert);
                }
            }
            return reciepes;
        }
        public void displayMessage(string message)
        {
            displayLabel.Content = message;

        }


        private void TitleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (recipesContext = new RecipesContext())
            {

                if (TitleListBox.SelectedItem != null)
                {
                    Recipe recipe = (Recipe)TitleListBox.SelectedItem;
                    //Ingredient ingredient = (from i in recipesContext.Ingredients where i.RecipeID == recipe.RecipeID select i).First();
                    var ingredient = (from i in recipesContext.Ingredients where i.RecipeID == recipe.RecipeID select i).ToList();

                    TitleTB.Text = recipe.Title;
                    YieldTB.Text = recipe.Yield;
                    ServignSizeTB.Text = recipe.ServingSize;
                    DirectionsTB.Text = recipe.Directions;
                    CommentsTB.Text = recipe.Comment;
                    //IngredientsTB.Text = ingredient.Description;
                    IngredientsListbox.DataContext = ingredient;
                    RecipeTypeTB.Text = recipe.RecipeType;
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            TitleTB.Text = null;
            YieldTB.Text = null;
            ServignSizeTB.Text = null;
            DirectionsTB.Text = null;
            CommentsTB.Text = null;
            IngredientsTB.Text = null;
            RecipeTypeTB.Text = null;

            TitleListBox.SelectedItem = null;
            using (recipesContext = new RecipesContext())
            {
                RefreshTitleBox();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            displayMessage("The window cannot be closed, please use the exit button to close the application");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
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
            Environment.Exit(0);

        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshButton_Click(null, null);

            SearchDialogue searchDialogue = new SearchDialogue();

            if (searchDialogue.ShowDialog() == true)
            {


            }
            var keywordArray = (searchDialogue.Keywords).Trim().Split(';');
            List<Recipe> foundRecipeList = new List<Recipe>();


            using (recipesContext = new RecipesContext())
            {

                var recipeList = (from a in recipesContext.Recipes
                                  orderby a.RecipeType, a.Title
                                  select a).ToList();
                foreach (var x in recipeList)
                {
                    List<string> recipeProperties = new List<string>();
                    Ingredient ingredient = (from i in recipesContext.Ingredients where i.RecipeID == x.RecipeID select i).First();
                    recipeProperties.Add(ingredient.Description);
                    recipeProperties.Add(x.Comment);
                    recipeProperties.Add(x.Directions);
                    recipeProperties.Add(x.RecipeID.ToString());
                    recipeProperties.Add(x.RecipeType);
                    recipeProperties.Add(x.ServingSize);
                    recipeProperties.Add(x.Title);
                    recipeProperties.Add(x.Yield);

                    bool found = SearchLibrary.SearchLibrary.IsKeywordInList(keywordArray, recipeProperties);
                    recipeProperties = null;
                    if (found)
                    {
                        if (x.RecipeType.Trim().Equals("Meal Item"))
                        {
                            MealItem mealItem = new MealItem(x.RecipeID, x.Title, x.Yield, x.ServingSize, x.Directions, x.Comment, x.RecipeType);
                            foundRecipeList.Add(mealItem);
                        }
                        else
                        {
                            Dessert dessert = new Dessert(x.RecipeID, x.Title, x.Yield, x.ServingSize, x.Directions, x.Comment, x.RecipeType);
                            foundRecipeList.Add(dessert);
                        }


                    }

                }

            }

            TitleListBox.DataContext = (from a in foundRecipeList
                                        orderby a.RecipeType, a.Title
                                        select a).ToArray();
            if (foundRecipeList.Count == 0)
            {
                displayMessage("No Recipe matching the keywords searched.");
            }
            foundRecipeList = null;
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddEditWindow addEditWindow = new AddEditWindow();

            if(addEditWindow.ShowDialog()==true)
            { }

        }
    }
}