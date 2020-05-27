using AGLPetLister.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGLPetLister.Services
{
    /// <summary>
    /// Service to provide business logic and return list of pets
    /// </summary>
    public class PetListerService
    {
        private readonly IPetAPIService _petAPIService;
        public PetListerService(IPetAPIService petAPIService)
        {
            _petAPIService = petAPIService;
        }

        /// <summary>
        /// Return list of Pets based on the Owner's Gender
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public async Task<List<Pet>> GetPets(Sex sex)
        {
            var petOwners = await _petAPIService.GetOwners();

            if(petOwners != null)
            {
                List<Pet> petList = new List<Pet>();
                foreach (var petOwner in petOwners.Where(o => o.Gender == sex))
                {
                    if (petOwner.Pets != null)
                    {
                        // Find Cats
                        var cats = petOwner.Pets.Where(p => p.Type == PetTypes.Cat);
                        petList.AddRange(cats);
                    }
                }

                // Order PetList by name
                return petList.OrderBy(p => p.Name).ToList();
            }
            else
            {
                // TODO: Log errors
                return null;
            }
        }
    }
}
