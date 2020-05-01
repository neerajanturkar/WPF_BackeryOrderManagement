using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Bakery_Order_Processing
{
    public class FileHandler
    {
        public static void WriteXML<T> (T data, string file)
        {
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				FileStream stream = new FileStream(file, FileMode.Create);
				serializer.Serialize(stream, data);
				stream.Close();

			}
			catch (Exception x)
			{
				MessageBox.Show(x.ToString(), "Error");
				throw;
			}
        }

		public static T ReadXML<T>(string file)
		{
			try
			{
				using (StreamReader streamReader = new StreamReader(file))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(T));
					return (T)serializer.Deserialize(streamReader);
				}
			}
			catch 
			{

				return default(T);
			}

		}
    }
}
