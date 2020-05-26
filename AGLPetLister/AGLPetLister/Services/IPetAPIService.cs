﻿using AGLPetLister.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGLPetLister.Services
{
    interface IPetAPIService
    {
        Task<List<Owner>> GetOwners();
    }
}
