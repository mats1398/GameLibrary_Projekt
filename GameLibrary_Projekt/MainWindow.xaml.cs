using GameLibrary_Projekt.UserControls;
using Microsoft.Win32;
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
        private string pfad = @"c:..\SavedGames.txt";

        /// <summary>
        /// Die App wird durch ein durchgänginges Mausklicken verschoben.
        /// </summary>
       
        private void MoveApp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();

            }
        }

       /// <summary>
       /// Durch den Button "-" oben rechts in der Ecke, wird diese Methode aufgerufen und die App wird minimiert
       /// </summary>
      
        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// Durch den Button "x" oben rechts in der Ecke, wird diese Methode aufgerufen und die App schließt sich. Bevor die App sich schließt, erscheint eine Messagebox, ob die App wirklich geschlossen werden soll
        /// </summary>
        private void ShutDown(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wollen Sie die App wirklich schließen?", "Programm schließen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
               
                
                this.Close(); 

            }



        }
        /// <summary>
        /// Die Ansicht in der App wird mit dieser Methode von dem PersonalGrid auf das SearchGrid gewechselt. Diese Methode wird durch den Knopf SearchGames ausgelöst
        /// </summary>
      
        private void ChangeToSearchGames(object sender, RoutedEventArgs e)
        {
            Personal.Visibility = Visibility.Collapsed;
            GridSearch.Visibility = Visibility.Visible;
            SavePersonalListButton.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        ///  Die Ansicht in der App wird mit dieser Methode von dem SearchGrid auf das PersonalGrid gewechselt. Diese Methode wird durch den Knopf Your Games ausgelöst
        /// </summary>

        private void ChangeToPersonalGames(object sender, RoutedEventArgs e)
        {
            GridSearch.Visibility = Visibility.Collapsed;
            Personal.Visibility = Visibility.Visible;
            SavePersonalListButton.Visibility = Visibility.Visible;

        }
        /// <summary>
        /// Diese Methode wird aufgerufen, wenn sich in einer Zeile im PersonalDataGrid was geändert hat. Da dadurch die Textdatei SavedGames.txt angepasst werden muss, wird die ChangePersonalGame Methode aufgerufen
        /// </summary>

        private void Personal_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            ChangePersonalGame();
            
        }
        /// <summary>
        /// Diese Methode ist dafür da, um die vorhandene SavedGames.txt mit den neuen Inhalten des DataGrids zu überschreiben. Dadurch wird gegeben, dass die Änderungen der Daten gespeichert werden.
        /// </summary>
        private void ChangePersonalGame()
        {
            var list = Games.PersonallyList;
            string newList = $"Name, Platform, ReleaseDate, Summary, MetaScore, UserReview, Comment \n";
           
            for (int i = 0; i < list.Count; i++)
            {
                newList += $"{list[i].Titel},{list[i].Platform},{list[i].ReleaseDate:MMMM dd, yyyy},{list[i].GameDetails},{list[i].Score},{list[i].Review} <{list[i].Comment} \n";
            }
            File.WriteAllText(pfad, newList);
        }
        /// <summary>
        /// In dieser Zeile wird geprüft, ob der Nutzer eine Zeile im DataGrid gelöscht hat. Wenn dies zutrifft, wird de DeleteGame Methode aufgerúfen und der SelectedItem des PersonalDataGrids übergeben 
        /// </summary>
        /// <param name="sender">Wenn e.Key der entf Knopf ist + der "sender" Typ == DataGridCell, wird die DeleteGame Methode aufgerufen</param>
        /// <param name="e"> Wenn e.Key der entf Knopf ist + der "sender" Typ == DataGridCell, wird die DeleteGame Methode aufgerufen</param>
        private void PersonalDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string s = sender.GetType().Name;
            if ("DataGridCell" == e.OriginalSource.GetType().Name)
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
        /// <summary>
        /// Diese Methode löscht ein Spiel aus der PersonalGames Liste
        /// </summary>
        /// <param name="game"> Wird der Methode übergeben. Das Spiel, welches den gleichen Titel und Gerne hat wird aus der List gelöscht bzw. nicht erneut reingeschrieben</param>
        private void DeleteGame(Game game)
        {
            var list = Games.PersonallyList;
            
            string newList = $"Name, Platform, ReleaseDate, Summary, MetaScore, UserReview, Comment \n";
            for (int i = 0; i < list.Count; i++)
            {
                
                if (list[i].Titel != game.Titel && list[i].Genre != game.Genre)
                {
                    newList += $"{list[i].Titel},{list[i].Platform},{list[i].ReleaseDate:MMMM dd, yyyy},{list[i].GameDetails},{list[i].Score},{list[i].Review} <{list[i].Comment} \n";
                }
                
                
                  


            }
            File.WriteAllText(pfad, newList);
        }
        
        /// <summary>
        /// Wird durch ein Verändern in der Suchleiste des GridSearch aufgerufen. Diese Methode ruft die FilterOrSearchChanged Methode auf. 
        /// </summary>

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterorSearchChanged();
            
        }
        /// <summary>
        /// Wird durch ein Verändern eines Filters des Search Grids aufgerufen. Diese Methode ruft die FilterOrSearchChanged Methode auf. 
        /// </summary
        private void SearchFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterorSearchChanged();
        }

        /// <summary>
        /// Wird durch die "Add" Knöpfe in dem SearchDataGrid aufgerufen. Diese Methode speichert das ausgewählt Spiel in die PersonalList und in SavedGames.txt Datei
        /// </summary>
        
        private void AddGameToPersonal(object sender, RoutedEventArgs e)
        {
            Game game = (Game)SearchDataGrid.SelectedItem;
            
            if (Games.PersonallyList.Contains(game) == false)
            {
                string newLine = $"{game.Titel},{game.Platform},{game.ReleaseDate:MMMM dd, yyyy},{game.GameDetails},{game.Score},{game.Review} <{game.Comment} \n";    // Console,GameName,Review,Score
                File.AppendAllText(pfad, newLine);
                Games.PersonallyList.Add(game);
                var newList = Games.PersonallyList;
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;
            }
            else
            {
                MessageBox.Show("You already have this Game in your Library", "", MessageBoxButton.OK);
                
            }
            
        }
        /// <summary>
        /// Wenn ein sich ein Filter oder der Text in der Suchleiste des SearchGrids verändert, wird diese Methode aufgerufen. Diese Methode beinhaltet alle möglichen Kombinationen der Filter und der Suchleiste und verändert den Inhalt des DataGrids dementsprechend direkt
        /// </summary>
        
        private void FilterorSearchChanged()
        {
           

            if (SearchBox.Text != "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex != -1) //alle
            {
                var newList = Games.GameList.Where(game =>  game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.Genre.Contains(FilterGenre.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex == -1) //Search, Platform, Genre
            {
                 var newList = Games.GameList.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.Genre.Contains(FilterGenre.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;


            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex != -1) // Search, Platform, Release
            {
                var newList = Games.GameList.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;


            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex != -1) // Search, Genre, Release
            {
                var newList = Games.GameList.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Genre.Contains(FilterGenre.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;


            }
            else if(SearchBox.Text == "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex != -1) // Platform, Genre, Release
            {
                var newList = Games.GameList.Where(game => game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.Genre.Contains(FilterGenre.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if(SearchBox.Text != "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex == -1) // Search
            {
                var newList = Games.GameList.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;
            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex == -1) //Platform
            {
                var newList = Games.GameList.Where(game => game.Platform.Contains(FilterPlatform.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;
            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex == -1) //Genre
            {
               var  newList = Games.GameList.Where(game =>  game.Genre.Contains(FilterGenre.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex != -1) // Release
            {
               var  newList = Games.GameList.Where(game => game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex == -1) //Search, Platform
            {
               var newList = Games.GameList.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.Platform.Contains(FilterPlatform.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex == -1) // Search, Genre
            {
               var newList = Games.GameList.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower())  && game.Genre.Contains(FilterGenre.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text != "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex != -1) // Search, Release
            {
                var newList = Games.GameList.Where(game => game.Titel.ToLower().Contains(SearchBox.Text.ToLower()) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex == -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex != -1) //Genre, Release
            {
                var newList = Games.GameList.Where(game => game.Genre.Contains(FilterGenre.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex != -1 && FilterRelease.SelectedIndex == -1) // Platform, Genre
            {
                var newList = Games.GameList.Where(game => game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.Genre.Contains(FilterGenre.SelectedItem as string));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else if (SearchBox.Text == "" && FilterPlatform.SelectedIndex != -1 && FilterGenre.SelectedIndex == -1 && FilterRelease.SelectedIndex != -1)// platform, Release
            {
                var newList = Games.GameList.Where(game => game.Platform.Contains(FilterPlatform.SelectedItem as string) && game.ReleaseDate.Year.Equals(FilterRelease.SelectedItem));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;

            }
            else
            {
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = Games.GameList;
            }  
           

            


        }
        /// <summary>
        /// Alle Filter + die Suchleiste des SearchGrids wird zurückgesetzt. Dadurch werden die Einschränkungen der angezeigten Spiele entfernt und es sind wieder alle Spiele sichtbar
        /// </summary>
      

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            FilterPlatform.SelectedIndex = -1;
            FilterRelease.SelectedIndex = -1;
            FilterGenre.SelectedIndex = -1;
            SearchBox.Clear();
            SearchDataGrid.ItemsSource = null;
            SearchDataGrid.ItemsSource = Games.GameList;
        }        

        /// <summary>
        /// Wird durch ein Verändern in der Suchleiste des PersonalDataGrids aufgerufen. Diese Methode ruft die FilterOrSearchChangedPersonal Methode auf. 
        /// </summary>
        private void Search_TextChangedPersonal(object sender, TextChangedEventArgs e) 
        {
            FilterorSearchChangedPersonal();
        }
        /// <summary>
        /// Wird durch ein Verändern eines Filters des PersonalGrids aufgerufen. Diese Methode ruft die FilterOrSearchChanged Methode auf. 
        /// </summary
        private void PersonalFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterorSearchChangedPersonal();
        }

        /// <summary>
        /// Wenn ein sich ein Filter oder der Text in der Suchleiste des PersonalGrids verändert, wird diese Methode aufgerufen. Diese Methode beinhaltet alle möglichen Kombinationen der Filter und der Suchleiste und verändert den Inhalt des DataGrids dementsprechend direkt
        /// </summary>
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
        /// <summary>
        /// Alle Filter + die Suchleiste des PersonalGrids wird zurückgesetzt. Dadurch werden die Einschränkungen der angezeigten Spiele entfernt und es sind wieder alle Spiele sichtbar
        /// </summary>
        private void ResetFilterPersonal(object sender, RoutedEventArgs e)
        {
            FilterPlatformPersonal.SelectedIndex = -1;
            FilterReleasePersonal.SelectedIndex = -1;
            FilterGenrePersonal.SelectedIndex = -1;
            SearchBoxPersonal.Clear();
            PersonalDataGrid.ItemsSource = null;
            PersonalDataGrid.ItemsSource = Games.PersonallyList;
            
        }
        /// <summary>
        /// Speichert alle Spiele die sich in der PersonallyList befinden ab indem die Methode, eine Textdatei erstellt und dort die wichtigsten Spieleinfomationen reinschreibt.
        /// Wird durch den Save Knopf ausgelöst
        /// Der Nutzer kann durch einen Dialog, der sich öffnet, entscheiden wo er diese Datei speichern möchte.
        /// </summary>      

        private void OnSavePersonalList(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = @"C:\";
            saveFile.RestoreDirectory = true;
            saveFile.Filter = "txt files (*.txt) | *.txt";
            saveFile.DefaultExt = "txt";
            saveFile.FileName = "Name_PersonalGameList.txt";
            bool? result = saveFile.ShowDialog();
            if (result == true)
            {
               Stream stream = saveFile.OpenFile();
               StreamWriter sWriter = new StreamWriter(stream);


                sWriter.WriteLine($"My Games: \n __________________________________________\n");

                foreach (Game game in Games.PersonallyList)
                {
                    sWriter.WriteLine(game);
                }
                sWriter.Close();
                stream.Close();

                
            }
            
        }

        
    }

}
