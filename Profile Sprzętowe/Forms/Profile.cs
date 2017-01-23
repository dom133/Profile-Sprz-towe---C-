using System;
using System.Collections;
using System.Collections.Generic;
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
        Class.Console console = new Class.Console();
        private bool isClosed = false;

        public Profile(){ InitializeComponent(); }

        private void Profile_Load(object sender, EventArgs e)
        {
            Wait.Instance.updateProgressBar(20);
            instance = this;

            //Read peripheral list
            console.sendCmdCommand("/c devcon find * > list.txt", directory, false);
            Wait.Instance.updateProgressBar(40);
            if (!Main.Instance.isEdit) {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(directory + "\\list.txt", Encoding.GetEncoding("ISO-8859-2"));
                Wait.Instance.updateProgressBar(50);
                while ((line = file.ReadLine()) != null)
                {
                    string[] temp_line = line.Split(':', ':');
                    if (temp_line.Length != 1)
                    {
                        string output = temp_line[0];
                        string name = temp_line[1];
                        output = Regex.Replace(output, @"\s+", "");
                        int index_1 = output.LastIndexOf("\\");
                        if (index_1 > 0) { output = output.Substring(0, index_1); }
                        console.sendCmdCommand("/c devcon hwids \"" + output + "\" > out.txt", directory, false);
                        Wait.Instance.updateProgressBar(70);
                        System.IO.StreamReader outline = new System.IO.StreamReader(directory + "\\out.txt");
                        if (outline.ReadLine() != "No matching devices found.")
                        {
                            if (!id_hardware.Contains(output))
                            {
                                id_hardware.Add(output);
                                listBox2.Items.Add(name);
                                name_hardware.Add(name);
                            }
                        }
                        outline.Close();
                    }
                }
                Wait.Instance.updateProgressBar(100);
                Wait.Instance.Close();
            } else {
                save_button.Text = "Edytuj profil";
                this.Text = "Edytuj profil";
                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory+"\\"+Main.Instance.profiles[Main.Instance.chid]+".json"));
                ArrayList tmp_id = new ArrayList();
                ArrayList tmp_name = new ArrayList();
                Wait.Instance.updateProgressBar(50);
                for (int i=0; i<=json[0]["Value"].Count -1; i++){
                    tmp_name.Add(json[0]["Value"][i]);
                    tmp_id.Add(json[1]["Value"][i]);
                }

                name_txt.Text = Main.Instance.profiles[Main.Instance.chid].ToString();
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(directory + "\\list.txt", Encoding.GetEncoding("ISO-8859-2"));
                while ((line = file.ReadLine()) != null)
                {
                    Wait.Instance.updateProgressBar(70);
                    string[] temp_line = line.Split(':', ':');
                    if (temp_line.Length != 1)
                    {
                        string output = temp_line[0];
                        string name = temp_line[1];
                        output = Regex.Replace(output, @"\s+", "");
                        int index = output.LastIndexOf("\\");
                        if (index > 0) { output = output.Substring(0, index); }
                        console.sendCmdCommand("/c devcon hwids \"" + output + "\" > out.txt", directory, false);
                        Wait.Instance.updateProgressBar(80);
                        System.IO.StreamReader outline = new System.IO.StreamReader(directory + "\\out.txt");
                        if (outline.ReadLine() != "No matching devices found.") {
                            if (!id_hardware.Contains(output) && !id_active.Contains(output)) {
                                bool isset = false;

                                for (int i = 0; i <= tmp_name.Count - 1; i++)
                                {
                                    if (output == tmp_id[i].ToString())
                                    {
                                        id_active.Add(output);
                                        name_active.Add(name);
                                        listBox1.Items.Add(name);
                                        isset = true;
                                        break;
                                    }
                                }

                                if (!isset)
                                {
                                    id_hardware.Add(output);
                                    name_hardware.Add(name);
                                    listBox2.Items.Add(name);
                                }
                            }
                        }
                    }
                }
                Wait.Instance.updateProgressBar(100);
                Wait.Instance.Close();
            }
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
                } else { Main.Instance.isEdit = false; Main.Instance.Show(); }
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
            else if (File.Exists(directory + "\\" + name_txt.Text + ".json") && !Main.Instance.isEdit) { MessageBox.Show("Profil już istnieje!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else if (id_active.Count == 0) { MessageBox.Show("Brak wyłączonych urządzeń!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else {
                if (!Main.Instance.isEdit) {
                    Dictionary<string, ArrayList> arrays = new Dictionary<string, ArrayList>();
                    arrays.Add("name", name_active); arrays.Add("id", id_active);
                    File.WriteAllText(directory + "\\" + name_txt.Text + ".json", SimpleJson.SerializeObject(arrays));
                    MessageBox.Show("Poprawnie utworzono profil sprzętowy!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Main.Instance.listBox1.Items.Add(name_txt.Text);
                    Main.Instance.profiles.Add(name_txt.Text);
                    isClosed = true;
                    File.WriteAllText(directory + "\\profiles.json", SimpleJson.SerializeObject(Main.Instance.profiles));
                    this.Close();
                } else {
                    Dictionary<string, ArrayList> arrays = new Dictionary<string, ArrayList>();
                    arrays.Add("name", name_active); arrays.Add("id", id_active);
                    File.Delete(directory + "\\" + Main.Instance.profiles[Main.Instance.chid] + ".json");
                    File.WriteAllText(directory + "\\" + name_txt.Text + ".json", SimpleJson.SerializeObject(arrays));
                    MessageBox.Show("Poprawnie edytowano profil sprzętowy!!!", "Profile sprzętowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Main.Instance.listBox1.Items.RemoveAt(Main.Instance.chid);
                    Main.Instance.profiles.RemoveAt(Main.Instance.chid);
                    Main.Instance.listBox1.Items.Insert(Main.Instance.chid, name_txt.Text);
                    Main.Instance.profiles.Insert(Main.Instance.chid, name_txt.Text);
                    Main.Instance.chid = -1;
                    isClosed = true;
                    File.WriteAllText(directory + "\\profiles.json", SimpleJson.SerializeObject(Main.Instance.profiles));
                    Main.Instance.isEdit = false;
                    this.Close();
                }
            }
        }
    }
}