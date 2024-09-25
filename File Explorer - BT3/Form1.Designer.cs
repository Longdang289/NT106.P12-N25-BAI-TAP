namespace File_Explorer___BT3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            treeView1 = new TreeView();
            listView1 = new ListView();
            txtSearch = new TextBox();
            undo = new Button();
            redo = new Button();
            refresh = new Button();
            button4 = new Button();
            txtPath = new TextBox();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.Location = new Point(0, 32);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(151, 418);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += TreeView1_AfterSelect;
            // 
            // listView1
            // 
            listView1.Location = new Point(157, 32);
            listView1.Name = "listView1";
            listView1.Size = new Size(644, 418);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(628, 0);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(144, 27);
            txtSearch.TabIndex = 2;
            txtSearch.Text = "Search";
            txtSearch.TextChanged += textBox1_TextChanged;
            // 
            // undo
            // 
            undo.Location = new Point(0, 0);
            undo.Name = "undo";
            undo.Size = new Size(49, 29);
            undo.TabIndex = 3;
            undo.Text = "↶";
            undo.UseVisualStyleBackColor = true;
            // 
            // redo
            // 
            redo.Font = new Font("Segoe UI", 9F);
            redo.Location = new Point(55, 0);
            redo.Name = "redo";
            redo.Size = new Size(48, 29);
            redo.TabIndex = 4;
            redo.Text = "↷";
            redo.UseVisualStyleBackColor = true;
            // 
            // refresh
            // 
            refresh.Location = new Point(109, 0);
            refresh.Name = "refresh";
            refresh.Size = new Size(42, 29);
            refresh.TabIndex = 5;
            refresh.Text = "⟳";
            refresh.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(772, -1);
            button4.Name = "button4";
            button4.Size = new Size(29, 29);
            button4.TabIndex = 6;
            button4.Text = "➤";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // txtPath
            // 
            txtPath.Location = new Point(157, -1);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(465, 27);
            txtPath.TabIndex = 7;
            txtPath.TextChanged += txtPath_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtPath);
            Controls.Add(button4);
            Controls.Add(refresh);
            Controls.Add(redo);
            Controls.Add(undo);
            Controls.Add(txtSearch);
            Controls.Add(listView1);
            Controls.Add(treeView1);
            Name = "Form1";
            Text = "File Explorer";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeView1;
        private ListView listView1;
        private TextBox txtSearch;
        private Button undo;
        private Button redo;
        private Button refresh;
        private Button button4;
        private TextBox txtPath;
    }
}
