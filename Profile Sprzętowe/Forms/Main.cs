using Profile_Sprzętowe.Forms;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Profile_Sprzętowe
{
    public partial class Main : Form
    {
        static Main instance;
        public static Main Instance { get { return instance; } }
        bool is64bit = Environment.Is64BitOperatingSystem;
        public string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HM";
        public WebClient webClient = new WebClient();


        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            instance = this;
            //Create Main Dir
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            if (!File.Exists(directory+"\\devcon.exe")) {
                if(!is64bit) { webClient.DownloadFile("https://www.dropbox.com/s/1s7jgr0clv5g1rd/devconx32.exe?dl=1", directory + "\\devcon.exe");
                }else{webClient.DownloadFile("https://www.dropbox.com/s/q0z3j70g1fbquj9/devconx64.exe?dl=1", directory + "\\devcon.exe");}
            }
            MessageBox.Show("is64bit: " + is64bit);
        }

        private void new_button_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Enabled = false;
        }

    }
}
