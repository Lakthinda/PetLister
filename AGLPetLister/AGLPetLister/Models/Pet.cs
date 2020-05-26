namespace AGLPetLister.Models
{
    public class Pet
    {
        public string Name { get; set; }
        public PetTypes Type { get; set; }
    }
    public enum PetTypes { Dog,Cat,Fish};
}
