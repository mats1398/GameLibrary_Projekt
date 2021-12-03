using GameLibrary_Projekt.UserControls;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameLibrary_Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
           InitializeComponent();
        }


        private void MoveApp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();

            }
        }

       
        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ShutDown(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wollen Sie die App wirklich schließen?", "Programm schließen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
               
                
                this.Close(); 

            }



        }

        private void ChangeToSearchGames(object sender, RoutedEventArgs e)
        {
            Personal.Visibility = Visibility.Collapsed;
            GridSearch.Visibility = Visibility.Visible;       
        }

        private void ChangeToPersonalGames(object sender, RoutedEventArgs e)
        {
            GridSearch.Visibility = Visibility.Collapsed;
            Personal.Visibility = Visibility.Visible;
        }

        private void Personal_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            Game newgame = (Game)e.Row.DataContext;
            ChangePersonalGame();
            
        }
        private void ChangePersonalGame()
        {
            var list = Games.PersonallyList;
            string newList = $"Console, GameName,Review, Score \n";
            string pfad = @"C:\Users\Mats Ramsl\Desktop\Lokale Daten\SavedGames.txt";
            for (int i = 0; i < list.Count; i++)
            {
                newList += $"{list[i].Titel}, {list[i].Platform}, {list[i].ReleaseDate:MMMM dd, yyyy}, {list[i].GameDetails}, {list[i].Score}, {list[i].Review} \n";
            }
            File.WriteAllText(pfad, newList);
        }       

        private void PersonalDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender.GetType().Name == e.OriginalSource.GetType().Name)
            {
                if (e.Key == Key.Delete)
                {
                    Game game = PersonalDataGrid.SelectedItem as Game;
                    if (game != null)
                    {
                        DeleteGame(game);
                    }


                }
            }      

            

            
            
        }
        private void DeleteGame(Game game)
        {
            var list = Games.PersonallyList;
            string newList = $"Name, Platform, ReleaseDate, Summary, MetaScore, UserReview \n";
            string pfad = @"C:\Users\Mats Ramsl\Desktop\Lokale Daten\SavedGames.txt";
            for (int i = 0; i < list.Count; i++)
            {
                
                if (list[i].Titel != game.Titel)
                {
                    newList += $"{list[i].Titel}, {list[i].Platform}, {list[i].ReleaseDate:MMMM dd, yyyy}, {list[i].GameDetails}, {list[i].Score}, {list[i].Review} \n";
                }
                
                  


            }
            File.WriteAllText(pfad, newList);
        }

        private void SearchPersonally_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            FilterorSearchChanged();
           
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterorSearchChanged();
            
        }
        private void ReleaseYearChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterorSearchChanged();
        }
        private void PlatformChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterorSearchChanged();
            


        }

        private void AddGameToPersonal(object sender, RoutedEventArgs e)
        {
            Game game = (Game)SearchDataGrid.SelectedItem;
            string pfad = @"C:\Users\Mats Ramsl\Desktop\Lokale Daten\SavedGames.txt";
            string newLine = $"{game.Titel}, {game.Platform}, {game.ReleaseDate:MMMM dd, yyyy}, {game.GameDetails}, {game.Score}, {game.Review} \n";    // Console,GameName,Review,Score
            File.AppendAllText(pfad, newLine);
            Games.PersonallyList.Add(game);
            var  newList = Games.PersonallyList;
            PersonalDataGrid.ItemsSource = null;
            PersonalDataGrid.ItemsSource = newList;
        }

        
        private void FilterorSearchChanged()
        {
           

            if (SearchBox.Text != "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex != -1) //alle
            {
                var newList = Games.gamelist.Where(game =>  game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.Genre.Contains(FilterGenre.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex == -1) //Search, Platform, Genre
            {
                 var newList = Games.gamelist.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.Genre.Contains(FilterGenre.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;


            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex != -1) // Search, Platform, Release
            {
                var newList = Games.gamelist.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;


            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex != -1) // Search, Genre, Release
            {
                var newList = Games.gamelist.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Genre.Contains(FilterGenre.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;


            }
            else if(SearchBox.Text == "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex != -1) // Platform, Genre, Release
            {
                var newList = Games.gamelist.Where(game => game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.Genre.Contains(FilterGenre.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if(SearchBox.Text != "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex == -1) // Search
            {
                var newList = Games.gamelist.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;
            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex == -1) //Platform
            {
                var newList = Games.gamelist.Where(game => game.Platform.Contains(FilterPlatform.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;
            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex == -1) //Genre
            {
               var  newList = Games.gamelist.Where(game =>  game.Genre.Contains(FilterGenre.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex != -1) // Release
            {
               var  newList = Games.gamelist.Where(game => game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex == -1) //Search, Platform
            {
               var newList = Games.gamelist.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Platform.Contains(FilterPlatform.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex == -1) // Search, Genre
            {
               var newList = Games.gamelist.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower())  && game.Genre.Contains(FilterGenre.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex != -1) // Search, Release
            {
                var newList = Games.gamelist.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex != -1) //Genre, Release
            {
                var newList = Games.gamelist.Where(game => game.Genre.Contains(FilterGenre.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex == -1) // Platform, Genre
            {
                var newList = Games.gamelist.Where(game => game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.Genre.Contains(FilterGenre.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex != -1)// platform, Release
            {
                var newList = Games.gamelist.Where(game => game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else
            {
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = Games.gamelist;
            }







       
           

            


        }

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            FilterPlatform.SelectedIndex = -1;
            FilterRelease.SelectedIndex = -1;
            FilterGenre.SelectedIndex = -1;
            SearchBox.Clear();
            SearchDataGrid.ItemsSource = null;
            SearchDataGrid.ItemsSource = Games.gamelist;
        }

        

       

        private void PlatformChangedPersonal(object sender, SelectionChangedEventArgs e)
        {
            FilterorSearchChangedPersonal();
        }

        private void ReleaseYearChangedPesonal(object sender, SelectionChangedEventArgs e)
        {
            FilterorSearchChangedPersonal();
        }

        private void Search_TextChangedPersonal(object sender, TextChangedEventArgs e)
        {
            FilterorSearchChangedPersonal();
        }
        private void FilterorSearchChangedPersonal()
        {

            if (SearchBoxPersonal.Text != "" && FilterPlatformPersonal.SelectedIndex != -1 && FilterGenrePersonal.SelectedIndex != -1 && FilterReleasePersonal.SelectedIndex != -1) //alle
            {
                var newList = Games.PersonallyList.Where(game => game.Titel.ToLower().Contains(SearchBoxPersonal.Text.ToLower()) && game.Platform.Contains(FilterPlatformPersonal.SelectedItem as string) && game.Genre.Contains(FilterGenrePersonal.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterReleasePersonal.SelectedItem));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else if (SearchBoxPersonal.Text != "" && FilterPlatformPersonal.SelectedIndex != -1 && FilterGenrePersonal.SelectedIndex != -1 && FilterReleasePersonal.SelectedIndex == -1) //Search, Platform, Genre
            {
                var newList = Games.PersonallyList.Where(game => game.Titel.ToLower().Contains(SearchBoxPersonal.Text.ToLower()) && game.Platform.Contains(FilterPlatformPersonal.SelectedItem as string) && game.Genre.Contains(FilterGenrePersonal.SelectedItem as string));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;


            }
            else if (SearchBoxPersonal.Text != "" && FilterPlatformPersonal.SelectedIndex != -1 && FilterGenrePersonal.SelectedIndex == -1 && FilterReleasePersonal.SelectedIndex != -1) // Search, Platform, Release
            {
                var newList = Games.PersonallyList.Where(game => game.Titel.ToLower().Contains(SearchBoxPersonal.Text.ToLower()) && game.Platform.Contains(FilterPlatformPersonal.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterReleasePersonal.SelectedItem));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;


            }
            else if (SearchBoxPersonal.Text != "" && FilterPlatformPersonal.SelectedIndex == -1 && FilterGenrePersonal.SelectedIndex != -1 && FilterReleasePersonal.SelectedIndex != -1) // Search, Genre, Release
            {
                var newList = Games.PersonallyList.Where(game => game.Titel.ToLower().Contains(SearchBoxPersonal.Text.ToLower()) && game.Genre.Contains(FilterGenrePersonal.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterReleasePersonal.SelectedItem));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;


            }
            else if (SearchBoxPersonal.Text == "" && FilterPlatformPersonal.SelectedIndex != -1 && FilterGenrePersonal.SelectedIndex != -1 && FilterReleasePersonal.SelectedIndex != -1) // Platform, Genre, Release
            {
                var newList = Games.PersonallyList.Where(game => game.Platform.Contains(FilterPlatformPersonal.SelectedItem as string) && game.Genre.Contains(FilterGenrePersonal.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterReleasePersonal.SelectedItem));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else if (SearchBoxPersonal.Text != "" && FilterPlatformPersonal.SelectedIndex == -1 && FilterGenrePersonal.SelectedIndex == -1 && FilterReleasePersonal.SelectedIndex == -1) // Search
            {
                var newList = Games.PersonallyList.Where(game => game.Titel.ToLower().Contains(SearchBoxPersonal.Text.ToLower()));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;
            }
            else if (SearchBoxPersonal.Text == "" && FilterPlatformPersonal.SelectedIndex != -1 && FilterGenrePersonal.SelectedIndex == -1 && FilterReleasePersonal.SelectedIndex == -1) //Platform
            {
                var newList = Games.PersonallyList.Where(game => game.Platform.Contains(FilterPlatformPersonal.SelectedItem as string));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;
            }
            else if (SearchBoxPersonal.Text == "" && FilterPlatformPersonal.SelectedIndex == -1 && FilterGenrePersonal.SelectedIndex != -1 && FilterReleasePersonal.SelectedIndex == -1) //Genre
            {
                var newList = Games.PersonallyList.Where(game => game.Genre.Contains(FilterGenrePersonal.SelectedItem as string));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else if (SearchBoxPersonal.Text == "" && FilterPlatformPersonal.SelectedIndex == -1 && FilterGenrePersonal.SelectedIndex == -1 && FilterReleasePersonal.SelectedIndex != -1) // Release
            {
                var newList = Games.PersonallyList.Where(game => game.ReleaseDate.Year.Equals(FilterReleasePersonal.SelectedItem));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else if (SearchBoxPersonal.Text != "" && FilterPlatformPersonal.SelectedIndex != -1 && FilterGenrePersonal.SelectedIndex == -1 && FilterReleasePersonal.SelectedIndex == -1) //Search, Platform
            {
                var newList = Games.PersonallyList.Where(game => game.Titel.ToLower().Contains(SearchBoxPersonal.Text.ToLower()) && game.Platform.Contains(FilterPlatformPersonal.SelectedItem as string));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else if (SearchBoxPersonal.Text != "" && FilterPlatformPersonal.SelectedIndex == -1 && FilterGenrePersonal.SelectedIndex != -1 && FilterReleasePersonal.SelectedIndex == -1) // Search, Genre
            {
                var newList = Games.PersonallyList.Where(game => game.Titel.ToLower().Contains(SearchBoxPersonal.Text.ToLower()) && game.Genre.Contains(FilterGenrePersonal.SelectedItem as string));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else if (SearchBoxPersonal.Text != "" && FilterPlatformPersonal.SelectedIndex == -1 && FilterGenrePersonal.SelectedIndex == -1 && FilterReleasePersonal.SelectedIndex != -1) // Search, Release
            {
                var newList = Games.PersonallyList.Where(game => game.Titel.ToLower().Contains(SearchBoxPersonal.Text.ToLower()) && game.ReleaseDate.Year.Equals(FilterReleasePersonal.SelectedItem));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else if (SearchBoxPersonal.Text == "" && FilterPlatformPersonal.SelectedIndex == -1 && FilterGenrePersonal.SelectedIndex != -1 && FilterReleasePersonal.SelectedIndex != -1) //Genre, Release
            {
                var newList = Games.PersonallyList.Where(game => game.Genre.Contains(FilterGenrePersonal.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterReleasePersonal.SelectedItem));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else if (SearchBoxPersonal.Text == "" && FilterPlatformPersonal.SelectedIndex != -1 && FilterGenrePersonal.SelectedIndex != -1 && FilterReleasePersonal.SelectedIndex == -1) // Platform, Genre
            {
                var newList = Games.PersonallyList.Where(game => game.Platform.Contains(FilterPlatformPersonal.SelectedItem as string) && game.Genre.Contains(FilterGenrePersonal.SelectedItem as string));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else if (SearchBoxPersonal.Text == "" && FilterPlatformPersonal.SelectedIndex != -1 && FilterGenrePersonal.SelectedIndex == -1 && FilterReleasePersonal.SelectedIndex != -1)// platform, Release
            {
                var newList = Games.PersonallyList.Where(game => game.Platform.Contains(FilterPlatformPersonal.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterReleasePersonal.SelectedItem));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;

            }
            else
            {
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = Games.PersonallyList;
            }
        }
        private void ResetFilterPersonal(object sender, RoutedEventArgs e)
        {
            FilterPlatformPersonal.SelectedIndex = -1;
            FilterReleasePersonal.SelectedIndex = -1;
            FilterGenrePersonal.SelectedIndex = -1;
            SearchBoxPersonal.Clear();
            PersonalDataGrid.ItemsSource = null;
            PersonalDataGrid.ItemsSource = Games.PersonallyList;
            
        }
    }

}
