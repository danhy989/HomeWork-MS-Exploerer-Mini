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
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace bt3_dotnet
{
	[Serializable]
	public partial class Form1 : Form
	{

		String PREFIX_TEST = @"D:\";

		IsolatedStorageFile isf = null;

		List<FileDir> fileDirs = new List<FileDir>();

		List<String> listExceptionStr = new List<string>();

		String currentDirPath;

		String currentTypeView;

		Boolean isButtonClick = false;

		public Form1()
		{
			InitializeComponent();

			isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.User |
		IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain,
		typeof(System.Security.Policy.Url), typeof(System.Security.Policy.Url));

			//DriveInfo[] allDrives = DriveInfo.GetDrives();
			//foreach (DriveInfo d in allDrives)
			//{
			//	if (d.IsReady == true)
			//	{
			//		this.ListDirectory(this.treeView1, d.Name);
			//	}
			//}

			this.ListDirectory(this.treeView1, PREFIX_TEST+"UIT");

			this.setDataWithTypeView(PREFIX_TEST+"UIT", "Details");

			this.setContextMenuRightButtonListView();

			this.listView1.ContextMenuStrip = contextMenuStrip1;

			


		}

		private ToolStripMenuItem createToolStripMenuItem(String value)
		{
			ToolStripMenuItem s = new ToolStripMenuItem();
			s.Name = value;
			s.Text = value;
			s.Click += contextMenuStrip_ItemClick;
			return s;
		}

		private void setContextMenuRightButtonListView()
		{
			ToolStripMenuItem tool1 =  this.createToolStripMenuItem("Details");
			ToolStripMenuItem tool2 = this.createToolStripMenuItem("LargeIcon");
			ToolStripMenuItem tool3 = this.createToolStripMenuItem("List");
			ToolStripMenuItem tool4 = this.createToolStripMenuItem("SmallIcon");
			ToolStripMenuItem tool5 = this.createToolStripMenuItem("Tile");

			ToolStripMenuItem toolCreateFolder = new ToolStripMenuItem();
			toolCreateFolder.Name = "NewFolder";
			toolCreateFolder.Text = "New Folder";
			toolCreateFolder.Click += contextMenuStrip_CreateFolder;

			ToolStripMenuItem toolDeleteFolder = new ToolStripMenuItem();
			toolDeleteFolder.Name = "DeleteFolder";
			toolDeleteFolder.Text = "Delete Folder";
			toolDeleteFolder.Click += contextMenuStrip_DeleteFolder;

			ToolStripMenuItem toolCreateFile = new ToolStripMenuItem();
			toolDeleteFolder.Name = "NewFile";
			toolDeleteFolder.Text = "New File";
			toolDeleteFolder.Click += contextMenuStrip_NewFile;

			ToolStripMenuItem[] tools = new ToolStripMenuItem[] { tool1,tool2,tool3,tool4,tool5 ,toolCreateFolder,toolDeleteFolder,toolCreateFile};
			contextMenuStrip1.Items.AddRange(tools);
		}

		private void contextMenuStrip_NewFile(object sender, EventArgs e)
		{
			string path = this.currentDirPath + "\\" + "New Text Document.txt";
			string typeView = this.currentTypeView;
			try
			{

				if (File.Exists(path))
				{
					File.Delete(path);
				}

				using (FileStream fs = File.Create(path))
				{
					Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
					fs.Write(info, 0, info.Length);
				}

				using (StreamReader sr = File.OpenText(path))
				{
					string s = "";
					while ((s = sr.ReadLine()) != null)
					{
						Console.WriteLine(s);
					}
				}
				this.setDataWithTypeView(this.currentDirPath, typeView);
				this.ListDirectory(this.treeView1, PREFIX_TEST + "UIT");
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private void contextMenuStrip_DeleteFolder(object sender, EventArgs e)
		{

		}

		private void contextMenuStrip_CreateFolder(object sender,EventArgs e)
		{

			string path = this.currentDirPath + "\\" + "New Folder";
			string typeView = this.currentTypeView;

			try
			{
				// Determine whether the directory exists.
				if (Directory.Exists(path))
				{
					Console.WriteLine("That path exists already.");
					return;
				}

				// Try to create the directory.
				DirectoryInfo di = Directory.CreateDirectory(path);
				Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
				this.setDataWithTypeView(this.currentDirPath, typeView);
				this.ListDirectory(this.treeView1, PREFIX_TEST + "UIT");
			}
			catch (Exception ex)
			{
				Console.WriteLine("The process failed: {0}", ex.ToString());
			}
			finally { }
		}

		private void contextMenuStrip_ItemClick(object sender, EventArgs e)
		{
			ToolStripItem item = (ToolStripItem)sender;
			this.setHeaderListView(item.Name);
		}


		private void saveTreeNodeRootIntoMemory(TreeNode treeNode)
		{
			IsolatedStorageFileStream isfs;
			byte[] data;
			isfs = new IsolatedStorageFileStream("dada123456", FileMode.Create, isf);
			BinaryFormatter bf = new BinaryFormatter();
			using (var ms = new MemoryStream())
			{
				bf.Serialize(ms, treeNode);
				data = ms.ToArray();
			}
			isfs.Write(data, 0, data.Length);
			isfs.Close();

		}


		private TreeNode getTreeNodeRootFromMemory()
		{
			BinaryFormatter bf = new BinaryFormatter();
			IsolatedStorageFileStream isf1 = new IsolatedStorageFileStream
			("dada123456", FileMode.Open, this.isf);

			TreeNode treeNode = (TreeNode)bf.Deserialize(isf1);
			isf1.Close();
			return treeNode;
		}

		private bool isCheckStoraged()
		{
			return this.isf.FileExists("dada123456");
		}


		private void addColumn(int pos, string text)
		{
			this.listView1.Columns.Add(new ColumnHeader());
			this.listView1.Columns[pos].Text = text;
			this.listView1.Columns[pos].Width = this.listView1.Width / 4;
		}

		private void setHeaderListView(string typeView)
		{
			this.currentTypeView = typeView;
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


		//private void setEntryDataForListBox()
		//{
		//	this.listBox1.Items.Add("3D Objects");
		//	this.listBox1.Items.Add("Desktop");
		//	this.listBox1.Items.Add("Documents");
		//	this.listBox1.Items.Add("Downloads");
		//	this.listBox1.Items.Add("Music");
		//	this.listBox1.Items.Add("Pictures");
		//	this.listBox1.Items.Add("Videos");
		//	DriveInfo[] allDrives = DriveInfo.GetDrives();
		//	foreach (DriveInfo d in allDrives)
		//	{
		//		if (d.IsReady == true)
		//		{
		//			this.listBox1.Items.Add(d.VolumeLabel+" ("+d.Name.Substring(0,2)+")");

		//		}
		//	}
		//}


		private void setDataForListView(String path)
		{
			this.fileDirs.Clear();
			this.listView1.Items.Clear();

			DirectoryInfo d = new DirectoryInfo(path);
			try
			{
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
					if (file.Extension == "")
					{
						fileDir.Type = "File";
					}
					else
					{
						fileDir.Type = file.Extension.Substring(1).ToUpper() + " File";
					}
					fileDir.DateModified = file.LastWriteTime;
					fileDir.Size = string.Format("{0:#,0}", file.Length);
					fileDir.FullName = file.FullName;
					this.fileDirs.Add(fileDir);
				}
			}
			catch (IOException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private void setDataWithTypeView(String path, String typeView)
		{
			this.setDataForListView(path);
			this.setHeaderListView(typeView);
			this.details();
			this.currentDirPath = path;
		}

		private void details()
		{

			ImageList imageList = new ImageList();

			ListViewItem[] listViewItems = new ListViewItem[this.fileDirs.Count];

			for (int i = 0; i < this.fileDirs.Count; i++)
			{

				ListViewItem listViewItem = new ListViewItem(new string[] { this.fileDirs[i].Name, this.fileDirs[i].Size, this.fileDirs[i].Type, this.fileDirs[i].DateModified.ToString() });

				listViewItems[i] = listViewItem;

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

		private async void ListDirectory(TreeView treeView, string path)
		{
			await Task.Delay(1000);


			treeView.Nodes.Clear();

			var rootDirectoryInfo = new DirectoryInfo(path);

			TreeNode treeNodeRoot = new TreeNode();

			//if (this.isCheckStoraged())
			//{
			//	treeNodeRoot = this.getTreeNodeRootFromMemory();
			//}
			//else
			//{
			//Console.WriteLine("Before get data from memory");
			//	treeNodeRoot = this.CreateDirectoryNode(rootDirectoryInfo);
			//	this.saveTreeNodeRootIntoMemory(treeNodeRoot);
			//}

			treeNodeRoot = this.CreateDirectoryNode(rootDirectoryInfo);


			treeView.Nodes.Add(treeNodeRoot);

			this.listExceptionStr.ForEach(f =>

				Console.WriteLine(f)
			);


		}


		private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
		{
			if (directoryInfo != null)
			{
				var directoryNode = new TreeNode(directoryInfo.Name);
				try
				{
				
					DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories()
						.Where(f => !this.listExceptionStr.Contains(f.Name) && !f.Attributes.HasFlag(FileAttributes.Hidden)).ToArray();

					foreach (var directory in directoryInfos)
					{
						directoryNode.Nodes.Add(CreateDirectoryNode(directory));
					}
				}
				catch (UnauthorizedAccessException e)
				{
					Regex regex = new Regex("'(.*?)'");
					Match match = regex.Match(e.Message);
					int lenghtPath = directoryInfo.FullName.Length;

					directoryNode.Nodes.Add(new TreeNode("[Access Denied] " + match.Groups[1].Value.Substring(lenghtPath)));
					this.listExceptionStr.Add(directoryInfo.Name);
					CreateDirectoryNode(directoryInfo.Parent);
					Console.WriteLine(e.Message);
				}
				catch (DirectoryNotFoundException e)
				{
					Console.WriteLine(e.Message);
				}

				foreach (var file in directoryInfo.GetFiles())
					directoryNode.Nodes.Add(new TreeNode(file.Name));

				Console.Out.WriteLine("Loading success : " + directoryInfo.FullName);
				return directoryNode;
			}
			return null;
		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			
			this.setDataWithTypeView(PREFIX_TEST+this.treeView1.SelectedNode.FullPath, "Details");

		}

		private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.isButtonClick)
			{
				try
				{
					String f = this.listView1.SelectedItems[0].ImageKey;
					if (!f.Contains('.'))
					{
						this.setDataWithTypeView(f, "Details");
					}
					
				}
				catch(Exception ex)
				{
					
				}
				
			}
		}


		private void ListView1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.isButtonClick = true;
				this.listView1.ContextMenuStrip.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void ListView1_MouseUp(object sender, MouseEventArgs e)
		{
			this.isButtonClick = false;
		}

		private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			
		}

		private void Label1_Click(object sender, EventArgs e)
		{

		}
	}
}

