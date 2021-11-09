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
        public DateTime Erscheinungsdatum { get; set; }

        public string[] Plattform { get; set; }
        public decimal Rating { get; set; }
        public string Genre { get; set; }

       

    }
}
