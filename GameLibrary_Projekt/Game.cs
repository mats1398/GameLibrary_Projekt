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

        public string Plattform { get; set; }
        public int Score { get; set; }
        public string Review { get; set; }

        public override string ToString()
        {
            string plattform = "";
            foreach (var item in Plattform)
            {
                plattform += $", {item}";
            }
            return $"Titel: {Titel}";
        }



    }
}
