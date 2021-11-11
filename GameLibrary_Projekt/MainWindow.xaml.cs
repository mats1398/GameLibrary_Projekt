using System.Windows;


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
            Games g = new Games(@"C:\Users\Mats Ramsl\Desktop\Lokale Daten\Games.csv");
            //Games g2 = new Games(@"Games.csv");

            

        }
    }
}
