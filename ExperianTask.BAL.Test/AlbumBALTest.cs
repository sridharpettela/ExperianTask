using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExperianTask.BAL.Test
{
    [TestClass]
    public class AlbumBALTest
    {
        private readonly AlbumBAL _albumBAL;
        private readonly PhotoBAL _photoBAL;

        public AlbumBALTest()
        {
            _photoBAL = new PhotoBAL();
            _albumBAL = new AlbumBAL(_photoBAL);
        }


        [TestMethod]
        [DataRow(1,10)]
        [DataRow(2,10)]
        public void GetUserAlbumCount_Test_Return_Album_Count(int userId,int exceptedCount)
        {
            // Act
            var result = _albumBAL.GetAlbums(userId);
            // Assert
            Assert.AreEqual(exceptedCount, result.Count, $"User {userId} has {result.Count} albums");
        }

        [TestMethod]
        [DataRow(1,1, 50)]
        [DataRow(1, 2, 50)]
        public void GetUserAlbumPhotoCount_Test_Return_Album_Photo_Count(int userId, int albumId, int exceptedCount)
        {
            // Act
            var result = _albumBAL.GetAlbum(userId,albumId);
            // Assert
            Assert.AreEqual(exceptedCount, result.Photos.Count, $"User {userId} owns album {result.Id} and that album contains {result.Photos.Count} photos");
        }
    }
}
