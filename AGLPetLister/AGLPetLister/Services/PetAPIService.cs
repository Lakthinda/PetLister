using AGLPetLister.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public PetAPIService(IHttpClientFactory clientFactory,
                             IConfiguration config,
                             ILogger<PetAPIService> logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
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
                else
                {                    
                    _logger.LogError($"Response: {response.StatusCode}, Content: {response.Content}");
                    return null;
                }

                
            }
            catch (Exception e)
            {
                _logger.LogError($"Response: {e.Message}",e);
                return null;
            }
        }
    }
}
