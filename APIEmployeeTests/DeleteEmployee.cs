using System.Net;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

namespace APIEmployeeTests
{
    public class DeleteEmployee
    {
        private readonly RestClient client = new RestClient("http://localhost:5000/");

        [Fact]
        public void DeleteEmployee_Test()
        {
            RestRequest request = new RestRequest("employees/5cca2e2e35e9fb1c4a1ab844", Method.DELETE);

            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
