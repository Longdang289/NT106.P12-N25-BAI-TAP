using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
namespace File_Explorer___BT3
{
    public partial class Form1 : Form
    {
        private List<string> history = new List<string>();
        private int currentIndex = -1;
        private string currentDirectory = "";
        public Form1()
        {
            InitializeComponent();
            InitializeTreeView();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Name", 250);
            listView1.Columns.Add("Type", 100);
            listView1.DoubleClick += ListView1_DoubleClick;
        }

        private void InitializeTreeView()
        {
            treeView1.BeforeExpand += TreeView1_BeforeExpand;
            treeView1.AfterSelect += TreeView1_AfterSelect;
            undo.Click += undo_click;
            redo.Click += redo_click;
            refresh.Click += refresh_click;
            foreach (var drive in Directory.GetLogicalDrives())
            {
                TreeNode driveNode = new TreeNode(drive) { Tag = drive };
                driveNode.Nodes.Add("");
                treeView1.Nodes.Add(driveNode);
            }

        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node;
            node.Nodes.Clear();
            string path = node.Tag.ToString();
            try
            {
                var directories = Directory.GetDirectories(path);
                foreach (var dir in directories)
                {
                    TreeNode dirNode = new TreeNode(Path.GetFileName(dir)) { Tag = dir };
                    dirNode.Nodes.Add("");
                    node.Nodes.Add(dirNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Không thể truy cập vào thư mục này");
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedPath = e.Node.Tag.ToString();
            LoadDirectoriesAndFiles(selectedPath);
            if(currentIndex == -1 || history[currentIndex] != selectedPath)
            {
                history.Add(selectedPath);
                currentIndex++;
            }
            UpdateUndoRedoButtons();
            UpdateRefreshButton();
        }
        private void LoadDirectories(string dir, TreeNodeCollection node)
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            try
            {
                foreach (DirectoryInfo d in di.GetDirectories())
                {
                    TreeNode tNode = new TreeNode(d.Name);
                    node.Add(tNode);
                    LoadDirectories(d.FullName, tNode.Nodes);
                }
            }
            catch (UnauthorizedAccessException) { }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedPath = GetFullPathFromNode(e.Node);
            LoadDirectoriesAndFiles(selectedPath);
        }

        private void LoadDirectoriesAndFiles(string path)
        {
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Không có thư mục!");
                return;
            }
            currentDirectory = path;
            listView1.Items.Clear();
            try
            {
                var directories = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                foreach (var dir in directories)
                {
                    var dirInfo = new DirectoryInfo(dir);
                    var item = new ListViewItem(dirInfo.Name);
                    item.SubItems.Add("Folder");
                    item.Tag = dirInfo.FullName;
                    listView1.Items.Add(item);
                }
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    var item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add("File");
                    item.Tag = fileInfo.FullName;
                    listView1.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Không có quyền truy cập thư mục này");
            }
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    listView1.Items.Add(new ListViewItem(dir.Name, 0));
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    listView1.Items.Add(new ListViewItem(file.Name, 1));
                }

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Không có quyền truy cập thư mục này");
            }
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(dir.Name);
                    item.SubItems.Add("Folder");
                    item.Tag = dir.FullName;
                    listView1.Items.Add(item);
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add("File");
                    item.Tag = file.FullName;
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            txtPath.Text = path;
            if (currentIndex == -1 || history[currentIndex] != path)
            {
                history.Add(path);
                currentIndex++;
            }
        }
        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string path = selectedItem.Tag.ToString();

                if (Directory.Exists(path))
                {
                    LoadDirectoriesAndFiles(path);
                }
                else if (File.Exists(path))
                {
                    System.Diagnostics.Process.Start(path);
                }
                UpdateUndoRedoButtons();
                UpdateRefreshButton();
            }
        }

        private void undo_click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                LoadDirectoriesAndFiles(history[currentIndex]);
            }
            UpdateUndoRedoButtons();
        }

        private void redo_click(object sender, EventArgs e)
        {
            if (currentIndex < history.Count - 1)
            {
                currentIndex++;
                LoadDirectoriesAndFiles(history[currentIndex]);
            }
            UpdateUndoRedoButtons();
        }
        private void refresh_click(object sender, EventArgs e)
        {
            if (currentIndex != -1)
            {
                LoadDirectoriesAndFiles(history[currentIndex]);
            }
            UpdateRefreshButton();
        }

        private string GetFullPathFromNode(TreeNode node)
        {
            string fullPath = node.Text;
            TreeNode parentNode = node.Parent;
            while (parentNode != null)
            {
                fullPath = Path.Combine(parentNode.Text, fullPath);
                parentNode = parentNode.Parent;
            }
            return fullPath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                Search(currentDirectory, keyword);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm");
            }
        }

        private void Search(string directory, string keyword)
        {
            listView1.Items.Clear();
            try
            {
                DirectoryInfo di = new DirectoryInfo(directory);
                var directories = di.GetDirectories().Where(d => d.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                var files = di.GetFiles().Where(f => f.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                foreach (var dir in directories)
                {
                    listView1.Items.Add(new ListViewItem(dir.Name, 0));
                }
                foreach (var file in files)
                {
                    listView1.Items.Add(new ListViewItem(file.Name, 1));
                }
                if (directories.Count == 0 && files.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả nào");
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Không có quyền truy cập vào thư mục này");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void UpdateUndoRedoButtons()
        {
            undo.Enabled = currentIndex > 0;
            redo.Enabled = currentIndex < history.Count - 1;
        }

        private void UpdateRefreshButton()
        {
            refresh.Enabled = currentIndex != -1;
        }
        private void txtPath_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

    

