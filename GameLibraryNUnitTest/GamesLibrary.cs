using GameLibrary_Projekt;
using NUnit.Framework;


namespace GameLibraryNUnitTest
{
    public class Tests
    {
       
        [Test]
        public void GetGameList() //bestanden
        {
            var list = Games.GameList;
            Assert.AreEqual("The Legend of Zelda: Ocarina of Time", list[0].Titel);
        }

        [Test]
        public void GetPersonalList() // bestanden
        {
            var list = Games.PersonallyList;

            Assert.IsNotNull(list);
        }

        //--> diese beiden Tests wurden auskommentiert, da die Methoden auf private gesetzt worden sind
        //[Test]
        //public void GetGenre()
        //{
        //    string s = Games.GetGenre(1);

        //    Assert.AreEqual("Shooter", s);
        //}
        //[Test]
        //public void GetImagePath()
        //{
        //    Random rnd = new Random();
        //    int random = rnd.Next(0, 9);
        //    string s = Games.GetImagePath(random);
        //    Assert.AreEqual($"/ExampleGameImages/Bild{random+1}.jpg", s); 

        //}
       
    }
}