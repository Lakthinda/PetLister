using System.Collections.Generic;

namespace AGLPetLister.Models
{
    public class Owner
    {
        public string Name { get; set; }
        public Sex Gender { get; set; }
        public int Age { get; set; }
        public List<Pet> Pets { get; set; }
    }

    public enum Sex { Male, Female};
}
