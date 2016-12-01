using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Profile_Sprzętowe.Forms
{
    public partial class Profile : Form
    {
        static Profile instance;
        public static Profile Instance { get { return instance; } }
        public string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HM";
        public ArrayList name_hardware = new ArrayList();
        public ArrayList id_hardware = new ArrayList();
        public ArrayList id_active = new ArrayList();
        public ArrayList name_active = new ArrayList();
        public ArrayList to_remove = new ArrayList();
        private bool isClosed = false;

        public Profile(){ InitializeComponent(); }

        private void Profile_Load(object sender, EventArgs e)
        {
            instance = this;

            //Read peripheral list
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.WorkingDirectory = directory;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c devcon find *> list.txt";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(directory + "\\list.txt", Encoding.GetEncoding("ISO-8859-2"));
            while((line = file.ReadLine()) != null){
                string[] temp_line = line.Split(':', ':');
                if (temp_line.Length != 1) {
                    string output = temp_line[0];
                    output = Regex.Replace(output, @"\s+", "");
                    int index = output.LastIndexOf("\\");
                    if (index > 0) { output = output.Substring(0, index); }
                    id_hardware.Add(output);
                    name_hardware.Add(temp_line[1]);                                  
                }
            }

            for (int i = 0; i <= id_hardware.Count - 1; i++){ listBox2.Items.Add(name_hardware[i]); }

        }

        private void Profile_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main.Instance.Enabled = true;
        }

        private void Profile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !isClosed)
            {
                if (MessageBox.Show("Czy na pewno chcesz zamknąć okno?", "Profile Sprzętowe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            int item = listBox2.SelectedIndex;

            if (item != -1)
            {
                id_active.Add(id_hardware[item]);
                name_active.Add(name_hardware[item]);
                listBox2.Items.RemoveAt(item);
                listBox1.Items.Add(name_hardware[item]);
                id_hardware.RemoveAt(item);
                name_hardware.RemoveAt(item);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            int item = listBox1.SelectedIndex;

            if (item != -1)
            {
                id_hardware.Add(id_active[item]);
                name_hardware.Add(name_active[item]);
                listBox1.Items.RemoveAt(item);
                listBox2.Items.Add(name_active[item]);
                id_active.RemoveAt(item);
                name_active.RemoveAt(item);
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (name_txt.Text == "") { MessageBox.Show("Pole nazwa nie może być puste!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else if (File.Exists(directory + "\\" + name_txt.Text + ".json")) { MessageBox.Show("Profil już istnieje!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else if (id_active.Count == 0) { MessageBox.Show("Brak wyłączonych urządzeń!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else {
                File.WriteAllText(directory+"\\"+name_txt.Text+".json", SimpleJson.SerializeObject(id_active));
                MessageBox.Show("Poprawnie utworzono profil sprzętowy!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Main.Instance.listBox1.Items.Add(name_txt.Text);
                Main.Instance.profiles.Add(name_txt.Text);
                isClosed = true;
                this.Close();
            }
        }
    }
}
