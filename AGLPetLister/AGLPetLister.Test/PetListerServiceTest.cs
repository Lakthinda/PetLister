using AGLPetLister.Models;
using AGLPetLister.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGLPetLister.Test
{
    public class PetListerServiceTest
    {
        private PetListerService petListerService;
        [SetUp]
        public void Setup()
        {
            List<Owner> petOwners = new List<Owner>
            {
                new Owner()
                {
                    Name = "Owner A",
                    Age = 10,
                    Gender = Sex.Male,
                    Pets = new List<Pet>
                    {                        
                        new Pet()
                        {
                            Name = "Bitsy",
                            Type = PetTypes.Cat
                        },
                        new Pet()
                        {
                            Name = "Mitsy",
                            Type = PetTypes.Cat
                        }
                    }
                },
                new Owner()
                {
                    Name = "Owner A",
                    Age = 20,
                    Gender = Sex.Female,
                    Pets = new List<Pet>
                    {
                        new Pet()
                        {
                            Name = "Topsy",
                            Type = PetTypes.Cat
                        },
                        new Pet()
                        {
                            Name = "Dopsy",
                            Type = PetTypes.Cat
                        },
                        new Pet()
                        {
                            Name = "Betsy",
                            Type = PetTypes.Cat
                        }
                    }
                }
            };

            Mock<IPetAPIService> petAPIService = new Mock<IPetAPIService>();
            petAPIService.Setup(p => p.GetOwners())
                         .Returns(Task.FromResult(petOwners));
                        
            petListerService = new PetListerService(petAPIService.Object);
        }

        [TestCase(Sex.Male)]
        [TestCase(Sex.Female)]
        public void GetPets_IsNotNull_RetunsExpectedResults(Sex sex)
        {
            // Arrange
            // Act
            var result = petListerService.GetPets(sex);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestCase(Sex.Male,2)]
        [TestCase(Sex.Female,3)]
        public async Task GetPets_TotalResultCount_ReturnsExpectedResults(Sex sex,int count)
        {
            // Act
            var result = await petListerService.GetPets(sex);
            // Assert
            Assert.IsTrue(result.Count == count);
        }

        [TestCase(Sex.Male, "Bitsy")]
        [TestCase(Sex.Female, "Betsy")]
        public async Task GetPets_CorrectPetNameSort_ReturnsExpectedResults(Sex sex, string petName)
        {
            // Act
            var result = await petListerService.GetPets(sex);
            // Assert
            Assert.AreEqual(result.FirstOrDefault().Name,petName);
        }
    }
}