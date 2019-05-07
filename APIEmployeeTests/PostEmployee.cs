using System.Net;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

namespace APIEmployeeTests
{
    public class PostEmployee
    {
        private readonly RestClient client = new RestClient("http://localhost:5000/");

        [Fact]
        public void PostNewEmployee_Test()
        {
            RestRequest request = new RestRequest("employees", Method.POST);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", request.AddJsonBody(new { name = "TesterFarfetchRamilo", nif = 999888778, address = "Vila Nova de Gaia" }), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            JObject jsonVal = JObject.Parse(response.Content) as JObject;
            dynamic employees = jsonVal;

            string name = employees.name;
            name.Should().Be("TesterFarfetchRamilo");

            int nif = employees.nif;
            nif.Should().Be(999888778);

            string address = employees.address;
            address.Should().Be("Vila Nova de Gaia");
        }

        [Fact]
        public void PostNewEmployeeMissingName_Test()
        {
            RestRequest request = new RestRequest("employees", Method.POST);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", request.AddJsonBody(new { nif = 999888778, address = "Vila Nova de Gaia" }), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            JValue jsonVal = JToken.Parse(response.Content) as JValue;
            dynamic responseText = jsonVal;

            string responseMsg = responseText;
            responseMsg.Should().Be("name is required.");
        }

        [Fact]
        public void PostNewEmployeeMissingNIF_Test()
        {
            RestRequest request = new RestRequest("employees", Method.POST);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", request.AddJsonBody(new { name = "TesterFarfetchRamilo", address = "Vila Nova de Gaia" }), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            JValue jsonVal = JToken.Parse(response.Content) as JValue;
            dynamic responseText = jsonVal;

            string responseMsg = responseText;
            responseMsg.Should().Be("nif is required.");
        }

        [Fact]
        public void PostNewEmployeeMissingNameAndNIF_Test()
        {
            RestRequest request = new RestRequest("employees", Method.POST);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", request.AddJsonBody(new { address = "Vila Nova de Gaia" }), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            JValue jsonVal = JToken.Parse(response.Content) as JValue;
            dynamic responseText = jsonVal;

            string responseMsg = responseText;
            responseMsg.Should().Be("name is required.");
        }
    }
}