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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.new_button = new System.Windows.Forms.Button();
            this.chname_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.chcancle_button = new System.Windows.Forms.Button();
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
            // new_button
            // 
            this.new_button.Location = new System.Drawing.Point(13, 271);
            this.new_button.Name = "new_button";
            this.new_button.Size = new System.Drawing.Size(75, 23);
            this.new_button.TabIndex = 1;
            this.new_button.Text = "Nowy Profil";
            this.new_button.UseVisualStyleBackColor = true;
            this.new_button.Click += new System.EventHandler(this.new_button_Click);
            // 
            // chname_button
            // 
            this.chname_button.Enabled = false;
            this.chname_button.Location = new System.Drawing.Point(94, 271);
            this.chname_button.Name = "chname_button";
            this.chname_button.Size = new System.Drawing.Size(87, 23);
            this.chname_button.TabIndex = 3;
            this.chname_button.Text = "Zmień nazwę";
            this.chname_button.UseVisualStyleBackColor = true;
            this.chname_button.Click += new System.EventHandler(this.chname_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Enabled = false;
            this.delete_button.Location = new System.Drawing.Point(187, 271);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(87, 23);
            this.delete_button.TabIndex = 4;
            this.delete_button.Text = "Usuń";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // save_button
            // 
            this.save_button.Enabled = false;
            this.save_button.Location = new System.Drawing.Point(280, 271);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(87, 23);
            this.save_button.TabIndex = 6;
            this.save_button.Text = "Zastosuj";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // chcancle_button
            // 
            this.chcancle_button.Enabled = false;
            this.chcancle_button.Location = new System.Drawing.Point(373, 271);
            this.chcancle_button.Name = "chcancle_button";
            this.chcancle_button.Size = new System.Drawing.Size(101, 23);
            this.chcancle_button.TabIndex = 7;
            this.chcancle_button.Text = "Cofnij zmiany";
            this.chcancle_button.UseVisualStyleBackColor = true;
            this.chcancle_button.Click += new System.EventHandler(this.chcancle_button_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 306);
            this.Controls.Add(this.chcancle_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.chname_button);
            this.Controls.Add(this.new_button);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Profile Sprzetowe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button new_button;
        private System.Windows.Forms.Button chname_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button save_button;
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button chcancle_button;
    }
}

