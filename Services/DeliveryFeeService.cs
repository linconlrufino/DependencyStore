using DependencyStore.Services.Contracts;
using RestSharp;

namespace DependencyStore.Services
{
    public class DeliveryFeeService : IDeliveryFeeService
    {
        private readonly Configuration _configuration;

        public DeliveryFeeService(Configuration configuration)
        {
            _configuration = configuration;
        }

        public async Task<decimal> GetDeliveryFeeAsync(string zipCode)
        {
            var client = new RestClient(_configuration.DeliveryFeeSerivceUrl);
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
