using AGLPetLister.Models;
using AGLPetLister.Services;
using log4net.Core;
//using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AGLPetLister
{
    /// <summary>
    /// Client viewing component Displays results
    /// </summary>
    public class DisplayApp
    {        
        private readonly PetListerService _petListerService;
        //private readonly ILogger _logger;

        //public DisplayApp(PetListerService petListerService,ILogger logger)
        //{
        //    _petListerService = petListerService;
        //    _logger = logger;
        //}
        
        public DisplayApp(PetListerService petListerService)
        {
            _petListerService = petListerService;            
        }
        public void Run()
        {           
            OnGet().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Display List of pet names under each Gender type
        /// </summary>
        /// <returns></returns>
        public async Task OnGet()
        {
            var maleOwnerCatList = await _petListerService.GetPets(Sex.Male);
            var femaleOwnerCatList = await _petListerService.GetPets(Sex.Female);
                        
            if(maleOwnerCatList == null)
            {
                //TODO: Log
                //_logger.LogInformation("No Male Owner Cats to Display.");
                //_logger.Log()
                return;
            }
            if (maleOwnerCatList == null)
            {
                //TODO: Log
                //_logger.LogInformation("No Female Owner Cats to Display.");
                return;
            }

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
