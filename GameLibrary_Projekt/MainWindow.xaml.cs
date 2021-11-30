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
            //ChangeUserControl.Children.Clear();
            //PersonallyGames personal = new PersonallyGames();
           
            //ChangeUserControl.Children.Add(personal);
            //SearchingGamesView searchView = new SearchingGamesView();
            //ChangeUserControl.Children.Add(searchView);



        }


        private void MoveApp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();

            }
        }

        private void OnCLick(object sender, RoutedEventArgs e)
        {
            //Text.Text = games[0].Titel; 
           
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
            
        //    ChangeUserControl.Children.Clear();
        //    SearchingGamesView searchView = new SearchingGamesView();
        //    ChangeUserControl.Children.Add(searchView);
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
            //AddPersonalGame(newgame);
        }
        private void ChangePersonalGame()
        {
            var list = Games.PersonallyList;
            string newList = $"Console, GameName,Review, Score \n";
            string pfad = @"C:\Users\Mats Ramsl\Desktop\Lokale Daten\SavedGames.txt";
            for (int i = 0; i < list.Count; i++)
            {
                newList += $"{list[i].Plattform}, {list[i].Titel}, {list[i].Review}, {list[i].Score} \n";
            }
            File.WriteAllText(pfad, newList);
        }
        private void AddPersonalGame(Game game)
        {
            if (string.IsNullOrEmpty(game.Plattform)|| string.IsNullOrEmpty(game.Titel) || string.IsNullOrEmpty(game.Review) || string.IsNullOrEmpty(game.Score.ToString()))
            {
                MessageBox.Show("Das von Ihnen eingegene Spiel, konnte nicht gespeichert werden");
                
            }
            else
            {
                string pfad = @"C:\Users\Mats Ramsl\Desktop\Lokale Daten\SavedGames.txt";
                string newLine = $"\n {game.Plattform}, {game.Titel}, {game.Review},{game.Score}  ";    // Console,GameName,Review,Score
                File.AppendAllText(pfad, newLine);
            }
            
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
            string newList = $"Console, GameName,Review, Score \n";
            string pfad = @"C:\Users\Mats Ramsl\Desktop\Lokale Daten\SavedGames.txt";
            for (int i = 0; i < list.Count; i++)
            {
                
                if (list[i].Titel != game.Titel)
                {
                    newList += $"{list[i].Plattform}, {list[i].Titel}, {list[i].Review}, {list[i].Score} \n";
                }
                
                  


            }
            File.WriteAllText(pfad, newList);
        }

        private void SearchPersonally_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text != "")
            {
                var newList = Games.PersonallyList.Where(game => game.Titel.ToLower().Contains(textBox.Text.ToLower()) || game.GameDetails.ToLower().Contains(textBox.Text.ToLower()));
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = newList;
            }
            else
            {
                PersonalDataGrid.ItemsSource = null;
                PersonalDataGrid.ItemsSource = Games.PersonallyList;
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text != "")
            {
               
                var newList = Games.gamelist.Where(game => game.Titel.ToLower().Contains(textBox.Text.ToLower()) || game.GameDetails.ToLower().Contains(textBox.Text.ToLower()));
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = newList;
            }
            else
            {
                SearchDataGrid.ItemsSource = null;
                SearchDataGrid.ItemsSource = Games.gamelist;
            }
        }

        private void AddGameToPersonal(object sender, RoutedEventArgs e)
        {
            Game game = (Game)SearchDataGrid.SelectedItem;
            string pfad = @"C:\Users\Mats Ramsl\Desktop\Lokale Daten\SavedGames.txt";
            string newLine = $"{game.Plattform}, {game.Titel}, {game.Review},{game.Score} \n";    // Console,GameName,Review,Score
            File.AppendAllText(pfad, newLine);
            Games.PersonallyList.Add(game);
            var  newList = Games.PersonallyList;
            PersonalDataGrid.ItemsSource = null;
            PersonalDataGrid.ItemsSource = newList;
        }

        private void PlatformChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            string platform = comboBox.SelectedItem as string;
            if (comboBox.SelectedIndex != -1)
            {
                if (Search.Text == "")
                {

                    var newList = Games.gamelist.Where(game => game.Plattform.EndsWith(platform));

                    SearchDataGrid.ItemsSource = null;
                    SearchDataGrid.ItemsSource = newList;
                }
                else
                {
                    var newList = Games.gamelist.Where(game => game.Plattform.EndsWith(platform) && game.Titel.ToLower().Contains(Search.Text.ToLower()));

                    SearchDataGrid.ItemsSource = null;
                    SearchDataGrid.ItemsSource = newList;
                }
            
            }
           

        }

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            FilterPlatform.SelectedIndex = -1;
            SearchDataGrid.ItemsSource = null;
            SearchDataGrid.ItemsSource = Games.gamelist;
        }
    }

}
