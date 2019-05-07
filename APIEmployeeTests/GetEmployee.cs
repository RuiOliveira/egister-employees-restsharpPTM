using System.Net;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

namespace APIEmployeeTests
{
    public class GetEmployee
    {
        private readonly RestClient client = new RestClient("http://localhost:5000/");

        [Fact]
        public void GetAllEmployees_Test()
        {
            RestRequest request = new RestRequest("employees", Method.GET);
            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetSpecificEmployee_Test()
        {
            RestRequest request = new RestRequest("employees?name=TesterFarfetchRamilo", Method.GET);
            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            JArray jsonVal = JArray.Parse(response.Content) as JArray;
            dynamic employees = jsonVal;

            string name = employees[0].name;
            name.Should().Be("TesterFarfetchRamilo");

            int nif = employees[0].nif;
            nif.Should().Be(999888778);

            string address = employees[0].address;
            address.Should().Be("Vila Nova de Gaia");
        }

        [Fact]
        public void GetSpecificEmployee_Test_NotFound()
        {
            RestRequest request = new RestRequest("employees?name=TesterFarfetchRamilo2", Method.GET);
            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
