namespace filebd
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
            this.browsebutton = new System.Windows.Forms.Button();
            this.pathtb = new System.Windows.Forms.TextBox();
            this.loadbutton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.mainlist = new System.Windows.Forms.ListBox();
            this.showdbbutton = new System.Windows.Forms.Button();
            this.showtb = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.loadbutton2 = new System.Windows.Forms.Button();
            this.savebutton = new System.Windows.Forms.Button();
            this.renbutton = new System.Windows.Forms.Button();
            this.delbutton = new System.Windows.Forms.Button();
            this.rentb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // browsebutton
            // 
            this.browsebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.browsebutton.Location = new System.Drawing.Point(33, 27);
            this.browsebutton.Name = "browsebutton";
            this.browsebutton.Size = new System.Drawing.Size(110, 27);
            this.browsebutton.TabIndex = 0;
            this.browsebutton.Text = "Browse";
            this.browsebutton.UseVisualStyleBackColor = true;
            this.browsebutton.Click += new System.EventHandler(this.browsebutton_Click);
            // 
            // pathtb
            // 
            this.pathtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pathtb.Location = new System.Drawing.Point(159, 32);
            this.pathtb.Name = "pathtb";
            this.pathtb.ReadOnly = true;
            this.pathtb.Size = new System.Drawing.Size(259, 20);
            this.pathtb.TabIndex = 1;
            this.pathtb.Text = "U:\\stravos\\So";
            // 
            // loadbutton
            // 
            this.loadbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loadbutton.Location = new System.Drawing.Point(441, 25);
            this.loadbutton.Name = "loadbutton";
            this.loadbutton.Size = new System.Drawing.Size(156, 27);
            this.loadbutton.TabIndex = 2;
            this.loadbutton.Text = "Load from folder";
            this.loadbutton.UseVisualStyleBackColor = true;
            this.loadbutton.Click += new System.EventHandler(this.loadbutton_Click);
            // 
            // mainlist
            // 
            this.mainlist.FormattingEnabled = true;
            this.mainlist.Location = new System.Drawing.Point(33, 88);
            this.mainlist.Name = "mainlist";
            this.mainlist.Size = new System.Drawing.Size(230, 368);
            this.mainlist.TabIndex = 3;
            this.mainlist.SelectedValueChanged += new System.EventHandler(this.mainlist_SelectedValueChanged);
            // 
            // showdbbutton
            // 
            this.showdbbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.showdbbutton.Location = new System.Drawing.Point(603, 45);
            this.showdbbutton.Name = "showdbbutton";
            this.showdbbutton.Size = new System.Drawing.Size(140, 27);
            this.showdbbutton.TabIndex = 4;
            this.showdbbutton.Text = "Show Files";
            this.showdbbutton.UseVisualStyleBackColor = true;
            this.showdbbutton.Click += new System.EventHandler(this.showdbbutton_Click);
            // 
            // showtb
            // 
            this.showtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.showtb.Location = new System.Drawing.Point(603, 12);
            this.showtb.Name = "showtb";
            this.showtb.Size = new System.Drawing.Size(140, 27);
            this.showtb.TabIndex = 6;
            this.showtb.Text = "Show Table";
            this.showtb.UseVisualStyleBackColor = true;
            this.showtb.Click += new System.EventHandler(this.showtb_Click);
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listView1.Location = new System.Drawing.Point(285, 88);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(669, 483);
            this.listView1.TabIndex = 7;
            this.listView1.TileSize = new System.Drawing.Size(100, 100);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            // 
            // loadbutton2
            // 
            this.loadbutton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loadbutton2.Location = new System.Drawing.Point(762, 12);
            this.loadbutton2.Name = "loadbutton2";
            this.loadbutton2.Size = new System.Drawing.Size(156, 27);
            this.loadbutton2.TabIndex = 8;
            this.loadbutton2.Text = "Load from dqldb";
            this.loadbutton2.UseVisualStyleBackColor = true;
            this.loadbutton2.Click += new System.EventHandler(this.loadbutton2_Click);
            // 
            // savebutton
            // 
            this.savebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.savebutton.Location = new System.Drawing.Point(762, 45);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(156, 27);
            this.savebutton.TabIndex = 9;
            this.savebutton.Text = "Save to dqldb";
            this.savebutton.UseVisualStyleBackColor = true;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // renbutton
            // 
            this.renbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.renbutton.Location = new System.Drawing.Point(12, 528);
            this.renbutton.Name = "renbutton";
            this.renbutton.Size = new System.Drawing.Size(110, 27);
            this.renbutton.TabIndex = 10;
            this.renbutton.Text = "Rename";
            this.renbutton.UseVisualStyleBackColor = true;
            this.renbutton.Click += new System.EventHandler(this.renbutton_Click);
            // 
            // delbutton
            // 
            this.delbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.delbutton.Location = new System.Drawing.Point(12, 482);
            this.delbutton.Name = "delbutton";
            this.delbutton.Size = new System.Drawing.Size(110, 27);
            this.delbutton.TabIndex = 11;
            this.delbutton.Text = "Delete";
            this.delbutton.UseVisualStyleBackColor = true;
            this.delbutton.Click += new System.EventHandler(this.delbutton_Click);
            // 
            // rentb
            // 
            this.rentb.Location = new System.Drawing.Point(128, 534);
            this.rentb.Name = "rentb";
            this.rentb.Size = new System.Drawing.Size(135, 20);
            this.rentb.TabIndex = 12;
            this.rentb.Text = "fileX";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 583);
            this.Controls.Add(this.rentb);
            this.Controls.Add(this.delbutton);
            this.Controls.Add(this.renbutton);
            this.Controls.Add(this.savebutton);
            this.Controls.Add(this.loadbutton2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.showtb);
            this.Controls.Add(this.showdbbutton);
            this.Controls.Add(this.mainlist);
            this.Controls.Add(this.loadbutton);
            this.Controls.Add(this.pathtb);
            this.Controls.Add(this.browsebutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browsebutton;
        private System.Windows.Forms.TextBox pathtb;
        private System.Windows.Forms.Button loadbutton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ListBox mainlist;
        private System.Windows.Forms.Button showdbbutton;
        private System.Windows.Forms.Button showtb;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button loadbutton2;
        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.Button renbutton;
        private System.Windows.Forms.Button delbutton;
        private System.Windows.Forms.TextBox rentb;
    }
}

