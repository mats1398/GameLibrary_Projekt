using System;
using System.Collections.Generic;
using System.IO;

namespace GameLibrary_Projekt
{
    public class Games
    {
        public static List<Game> gamelist { get; set; } = GetGames(@"all_games.csv");
        public static List<Game> PersonallyList { get; set; } = GetGames(@"c:..\SavedGames.txt");

        public static List<Game> GetGames(string pfad)
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
        
        public static string GetGenre(int counter)
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
        public static string GetImagePath( int counter)
        {
            int zahl = (counter % 10) + 1;         

            string s = $"/ExampleGameImages/Bild{zahl}.jpg";
            return s;

        }
        public static DateTime GetRandomDate()
        {
            Random rnd = new Random();

            int randomYear = rnd.Next(1995, DateTime.Now.Year + 10);
            int randomMonth = rnd.Next(1, 12);
            int randomDay = rnd.Next(1, 28);

            DateTime randomDate = new DateTime(randomYear, randomMonth, randomDay);


            return randomDate;
        }
        public Game[] GetAllGames()
        {
            Game[] array = new Game[gamelist.Count];

            for (int i = 0; i < gamelist.Count; i++)
            {

                array[i] = gamelist[i];
            }
            return array;
        }
    }
}
