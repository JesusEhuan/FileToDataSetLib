using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileToDataSetLib.Interfaces
{
	public interface IFileParser
	{
		bool CanParse(string extension);
		DataSet Parse(Stream fileStream, string fileName);
	}
}
