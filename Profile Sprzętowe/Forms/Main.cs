using Profile_Sprzętowe.Class;
using Profile_Sprzętowe.Forms;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
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
        Class.Console console = new Class.Console();
        public int chid = -1;
        public bool isEdit = false;

        public Main(){ InitializeComponent(); }

        public static bool IsConnectedToInternet()
        {
            string host = "google.pl";
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            instance = this;
            this.Text = this.Text + " V" + this.ProductVersion + " by dom133";
            //Create Main Dir
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            if (!File.Exists(directory+"\\devcon.exe")) {
                if (IsConnectedToInternet())
                {
                    if (!is64bit){ webClient.DownloadFile("https://www.dropbox.com/s/1s7jgr0clv5g1rd/devconx32.exe?dl=1", directory + "\\devcon.exe"); }
                    else { webClient.DownloadFile("https://www.dropbox.com/s/q0z3j70g1fbquj9/devconx64.exe?dl=1", directory + "\\devcon.exe"); }
                } else { MessageBox.Show("Brak połączenia z ineternetem, aplikacja wymaga połączenia z internetem podczas pierwszego uruchomienia", "Profile Sprzętowe"); Application.Exit(); }
            }

            //Load profiles if exist
            if(File.Exists(directory+"\\profiles.json")) {
                int active = -1;
                if(File.Exists(directory+"\\active.txt")) { active = Convert.ToInt32(File.ReadAllText(directory+"\\active.txt")); }
                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory+"\\profiles.json"));
                for(int i=0; i<=json.Count-1; i++){ profiles.Add(json[i]); }
                for(int i=0; i<=profiles.Count-1; i++) { listBox1.Items.Add(profiles[i]); }
            }

            if(File.Exists(directory+"\\changes.json")) { chcancle_button.Visible = true; }

            //MessageBox.Show("is64bit: " + is64bit);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                if (MessageBox.Show("Czy na pewno chcesz zamknąć aplikacje?", "Profile Sprzętowe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else { File.WriteAllText(directory + "\\profiles.json", SimpleJson.SerializeObject(profiles)); }
            }
        }

        private void new_button_Click(object sender, EventArgs e)
        {
            new Wait().Show();
            this.Enabled = false;
            new Profile().Show();           
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
                this.Enabled = false;
                new Chname().Show();                 
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) { profiles.RemoveAt(listBox1.SelectedIndex); File.Delete(directory + "\\" + listBox1.SelectedItem + ".json"); listBox1.Items.RemoveAt(listBox1.SelectedIndex); File.WriteAllText(directory + "\\profiles.json", SimpleJson.SerializeObject(profiles)); }
            MessageBox.Show("Poprawnie usunięto profil!!!");
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) {
                if (File.Exists(directory + "\\changes.json")) {
                    dynamic json_changes = SimpleJson.DeserializeObject(File.ReadAllText(directory + "\\changes.json"));
                    this.Enabled = false;
                    string args_changes = "/c devcon enable \"" + json_changes[0] + "\"";
                    for (int i = 1; i <= json_changes.Count - 1; i++)
                    {
                        args_changes += " & devcon enable \"" + json_changes[i] + "\"";
                    }
                    
                    for (int i = 0; i <= 1; i++)
                    {
                        console.sendCmdCommand(args_changes, directory, false);
                    }
                    File.Delete(directory + "\\changes.json");
                }
                
                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory + "\\" + listBox1.SelectedItem + ".json"));
                ArrayList changes = new ArrayList();
                string args = "/c devcon disable \"" + json[1]["Value"][0] + "\"";
                changes.Add(json[1]["Value"][0]);
                for (int i=1; i<=json[1]["Value"].Count-1; i++) {
                    changes.Add(json[1]["Value"][i]);
                    args += " & devcon disable \"" + json[1]["Value"][i] + "\"";
                }

                console.sendCmdCommand(args, directory, false);

                this.Enabled = true;
                File.Delete(directory + "\\active.txt"); File.WriteAllText(directory + "\\active.txt", Convert.ToString(listBox1.SelectedIndex));
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

            for (int i = 0; i <= 1; i++)
            {
                console.sendCmdCommand(args, directory, false);
            }
            this.Enabled = true;
            File.Delete(directory + "\\changes.json"); File.Delete(directory + "\\active.txt");
            MessageBox.Show("Poprawnie cofnięto zmiany", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            chcancle_button.Visible = false;
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) {
                chid = listBox1.SelectedIndex;
                isEdit = true;
                this.Enabled = false;
                new Wait().Show();
                new Profile().Show();
            }
        }

        private void opcjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options().Show();
        }
    }
}