


namespace Cliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_fotociudad = new System.Windows.Forms.Button();
            this.btn_fotoplaya = new System.Windows.Forms.Button();
            this.btn_fotomonte = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ConnectBtn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ConnectBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.ConnectBtn, "ConnectBtn");
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.UseVisualStyleBackColor = false;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_fotociudad);
            this.groupBox2.Controls.Add(this.btn_fotoplaya);
            this.groupBox2.Controls.Add(this.btn_fotomonte);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btn_fotociudad
            // 
            resources.ApplyResources(this.btn_fotociudad, "btn_fotociudad");
            this.btn_fotociudad.Name = "btn_fotociudad";
            this.btn_fotociudad.UseVisualStyleBackColor = true;
            this.btn_fotociudad.Click += new System.EventHandler(this.btn_fotociudad_Click);
            // 
            // btn_fotoplaya
            // 
            resources.ApplyResources(this.btn_fotoplaya, "btn_fotoplaya");
            this.btn_fotoplaya.Name = "btn_fotoplaya";
            this.btn_fotoplaya.UseVisualStyleBackColor = true;
            this.btn_fotoplaya.Click += new System.EventHandler(this.btn_fotoplaya_Click);
            // 
            // btn_fotomonte
            // 
            resources.ApplyResources(this.btn_fotomonte, "btn_fotomonte");
            this.btn_fotomonte.Name = "btn_fotomonte";
            this.btn_fotomonte.UseVisualStyleBackColor = true;
            this.btn_fotomonte.Click += new System.EventHandler(this.btn_fotomonte_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        //private System.Windows.Forms.TreeView FilesTree;
        private System.Windows.Forms.Button btn_fotomonte;
        private System.Windows.Forms.Button btn_fotociudad;
        private System.Windows.Forms.Button btn_fotoplaya;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}
/*
namespace Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PortTb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IpTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Info = new System.Windows.Forms.RichTextBox();
            this.DownloadBtn = new System.Windows.Forms.Button();
            this.FilesTree = new System.Windows.Forms.TreeView();
            this.btn_fotocampo = new System.Windows.Forms.Button();
            this.btn_fotoplaya = new System.Windows.Forms.Button();
            this.btn_fotomonte = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PortTb);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.IpTb);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ConnectBtn);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // PortTb
            // 
            resources.ApplyResources(this.PortTb, "PortTb");
            this.PortTb.Name = "PortTb";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // IpTb
            // 
            resources.ApplyResources(this.IpTb, "IpTb");
            this.IpTb.Name = "IpTb";
            this.IpTb.TextChanged += new System.EventHandler(this.IpTb_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ConnectBtn
            // 
            resources.ApplyResources(this.ConnectBtn, "ConnectBtn");
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Info);
            this.groupBox2.Controls.Add(this.DownloadBtn);
            this.groupBox2.Controls.Add(this.FilesTree);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // Info
            // 
            resources.ApplyResources(this.Info, "Info");
            this.Info.Name = "Info";
            this.Info.TextChanged += new System.EventHandler(this.Info_TextChanged);
            // 
            // DownloadBtn
            // 
            resources.ApplyResources(this.DownloadBtn, "DownloadBtn");
            this.DownloadBtn.Name = "DownloadBtn";
            this.DownloadBtn.UseVisualStyleBackColor = true;
            this.DownloadBtn.Click += new System.EventHandler(this.DownloadBtn_Click);
            // 
            // FilesTree
            // 
            resources.ApplyResources(this.FilesTree, "FilesTree");
            this.FilesTree.Name = "FilesTree";
            this.FilesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FilesTree_AfterSelect);
            // 
            // btn_fotocampo
            // 
            resources.ApplyResources(this.btn_fotocampo, "btn_fotocampo");
            this.btn_fotocampo.Name = "btn_fotocampo";
            this.btn_fotocampo.UseVisualStyleBackColor = true;
            this.btn_fotocampo.Click += new System.EventHandler(this.btn_fotociudad_Click);
            // 
            // btn_fotoplaya
            // 
            resources.ApplyResources(this.btn_fotoplaya, "btn_fotoplaya");
            this.btn_fotoplaya.Name = "btn_fotoplaya";
            this.btn_fotoplaya.UseVisualStyleBackColor = true;
            this.btn_fotoplaya.Click += new System.EventHandler(this.btn_fotoplaya_Click);
            // 
            // btn_fotomonte
            // 
            resources.ApplyResources(this.btn_fotomonte, "btn_fotomonte");
            this.btn_fotomonte.Name = "btn_fotomonte";
            this.btn_fotomonte.UseVisualStyleBackColor = true;
            this.btn_fotomonte.Click += new System.EventHandler(this.btn_fotomonte_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox PortTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IpTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button DownloadBtn;
        private System.Windows.Forms.RichTextBox Info;
        private System.Windows.Forms.TreeView FilesTree;
        private System.Windows.Forms.Button btn_fotomonte;
        private System.Windows.Forms.Button btn_fotocampo;
        private System.Windows.Forms.Button btn_fotoplaya;
    }
}

*/