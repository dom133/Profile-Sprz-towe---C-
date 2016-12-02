namespace Profile_Sprzętowe
{
    partial class Main
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.new_button = new System.Windows.Forms.ToolStripMenuItem();
            this.chname_button = new System.Windows.Forms.ToolStripMenuItem();
            this.save_button = new System.Windows.Forms.ToolStripMenuItem();
            this.chcancle_button = new System.Windows.Forms.ToolStripMenuItem();
            this.delete_button = new System.Windows.Forms.ToolStripMenuItem();
            this.edit_button = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(462, 251);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.new_button,
            this.edit_button,
            this.chname_button,
            this.save_button,
            this.chcancle_button,
            this.delete_button});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 158);
            // 
            // new_button
            // 
            this.new_button.Name = "new_button";
            this.new_button.Size = new System.Drawing.Size(127, 22);
            this.new_button.Text = "Nowy Profil";
            this.new_button.Click += new System.EventHandler(this.new_button_Click);
            // 
            // chname_button
            // 
            this.chname_button.Name = "chname_button";
            this.chname_button.Size = new System.Drawing.Size(127, 22);
            this.chname_button.Text = "Zmień nazwę";
            this.chname_button.Visible = false;
            this.chname_button.Click += new System.EventHandler(this.chname_button_Click);
            // 
            // save_button
            // 
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(127, 22);
            this.save_button.Text = "Zastosuj";
            this.save_button.Visible = false;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // chcancle_button
            // 
            this.chcancle_button.Name = "chcancle_button";
            this.chcancle_button.Size = new System.Drawing.Size(127, 22);
            this.chcancle_button.Text = "Cofnij zmiany";
            this.chcancle_button.Visible = false;
            this.chcancle_button.Click += new System.EventHandler(this.chcancle_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(127, 22);
            this.delete_button.Text = "Usuń";
            this.delete_button.Visible = false;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // edit_button
            // 
            this.edit_button.Name = "edit_button";
            this.edit_button.Size = new System.Drawing.Size(127, 22);
            this.edit_button.Text = "Edytuj";
            this.edit_button.Visible = false;
            this.edit_button.Click += new System.EventHandler(this.edit_button_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 272);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Profile Sprzetowe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem new_button;
        private System.Windows.Forms.ToolStripMenuItem chname_button;
        private System.Windows.Forms.ToolStripMenuItem save_button;
        private System.Windows.Forms.ToolStripMenuItem chcancle_button;
        private System.Windows.Forms.ToolStripMenuItem delete_button;
        private System.Windows.Forms.ToolStripMenuItem edit_button;
    }
}

