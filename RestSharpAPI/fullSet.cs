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
        

        [Test]

        public void GetBook()
        {
            
            var booksApi = new RestClient($"{url}");
            var request = new RestRequest("status", DataFormat.Json);
            var response = booksApi.Get(request);
        }
/*        [Test]
        public void GetListOfBooks()
        {

        }
        [Test]
        public void GetSingleBook()
        {

        }
        [Test]
        public void SubmitAnOrder()
        {

        }
        [Test]
        public void GetAllOrder()
        {

        }
        [Test]
        public void UpdateAnOrder()
        {

        }
        [Test]
        public void DeleteAnOrder()
        {

        }*/
    }
}
