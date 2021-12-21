using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;

namespace RestSharpAPI
{
    [TestFixture]
    public class fullSet
    {

        public string url = "https://simple-books-api.glitch.me/";
        static Random random = new Random();
        public int randomNumber = random.Next(1, 6);
        public int postRandomNumber = random.Next(20, 99);


        [Test]

        public void GetBook()
        {
            
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("status");
            var response = booksApi.Get(request);


        }
        [Test]
        public void GetListOfBooks()
        {
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("books");
            var response = booksApi.Get(request);

            /// <summary>
            /// Additional parameters
            /// Type: fiction or non-fiction
            /// Limit: a number between 1 and 20
            /// </summary>

        }
        [Test]
        public void GetSingleBook()
        {
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("books/{:bookId}")
                .AddUrlSegment(":bookId", randomNumber);
            var response = booksApi.Get(request);
        }
        [Test]
        public void SubmitAnOrder()
        {
            var body = new bodyClass { bookId = postRandomNumber, customerName = "Jakub" };

            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("orders", DataFormat.Json);
            request.AddJsonBody(body);
            var response = booksApi.Post(request);

        }
        [Test]
        public void GetAllOrder()
        {
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("status", DataFormat.Json);
            var response = booksApi.Get(request);
        }
        [Test]
        public void UpdateAnOrder()
        {
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("status", DataFormat.Json);
            var response = booksApi.Get(request);
        }
        [Test]
        public void DeleteAnOrder()
        {
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("status", DataFormat.Json);
            var response = booksApi.Get(request);
        }
    }
}
