using AGLPetLister.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGLPetLister.Services
{
    public class PetListerService
    {
        private readonly IPetAPIService _petAPIService;
        public PetListerService(IPetAPIService petAPIService)
        {
            _petAPIService = petAPIService;
        }

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
