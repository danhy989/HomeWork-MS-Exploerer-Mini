using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bt1_dotnet
{
	class FileDir
	{
		private String name;
		private String type;
		private DateTime dateModified;
		private String size;
		private String fullName;

		public FileDir()
		{

		}

		public FileDir(string name, string type, DateTime dateModified, String size,String fullName)
		{
			this.name = name;
			this.type = type;
			this.dateModified = dateModified;
			this.size = size;
			this.fullName = fullName;
		}

		public string Name { get => name; set => name = value; }
		public string Type { get => type; set => type = value; }
		public DateTime DateModified { get => dateModified; set => dateModified = value; }
		public String Size { get => size; set => size = value; }
		public string FullName { get => fullName; set => fullName = value; }
	}
}
