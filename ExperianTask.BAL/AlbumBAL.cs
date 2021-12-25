using ExperianTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ExperianTask.BAL
{
    public interface IAlbumBAL
    {
        AlbumEntity GetAlbum(int userId, int albumId);
        IList<AlbumEntity> GetList();
        IList<AlbumEntity> GetAlbums();
        int GetAlbumCount(int userId);
        IList<AlbumEntity> GetAlbums(int userId);
    }
    public class AlbumBAL : IAlbumBAL
    {
        private IPhotoBAL _photoBAL;
        public AlbumBAL(IPhotoBAL photoBAL)
        {
            _photoBAL = photoBAL;
        }
        public IList<AlbumEntity> GetAlbums(int userId)
        {
            IList<AlbumEntity> albums = GetAlbums();
            return albums.Where(x => x.UserId == userId).ToList();
        }

        public AlbumEntity GetAlbum(int userId, int albumId)
        {
            IList<AlbumEntity> albums = GetAlbums();
            return albums.FirstOrDefault(x => x.UserId == userId && x.Id == albumId);
        }

        public int GetAlbumCount(int userId)
        {
            IList<AlbumEntity> albums = GetAlbums();
            return albums.Where(x => x.UserId == userId).Count();
        }

        public IList<AlbumEntity> GetAlbums()
        {
            IList<AlbumEntity> albumEntities = GetList();
            IList<PhotoEntity> photoEntities = _photoBAL.GetList();

            IEnumerable<AlbumEntity> albums = from album in albumEntities
                              join photo in photoEntities on album.Id equals photo.AlbumId into albumPhotos
                              select new AlbumEntity 
                              { 
                                  Id = album.Id, 
                                  Title = album.Title, 
                                  UserId = album.UserId,
                                  Photos = albumPhotos.ToList()
                              };
            return albums.ToList();
        }

        public IList<AlbumEntity> GetList()
        {
            IList<AlbumEntity> albums = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CommonUtil.BaseUrl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(CommonUtil.GetAlbumUrl).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                albums = response.Content.ReadAsAsync<IList<AlbumEntity>>().Result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);//throw exception
            }
            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
            return albums;


        }
    }
}
