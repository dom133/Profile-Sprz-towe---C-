namespace Profile_Sprzętowe.Class
{
    class Console
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

        public bool sendCmdCommand(string command, string directory, bool iscmdVisible)
        {
            if (!iscmdVisible) { startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; }
            else { startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal; }
            startInfo.WorkingDirectory = directory;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = command;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            return true;
        }
    }
}