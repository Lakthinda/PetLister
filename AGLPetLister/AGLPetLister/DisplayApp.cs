using AGLPetLister.Models;
using AGLPetLister.Services;
using System;
using System.Threading.Tasks;

namespace AGLPetLister
{
    public class DisplayApp
    {        
        private readonly PetListerService _petListerService;

        public DisplayApp(PetListerService petListerService)
        {
            _petListerService = petListerService;
        }
        public void Run()
        {           
            OnGet().GetAwaiter().GetResult();
        }

        public async Task OnGet()
        {
            var maleOwnerCatList = await _petListerService.GetPets(Sex.Male);
            var femaleOwnerCatList = await _petListerService.GetPets(Sex.Female);

            Console.WriteLine("~~~ Male ~~~");
            foreach (var pet in maleOwnerCatList)
            {
                Console.WriteLine(pet.Name);
            }

            Console.WriteLine("\n\n~~~ Female ~~~");
            foreach (var pet in femaleOwnerCatList)
            {
                Console.WriteLine(pet.Name);
            }

            Console.ReadLine();            
        }
    }
}
