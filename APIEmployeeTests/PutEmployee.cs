using System.Net;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

namespace APIEmployeeTests
{
    public class PutEmployee
    {
        private readonly RestClient client = new RestClient("http://localhost:5000/");

        [Fact]
        public void UpdateEmployee_Test()
        {
            RestRequest request = new RestRequest("employees/5cca2e2e35e9fb1c4a1ab844", Method.PUT);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", request.AddJsonBody(new { name = "TesterFarfetchRamilo", nif = 999888778, address = "Porto" }), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
