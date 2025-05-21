using System;
using System.Data;
using System.IO;
using ExcelDataReader;
using FileToDataSetLib.Interfaces;

namespace FileToDataSetLib.Parsers
{
	public class ExcelParser : IFileParser
	{
		public bool CanParse(string extension)
		{
			return extension.Equals("xls", StringComparison.OrdinalIgnoreCase) ||
				   extension.Equals("xlsx", StringComparison.OrdinalIgnoreCase);
		}

		public DataSet Parse(Stream fileStream, string fileName)
		{
			using var reader = ExcelReaderFactory.CreateReader(fileStream);

			var config = new ExcelDataSetConfiguration
			{
				UseColumnDataType = true,
				ConfigureDataTable = _ => new ExcelDataTableConfiguration
				{
					UseHeaderRow = true
				}
			};

			return reader.AsDataSet(config);
		}
	}
}
