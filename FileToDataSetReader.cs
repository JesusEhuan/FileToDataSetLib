using System.Collections.Generic;
using System;
using System.Data;
using System.IO;
using FileToDataSetLib.Interfaces;
using FileToDataSetLib.Parsers;
using System.Linq;

namespace FileToDataSetLib
{
	public class FileToDataSetReader
	{
		private readonly List<IFileParser> _parsers;

		public FileToDataSetReader()
		{
			_parsers = new List<IFileParser>
			{
				new ExcelParser(),
				new CsvParser(),
				new JsonParser(),
				new XmlParser()
			};
			_parsers.Add(new ZipParser(this)); // <- Aquí sí va
		}

		public DataSet ReadFile(Stream stream, string fileName)
		{
			if (string.IsNullOrWhiteSpace(fileName))
				throw new ArgumentException("El nombre del archivo no puede estar vacío.");

			var extension = Path.GetExtension(fileName)?.TrimStart('.').ToLowerInvariant();

			if (string.IsNullOrEmpty(extension))
				throw new NotSupportedException("No se pudo determinar la extensión del archivo.");

			var parser = _parsers.FirstOrDefault(p => p.CanParse(extension));

			if (parser == null)
				throw new NotSupportedException($"No hay soporte para archivos con extensión '.{extension}'.");

			return parser.Parse(stream, fileName);
		}
		public IFileParser? GetParserForExtension(string extension)
		{
			return _parsers.FirstOrDefault(p => p.CanParse(extension));
		}
	}
}
