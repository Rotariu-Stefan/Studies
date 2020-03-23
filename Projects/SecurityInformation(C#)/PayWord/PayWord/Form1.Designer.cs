namespace PayWord
{
    partial class Payword
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
            this.UserPanel = new System.Windows.Forms.Panel();
            this.Pleftlabel = new System.Windows.Forms.Label();
            this.Usetbutton = new System.Windows.Forms.Button();
            this.Ucctb = new System.Windows.Forms.TextBox();
            this.Umaintb = new System.Windows.Forms.TextBox();
            this.Unametb = new System.Windows.Forms.TextBox();
            this.Uname = new System.Windows.Forms.Label();
            this.Ucc = new System.Windows.Forms.Label();
            this.Umail = new System.Windows.Forms.Label();
            this.Ucomitbutton = new System.Windows.Forms.Button();
            this.Ureqbutton = new System.Windows.Forms.Button();
            this.Upaybutton = new System.Windows.Forms.Button();
            this.Upaylabel = new System.Windows.Forms.Label();
            this.Pamount = new System.Windows.Forms.TextBox();
            this.UtextBox = new System.Windows.Forms.TextBox();
            this.Ulabel = new System.Windows.Forms.Label();
            this.BrokerPanel = new System.Windows.Forms.Panel();
            this.Btextbox = new System.Windows.Forms.TextBox();
            this.Blabel = new System.Windows.Forms.Label();
            this.VendorPanel = new System.Windows.Forms.Panel();
            this.Vtextbox = new System.Windows.Forms.TextBox();
            this.Vlabel = new System.Windows.Forms.Label();
            this.UserPanel.SuspendLayout();
            this.BrokerPanel.SuspendLayout();
            this.VendorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserPanel
            // 
            this.UserPanel.Controls.Add(this.Pleftlabel);
            this.UserPanel.Controls.Add(this.Usetbutton);
            this.UserPanel.Controls.Add(this.Ucctb);
            this.UserPanel.Controls.Add(this.Umaintb);
            this.UserPanel.Controls.Add(this.Unametb);
            this.UserPanel.Controls.Add(this.Uname);
            this.UserPanel.Controls.Add(this.Ucc);
            this.UserPanel.Controls.Add(this.Umail);
            this.UserPanel.Controls.Add(this.Ucomitbutton);
            this.UserPanel.Controls.Add(this.Ureqbutton);
            this.UserPanel.Controls.Add(this.Upaybutton);
            this.UserPanel.Controls.Add(this.Upaylabel);
            this.UserPanel.Controls.Add(this.Pamount);
            this.UserPanel.Controls.Add(this.UtextBox);
            this.UserPanel.Controls.Add(this.Ulabel);
            this.UserPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.UserPanel.Location = new System.Drawing.Point(0, 0);
            this.UserPanel.Name = "UserPanel";
            this.UserPanel.Size = new System.Drawing.Size(462, 545);
            this.UserPanel.TabIndex = 0;
            // 
            // Pleftlabel
            // 
            this.Pleftlabel.AutoSize = true;
            this.Pleftlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Pleftlabel.Location = new System.Drawing.Point(23, 254);
            this.Pleftlabel.Name = "Pleftlabel";
            this.Pleftlabel.Size = new System.Drawing.Size(203, 13);
            this.Pleftlabel.TabIndex = 16;
            this.Pleftlabel.Text = "Remaining paywords for the day: 0";
            // 
            // Usetbutton
            // 
            this.Usetbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Usetbutton.Location = new System.Drawing.Point(327, 70);
            this.Usetbutton.Name = "Usetbutton";
            this.Usetbutton.Size = new System.Drawing.Size(100, 32);
            this.Usetbutton.TabIndex = 15;
            this.Usetbutton.Text = "Set User";
            this.Usetbutton.UseVisualStyleBackColor = true;
            this.Usetbutton.Click += new System.EventHandler(this.Usetbutton_Click);
            // 
            // Ucctb
            // 
            this.Ucctb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Ucctb.Location = new System.Drawing.Point(124, 109);
            this.Ucctb.Name = "Ucctb";
            this.Ucctb.Size = new System.Drawing.Size(163, 20);
            this.Ucctb.TabIndex = 14;
            this.Ucctb.Text = "1111111111111111";
            // 
            // Umaintb
            // 
            this.Umaintb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Umaintb.Location = new System.Drawing.Point(124, 82);
            this.Umaintb.Name = "Umaintb";
            this.Umaintb.Size = new System.Drawing.Size(163, 20);
            this.Umaintb.TabIndex = 13;
            this.Umaintb.Text = "defaultuser@mail.com";
            // 
            // Unametb
            // 
            this.Unametb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Unametb.Location = new System.Drawing.Point(124, 53);
            this.Unametb.Name = "Unametb";
            this.Unametb.Size = new System.Drawing.Size(163, 20);
            this.Unametb.TabIndex = 12;
            this.Unametb.Text = "DefaultUser";
            // 
            // Uname
            // 
            this.Uname.AutoSize = true;
            this.Uname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Uname.Location = new System.Drawing.Point(29, 52);
            this.Uname.Name = "Uname";
            this.Uname.Size = new System.Drawing.Size(62, 18);
            this.Uname.TabIndex = 11;
            this.Uname.Text = "Name :";
            // 
            // Ucc
            // 
            this.Ucc.AutoSize = true;
            this.Ucc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Ucc.Location = new System.Drawing.Point(29, 108);
            this.Ucc.Name = "Ucc";
            this.Ucc.Size = new System.Drawing.Size(49, 18);
            this.Ucc.TabIndex = 10;
            this.Ucc.Text = "Card:";
            // 
            // Umail
            // 
            this.Umail.AutoSize = true;
            this.Umail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Umail.Location = new System.Drawing.Point(29, 81);
            this.Umail.Name = "Umail";
            this.Umail.Size = new System.Drawing.Size(60, 18);
            this.Umail.TabIndex = 9;
            this.Umail.Text = "Email :";
            // 
            // Ucomitbutton
            // 
            this.Ucomitbutton.Enabled = false;
            this.Ucomitbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Ucomitbutton.Location = new System.Drawing.Point(262, 151);
            this.Ucomitbutton.Name = "Ucomitbutton";
            this.Ucomitbutton.Size = new System.Drawing.Size(91, 32);
            this.Ucomitbutton.TabIndex = 8;
            this.Ucomitbutton.Text = "Commit";
            this.Ucomitbutton.UseVisualStyleBackColor = true;
            this.Ucomitbutton.Click += new System.EventHandler(this.Ucomitbutton_Click);
            // 
            // Ureqbutton
            // 
            this.Ureqbutton.Enabled = false;
            this.Ureqbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Ureqbutton.Location = new System.Drawing.Point(53, 151);
            this.Ureqbutton.Name = "Ureqbutton";
            this.Ureqbutton.Size = new System.Drawing.Size(91, 32);
            this.Ureqbutton.TabIndex = 7;
            this.Ureqbutton.Text = "Request";
            this.Ureqbutton.UseVisualStyleBackColor = true;
            this.Ureqbutton.Click += new System.EventHandler(this.Ureqbutton_Click);
            // 
            // Upaybutton
            // 
            this.Upaybutton.Enabled = false;
            this.Upaybutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Upaybutton.Location = new System.Drawing.Point(338, 210);
            this.Upaybutton.Name = "Upaybutton";
            this.Upaybutton.Size = new System.Drawing.Size(81, 26);
            this.Upaybutton.TabIndex = 6;
            this.Upaybutton.Text = "Pay";
            this.Upaybutton.UseVisualStyleBackColor = true;
            this.Upaybutton.Click += new System.EventHandler(this.Upaybutton_Click);
            // 
            // Upaylabel
            // 
            this.Upaylabel.AutoSize = true;
            this.Upaylabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Upaylabel.Location = new System.Drawing.Point(23, 216);
            this.Upaylabel.Name = "Upaylabel";
            this.Upaylabel.Size = new System.Drawing.Size(92, 18);
            this.Upaylabel.TabIndex = 5;
            this.Upaylabel.Text = "Paywords :";
            // 
            // Pamount
            // 
            this.Pamount.Location = new System.Drawing.Point(138, 216);
            this.Pamount.Name = "Pamount";
            this.Pamount.ReadOnly = true;
            this.Pamount.Size = new System.Drawing.Size(160, 20);
            this.Pamount.TabIndex = 4;
            this.Pamount.Text = "0";
            // 
            // UtextBox
            // 
            this.UtextBox.Location = new System.Drawing.Point(3, 275);
            this.UtextBox.Multiline = true;
            this.UtextBox.Name = "UtextBox";
            this.UtextBox.ReadOnly = true;
            this.UtextBox.Size = new System.Drawing.Size(444, 264);
            this.UtextBox.TabIndex = 3;
            // 
            // Ulabel
            // 
            this.Ulabel.AutoSize = true;
            this.Ulabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Ulabel.Location = new System.Drawing.Point(195, 9);
            this.Ulabel.Name = "Ulabel";
            this.Ulabel.Size = new System.Drawing.Size(44, 18);
            this.Ulabel.TabIndex = 0;
            this.Ulabel.Text = "User";
            // 
            // BrokerPanel
            // 
            this.BrokerPanel.Controls.Add(this.Btextbox);
            this.BrokerPanel.Controls.Add(this.Blabel);
            this.BrokerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BrokerPanel.Location = new System.Drawing.Point(462, 0);
            this.BrokerPanel.Name = "BrokerPanel";
            this.BrokerPanel.Size = new System.Drawing.Size(465, 236);
            this.BrokerPanel.TabIndex = 1;
            // 
            // Btextbox
            // 
            this.Btextbox.Location = new System.Drawing.Point(17, 30);
            this.Btextbox.Multiline = true;
            this.Btextbox.Name = "Btextbox";
            this.Btextbox.ReadOnly = true;
            this.Btextbox.Size = new System.Drawing.Size(444, 194);
            this.Btextbox.TabIndex = 1;
            // 
            // Blabel
            // 
            this.Blabel.AutoSize = true;
            this.Blabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Blabel.Location = new System.Drawing.Point(194, 9);
            this.Blabel.Name = "Blabel";
            this.Blabel.Size = new System.Drawing.Size(59, 18);
            this.Blabel.TabIndex = 0;
            this.Blabel.Text = "Broker";
            // 
            // VendorPanel
            // 
            this.VendorPanel.Controls.Add(this.Vtextbox);
            this.VendorPanel.Controls.Add(this.Vlabel);
            this.VendorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VendorPanel.Location = new System.Drawing.Point(462, 236);
            this.VendorPanel.Name = "VendorPanel";
            this.VendorPanel.Size = new System.Drawing.Size(465, 309);
            this.VendorPanel.TabIndex = 2;
            // 
            // Vtextbox
            // 
            this.Vtextbox.Location = new System.Drawing.Point(14, 39);
            this.Vtextbox.Multiline = true;
            this.Vtextbox.Name = "Vtextbox";
            this.Vtextbox.ReadOnly = true;
            this.Vtextbox.Size = new System.Drawing.Size(444, 264);
            this.Vtextbox.TabIndex = 2;
            // 
            // Vlabel
            // 
            this.Vlabel.AutoSize = true;
            this.Vlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Vlabel.Location = new System.Drawing.Point(192, 18);
            this.Vlabel.Name = "Vlabel";
            this.Vlabel.Size = new System.Drawing.Size(61, 18);
            this.Vlabel.TabIndex = 0;
            this.Vlabel.Text = "Vendor";
            // 
            // Payword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 545);
            this.Controls.Add(this.VendorPanel);
            this.Controls.Add(this.BrokerPanel);
            this.Controls.Add(this.UserPanel);
            this.Name = "Payword";
            this.Text = "Payword";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Payword_FormClosed);
            this.Load += new System.EventHandler(this.Payword_Load);
            this.UserPanel.ResumeLayout(false);
            this.UserPanel.PerformLayout();
            this.BrokerPanel.ResumeLayout(false);
            this.BrokerPanel.PerformLayout();
            this.VendorPanel.ResumeLayout(false);
            this.VendorPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel UserPanel;
        private System.Windows.Forms.Panel BrokerPanel;
        private System.Windows.Forms.Panel VendorPanel;
        private System.Windows.Forms.Label Ulabel;
        private System.Windows.Forms.Label Blabel;
        private System.Windows.Forms.Label Vlabel;
        private System.Windows.Forms.TextBox UtextBox;
        private System.Windows.Forms.TextBox Btextbox;
        private System.Windows.Forms.TextBox Vtextbox;
        private System.Windows.Forms.Button Upaybutton;
        private System.Windows.Forms.Label Upaylabel;
        private System.Windows.Forms.TextBox Pamount;
        private System.Windows.Forms.TextBox Ucctb;
        private System.Windows.Forms.TextBox Umaintb;
        private System.Windows.Forms.TextBox Unametb;
        private System.Windows.Forms.Label Uname;
        private System.Windows.Forms.Label Ucc;
        private System.Windows.Forms.Label Umail;
        private System.Windows.Forms.Button Ucomitbutton;
        private System.Windows.Forms.Button Ureqbutton;
        private System.Windows.Forms.Button Usetbutton;
        private System.Windows.Forms.Label Pleftlabel;
    }
}

