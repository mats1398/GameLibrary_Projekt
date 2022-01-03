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
        public string Titel { get; set; } 
        public DateTime ReleaseDate { get; set; } 
        public string GameDetails { get; set; } 
        public string Platform { get; set; }
        public int Score { get; set; } 
        public string Review { get; set; } 
        public string Image { get; set; }

        public string Genre { get; set; }
        
        public string  Comment { get; set; }

        /// <summary>
        /// Der Nutzer kann die Funktion "Save" nutzen, um seine Spiele in einer Textdatei zu speichern. Damit diese geordnet dargestellt werden, wurde die ToString Methode genutzt
        /// </summary>
        /// <returns>String mit allen Informationen des Spiels.</returns>
        public override string ToString()
        {
            
            return $"Titel: {Titel} \nReleaseDate: {ReleaseDate:MMMM dd, yyyy} \nPlatform: {Platform} \nGenre: {Genre} \nComment:{Comment}\n\n";
        }



    }
}
