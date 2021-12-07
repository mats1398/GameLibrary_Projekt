using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary_Projekt
{
    public class Game 
    {
        // Eigenschaften
        public string Titel { get; set; } //
        public DateTime ReleaseDate { get; set; } //
        public string GameDetails { get; set; } //
        public string Platform { get; set; }//
        public int Score { get; set; } //
        public string Review { get; set; } //
        public string Image { get; set; }

        public string Genre { get; set; }
        public bool IsPublished { get; set; } //
        public string  Comment { get; set; }
        public override string ToString()
        {
            
            return $"\n __________________________________________\n Titel: {Titel} \nReleaseDate: {ReleaseDate:MMMM dd, yyyy} \nPlatform: {Platform} \nGenre: {Genre} \nComment:{Comment}\n\n";
        }



    }
}
