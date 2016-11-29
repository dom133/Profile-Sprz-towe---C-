using System;
using System.Collections;
using System.Windows.Forms;

namespace Profile_Sprzętowe.Forms
{
    public partial class Profile : Form
    {
        static Profile instance;
        public static Profile Instance { get { return instance; } }
        public string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HM";
        public ArrayList id = new ArrayList();

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
            startInfo.Arguments = "/c devcon find * > list.txt";
            process.StartInfo = startInfo;
            process.Start();

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(directory + "\\list.txt");
            while((line = file.ReadLine()) != null){
                string[] temp_line = line.Split(':', ':');
                if (temp_line.Length != 1) { listBox2.Items.Add(temp_line[1]); id.Add(temp_line[0]); }
            }

        }

        private void Profile_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main.Instance.Enabled = true;
        }

    }
}
