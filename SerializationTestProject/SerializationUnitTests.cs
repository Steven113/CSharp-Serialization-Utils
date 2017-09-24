using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace SerializationTestProject
{
    [TestClass]
    public class SerializationUnitTests
    {
        
        /// <summary>
        /// Test serialization to and from byte arrays
        /// </summary>
        [TestMethod]
        public void TestObjectSerialization()
        {
            //we use strings due to the dynamic size
            string[] testStrings = new string[]
            {
                "This string seems to serialize fine",
                "This string serializes fine",
                $"Apparently this won't work with Siverlight:{Environment.NewLine}So apparently Silverlight does not have the BinaryFormatter. However an open source project exists that may be able to provide similar functionality for you. It is called sharpSerializer. It will work with Silverlight and WP7.'"
            };

            TestObjectSerialization(testStrings);
        }

        public void TestObjectSerialization<T>(T [] testObjects)
        {


            foreach (T data in testObjects)
            {
                Debug.Assert(data.Equals(SerializationUtils.SerializationUtils.DeserializeFromByteArray<T>(SerializationUtils.SerializationUtils.SerializeToByteArray(data))));


            }
        }

        /// <summary>
        /// Test object deserialization to and from files
        /// </summary>
        [TestMethod]
        public void TestObjectSerializationToAndFromFile()
        {
            //we use strings due to the dynamic size
            string[] testStrings = new string[]
            {
                "This string seems to serialize fine",
                "This string serializes fine",
                $"Apparently this won't work with Siverlight:{Environment.NewLine}So apparently Silverlight does not have the BinaryFormatter. However an open source project exists that may be able to provide similar functionality for you. It is called sharpSerializer. It will work with Silverlight and WP7.'"
            };

            TestObjectSerializationToAndFromFile<string>(testStrings);
        }

        /// <summary>
        /// Generic serialization and deserialization test for a given class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="testObjects"></param>
        public void TestObjectSerializationToAndFromFile<T>(T [] testObjects) where T : class
        {
            
            int testNum = 0;
            foreach (T data in testObjects)
            {
                ++testNum;

                T temp = data;

                //create a unique file name for the test, one that should not exist
                string fileName = $"SerializationTestFileName{testNum}{DateTime.Now.Ticks}";

                SerializationUtils.SerializationUtils.SerializeToFile<T>(fileName, temp);
                T deserializedTemp = default(T);
                Debug.Assert(SerializationUtils.SerializationUtils.DeserializeFromFile<T>(fileName, out deserializedTemp));

                Debug.Assert(data.Equals(deserializedTemp));

                //delete this file
                File.Delete(fileName);


            }
        }
    }
}
