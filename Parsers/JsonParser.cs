using System;
using System.Data;
using System.IO;
using System.Xml;
using FileToDataSetLib.Interfaces;
using Newtonsoft.Json;

namespace FileToDataSetLib.Parsers
{
	public class JsonParser : IFileParser
	{
		public bool CanParse(string extension)
		{
			return extension.Equals("json", StringComparison.OrdinalIgnoreCase);
		}

		public DataSet Parse(Stream fileStream, string fileName)
		{
			using var reader = new StreamReader(fileStream);
			var jsonContent = reader.ReadToEnd();

			var wrappedJson = $"{{ \"rootNode\": {jsonContent.Trim()} }}";
			var xmlDoc = JsonConvert.DeserializeXmlNode(wrappedJson);
			var dataSet = new DataSet();
			dataSet.ReadXml(new XmlNodeReader(xmlDoc), XmlReadMode.InferTypedSchema);

			return dataSet;
		}
	}
}
