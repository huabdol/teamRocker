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

namespace Reciepe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Database.SetInitializer<RecipeContext>(new EFRecipeorganizer());
            using (RecipeContext context = new RecipeContext())
            {
                List<Recipe> recipes = (from a in context.Recipes
                                        orderby a.Title
                                        select a).ToList();

                if (recipes.Count == 0)
                {
                    Console.WriteLine("\tNo recipes at this time.");
                }
                else
                {
                    //TitleListBox.DataContext = (from recipe in )

                }
            }
        }
    }
}
