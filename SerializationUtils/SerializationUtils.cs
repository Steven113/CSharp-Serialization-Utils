using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SerializationUtils
{
    public class SerializationUtils
    {
        /// <summary>
        /// Deserialize the data in file fileName and put it in the given object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="objectToLoadDataInto"></param>
        /// <returns></returns>
        public static bool DeserializeFromFile<T>(string fileName, out T objectToLoadDataInto) where T : class
        {
            objectToLoadDataInto = default(T);
            if (File.Exists(fileName))
            {//first confirm that the file exists
                using (Stream dataStream = File.OpenRead(fileName))
                { //File.OpenRead opens the file with the read flag, rather than us having to set it. The using statement ensures that the created stream is cleaned up at the end of the block
                    BinaryFormatter formatter = new BinaryFormatter();
                    objectToLoadDataInto = ((formatter.Deserialize(dataStream) as T));
                    return true;
                }
                //return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Serialize the given object to the file named fileName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="objectToLoadDataInto"></param>
        public static void SerializeToFile<T>(string fileName, T objectToLoadDataInto) where T : class
        {
            using (Stream stream = File.OpenWrite(fileName /*+ "_"+ ((int)(System.DateTime.Now.ToOADate()*1000))+ ".qsf"*/)) //en sure that stream is cleaned up by creating it in a using statement - the stream will be cleaned up at the end of the using block
            {
                //Debug.Log("Serializing tree!");
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, objectToLoadDataInto);
            }
        }

        /// <summary>
        /// For given object, serialize it and return the byte array
        /// containing the serialized data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeToByteArray<T>(T obj)
        {
            if (obj == null)
            {
                return null; //prevent errors to do with trying to serialize null
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        
        /// <summary>
        /// For a given byte array, attemts to deserialize it to an object of the given
        /// type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeserializeFromByteArray<T>(byte[] data)
        {
            if (data == null)
            {
                return default(T); //prevent errors to do with deserialing a null byte array
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}
