using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture]
    public class SDKSampleTest
    {
        public static void Main(string[] args) {
			GroupManagementExample example = new GroupManagementExample(Props.GetInstance());
			example.Run();
//            new SDKSampleTest().test();
        }

        private Props properties = Props.GetInstance();

        private SDKSample instantiateSample( Type sampleType ) {
            ConstructorInfo constructorInfo = sampleType.GetConstructor(new[] { typeof(Props) } );
            return (SDKSample)constructorInfo.Invoke(new object[] { properties });
        }

        public void runAllSamples(List<Exception> exceptionsThrown) {
            List<Type> sampleTypes = getSampleTypes();
            foreach (Type t in sampleTypes) {
                try {
                    Console.Out.WriteLine("\nInstantiating " + t.Name);
                    SDKSample sample = instantiateSample(t);
                    Console.Out.WriteLine("Running " + t.Name);
                    sample.Run();
                    Console.Out.WriteLine("Exiting " + t.Name);
                } catch( Exception e ) {
                    Console.Error.WriteLine("Unable to instantiate " + t.Name);
                    exceptionsThrown.Add(e);
                    Console.Error.Write(e.StackTrace);
                }
            }
        }

        private List<Type> getSampleTypes() {
            List<Type> result = new List<Type>();
            Assembly a = Assembly.GetAssembly(typeof(SDKSample));
            {
                foreach (Type t in a.GetTypes())
                {
                    if (t.IsSubclassOf(typeof(SDKSample)))
                    {
                        result.Add(t);
                    }
                }
            }
            return result;
        }

        [Test]
        public void test() {
            List<Exception> exceptionsThrown = new List<Exception>();
            try {
                SDKSampleTest runner = new SDKSampleTest();
                runner.runAllSamples(exceptionsThrown);
            } catch (Exception e) {
                Assert.Fail("Unable to inspect packages and classes." + e);
            }
            Assert.AreEqual(exceptionsThrown.Count,0,"At least one test failed");
        }
    }
}

