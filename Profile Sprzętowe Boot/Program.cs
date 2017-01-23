using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Profile_Sprzętowe_Boot
{
    class Program
    {

        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        static string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HM";
        static ArrayList profiles = new ArrayList();

        public static string showProfiles()
        {
            Console.WriteLine("Lista profili: ");
            Console.WriteLine("[0] Uruchom system bez wyboru profilu");
            if (File.Exists(directory + "\\profiles.json"))
            {
                int active = -1;
                if(File.Exists(directory + "\\active.txt")) { active = Convert.ToInt32(File.ReadAllText(directory + "\\active.txt")); }
                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory + "\\profiles.json"));
                for (int i = 0; i <= json.Count - 1; i++) { profiles.Add(json[i]); }
                for (int i = 0; i <= profiles.Count - 1; i++) { if (i == active) { Console.WriteLine("[" + (i + 1) + "] " + profiles[i]+"(Aktywny)"); } else { Console.WriteLine("[" + (i + 1) + "] " + profiles[i]); } }

            }
            else
            {
                Console.WriteLine("Nie posiadasz żadnych profili!!!");
            }

            Console.Write("Wybierz jeden z profili: ");           
            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            ShowWindow(ThisConsole, 3);

            Console.WriteLine("Cieszymy się, że chcesz korzystać z eksperymentalnej funkcji naszej aplikacji");
            Console.WriteLine("Tryb ten jest w fazie testowej i nie odpowiadamy za wszelkie szkody przez niego wyrządzone");
            Console.WriteLine("Życzymy miłego korzystania z aplkacji");
            Console.WriteLine("");
            
            string action=null;
            int selected;
            int active = -1;
            if (File.Exists(directory + "\\active.txt")) { active = Convert.ToInt32(File.ReadAllText(directory + "\\active.txt")); }

            while (true)
            {
                try
                {
                    selected = Convert.ToInt32(showProfiles());
                    if (selected == 0) {
                        action = "e";
                        System.Diagnostics.Process process_changes = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo_changes = new System.Diagnostics.ProcessStartInfo();
                        startInfo_changes.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo_changes.WorkingDirectory = directory;
                        startInfo_changes.FileName = "cmd.exe";
                        startInfo_changes.Arguments = "/c start explorer.exe";
                        process_changes.StartInfo = startInfo_changes;
                        process_changes.Start();
                        process_changes.WaitForExit();
                        break;
                    }
                    else if (selected > profiles.Count)
                    {
                        profiles.Clear();
                        Console.WriteLine("Nie istnieje taki profil!! Spróbuj ponownie");
                        Console.WriteLine("");
                    }
                    else if ((selected - 1) == active)
                    {
                        profiles.Clear();
                        Console.WriteLine("Wybrany profil jest aktywny. Kontynuować T/N");
                        string output = Console.ReadLine().ToLower();
                        if (output == "t")
                        {
                            action = "e";
                            System.Diagnostics.Process process_changes = new System.Diagnostics.Process();
                            System.Diagnostics.ProcessStartInfo startInfo_changes = new System.Diagnostics.ProcessStartInfo();
                            startInfo_changes.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                            startInfo_changes.WorkingDirectory = directory;
                            startInfo_changes.FileName = "cmd.exe";
                            startInfo_changes.Arguments = "/c start explorer.exe";
                            process_changes.StartInfo = startInfo_changes;
                            process_changes.Start();
                            process_changes.WaitForExit();
                            break;
                        }
                        else { Console.WriteLine(""); }
                    } else { break; }
                }
                catch (Exception e)
                {
                    profiles.Clear();
                    Console.WriteLine("Musisz podać liczbę!! Spróbuj ponownie");
                    Console.WriteLine("");
                }
            }

            if (action != "e")
            {
                if (File.Exists(directory + "\\changes.json"))
                {
                    dynamic json_changes = SimpleJson.DeserializeObject(File.ReadAllText(directory + "\\changes.json"));
                    string args_changes = "/c devcon enable \"" + json_changes[0] + "\"";
                    for (int i = 1; i <= json_changes.Count - 1; i++)
                    {
                        args_changes += " & devcon enable \"" + json_changes[i] + "\"";
                    }

                    for (int i = 0; i <= 1; i++)
                    {
                        System.Diagnostics.Process process_changes = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo_changes = new System.Diagnostics.ProcessStartInfo();
                        startInfo_changes.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo_changes.WorkingDirectory = directory;
                        startInfo_changes.FileName = "cmd.exe";
                        startInfo_changes.Arguments = args_changes;
                        process_changes.StartInfo = startInfo_changes;
                        process_changes.Start();
                        process_changes.WaitForExit();
                    }
                    File.Delete(directory + "\\changes.json");
                }

                dynamic json = SimpleJson.DeserializeObject(File.ReadAllText(directory + "\\" + profiles[selected - 1] + ".json"));
                ArrayList changes = new ArrayList();
                string args_console = "/c devcon disable \"" + json[1]["Value"][0] + "\"";
                changes.Add(json[1]["Value"][0]);
                for (int i = 1; i <= json[1]["Value"].Count - 1; i++)
                {
                    changes.Add(json[1]["Value"][i]);
                    args_console += " & devcon disable \"" + json[1]["Value"][i] + "\"";
                }

                Console.WriteLine("Poprawnie zastsosowano profil. Za chwilę zostanie uruchomiony system");
                args_console += " & start explorer.exe";

                File.WriteAllText(directory + "\\active.txt", Convert.ToString(selected - 1));

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.WorkingDirectory = directory;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = args_console;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                File.WriteAllText(directory + "\\changes.json", SimpleJson.SerializeObject(changes));
            }
        }
    }
}