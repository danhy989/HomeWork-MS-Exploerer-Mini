using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace bt1_dotnet
{
	public partial class Form1 : Form
	{
		List<FileDir> fileDirs = new List<FileDir>();
		String comboBox1Value = "";
		String comboBox2Value = "";

		public Form1()
		{
			InitializeComponent();
			this.setDriversComboBox();
			this.setComboBoxListView();
		}

		private void addColumn(int pos,string text)
		{
			this.listView1.Columns.Add(new ColumnHeader());
			this.listView1.Columns[pos].Text = text;
			this.listView1.Columns[pos].Width = this.listView1.Width / 4;
		}

		private void setHeaderListView(string typeView)
		{
			this.listView1.Sorting = SortOrder.None;
			switch (typeView)
			{
				case "details":
					this.listView1.View = View.Details;
					this.addColumn(0, "Name");
					this.addColumn(1, "Size");
					this.addColumn(2, "Type");
					this.addColumn(3, "Date modified");
					break;
				case "LargeIcon":
					this.listView1.View = View.LargeIcon;
					break;
				case "List":
					this.listView1.View = View.List;
					break;
				case "SmallIcon":
					this.listView1.View = View.SmallIcon;
					break;
				case "Tile":
					this.listView1.View = View.Tile;
					break;
				default:
					this.setHeaderListView("details");
					break;
			}

			
		}

		private void setDataForListView(String path)
		{
			this.fileDirs.Clear();
			this.listView1.Items.Clear();

			DirectoryInfo d = new DirectoryInfo(path);

			DirectoryInfo[] directories = d.GetDirectories("*");

			foreach (DirectoryInfo directoryInfo in directories)
			{
				FileDir fileDir = new FileDir();
				fileDir.Name = directoryInfo.Name;
				fileDir.Type = "File folder";
				fileDir.DateModified = directoryInfo.LastWriteTime;
				fileDir.FullName = directoryInfo.FullName;
				this.fileDirs.Add(fileDir);

			}

			FileInfo[] Files = d.GetFiles("*");
			foreach (FileInfo file in Files)
			{
				FileDir fileDir = new FileDir();
				fileDir.Name = file.Name;
				fileDir.Type = file.Extension.Substring(1).ToUpper() + " File";
				fileDir.DateModified = file.LastWriteTime;
				fileDir.Size = string.Format("{0:#,0}", file.Length);
				fileDir.FullName = file.FullName;
				this.fileDirs.Add(fileDir);
			}
		}

		private void setDataWithTypeView(String path,String typeView)
		{
			this.setDataForListView(path);
			this.setHeaderListView(typeView);
			this.details();
		}

		private void details()
		{

			ImageList imageList = new ImageList();

			ListViewItem[] listViewItems = new ListViewItem[this.fileDirs.Count];

			for (int i = 0; i < this.fileDirs.Count; i++)
			{

				ListViewItem listViewItem = new ListViewItem(new string[] { this.fileDirs[i].Name, this.fileDirs[i].Size, this.fileDirs[i].Type, this.fileDirs[i].DateModified.ToString() });

				listViewItems[i] = listViewItem;
				Console.WriteLine(this.fileDirs[i].Type);

				if (this.fileDirs[i].FullName != "" && this.fileDirs[i].Size != null)
				{
					Icon ico = this.IconFromFilePath(this.fileDirs[i].FullName);
					imageList.Images.Add(this.fileDirs[i].FullName, ico);
				}

				if (this.fileDirs[i].FullName != "" && this.fileDirs[i].Size == null)
				{
					imageList.Images.Add(this.fileDirs[i].FullName, Image.FromFile(@"../../icon-folder.png"));
				}

				listViewItem.ImageKey = this.fileDirs[i].FullName;
			}

			this.listView1.LargeImageList = imageList;
			this.listView1.SmallImageList = imageList;

			this.listView1.Items.AddRange(listViewItems);


			this.listView1.LabelEdit = false;
		}

		private Icon IconFromFilePath(string filePath)
		{
			var result = (Icon)null;

			try
			{
				result = Icon.ExtractAssociatedIcon(filePath);
			}
			catch (System.Exception)
			{
				
			}

			return result;
		}


		private void setDriversComboBox()
		{
			DriveInfo[] allDrives = DriveInfo.GetDrives();

			foreach (DriveInfo d in allDrives)
			{
				if (d.IsReady == true)
				{
					this.comboBox1.Items.Add(d.Name);
					
				}
			}
			this.comboBox1.SelectedIndex = 0;
		}

		private void setComboBoxListView()
		{
			this.comboBox2.Items.Add("Details");
			this.comboBox2.Items.Add("LargeIcon");
			this.comboBox2.Items.Add("List");
			this.comboBox2.Items.Add("SmallIcon");
			this.comboBox2.Items.Add("Tile");
			this.comboBox2.SelectedIndex = 0;
		}

		private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.comboBox1Value = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
			this.setDataWithTypeView(comboBox1Value,comboBox2Value);
		}

		private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.comboBox2Value = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
			this.setDataWithTypeView(comboBox1Value,comboBox2Value);
		}
	}
}
