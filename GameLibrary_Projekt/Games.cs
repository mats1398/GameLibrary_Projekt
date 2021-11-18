using System;
using System.Collections.Generic;
using System.IO;

namespace GameLibrary_Projekt
{
    public class Games
    {
        public static List<Game> gamelist { get; set; } = GetGames();
         
        public static List<Game> GetGames()
        {
            var list = new List<Game>();
            string pfad = @"C:\Users\Mats Ramsl\Desktop\Lokale Daten\Games.csv";
            File.ReadAllLines(pfad);
            var reader = new StreamReader(pfad);
            string line =  reader.ReadLine();
           

            
            string[] column = line.Split(',');
            
            
            if (column.Length != 4)
            {
                throw new FileFormatException("Wrong format");
            }
            while ((line = reader.ReadLine()) != null)
            {
                
                line = line.Replace("\"", String.Empty);
                //line.Replace("\\", String.Empty);
              
                
                column = line.Split(',' , '-');
                Game game = new Game();
                game.Score = int.Parse(column[column.Length -1]);
                game.Review = column[column.Length - 2];
                
                game.Titel = column[column.Length - 3].Replace("Review", "");
                game.Erscheinungsdatum = GetRandomDate();
                int j = 0;
                game.Plattform = new string[column.Length - 3];
                for (int i = column.Length-4 ; i >= 0; i--)
                {
                    
                    game.Plattform[j] = column[i];
                    j++;
                }
                list.Add(game);


                
            }
            return list;
           





            reader.Close();

            DateTime GetRandomDate()
            {

                Random rnd = new Random();

                int randomYear = rnd.Next(1995, DateTime.Now.Year + 10);
                int randomMonth = rnd.Next(1, 12);
                int randomDay = rnd.Next(1, 28);

                DateTime randomDate = new DateTime(randomYear, randomMonth, randomDay);
                

                return randomDate;
            }
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
