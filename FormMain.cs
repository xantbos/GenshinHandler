/*
 * Why does Genshin Impact have to use a Ring 0 anti-cheat?
 * Especially one they've proven doesn't even work?
 * Better yet, why can't they be fucking arsed to stop it when they don't need it?
 * 
 * Unfortunately, only Mihoyo knows. Thus this was made to handle that goddamn service.
 * 
 * Fire and forget. Put it into your startup, soup, pants, whatever I don't care.
 * I hope they get their act together tho kthx.
 * 
 * -Xant
 * 
 * 2020-09-26
 */


using System;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.ServiceProcess;
using System.Diagnostics;
using System.IO;

namespace GenshinHandler
{
    public partial class FormMain : Form
    {

        private ServiceController sc; // make our controller so everything can access if need be
        private readonly String installPath; // floater installation path to be determined later
        private readonly String gameLauncher = "launcher.exe"; // launcher exe name
        private Thread t; // thread for later tick checks
        private bool silentMode; // flag for silent (no notif) mode
        private bool closeLauncher; // flag to close game launcher when game closes
        private FileManager f = new FileManager(); // file manager class for i/o ops
        private bool firstRun = true; // first run flag to prevent unecessary i/o ops

        public FormMain()
        {

            InitializeComponent();

            // reg crap to find install path of game
            // genshin won't even run on 32 so ezcheck
            RegistryKey localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            installPath = localKey.OpenSubKey(@"SOFTWARE\launcher").GetValue("InstPath").ToString();

            // determine if our i/o system can function on this setup or if read/write is blocked to us
            bool[] vals = f.LoadData();
            if(!vals[2])
            {
                Console.WriteLine("An error occurred writing to APPDATA. Config info will not be persistent.");
            }
            // set our config flags
            silentMode = vals[0];
            closeLauncher = vals[1];

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Visible = false; // ensure main form is hidden
            updateSilent(); // update config menu checkbox
            updateClose(); // update config menu checkbox
            t = new Thread(new ThreadStart(handler)); // handler will be our thread func
            t.Start(); // being threading
            firstRun = false; // first run is done, app runs normally now
        }

        private void quitApp()
        {
            t.Abort(); // kill our thread
            IconMain.Dispose(); // kill our tray icon
            Environment.Exit(0); // graceful termination with 0=>success
        }

        private void updateSilent()
        {
            SilentItem.Checked = silentMode; // set checked
            if (!firstRun && !f.failFlag) { f.WriteData(silentMode, closeLauncher); } // write if not first run and have read/write
        }

        private void updateClose()
        {
            CloseItem.Checked = closeLauncher; // set checked
            if (!firstRun && !f.failFlag) { f.WriteData(silentMode, closeLauncher); } // write if not first run and have read/write
        }

        private void LaunchItem_Click(object sender, EventArgs e)
        {
            Process.Start($"{installPath}\\{gameLauncher}"); // run launcher executable found in game install path
        }

        private void SilentItem_Click(object sender, EventArgs e)
        {
            silentMode = !silentMode; // inverse bool on click
            updateSilent(); // update func
        }

        private void CloseItem_Click(object sender, EventArgs e)
        {
            closeLauncher = !closeLauncher; // inverse bool on click
            updateClose(); // update func
        }

        private void RedditItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.reddit.com/r/Genshin_Impact/"); // launch native app for html link
        }

        private void WikiItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://genshin-impact.fandom.com/wiki/Genshin_Impact_Wiki"); // launch native app for html link
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            quitApp(); // call terminate func
        }

        private int checkRun()
        {
            sc = new ServiceController("mhyprot2"); // get handle for system service
            try // we use try in case the sys is not running atm
            {
                sc.WaitForStatus(ServiceControllerStatus.Running); // wait until it's running (in case state is STARTING)
                if (!silentMode) { IconMain.ShowBalloonTip(3000, "Genshin Impact Launched", "I'll just chill until it closes...", ToolTipIcon.Info); } // toast
                return 1; // return state for next check to switch
            }
            catch (System.InvalidOperationException) // sole error case
            {
                Console.WriteLine("Sys not running"); // no need to inform the user tbh
            }

            return 0; // return current state for switch
        }

        private int checkClose()
        {
            //check the exe every tick, if it is not running then murder the fucking sys
            Process[] pname = Process.GetProcessesByName("GenshinImpact"); // get processes that match given name
            if (pname.Length == 0) // if we have no results
            {
                //u ded now bucko, game is closed and your usefulness is at an end
                //knife.png
                sc.Stop(); // stop the service
                sc.WaitForStatus(ServiceControllerStatus.Stopped); // ensure the damn thing is dead
                if (!silentMode) { IconMain.ShowBalloonTip(3000, "Service Stopped", "mhyprot2.sys has been stopped.", ToolTipIcon.Info); } // toast
                if (closeLauncher) // if the close flag is up
                {
                    Process[] items = Process.GetProcessesByName("launcher"); // get processes that match given name
                    foreach (Process proc in items) // launcher is kinda common so we're going to double check before murdering
                    {
                        if (proc.MainModule.FileName.Contains(installPath)) // if the exe exists in the installation path of the game
                        {
                            proc.Kill(); // safe to say we got the right man. Get spaced - 0 processes remain.
                        }
                    }
                }
                return 0; // return state for next check to switch
            }

            return 1; // return current state for switch
        }

        private void handler()
        {
            try // as this is a permanently looping thread, we should check for interrupts from above
            {
                //cycle between close and run
                int state = 0; // 0 is check, 1 is close
                while (true) // permaloop
                {
                    Thread.Sleep(2000); // 2 second sleep so we don't burn out toasters, can be changed w/e
                    switch (state)
                    {
                        case 0: // state 0
                            state = checkRun(); // run func
                            break;
                        case 1:
                            state = checkClose(); // close func
                            break;
                    }
                }
            }
            catch (ThreadAbortException e) // in the scenario the thread is aborted by either critical error or otherwise
            {
                Console.WriteLine("Thread Aborted.");
            }
            finally // if anything needs to be done on thread closing, do it here
            {
                Console.WriteLine("Thread exited.");
            }

        }
    }

    public class FileManager // purely for i/o purposes
    {

        readonly static String workingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // get appdata
        readonly static String dataPath = $"{workingPath}\\GenshinHandler"; // insert snowflake folder name
        readonly static String configFile = $"{dataPath}\\config.conf"; // config filename
        public bool failFlag = false; // in case we cannot read/write, public so anything calling can quick check
        public FileManager()
        {
            // do nothing on init
        }

        private bool EnsureDataFolder()
        {
            try
            {
                if (!Directory.Exists(dataPath)) // if our snowflake folder doesn't exist yet
                {
                    Directory.CreateDirectory(dataPath); // make it
                }
                return true; // all is well
            }
            catch
            {
                Console.WriteLine("Error writing directory."); // well screw you too man
                failFlag = true; // no read/write allowed
                return false; // or fun apparently
            }
        }

        public bool[] LoadData()
        {
            if (EnsureDataFolder()) // it's a handy dandy bool to save us time here
            {
                bool silent = false; // default for silent flag
                bool close = false; // default for close flag
                if(File.Exists(configFile)) // check if config exists
                {
                    // file exists dawg, read values
                    string[] lines = File.ReadAllLines(configFile);
                    if(lines[0].ToLower() == "true") { silent = true; } // since default is false we only care if state change
                    if(lines[1].ToLower() == "true") { close = true; } // see above
                    bool[] retvals = { silent, close, true }; // create return bool array, final value is success or fail
                    return retvals;
                }    
                else
                {
                    if(WriteData(silent, close)) // config doesn't exists so we try to write with default values
                    {
                        bool[] retvals = { silent, close, true }; // we gucci
                        return retvals;
                    }
                    else
                    {
                        bool[] retvals = { false, false, false }; // no gucci, NO GUCCI
                        failFlag = true; // failflag to true so we know there was a bungle
                        return retvals;
                    }
                }
            }
            else
            {
                bool[] retvals = { false, false, false }; // well dang man, can't even make an appdata folder
                failFlag = true; // just end me
                return retvals;
            }
        }

        public bool WriteData(bool silent, bool close)
        {
            try
            {
                string[] lines = { Convert.ToString(silent).ToLower(), Convert.ToString(close).ToLower() }; // turn our flag bools to string
                File.WriteAllLines(configFile, lines); // write to conf
                return true; // all went fine
            }
            catch
            {
                failFlag = true; // are you kidding me
                return false; // did we come here just to suffer?
            }
        }
    }
}
