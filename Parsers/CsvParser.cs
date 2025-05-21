using System;
using System.Data;
using System.IO;
using ExcelDataReader;
using FileToDataSetLib.Interfaces;

namespace FileToDataSetLib.Parsers
{
	public class CsvParser : IFileParser
	{
		public bool CanParse(string extension)
		{
			return extension.Equals("csv", StringComparison.OrdinalIgnoreCase);
		}

		public DataSet Parse(Stream fileStream, string fileName)
		{
			using var reader = ExcelReaderFactory.CreateCsvReader(fileStream);

			var config = new ExcelDataSetConfiguration
			{
				UseColumnDataType = true,
				ConfigureDataTable = _ => new ExcelDataTableConfiguration
				{
					UseHeaderRow = true
				}
			};

			var dataSet = reader.AsDataSet(config);
			dataSet.Tables[0].TableName = Path.GetFileNameWithoutExtension(fileName);

			return dataSet;
		}
	}
}
