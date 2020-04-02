using System;
using System.Collections.Generic;
using System.Reflection;

namespace SDK.Examples
{
    public class SDKSampleTest
    {
        public static void Main(string[] args) {
            new SDKSampleTest().test();
        }

        private Props properties = Props.GetInstance();

        public void runAllSamples(List<Exception> exceptionsThrown) {
        }

        private SDKSample instantiateSample( Type sampleType ) {
            ConstructorInfo constructorInfo = sampleType.GetConstructor(new[] { typeof(Props) } );
            return (SDKSample)constructorInfo.Invoke(new object[] { properties });
        }

        public void runAllSamples() {
            List<Type> sampleTypes = getSampleTypes();
            foreach (Type t in sampleTypes) {
            }
        }

        private List<Type> getSampleTypes() {
            List<Type> result = new List<Type>();
            Assembly a = Assembly.GetExecutingAssembly();
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

        public void test() {
            List<Exception> exceptionsThrown = new List<Exception>();
            try {
                SDKSampleTest runner = new SDKSampleTest();
                runner.runAllSamples(exceptionsThrown);
            } catch (Exception e) {
                System.Console.WriteLine("Unable to inspect packages and classes." + e);
            }
            System.Console.WriteLine(exceptionsThrown.Count == 0);
        }
    }
}

