using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ChileComprasProxy.Framework.Utilities.Xml
{
    public static class XmlUtility
    {
        public static T ConvertXmlStringToObject<T>(string xmlString)
        {
            T objectResult;
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xmlString))
            {
                objectResult = (T) serializer.Deserialize(reader);
            }
            return objectResult;
        }


        public static string ConvertObjectToXmlString<T>(this T value)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(writer, value);
                return stringWriter.ToString();
            }
        }
    }
}