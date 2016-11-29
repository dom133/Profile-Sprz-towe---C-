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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.new_button = new System.Windows.Forms.Button();
            this.duplikate_button = new System.Windows.Forms.Button();
            this.chname_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(436, 251);
            this.listBox1.TabIndex = 0;
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
            // duplikate_button
            // 
            this.duplikate_button.Enabled = false;
            this.duplikate_button.Location = new System.Drawing.Point(94, 271);
            this.duplikate_button.Name = "duplikate_button";
            this.duplikate_button.Size = new System.Drawing.Size(75, 23);
            this.duplikate_button.TabIndex = 2;
            this.duplikate_button.Text = "Duplikuj";
            this.duplikate_button.UseVisualStyleBackColor = true;
            // 
            // chname_button
            // 
            this.chname_button.Enabled = false;
            this.chname_button.Location = new System.Drawing.Point(175, 271);
            this.chname_button.Name = "chname_button";
            this.chname_button.Size = new System.Drawing.Size(87, 23);
            this.chname_button.TabIndex = 3;
            this.chname_button.Text = "Zmień nazwę";
            this.chname_button.UseVisualStyleBackColor = true;
            // 
            // delete_button
            // 
            this.delete_button.Enabled = false;
            this.delete_button.Location = new System.Drawing.Point(268, 271);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(87, 23);
            this.delete_button.TabIndex = 4;
            this.delete_button.Text = "Usuń";
            this.delete_button.UseVisualStyleBackColor = true;
            // 
            // save_button
            // 
            this.save_button.Enabled = false;
            this.save_button.Location = new System.Drawing.Point(361, 271);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(87, 23);
            this.save_button.TabIndex = 6;
            this.save_button.Text = "Zastosuj";
            this.save_button.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 306);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.chname_button);
            this.Controls.Add(this.duplikate_button);
            this.Controls.Add(this.new_button);
            this.Controls.Add(this.listBox1);
            this.Name = "Main";
            this.Text = "Profile Sprzetowe";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button new_button;
        private System.Windows.Forms.Button duplikate_button;
        private System.Windows.Forms.Button chname_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button save_button;
    }
}

