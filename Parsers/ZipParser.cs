using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using FileToDataSetLib.Interfaces;

namespace FileToDataSetLib.Parsers
{
	public class ZipParser : IFileParser
	{
		private readonly FileToDataSetReader _reader;

		public ZipParser(FileToDataSetReader reader)
		{
			_reader = reader;
		}

		public bool CanParse(string extension)
		{
			return extension.Equals("zip", StringComparison.OrdinalIgnoreCase);
		}

		public DataSet Parse(Stream fileStream, string fileName)
		{
			var result = new DataSet();

			using var archive = new ZipArchive(fileStream, ZipArchiveMode.Read);
			foreach (var entry in archive.Entries)
			{
				if (string.IsNullOrWhiteSpace(entry.Name))
					continue;

				var ext = Path.GetExtension(entry.Name)?.TrimStart('.').ToLowerInvariant();
				if (string.IsNullOrEmpty(ext))
					continue;

				var parser = _reader.GetParserForExtension(ext);
				if (parser == null)
					continue; // O puedes lanzar excepción si prefieres

				using var entryStream = entry.Open();
				var dataSetTemp = parser.Parse(entryStream, entry.Name);

				foreach (DataTable dt in dataSetTemp.Tables)
				{
					var copy = dt.Copy();
					if (string.IsNullOrWhiteSpace(copy.TableName))
						copy.TableName = entry.Name;

					result.Tables.Add(copy);
				}
			}

			return result;
		}
	}
}
