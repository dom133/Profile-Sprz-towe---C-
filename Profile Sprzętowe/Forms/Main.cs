using Profile_Sprzętowe.Forms;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace Profile_Sprzętowe
{
    public partial class Main : Form
    {
        static Main instance;
        public static Main Instance { get { return instance; } }
        bool is64bit = Environment.Is64BitOperatingSystem;
        public string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HM";
        public WebClient webClient = new WebClient();
        public ArrayList profiles = new ArrayList();
        public int chid = -1;
        public bool isEdit = false;

        public Main(){ InitializeComponent(); }

        private void Main_Load(object sender, EventArgs e)
        {
            instance = this;

            //Create Main Dir
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            if (!File.Exists(directory+"\\devcon.exe")) {
                if (!is64bit) { webClient.DownloadFile("https://www.dropbox.com/s/1s7jgr0clv5g1rd/devconx32.exe?dl=1", directory + "\\devcon.exe");
                } else { webClient.DownloadFile("https://www.dropbox.com/s/q0z3j70g1fbquj9/devconx64.exe?dl=1", directory + "\\devcon.exe"); }
            }

            //Load profiles if exist
            if(File.Exists(directory+"\\profiles.json")) {
                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory+"\\profiles.json"));
                for(int i=0; i<=json.Count-1; i++){ profiles.Add(json[i]); }
                for(int i=0; i<=profiles.Count-1; i++) { listBox1.Items.Add(profiles[i]); }
            }

            if(File.Exists(directory+"\\changes.json")) { chcancle_button.Visible = true; }

            //MessageBox.Show("is64bit: " + is64bit);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz zamknąć aplikacje?", "Profile Sprzętowe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;               
            } else { File.WriteAllText(directory + "\\profiles.json", SimpleJson.SerializeObject(profiles)); }
        }

        private void new_button_Click(object sender, EventArgs e)
        {
            new Profile().Show();
            this.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex!=-1) {
                chname_button.Visible = true;
                delete_button.Visible = true;
                save_button.Visible = true;
                edit_button.Visible = true;
            } else {
                chname_button.Visible = false;
                delete_button.Visible = false;
                save_button.Visible = false;
                edit_button.Visible = false;
            }
        }

        private void chname_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) {
                chid = listBox1.SelectedIndex;
                new Chname().Show();
                this.Enabled = false; 
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) { profiles.RemoveAt(listBox1.SelectedIndex); File.Delete(directory + "\\" + listBox1.SelectedItem + ".json"); listBox1.Items.RemoveAt(listBox1.SelectedIndex); File.WriteAllText(directory + "\\profiles.json", SimpleJson.SerializeObject(profiles)); }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) {
                this.Enabled = false;
                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory + "\\" + listBox1.SelectedItem + ".json"));
                ArrayList changes = new ArrayList();
                string args = "/c devcon disable \"" + json[1]["Value"][0] + "\"";
                changes.Add(json[1]["Value"][0]);
                for (int i=1; i<=json[1]["Value"].Count-1; i++) {
                    changes.Add(json[1]["Value"][i]);
                    args += " & devcon disable \"" + json[1]["Value"][i] + "\"";
                }           

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.WorkingDirectory = directory;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = args;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                this.Enabled = true;
                File.WriteAllText(directory + "\\changes.json", SimpleJson.SerializeObject(changes));
                chcancle_button.Visible = true;
                MessageBox.Show("Poprawnie zastosowano profil, aby profil był aktywny w 100% należy uruchomić ponownie komputer", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chcancle_button_Click(object sender, EventArgs e)
        {
            dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory + "\\changes.json"));
            this.Enabled = false;
            string args = "/c devcon enable \"" + json[0] + "\"";
            for (int i = 1; i <= json.Count - 1; i++)
            {
                args += " & devcon enable \"" + json[i] + "\"";
            }

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.WorkingDirectory = directory;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = args;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            this.Enabled = true;
            File.Delete(directory + "\\changes.json");
            MessageBox.Show("Poprawnie cofnięto zmiany", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            chcancle_button.Visible = false;
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) {
                chid = listBox1.SelectedIndex;
                isEdit = true;
                this.Enabled = false;
                new Profile().Show();
            }
        }
    }
}
