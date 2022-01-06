using System;
using System.Collections.Generic;
using System.IO;

namespace GameLibrary_Projekt
{
    public class Games
    {
        public static List<Game> GameList { get; set; } = GetGames(@"all_games.csv");
        public static List<Game> PersonallyList { get; set; } = GetGames(@"c:..\SavedGames.txt");

        /// <summary>
        /// Diese Methode ist dazu da, eine Datei mit Spieledaten auszulesen und diesee einer Liste hinzuzufügen. Die Liste, der die Spiele hinzugefügt werden, ernthält Instanzen von "Game"
        /// </summary>
        /// <param name="pfad"> Da zwei verschiedene Liste eingelesen werden, wird der Pfad jeweils mit übergeben</param>
        /// <returns>Liste von Spielen </returns>

        private static List<Game> GetGames(string pfad)
        {
            if (pfad == @"c:..\SavedGames.txt")
            {
                if (File.Exists(pfad) == false)
                {
                    File.WriteAllText(pfad, $"Name, Platform, ReleaseDate, Summary, MetaScore, UserReview, Comment \n");
                }
                
                
            }

            var list = new List<Game>();
            
            File.ReadAllLines(pfad);
            var reader = new StreamReader(pfad);
            string line =  reader.ReadLine();
            Random rnd = new Random();
            int random = rnd.Next(1, 9);
            int counter = random;
           

            
            string[] column = line.Split(',');
            if (pfad == @"c:..\SavedGames.txt")
            {
                if (column.Length != 7)
                {
                    throw new FileFormatException("Wrong Format");
                }
            }
            else
            {
                if (column.Length != 6)
                {
                    throw new FileFormatException("Wrong Format");
                }
            }
            
            
            
            while ((line = reader.ReadLine()) != null)
            {
               
                line = line.Replace("\"", String.Empty);
                Game game = new Game();
                if (pfad == @"c:..\SavedGames.txt")
                {
                    string[] specialColumn= line.Split('<');
                    if (specialColumn.Length > 2)                        
                    {
                        string s = "";
                        for (int i = 1; i < specialColumn.Length; i++)
                        {
                            s += specialColumn[i];
                        }
                        game.Comment = s;
                    }
                    else
                    {
                        game.Comment = specialColumn[1];

                    }

                    line = specialColumn[0];
                }
              
                
                column = line.Split(',' );
                
                game.Score = int.Parse(column[column.Length -2]);
                game.Review = column[column.Length - 1];
                
                game.Titel = column[0];
                if (DateTime.TryParse($"{column[2]}, {column[3]}", out DateTime result) == true)
                {
                    game.ReleaseDate = DateTime.Parse($"{column[2]}, {column[3]}");
                }
                else
                {
                    game.ReleaseDate = GetRandomDate();
                }
                
                
                game.Platform = column[1];
                
                for (int i = 4 ; i < column.Length-2; i++)
                {
                    
                    game.GameDetails += column[i];
                }
                game.Image = GetImagePath(counter);
                game.Genre = GetGenre(counter);             
                
                list.Add(game);
                counter += random;
            }
            reader.Close();
            return list;



           
            





        }
        /// <summary>
        /// Da die Dateien (all_Games.csv und SavedGames.txt) kein Genre enthalt, wird dies mit dieser Methode erzeugt
        /// </summary>
        /// <param name="counter"> Der Counter wird in der GetGames Methode auf einen Random-Wert gesetzt und mitübergeben. Dies wird getan, damit die Reihenfolge der Genere immer unterschidlich ist.</param>
        /// <returns>Ein Genre als String</returns>
        private static string GetGenre(int counter)
        {
            int zahl = counter % 10;
            if (zahl == 0)
            {
                return "Roleplay";
            }
            else if (zahl == 1)
            {
                return "Shooter";
            }
            else if (zahl == 2)
            {
                return "Action";
            }
            else if (zahl == 3)
            {
                return "Simulation";
            }
            else if (zahl == 4)
            {
                return "Adventure";
            }
            else if (zahl == 5)
            {
                return "Sports";
            }
            else if (zahl == 6)
            {
                return "Racing";
            }
            else if (zahl == 7)
            {
                return "Strategy";
            }
            else if (zahl == 8)
            {
                return "Military";
            }
            else 
            {
                return "Phantasie";
            }

        }

        /// <summary>
        /// Da die Dateien (all_Games.csv und SavedGames.txt) kein CoberBild der Spiele enthalten, wird dies mit dieser Methode erzeugt
        /// </summary>
        /// <param name="counter"></param>
        /// <returns>Ein String, der den Pfad zu einem Spiel darstellt</returns>
        private static string GetImagePath( int counter)
        {
            int zahl = (counter % 10) + 1;         

            string s = $"/ExampleGameImages/Bild{zahl}.jpg";
            return s;

        }

        /// <summary>
        /// In der all_Games.csv ist die Bezichung innerhalb der Spalten unterschiedlich, weswegen stark Vereinzelt die Spalten nicht richtig eingelesen werden und nicht als Datum erkannt werden.
        /// Damit trotzdem ein Datum bei diesen Spielen steht, wird die Rndomisiert erzeugt
        /// </summary>
        /// <returns>Datum</returns>
        private static DateTime GetRandomDate()
        {
            Random rnd = new Random();

            int randomYear = rnd.Next(1995, DateTime.Now.Year + 10);
            int randomMonth = rnd.Next(1, 12);
            int randomDay = rnd.Next(1, 28);

            DateTime randomDate = new DateTime(randomYear, randomMonth, randomDay);


            return randomDate;
        }
        
    }
}
