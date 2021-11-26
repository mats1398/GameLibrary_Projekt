using GameLibrary_Projekt.UserControls;
using System.IO;
using System.Windows;
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
    }

}
