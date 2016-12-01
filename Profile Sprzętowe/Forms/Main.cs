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

        public Main(){ InitializeComponent(); }

        public static void editXml(string loc, string name_class, string name, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(loc);
            doc.SelectSingleNode(name_class + "/" + name).InnerText = value;
            doc.Save(loc);
        }

        public static string readXml(string loc, string name)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            FileStream fs = new FileStream(loc, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName(name);
            try
            {
                fs.Close();
                return xmlnode[0].ChildNodes.Item(0).InnerText.Trim();
            }
            catch
            {
                return "false";
            }
        }

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

            if(File.Exists(directory+"\\changes.json")) { chcancle_button.Enabled = true; }

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
                chname_button.Enabled = true;
                delete_button.Enabled = true;
                save_button.Enabled = true;
            } else {
                chname_button.Enabled = false;
                delete_button.Enabled = false;
                save_button.Enabled = false;
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
            if (listBox1.SelectedIndex != -1) { profiles.RemoveAt(listBox1.SelectedIndex); File.Delete(directory + "\\" + listBox1.SelectedItem + ".json"); listBox1.Items.RemoveAt(listBox1.SelectedIndex); }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) {
                this.Enabled = false;
                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory + "\\" + listBox1.SelectedItem + ".json"));
                ArrayList changes = new ArrayList();
                for (int i=0; i<=json.Count-1; i++) {
                    changes.Add(json[i]);
                    string output = json[i];
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.WorkingDirectory = directory;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/c devcon disable \""+output+"\"";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                }
                this.Enabled = true;
                File.WriteAllText(directory + "\\changes.json", SimpleJson.SerializeObject(changes));
                chcancle_button.Enabled = true;
                MessageBox.Show("Poprawnie zastosowano profil, aby profil był aktywny w 100% należy uruchomić ponownie komputer", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chcancle_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1){
                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory + "\\changes.json"));
                this.Enabled = false;
                for (int i = 0; i <= json.Count - 1; i++) {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.WorkingDirectory = directory;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/c devcon enable \"" + json[i] + "\"";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                }
                this.Enabled = true;
                File.Delete(directory + "\\changes.json");
                MessageBox.Show("Poprawnie cofnięto zmiany", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chcancle_button.Enabled = false;
            }
        }
    }
}
