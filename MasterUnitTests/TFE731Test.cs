//Jean-Philip Dauray - 2019-03-21
//Importé de 18942
using System.Collections.Generic;
using System.Reflection;
using KineticaRT.Servers.OpcClient;
using NUnit.Framework;
using PrjMaster_HMI.StudioDemonstration;

namespace MasterUnitTests
{
    [TestFixture]
    public class TFE731Test
    {

        #region Helpers

        #endregion

        [Test]
        public void GetAllFromCategory()
        {
            //Setup
            var smallEngines = Engines.GetAllEnginesFromCategory(Category.Small);
            var bigEngines = Engines.GetAllEnginesFromCategory(Category.Big);
            var pwEngines = Engines.GetAllEnginesFromCategory(Category.PW);
            var notEngine = Engines.GetAllEnginesFromCategory(Category.None);
            var allEngines = TestHelper.GetAllEnumValues(typeof(Engine));

            //Execute
            var count = smallEngines.Count + bigEngines.Count + pwEngines.Count + notEngine.Count;

            //Verify
            Assert.AreEqual(allEngines.Count, count);
        }

        [Test]
        public void GetEngineCategory()
        {
            //Setup
            var allEngines = TestHelper.GetAllEnumValues(typeof(Engine));

            foreach (var engine in allEngines)
            {
                //Execute & Verify
                Assert.DoesNotThrow(() => Engines.GetEngineCategory((Engine)engine));
            }
        }

        [Test]
        public void GetEngineGroup()
        {
            //Setup
            var allEngines = TestHelper.GetAllEnumValues(typeof(Engine));

            foreach (var engine in allEngines)
            {
                //Execute & Verify
                Assert.DoesNotThrow(() => Engines.GetEngineGroup((Engine)engine));
            }
        }

        [Test]
        public void GetEngineDisplayName()
        {
            //Setup
            var allEngines = TestHelper.GetAllEnumValues(typeof(Engine));

            foreach (var engine in allEngines)
            {
                //Execute & Verify
                Assert.DoesNotThrow(() => Engines.GetEngineDisplayName((Engine)engine));
            }
        }

        [Test]
        public void GetEngineFromDisplayName()
        {
            //Setup
            var allEngines = TestHelper.GetAllEnumValues(typeof(Engine));
            var allEnginesDictionary = new Dictionary<Engine, string>(allEngines.Count);
            foreach (var engine in allEngines)
            {
                var name = Engines.GetEngineDisplayName((Engine)engine);
                allEnginesDictionary.Add((Engine)engine, name);
            }

            foreach (var entry in allEnginesDictionary)
            {
                //Execute
                var engineType = Engines.GetEngineFromDisplayName(entry.Value);

                //Verify
                Assert.AreEqual(entry.Key, engineType);
            }
        }

        [TestCase("T1", 10)]
        [TestCase("Tt1dot2", 10)]
        public void ArraySize_Simple(string name, int expectedLength)
        {
            //Setup
            var engineSequence = new TFE731_Sequence();
            var assembly = engineSequence.GetType();

            //Execute
            var arrayProperty = assembly.GetField("OPC_" + name + "_DELETE", BindingFlags.NonPublic | BindingFlags.Instance);
            var array = arrayProperty.GetValue(engineSequence) as BooleanItem[];

            //Verify
            Assert.AreEqual(expectedLength, array.Length);
        }

        [TestCase("T8", 5, 5)]
        [TestCase("Tt60", 6, 5)]
        [TestCase("T11", 4, 6)]
        [TestCase("T155", 4, 6)]
        public void ArraySize_Multi(string name, int expectedRakesCount, int expectedProbesCount)
        {
            //Setup
            var engineSequence = new TFE731_Sequence();
            var assembly = engineSequence.GetType();

            //Execute
            var comparedArrayProperty = assembly.GetField("OPC_" + name + "_DELETE", BindingFlags.NonPublic | BindingFlags.Instance);
            var otherArray = comparedArrayProperty.GetValue(engineSequence) as BooleanItem[,];

            //Verify
            Assert.AreEqual(expectedRakesCount, otherArray.GetUpperBound(0) + 1);
            Assert.AreEqual(expectedProbesCount, otherArray.GetUpperBound(1) + 1);
        }
    }
}
