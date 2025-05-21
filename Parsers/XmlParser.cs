using System;
using System.Data;
using System.IO;
using FileToDataSetLib.Interfaces;

namespace FileToDataSetLib.Parsers
{
	public class XmlParser : IFileParser
	{
		public bool CanParse(string extension)
		{
			return extension.Equals("xml", StringComparison.OrdinalIgnoreCase);
		}

		public DataSet Parse(Stream fileStream, string fileName)
		{
			var dataSet = new DataSet();
			dataSet.ReadXml(fileStream, XmlReadMode.InferTypedSchema);
			return dataSet;
		}
	}
}
