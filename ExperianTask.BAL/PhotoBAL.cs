using ExperianTask.Entity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ExperianTask.BAL
{
    public interface IPhotoBAL
    {
        IList<PhotoEntity> GetList();
    }
    public class PhotoBAL: IPhotoBAL
    {
        public IList<PhotoEntity> GetList()
        {
            IList<PhotoEntity> photos = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CommonUtil.BaseUrl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(CommonUtil.GetPhotoUrl).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                photos = response.Content.ReadAsAsync<IList<PhotoEntity>>().Result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);//throw exception
            }
            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
            return photos;
        }
    }
}
