using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using RestSharpAPI.Model;
using System.Collections.Generic;

namespace RestSharpAPI
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void GetMethod()
        {
            var client = new RestClient("http://localhost:3000/");

            var request = new RestRequest("posts/{postid}", Method.GET);
            request.AddUrlSegment("postid", 1);

            var response = client.Execute(request);

            /*var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var result = output["author"];*/

            JObject obs = JObject.Parse(response.Content);

            Assert.That(obs["author"].ToString(), Is.EqualTo("typicode"), "Author is not correct");
        }

        [Test]
        public void PostMethod()
        {
            var client = new RestClient("http://localhost:3000/");

            var request = new RestRequest("posts/{postid}/profile", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { name = "Raj" });

            request.AddUrlSegment("postid", 1);

            var response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var result = output["name"];

            Assert.That(result, Is.EqualTo("Raj"), "Author is not correct");
        }

        [Test]
        public void PostWithTypeClass()
        {
            var client = new RestClient("http://localhost:3000/");

            var request = new RestRequest("posts", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Posts() { id = "22", author = "Jakub Struzik", title = "RestSharp" });

            var response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var result = output["author"];

            Assert.That(result, Is.EqualTo("Jakub Struzik"), "Author is not correct");
        }
        
    }
}
