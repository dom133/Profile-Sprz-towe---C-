using System;
using System.IO;
using System.Windows.Forms;

namespace Profile_Sprzętowe.Forms
{
    public partial class Chname : Form
    {
        public string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HM";
        bool isClosed = false;
        public Chname(){ InitializeComponent(); }

        private void Chname_Load(object sender, EventArgs e)
        {
            name_txt.Text = Main.Instance.profiles[Main.Instance.chid].ToString();
        }

        private void Chname_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !isClosed)
            {
                if (MessageBox.Show("Czy na pewno chcesz zamknąć okno?", "Profile Sprzętowe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Chname_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main.Instance.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(name_txt.Text!=""){
                if(!File.Exists(directory+"\\"+ name_txt.Text + ".json")) {
                    File.Move(directory + "\\" + Main.Instance.profiles[Main.Instance.chid].ToString() + ".json", directory + "\\" + name_txt.Text + ".json");
                    isClosed = true;
                    Main.Instance.profiles.RemoveAt(Main.Instance.chid);
                    Main.Instance.profiles.Insert(Main.Instance.chid, name_txt.Text);
                    Main.Instance.listBox1.Items.RemoveAt(Main.Instance.chid);
                    Main.Instance.listBox1.Items.Insert(Main.Instance.chid, name_txt.Text);
                    MessageBox.Show("Poprawnie zmieniono nazwę!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    File.WriteAllText(directory + "\\profiles.json", SimpleJson.SerializeObject(Main.Instance.profiles));
                    Main.Instance.chid = -1;
                    this.Close();
                } else { MessageBox.Show("Podana nazwa jest już zajęta!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }
    }
}