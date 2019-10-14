namespace bt3_dotnet
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
			this.listView1 = new System.Windows.Forms.ListView();
			this.directorySearcher2 = new System.DirectoryServices.DirectorySearcher();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.directorySearcher3 = new System.DirectoryServices.DirectorySearcher();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// directorySearcher1
			// 
			this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
			// 
			// listView1
			// 
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(277, 75);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(495, 459);
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
			this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListView1_MouseDown);
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListView1_MouseUp);
			// 
			// directorySearcher2
			// 
			this.directorySearcher2.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher2.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher2.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(12, 75);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(259, 459);
			this.treeView1.TabIndex = 2;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuStrip1_ItemClicked);
			// 
			// directorySearcher3
			// 
			this.directorySearcher3.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher3.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher3.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(807, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Vì Nếu Load tất cả thư mục ổ đĩa sẽ mất khoảng thời gian rất lâu (Đã code và lưu " +
    "TreeNode vào Storage ), nên đã demo trên một ổ đĩa D:\\UIT cho tiết kiệm thời gia" +
    "n ạ!";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.Click += new System.EventHandler(this.Label1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(604, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(159, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Chuột phải sẽ có list chức năng ";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(819, 531);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.listView1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.DirectoryServices.DirectorySearcher directorySearcher1;
		private System.Windows.Forms.ListView listView1;
		private System.DirectoryServices.DirectorySearcher directorySearcher2;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.DirectoryServices.DirectorySearcher directorySearcher3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

