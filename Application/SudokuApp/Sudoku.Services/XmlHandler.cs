using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Sudoku.Services
{
    public class XmlHandler
    {
        public T Read<T>(string fileName)
        {
            StreamReader stream = new StreamReader(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var result = (T)serializer.Deserialize(stream);
            return result;
        }

        public void Write<T>(T @object, string fileName)
        {
            using (FileStream stream = File.Create(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, @object);
            }
                
        }
    }
}
