using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;

namespace RestSharpAPI
{
    [TestFixture]
    public class fullSet
    {

        public string url = "https://simple-books-api.glitch.me/";
        static Random random = new Random();
        public int randomNumber = random.Next(1, 6);
        public int postRandomNumber = random.Next(20, 99);
        string username;
        string email;
        string token = "0d1441e87bc4cd9b9bb3f062336cc866c639d9e67ba83918ecf54c0ddba5a7d0";
        string bookId = "gm6l9SwOkvWsdjNf6ryzK";
        string deleteBookId = "d50YwwNZEVbMwOo64D8_U";

        [Test]
        public void Registration()
        {
            username = "RandomLogin";
            email = "RandomLogin@example.com";
            var body = new registerClass { clientName = username, clientEmail = email };


            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("/api-clients", DataFormat.Json);
            request.AddJsonBody(body);
            var response = booksApi.Post(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            try
            {
                var accessToken = output["accessToken"];

                Console.WriteLine(accessToken);
                Assert.That(accessToken, Is.Not.Null.And.Not.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Your username or email was used to generate a token. Make sure they are unique");
                Console.WriteLine(ex);
            }
        }
        [Test]

        public void GetBook()
        {
            
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("status");
            var response = booksApi.Get(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var result = output["status"];

            Assert.That(result, Contains.Substring("OK"));

        }
        [Test]
        public void GetListOfBooks()
        {
            int id;
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("books");
            var response = booksApi.Get(request);

            /// <summary>
            /// Additional parameters
            /// Type: fiction or non-fiction
            /// Limit: a number between 1 and 20
            /// </summary>
            
            //var requestWithType = new RestRequest("books?type=non-fiction");
            //var responseWithType = booksApi.Get(requestWithType);

            //var requestWithLimit = new RestRequest($"books?Limit={randomNumber}");
            //var responseWithLimit = booksApi.Get(requestWithLimit);

            //var requestWithAllData = new RestRequest($"books?type=non-fiction&Limit={randomNumber}");
            //var responseWithAllData = booksApi.Get(requestWithLimit);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<List<listOfBooks>>(response);

            Assert.That(output.Count, Is.Not.Null);
        }
        [Test]
        public void GetSingleBook()
        {
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("books/{:bookId}")
                .AddUrlSegment(":bookId", randomNumber);
            var response = booksApi.Get(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);

            Assert.Multiple(() =>
            {
                Assert.That(output.ContainsKey("type"));
                Assert.That(output.ContainsKey("id"));
                Assert.That(output.ContainsKey("author"));
                Assert.That(output.ContainsKey("available"));
            });
        }
        [Test]
        public void PostAnOrder()
        {
            var body = new bodyClass { id = postRandomNumber, customerName = "Jakub" };

            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("orders", DataFormat.Json);
            request.AddHeader("Authorization", token);
            request.AddJsonBody(body);
            var response = booksApi.Post(request);

        }
        [Test]
        public void GetAllOrder()
        {
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("orders", DataFormat.Json);
            var response = booksApi.Get(request);
        }
        [Test]
        public void GetOneOrder()
        {
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("books/{:bookId}")
                .AddUrlSegment(":bookId", randomNumber);
            request.AddHeader("Authorization", token);
            var response = booksApi.Get(request);
        }

        [Test]
        public void PatchAnOrder()
        {
            username = "JohnCustomer";
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("orders/{:orderId}")
                .AddUrlSegment(":orderId", bookId);
            request.AddHeader("Authorization", token);
            request.AddBody("customerName", username);
            var response = booksApi.Patch(request);
        }
        [Test]
        public void DeleteAnOrder()
        {
            username = "John Customer";
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("orders/{:orderId}")
                .AddUrlSegment(":orderId", deleteBookId);
            request.AddHeader("Authorization", token);
            request.AddBody("customerName", username);
            var response = booksApi.Delete(request);
        }
    }
}
