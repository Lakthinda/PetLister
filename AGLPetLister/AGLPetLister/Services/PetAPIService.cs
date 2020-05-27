using AGLPetLister.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGLPetLister.Services
{
    /// <summary>
    /// Use HTTPClient to communicate with API
    /// </summary>
    public class PetAPIService : IPetAPIService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        public PetAPIService(IHttpClientFactory clientFactory,IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        /// <summary>
        /// Returns List of Owners from Web API
        /// </summary>
        /// <returns></returns>
        public async Task<List<Owner>> GetOwners()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                                                     _config.GetValue<string>("APIUrl"));
                request.Headers.Add("Accept", "application/json");
                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var petOwners = JsonConvert.DeserializeObject<List<Owner>>(result);
                    return petOwners;
                }

                // TODO: Log Errors                
                return null;
            }
            catch (Exception e)
            {
                // TODO: Log Exception
                return null;
            }
        }
    }
}
