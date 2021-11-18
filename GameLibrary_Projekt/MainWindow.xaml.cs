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
        Game[] gameslist;
        
        public MainWindow()
        {
            InitializeComponent();
            Games g = new Games();
             gameslist =   g.GetAllGames();

            foreach (Game game in gameslist)
            {
                
                
                System.Console.WriteLine(game.ToString());
                
                
            }
            

            System.Console.ReadLine();
            


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
            listview.SelectedItem=gameslist.ToString();
        }
    }

}
