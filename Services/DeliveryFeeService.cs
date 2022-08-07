using DependencyStore.Services.Contracts;
using RestSharp;

namespace DependencyStore.Services
{
    public class DeliveryFeeService : IDeliveryFeeService
    {
        public async Task<decimal> GetDeliveryFeeAsync(string zipCode)
        {
            var client = new RestClient("https://api.myorg.com");

            var request = new RestRequest()
                .AddJsonBody(new
                {
                    zipCode = zipCode
                });

            var response = await client.PostAsync<decimal>(request);

            return response < 0 ? 5 : response;
        }
    }
}
