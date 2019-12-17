using XMLTestApp.Contracts;
using XMLTestApp.Models.DTOs;
using XMLTestApp.Models.Logic.XMLContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTestApp.Models.Logic.XMLCreators
{
    public class ConfigurationCreator : IXMLCreator<IXMLContainer>
    {
        private Random RandomGenerator = new Random();

        private const string Letters = "abcdefghijklmnopqrstuvwxyz";
        private int NewRandomInt
        {
            get
            {
                return RandomGenerator.Next(1, 10);
            }
        }
        private char NewRandomChar
        {
            get
            {
                return Letters.ElementAt(RandomGenerator.Next(0,Letters.Length));
            }
        }
        private string NewRandomChecksum
        {
            get
            {
                return $"{NewRandomInt}{NewRandomChar}";
            }
        }

        public Tuple<string, IXMLContainer> CreateRandomXML()
        {
            return new Tuple<string, IXMLContainer>(CreateRandomFilename(),
                                                    new ConfigurationContainer()
                                                    {
                                                        ProductsType1 = CreateRandomProducts(),
                                                        ProductsType2 = CreateRandomProducts()
                                                    });
        }

        private string CreateRandomFilename()
        {
            return $"{RandomGenerator.Next()}_{DateTime.Now.Ticks}.xml";
        }

        private List<Product> CreateRandomProducts()
        {
            return GenerateNRandom(NewRandomInt, CreateRandomProduct);
        }

        private List<T> GenerateNRandom<T>(int n, Func<T> randomObjectGenerator)
        {
            List<T> randomObjects = new List<T>();
            for (int i = 0; i < n; i++)
            {
                randomObjects.Add(randomObjectGenerator());
            }
            return randomObjects;
        }
        
        private Product CreateRandomProduct()
        {
            return new Product()
            {
                Checksum = NewRandomChecksum,
                Id = NewRandomInt * 100,
                Name = $"Product{NewRandomInt}",
                Var1 = NewRandomInt,
                Var2 = NewRandomInt,
                Var3 = $"Family{NewRandomInt}",
                SubProducts = CreateRandomSubproducts()
            };
        }
        private List<Subproduct> CreateRandomSubproducts()
        {
            return GenerateNRandom(NewRandomInt, CreateRandomSubproduct);
        }
        private Subproduct CreateRandomSubproduct()
        {
            return new Subproduct()
            {
                Area = NewRandomInt * NewRandomInt,
                Lenght = NewRandomInt * NewRandomInt,
                NearPoint = new NearPoint()
                {
                    X = NewRandomInt,
                    Y = NewRandomInt
                }
            };
        }
    }
}
