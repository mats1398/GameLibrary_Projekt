using System;
using System.Collections.Generic;
using System.IO;

namespace GameLibrary_Projekt
{
    public class Games
    {
        public static List<Game> gamelist { get; set; } = GetGames();
        public static List<Game> PersonallyList { get; set; } = GetPersonallyGames();

        public static List<Game> GetGames()
        {
           

            var list = new List<Game>();
            string pfad = @"all_games.csv";
            File.ReadAllLines(pfad);
            var reader = new StreamReader(pfad);
            string line =  reader.ReadLine();
            Random rnd = new Random();
            int random = rnd.Next(1, 9);
            int counter = random;
           

            
            string[] column = line.Split(',');
            if (column.Length != 6)
            {
                throw new FileFormatException("Wrong Format");
            }
            
            
            while ((line = reader.ReadLine()) != null)
            {
               
                    line = line.Replace("\"", String.Empty);
                //line.Replace("\\", String.Empty);
              
                
                column = line.Split(',' );
                Game game = new Game();
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
                
                
                game.Plattform = column[1];

                if (game.ReleaseDate <= DateTime.Now)
                {
                    game.IsPublished = true;
                }
                
                for (int i = 4 ; i < column.Length-2; i++)
                {
                    
                    game.GameDetails += column[i];
                }
                game.Image = GetRandomImagePath();
                game.Genre = GetGenre(counter);
               
                
                    list.Add(game);






                counter += random;
            }
            reader.Close();
            return list;



           
            





        }
        public static List<Game> GetPersonallyGames()
        {
            var list = new List<Game>();
            string pfad = @"C:\Users\Mats Ramsl\Desktop\Lokale Daten\SavedGames.txt";
            File.ReadAllLines(pfad);
            var reader = new StreamReader(pfad);
            string line = reader.ReadLine();



            string[] column = line.Split(',');


            if (column.Length != 4)
            {
                throw new FileFormatException("Wrong format");
            }
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\"", String.Empty);
                //line.Replace("\\", String.Empty);


                column = line.Split(',', '-');
                Game game = new Game();
                game.Score = int.Parse(column[column.Length - 1]);
                game.Review = column[column.Length - 2];

                game.Titel = column[column.Length - 3].Replace("Review", "");
                game.ReleaseDate = GetRandomDate();
                if (game.ReleaseDate <= DateTime.Now)
                {
                    game.IsPublished = true;
                }
                int j = 0;
                for (int i = column.Length - 4; i >= 0; i--)
                {
                    string platform = column[i].Replace(" ", String.Empty);
                    game.Plattform += platform;
                    j++;
                }
                game.Image = GetRandomImagePath();
                game.GameDetails = "Hallo";
                list.Add(game);




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
            else             {
                return "Phantasie";
            }

        }
        public static string GetRandomImagePath()
        {
            Random rnd = new Random();
            int random =  rnd.Next(1, 7);
            string s = $"/ExampleGameImages/Bild{random}.jpg";
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
