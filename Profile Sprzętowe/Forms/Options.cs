using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace Profile_Sprzętowe.Forms
{
    public partial class Options : Form
    {
        Class.Console console = new Class.Console();
        public ArrayList options = new ArrayList();
        public Boolean action = false;

        public Options(){ InitializeComponent(); }

        private void Options_Load(object sender, EventArgs e) {          
            if(File.Exists(Main.Instance.directory+"\\options.json")){
                action = true;
                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(Main.Instance.directory + "\\options.json"));
                options.AddRange(json);
                show_onstart_checkbox.Checked = Convert.ToBoolean(options[0]);
                action = false;
            } else {
                action = true;
                options.Insert(0, "false"); show_onstart_checkbox.Checked = false;
                action = false;           
            }
        }

        private void show_onstart_checkbox_CheckedChanged(object sender, EventArgs e) {
            if (!action)
            {
                if (show_onstart_checkbox.Checked == true)
                {
                    if (MessageBox.Show("Czy na pewno chcesz aktywować tą funkcje? Robisz to na własną odpowiedzialność", "Opcje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        console.sendCmdCommand("/c reg add \"HKCU\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\" /f /v Shell /d \"" + Environment.CurrentDirectory + "\\boot.exe\"", Main.Instance.directory, false);
                        options.RemoveAt(0); options.Insert(0, "true");
                        MessageBox.Show("Poprawnie aktywowano funkcję");
                    }
                    else { action = true; show_onstart_checkbox.Checked = false; action = false; }
                }
                else
                {
                    console.sendCmdCommand("/c reg delete \"HKCU\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\" /f /v Shell", Main.Instance.directory, false);
                    options.RemoveAt(0); options.Insert(0, "false");
                    MessageBox.Show("Poprawnie dezaktywowano funkcję");
                }
            }
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e){ File.WriteAllText(Main.Instance.directory + "\\options.json", SimpleJson.SerializeObject(options)); }
    }
}
