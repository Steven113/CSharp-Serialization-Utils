using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace SerializationTestProject
{
    [TestClass]
    public class SerializationUnitTests
    {
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

            
            foreach (string data in testStrings)
            {
                Debug.Assert(data.Equals(SerializationUtils.SerializationUtils.DeserializeFromByteArray<string>(SerializationUtils.SerializationUtils.SerializeToByteArray (data))));
                

            }
        }

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

            int testNum = 0;
            foreach (string data in testStrings)
            {
                ++testNum;

                string temp = data;

                //create a unique file name for the test, one that should not exist
                string fileName = $"SerializationTestFileName{testNum}{DateTime.Now.Ticks}";

                SerializationUtils.SerializationUtils.SerializeToFile<string>(fileName, ref temp);
                string deserializedTemp = "";
                Debug.Assert(SerializationUtils.SerializationUtils.DeserializeFromFile<string>(fileName, ref deserializedTemp));

                Debug.Assert(data.Equals(deserializedTemp));

                //delete this file
                File.Delete(fileName);


            }
        }
    }
}
