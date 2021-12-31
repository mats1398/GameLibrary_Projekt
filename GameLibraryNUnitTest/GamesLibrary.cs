using GameLibrary_Projekt;
using NUnit.Framework;
using System;

namespace GameLibraryNUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //Arrange

        }

        [Test]
        public void GetGameList()
        {
            var list = Games.GameList;
            Assert.AreEqual("The Legend of Zelda: Ocarina of Time", list[0].Titel);
        }

        [Test]
        public void GetPersonalList()
        {
            var list = Games.PersonallyList;

            Assert.IsNotNull(list);
        }
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