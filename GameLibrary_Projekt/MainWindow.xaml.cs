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
            SearchingGamesView searchView = new SearchingGamesView();
            ChangeUserControl.Children.Add(searchView);



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
    }

}
