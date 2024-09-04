using System.Diagnostics;
using System.Runtime.InteropServices;
using Memory;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        public Mem memory = new Mem();
        Process p;
        public string programDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public string mccProcessSteam = "MCC-Win64-Shipping";
        public string mccProcessWinstore = "MCCWinStore-Win64-Shipping";
        private string selectedProcessName;

        public string xPos; // Scan for float; Camera 'X'
        public string yPos; // +4
        public string zPos; // +8
        public string yawAng; // +12
        public string pitchAng; // +16
        public string rollAng; // +20

        public string playerFov; // Scan for float;

        public string theaterTime; // Scan for float; Convert theater time into seconds (ex: 25:07 is 1507)
        /*7FF47EFC0018, 7FF47EFAC528, 7FF47EB30018, 7FF47EB1C528 */

        public string locationTargetBase; // Scan for string; 'havok proxies'
        public string targetBaseOffset; // Base offset from pointer
        public string offsetMagic; // Offset to the first object 'X' in the list
        public string difference; // Offset to next item in list

        public string Header;
        public string BuildTag;
        public string LoadedMap;
        public string gameFramerate;

        public bool modulesUpdated = false;
        public bool isCheckingTime = false;

        private Thread cameraThread;
        private Thread checkTheaterTime;
        private Thread headerCheck;

        bool isHeaderLoaded = false;
        bool lockHotkeys = true;
        bool startup = false;

        private const int VK_I = 0x49;
        private const int VK_K = 0x4B;
        private const int VK_M = 0x4D;
        private const int VK_N = 0x4E;
        private const int VK_O = 0x4F;
        private const int VK_P = 0x50;
        private const int VK_U = 0x55;
        private const int VK_Home = 0x24;

        public bool messageShown = false;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        public MainForm()
        {
            InitializeComponent();
            GetModules();
            GetData();
            SetupDataGridView();

            _proc = HookCallback;
            _hookID = SetHook(_proc);

            string subfolderNamePlugins = "plugins";
            string subfolderPathPlugins = Path.Combine(programDirectory, subfolderNamePlugins);

            string subfolderNamePaths = "paths";
            string subfolderPathPaths = Path.Combine(programDirectory, subfolderNamePaths);

            DirectoryInfo d = new DirectoryInfo(@"plugins/");
            DirectoryInfo b = new DirectoryInfo(@"paths/");

            if (Directory.Exists(subfolderPathPlugins))
            {
                FileInfo[] Files = d.GetFiles("*.json");
                string[] jsonFiles = Directory.GetFiles(subfolderPathPlugins, "*.json");

                foreach (var file in Files)
                {
                    string fileName = Path.GetFileName(file.ToString());
                }
            }
            else
            {
                Console.WriteLine("The 'plugins' folder does not exist. Creating one now...");
                Directory.CreateDirectory(subfolderPathPlugins);
            }

            if (Directory.Exists(subfolderPathPaths))
            {
                FileInfo[] Files = b.GetFiles("*.json");

                string[] jsonFiles = Directory.GetFiles(subfolderPathPaths, "*.json");

                foreach (var file in Files)
                {
                    string fileName = Path.GetFileName(file.ToString());
                }
            }
            else
            {
                Console.WriteLine("The 'paths' folder does not exist. Creating one now...");
                Directory.CreateDirectory(subfolderPathPaths);
            }

            teleportToSelection_Button.Click += teleportToSelection_Button_Click;
            replaceCurrent_Button.Click += replaceCurrent_Button_Click;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            UnhookWindowsHookEx(_hookID);
        }

        public async Task CheckForMapHeader()
        {
            while (true)
            {
                string daeh = memory.ReadString(Header, "", 0x04);

                if (daeh == "daeh")
                {
                    isHeaderLoaded = true;
                }
                else
                {
                    isHeaderLoaded = false;
                }

                await Task.Delay(1);
            }
        }

        #region Hotkeys

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (lockHotkeys == false)
                {
                    switch (vkCode)
                    {
                        case VK_M:
                            pathStart_checkbox.Checked = true;
                            break;
                        case VK_N:
                            pathStart_checkbox.Checked = false;
                            break;
                        case VK_P:
                            rotateCameraClockwise();
                            break;
                        case VK_O:
                            rotateCameraCounterclockwise();
                            break;
                        case VK_I:
                            zoomCamera();
                            break;
                        case VK_U:
                            unzoomCamera();
                            break;
                        case VK_K:
                            addKey();
                            break;
                        case VK_Home:
                            if (keyframeDataGridView.RowCount > 0)
                            {
                                if (isHeaderLoaded == true)
                                {
                                    int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;
                                    float Time = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value);
                                    TimeSpan timeSpan = TimeSpan.FromSeconds(Time);
                                    string formattedTime = timeSpan.ToString(@"hh\:mm\:ss");

                                    string map = memory.ReadString(LoadedMap, "", 50);
                                    string build = memory.ReadString(BuildTag, "", 25);

                                    byte[] framerate = memory.ReadBytes(gameFramerate, 4);
                                    int framerateValue = BitConverter.ToInt32(framerate, 0);
                                    string framerateString = framerateValue.ToString();

                                    if (framerateString == "0")
                                    {
                                        framerateString = "Unlimited";
                                    }

                                    if (useTheaterTime.Checked == true)
                                    {
                                        MessageBox.Show($"Game stats:\n" +
                                            $"********************************************************************************\n" +
                                            $"Build: {build}\n" +
                                            $"Current Map: {map}\n" +
                                            $"Game Framerate: {framerateString}\n" +
                                            $"\nKeyframe stats:\n" +
                                            $"********************************************************************************\n" +
                                            $"Selected Keyframe: {selectedIndex + 1}\n" +
                                            $"Time: {formattedTime}\n" +
                                            $"Time (in seconds): {Time}\n" +
                                            $"\n Plugin details:\n" +
                                            $"********************************************************************************\n" +
                                            $"X: {xPos}\n" +
                                            $"Y: {yPos}\n" +
                                            $"Z: {zPos}\n" +
                                            $"Yaw: {yawAng}\n" +
                                            $"Pitch: {pitchAng}\n" +
                                            $"Roll: {rollAng}\n" +
                                            $"Fov: {playerFov}\n" +
                                            $"Theater time: {theaterTime}\n",
                                            "Debug", MessageBoxButtons.OK);
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Game stats:\n" +
                                            $"********************************************************************************\n" +
                                            $"Build: {build}\n" +
                                            $"Current Map: {map}\n" +
                                            $"Game Framerate: {framerateString}\n" +
                                            $"\nKeyframe stats:\n" +
                                            $"********************************************************************************\n" +
                                            $"Selected Keyframe: {selectedIndex + 1}\n" +
                                            $"Time (in seconds): {Time}\n" +
                                            $"\n Plugin details:\n" +
                                            $"********************************************************************************\n" +
                                            $"X: {xPos}\n" +
                                            $"Y: {yPos}\n" +
                                            $"Z: {zPos}\n" +
                                            $"Yaw: {yawAng}\n" +
                                            $"Pitch: {pitchAng}\n" +
                                            $"Roll: {rollAng}\n" +
                                            $"Fov: {playerFov}\n" +
                                            $"Theater time: {theaterTime}\n",
                                            "Debug", MessageBoxButtons.OK);
                                    }
                                }
                            }
                            else
                            {
                                string map = memory.ReadString(LoadedMap, "", 50);
                                string build = memory.ReadString(BuildTag, "", 25);

                                byte[] framerate = memory.ReadBytes(gameFramerate, 4);
                                int framerateValue = BitConverter.ToInt32(framerate, 0);
                                string framerateString = framerateValue.ToString();

                                MessageBox.Show($"Game stats:\n" +
                                            $"********************************************************************************\n" +
                                            $"Build: {build}\n" +
                                            $"Current Map: {map}\n" +
                                            $"Game Framerate: {framerateString}\n" +
                                            $"\n Plugin details:\n" +
                                            $"********************************************************************************\n" +
                                            $"X: {xPos}\n" +
                                            $"Y: {yPos}\n" +
                                            $"Z: {zPos}\n" +
                                            $"Yaw: {yawAng}\n" +
                                            $"Pitch: {pitchAng}\n" +
                                            $"Roll: {rollAng}\n" +
                                            $"Fov: {playerFov}\n" +
                                            $"Theater time: {theaterTime}\n",
                                            "Debug", MessageBoxButtons.OK);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        #endregion

        #region Memory

        public async Task GetModules()
        {
            try
            {
                Process[] processes = Process.GetProcesses();

                foreach (Process process in processes)
                {
                    if (process.ProcessName.Equals(mccProcessSteam, StringComparison.OrdinalIgnoreCase))
                    {
                        selectedProcessName = mccProcessSteam;
                        break;
                    }
                    else if (process.ProcessName.Equals(mccProcessWinstore, StringComparison.OrdinalIgnoreCase))
                    {
                        selectedProcessName = mccProcessWinstore;
                        break;
                    }
                }

                p = Process.GetProcessesByName(selectedProcessName)[0];
                memory.OpenProcess(p.Id);

                if (memory == null) return;
                if (memory.theProc == null) return;

                if (startup == false)
                {
                    if (selectedProcessName == null)
                    {
                        Console.WriteLine("Process not found! (Is it running?)");
                        startup = false;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Found: " + selectedProcessName.ToString() + " (" + p.Id + ")");

                        updateModules.Text = $"Load .plugin/.json";

                        startup = true;
                    }
                }

                memory.theProc.Refresh();
                memory.modules.Clear();

                foreach (ProcessModule Module in memory.theProc.Modules)
                {
                    if (!string.IsNullOrEmpty(Module.ModuleName) && !memory.modules.ContainsKey(Module.ModuleName))
                        memory.modules.Add(Module.ModuleName, Module.BaseAddress);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The MCC process was not found... Please open MCC and try again.");
            }
        }

        public async Task CheckForHeader()
        {
            try
            {
                while (true)
                {
                    string headerState = memory.ReadString(Header, "", 4);

                    if (headerState == "daeh")
                    {
                        string reachBuild = memory.ReadString(BuildTag, "", 24);

                        if (reachBuild != null)
                        {
                            //buildLabel.Text = "Build: (" + reachBuild + ")";
                        }

                        string reachMap = memory.ReadString(LoadedMap, "", 60);

                        if (reachMap != null)
                        {
                            //mapTextbox.Text = reachMap;
                        }

                        isHeaderLoaded = true;
                    }
                    if (headerState != "daeh")
                    {
                        isHeaderLoaded = false;
                    }
                }
            }
            catch
            {
                await CheckForHeader();
            }
        }

        public async Task GetData()
        {
            while (true)
            {
                if (isHeaderLoaded == false)
                {
                    return;
                }

                await Task.Delay(1);
            }
        }

        #endregion

        #region Camera

        private int CalculateOffsetX(string selectedItem, int targetBaseOffset, int offsetMagic, int difference)
        {
            switch (selectedItem)
            {
                case "Item 1":
                    return targetBaseOffset + offsetMagic;
                case "Item 2":
                    return targetBaseOffset + offsetMagic + difference;
                case "Item 3":
                    return targetBaseOffset + offsetMagic + difference * 2;
                case "Item 4":
                    return targetBaseOffset + offsetMagic + difference * 4;
                case "Item 5":
                    return targetBaseOffset + offsetMagic + difference * 6;
                case "Item 6":
                    return targetBaseOffset + offsetMagic + difference * 8;
                case "Item 7":
                    return targetBaseOffset + offsetMagic + difference * 10;
                case "Item 8":
                    return targetBaseOffset + offsetMagic + difference * 12;
                case "Item 9":
                    return targetBaseOffset + offsetMagic + difference * 14;
                case "Item 10":
                    return targetBaseOffset + offsetMagic + difference * 16;
                case "Item 11":
                    return targetBaseOffset + offsetMagic + difference * 18;
                case "Item 12":
                    return targetBaseOffset + offsetMagic + difference * 20;
                case "Item 13":
                    return targetBaseOffset + offsetMagic + difference * 22;
                case "Item 14":
                    return targetBaseOffset + offsetMagic + difference * 24;
                case "(havok proxies)":
                    targetTracking.Checked = false;
                    return targetBaseOffset = 0;
                default:
                    return targetBaseOffset = 0;
            }
        }

        static Tuple<float, float, float, float, float, float, float> CatmullRomPositionInterpolation(Tuple<float, float, float, float, float, float, float> p0, Tuple<float, float, float, float, float, float, float> p1, Tuple<float, float, float, float, float, float, float> p2, Tuple<float, float, float, float, float, float, float> p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;
            float[] coefficients = new float[4] { -0.5f * t3 + t2 - 0.5f * t, 1.5f * t3 - 2.5f * t2 + 1.0f, -1.5f * t3 + 2.0f * t2 + 0.5f * t, 0.5f * t3 - 0.5f * t2 };

            float x = coefficients[0] * p0.Item1 + coefficients[1] * p1.Item1 + coefficients[2] * p2.Item1 + coefficients[3] * p3.Item1;
            float y = coefficients[0] * p0.Item2 + coefficients[1] * p1.Item2 + coefficients[2] * p2.Item2 + coefficients[3] * p3.Item2;
            float z = coefficients[0] * p0.Item3 + coefficients[1] * p1.Item3 + coefficients[2] * p2.Item3 + coefficients[3] * p3.Item3;
            float yaw = coefficients[0] * p0.Item4 + coefficients[1] * p1.Item4 + coefficients[2] * p2.Item4 + coefficients[3] * p3.Item4;
            float pitch = coefficients[0] * p0.Item5 + coefficients[1] * p1.Item5 + coefficients[2] * p2.Item5 + coefficients[3] * p3.Item5;
            float roll = coefficients[0] * p0.Item6 + coefficients[1] * p1.Item6 + coefficients[2] * p2.Item6 + coefficients[3] * p3.Item6;
            float fov = coefficients[0] * p0.Item7 + coefficients[1] * p1.Item7 + coefficients[2] * p2.Item7 + coefficients[3] * p3.Item7;

            return new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, fov);
        }

        private Tuple<float, float, float, float, float, float, float> Interpolation(Tuple<float, float, float, float, float, float, float> p0, Tuple<float, float, float, float, float, float, float> p1, Tuple<float, float, float, float, float, float, float> p2, Tuple<float, float, float, float, float, float, float> p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;

            float x = 0.5f * ((2.0f * p1.Item1) +
                              (-p0.Item1 + p2.Item1) * t +
                              (2.0f * p0.Item1 - 5.0f * p1.Item1 + 4.0f * p2.Item1 - p3.Item1) * t2 +
                              (-p0.Item1 + 3.0f * p1.Item1 - 3.0f * p2.Item1 + p3.Item1) * t3);

            float y = 0.5f * ((2.0f * p1.Item2) +
                              (-p0.Item2 + p2.Item2) * t +
                              (2.0f * p0.Item2 - 5.0f * p1.Item2 + 4.0f * p2.Item2 - p3.Item2) * t2 +
                              (-p0.Item2 + 3.0f * p1.Item2 - 3.0f * p2.Item2 + p3.Item2) * t3);

            float z = 0.5f * ((2.0f * p1.Item3) +
                              (-p0.Item3 + p2.Item3) * t +
                              (2.0f * p0.Item3 - 5.0f * p1.Item3 + 4.0f * p2.Item3 - p3.Item3) * t2 +
                              (-p0.Item3 + 3.0f * p1.Item3 - 3.0f * p2.Item3 + p3.Item3) * t3);

            float yaw = 0.5f * ((2.0f * p1.Item4) +
                                (-p0.Item4 + p2.Item4) * t +
                                (2.0f * p0.Item4 - 5.0f * p1.Item4 + 4.0f * p2.Item4 - p3.Item4) * t2 +
                                (-p0.Item4 + 3.0f * p1.Item4 - 3.0f * p2.Item4 + p3.Item4) * t3);

            float pitch = 0.5f * ((2.0f * p1.Item5) +
                                  (-p0.Item5 + p2.Item5) * t +
                                  (2.0f * p0.Item5 - 5.0f * p1.Item5 + 4.0f * p2.Item5 - p3.Item5) * t2 +
                                  (-p0.Item5 + 3.0f * p1.Item5 - 3.0f * p2.Item5 + p3.Item5) * t3);

            float roll = 0.5f * ((2.0f * p1.Item6) +
                                 (-p0.Item6 + p2.Item6) * t +
                                 (2.0f * p0.Item6 - 5.0f * p1.Item6 + 4.0f * p2.Item6 - p3.Item6) * t2 +
                                 (-p0.Item6 + 3.0f * p1.Item6 - 3.0f * p2.Item6 + p3.Item6) * t3);

            float fov = 0.5f * ((2.0f * p1.Item7) +
                                (-p0.Item7 + p2.Item7) * t +
                                (2.0f * p0.Item7 - 5.0f * p1.Item7 + 4.0f * p2.Item7 - p3.Item7) * t2 +
                                (-p0.Item7 + 3.0f * p1.Item7 - 3.0f * p2.Item7 + p3.Item7) * t3);

            return new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, fov);
        }

        public async Task MoveCamera()
        {
            try
            {
                int originalSelectedRow = keyframeDataGridView.CurrentCell.RowIndex;
                int originalSelectedColumn = keyframeDataGridView.CurrentCell.ColumnIndex;

                if (useTheaterTime.Checked == true)
                {
                    List<Tuple<float, float, float, float, float, float, float>> keyPoints = new List<Tuple<float, float, float, float, float, float, float>>();
                    List<float> keyTimes = new List<float>();

                    for (int i = 0; i < keyframeDataGridView.Rows.Count; i++)
                    {
                        float x = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["X"].Value);
                        float y = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Y"].Value);
                        float z = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Z"].Value);
                        float yaw = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Yaw"].Value);
                        float pitch = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Pitch"].Value);
                        float roll = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Roll"].Value);
                        float fov = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["FOV"].Value);
                        float keyTime = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Transition Time"].Value);

                        keyPoints.Add(new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, fov));
                        keyTimes.Add(keyTime);
                    }

                    for (int i = 0; i < keyPoints.Count - 1; i++)
                    {
                        keyframeDataGridView.ClearSelection();
                        keyframeDataGridView.Rows[i].Selected = true;

                        var p0 = (i == 0) ? keyPoints[0] : keyPoints[i - 1];
                        var p1 = keyPoints[i];
                        var p2 = keyPoints[i + 1];
                        var p3 = (i + 2 < keyPoints.Count) ? keyPoints[i + 2] : p2;

                        float startTime = keyTimes[i];
                        float endTime = keyTimes[i + 1];
                        float segmentDuration = endTime - startTime;

                        float tNormalizedStart = 0.0f;
                        float tNormalizedEnd = 1.0f;
                        float tIncrement = 1.0f / segmentDuration;

                        while (true)
                        {
                            if (isHeaderLoaded == false)
                            {
                                break;
                            }
                            else
                            {
                                float currentTheaterTime = memory.ReadFloat(theaterTime);

                                if (currentTheaterTime >= endTime)
                                {
                                    pathStart_checkbox.Checked = false;
                                    break;
                                }

                                if (currentTheaterTime < startTime)
                                {
                                    var firstPoint = keyPoints[0];
                                    memory.WriteMemory(xPos, "float", $"{firstPoint.Item1}");
                                    memory.WriteMemory(yPos, "float", $"{firstPoint.Item2}");
                                    memory.WriteMemory(zPos, "float", $"{firstPoint.Item3}");
                                    memory.WriteMemory(yawAng, "float", $"{firstPoint.Item4}");
                                    memory.WriteMemory(pitchAng, "float", $"{firstPoint.Item5}");
                                    memory.WriteMemory(rollAng, "float", $"{firstPoint.Item6}");
                                    memory.WriteMemory(playerFov, "float", $"{firstPoint.Item7}");
                                }
                                else
                                {
                                    float tNormalized = (currentTheaterTime - startTime) / segmentDuration;

                                    var interpolatedPosition = Interpolation(p0, p1, p2, p3, tNormalized);

                                    memory.WriteMemory(xPos, "float", $"{interpolatedPosition.Item1}");
                                    memory.WriteMemory(yPos, "float", $"{interpolatedPosition.Item2}");
                                    memory.WriteMemory(zPos, "float", $"{interpolatedPosition.Item3}");
                                    memory.WriteMemory(yawAng, "float", $"{interpolatedPosition.Item4}");
                                    memory.WriteMemory(pitchAng, "float", $"{interpolatedPosition.Item5}");
                                    memory.WriteMemory(rollAng, "float", $"{interpolatedPosition.Item6}");
                                    memory.WriteMemory(playerFov, "float", $"{interpolatedPosition.Item7}");
                                }

                                tNormalizedStart += tIncrement;
                                tNormalizedEnd += tIncrement;

                                await Task.Delay(1);
                            }
                        }
                    }
                }
                else
                {
                    float fps = Convert.ToSingle(fpsTextbox.Text);

                    List<Tuple<float, float, float, float, float, float, float>> keyPoints = new List<Tuple<float, float, float, float, float, float, float>>();
                    List<float> transitionTimes = new List<float>();

                    int startIndex;
                    if (startFromSelection_checkbox.Checked && keyframeDataGridView.SelectedRows.Count > 0)
                    {
                        startIndex = keyframeDataGridView.SelectedRows[0].Index;
                    }
                    else
                    {
                        startIndex = 0;
                    }

                    for (int i = startIndex; i < keyframeDataGridView.Rows.Count; i++)
                    {
                        float x = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["X"].Value);
                        float y = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Y"].Value);
                        float z = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Z"].Value);
                        float yaw = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Yaw"].Value);
                        float pitch = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Pitch"].Value);
                        float roll = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Roll"].Value);
                        float playerFov = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["FOV"].Value);
                        float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[i].Cells["Transition Time"].Value);

                        keyPoints.Add(new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, playerFov));
                        transitionTimes.Add(transitionTime);
                    }

                    int totalFrames = 0;
                    foreach (var time in transitionTimes)
                    {
                        totalFrames += (int)(time * fps);
                    }

                    int currentFrame = 0;
                    for (int i = 0; i < keyPoints.Count - 1; i++)
                    {
                        var p0 = (i == 0) ? keyPoints[0] : keyPoints[i - 1];
                        var p1 = keyPoints[i];
                        var p2 = keyPoints[i + 1];
                        var p3 = (i + 2 < keyPoints.Count) ? keyPoints[i + 2] : p2;

                        float segmentTime = transitionTimes[i];
                        int frames = (int)(segmentTime * fps);

                        for (int frame = 0; frame <= frames; frame++)
                        {
                            if (pathStart_checkbox.Checked == false) return;

                            float tNormalized = (float)frame / frames;
                            float t = tNormalized;
                            var interpolatedPosition = CatmullRomPositionInterpolation(p0, p1, p2, p3, t);

                            memory.WriteMemory(xPos, "float", $"{interpolatedPosition.Item1}");
                            memory.WriteMemory(yPos, "float", $"{interpolatedPosition.Item2}");
                            memory.WriteMemory(zPos, "float", $"{interpolatedPosition.Item3}");

                            memory.WriteMemory(yawAng, "float", $"{interpolatedPosition.Item4}");
                            memory.WriteMemory(pitchAng, "float", $"{interpolatedPosition.Item5}");
                            memory.WriteMemory(rollAng, "float", $"{interpolatedPosition.Item6}");
                            memory.WriteMemory(playerFov, "float", $"{interpolatedPosition.Item7}");

                            await Task.Delay((int)(1000 / fps));
                            currentFrame++;
                        }
                    }
                    pathStart_checkbox.Checked = false;
                }

                keyframeDataGridView.Rows[originalSelectedRow].Cells[originalSelectedColumn].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.ToString(), MessageBoxButtons.OK);
                pathStart_checkbox.Checked = false;
            }
        }


        private void SetupDataGridView()
        {
            keyframeDataGridView.ColumnCount = 8;

            keyframeDataGridView.Columns[0].Name = "X";
            keyframeDataGridView.Columns[1].Name = "Y";
            keyframeDataGridView.Columns[2].Name = "Z";
            keyframeDataGridView.Columns[3].Name = "Yaw";
            keyframeDataGridView.Columns[4].Name = "Pitch";
            keyframeDataGridView.Columns[5].Name = "Roll";
            keyframeDataGridView.Columns[6].Name = "FOV";
            keyframeDataGridView.Columns[7].Name = "Transition Time";

            keyframeDataGridView.AllowUserToAddRows = false;
            keyframeDataGridView.AllowUserToDeleteRows = false;
            keyframeDataGridView.AllowUserToOrderColumns = false;
            keyframeDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            keyframeDataGridView.MultiSelect = false;

            //keyframeDataGridView.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            //keyframeDataGridView.Dock = DockStyle.Fill;
        }

        private void AddKeyPointRow(float x, float y, float z, float yaw, float pitch, float roll, float fov, float transitionTime)
        {
            keyframeDataGridView.Rows.Add(x, y, z, yaw, pitch, roll, fov, transitionTime);
        }

        void addKey()
        {
            byte[] CameraXBytes = memory.ReadBytes(xPos, 4);
            float x = (float)Math.Round(BitConverter.ToSingle(CameraXBytes, 0), 7);
            byte[] CameraYBytes = memory.ReadBytes(yPos, 4);
            float y = (float)Math.Round(BitConverter.ToSingle(CameraYBytes, 0), 7);
            byte[] CameraZBytes = memory.ReadBytes(zPos, 4);
            float z = (float)Math.Round(BitConverter.ToSingle(CameraZBytes, 0), 7);
            byte[] CameraYawBytes = memory.ReadBytes(yawAng, 4);
            float yaw = (float)Math.Round(BitConverter.ToSingle(CameraYawBytes, 0), 7);
            byte[] CameraPitchBytes = memory.ReadBytes(pitchAng, 4);
            float pitch = (float)Math.Round(BitConverter.ToSingle(CameraPitchBytes, 0), 7);
            byte[] CameraRollBytes = memory.ReadBytes(rollAng, 4);
            float roll = (float)Math.Round(BitConverter.ToSingle(CameraRollBytes, 0), 7);

            float fov = memory.ReadFloat(playerFov);

            float transitionTime;

            if (useTheaterTime.Checked == true)
            {
                byte[] timeBytes = memory.ReadBytes(theaterTime, 4);
                float time = (float)Math.Round(BitConverter.ToSingle(timeBytes, 0), 6);
                transitionTime = time;
            }
            else
            {
                transitionTime = 1;
            }


            AddKeyPointRow(x, y, z, yaw, pitch, roll, fov, transitionTime);
        }

        public static byte[] SwapByteOrder(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        void rotateCameraClockwise()
        {
            float current = memory.ReadFloat(rollAng);
            float moveAng = current + 0.1f;
            memory.WriteMemory(rollAng, "float", moveAng.ToString());
        }

        void rotateCameraCounterclockwise()
        {
            float current = memory.ReadFloat(rollAng);
            float moveAng = current - 0.1f;
            memory.WriteMemory(rollAng, "float", moveAng.ToString());
        }

        void zoomCamera()
        {
            float current = memory.ReadFloat(playerFov);
            float moveZoom = current + 1f;

            if (moveZoom >= 145)
            {
                memory.WriteMemory(playerFov, "float", "145.0");
            }
            else
            {
                memory.WriteMemory(playerFov, "float", moveZoom.ToString());
            }
        }

        void unzoomCamera()
        {
            float current = memory.ReadFloat(playerFov);
            float moveZoom = current - 1f;

            if (moveZoom <= 5)
            {
                memory.WriteMemory(playerFov, "float", "5.0");
            }
            else
            {
                memory.WriteMemory(playerFov, "float", moveZoom.ToString());
            }
        }

        void unlockUI()
        {
            groupBox8.Enabled = true;
            groupBox9.Enabled = true;
            saveGroupbox.Enabled = true;
            lockHotkeys = false;
            BackColor = Color.FromArgb(245, 245, 245);
        }

        private void ProcessSelected(string selectedProcessName)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            string programDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string subfolderPath = Path.Combine(programDirectory, "plugins");

            fileDialog.InitialDirectory = subfolderPath;
            fileDialog.Filter = "Plugin and JSON files (*.plugin, *.json)|*.plugin;*.json|Plugin files (*.plugin)|*.plugin|JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string jsonFilePath = fileDialog.FileName;
                string selectedFileName = Path.GetFileName(jsonFilePath);

                if (File.Exists(jsonFilePath))
                {
                    try
                    {
                        string jsonData = File.ReadAllText(jsonFilePath);
                        List<string> addresses = JsonConvert.DeserializeObject<List<string>>(jsonData);

                        if (addresses.Count >= 14)
                        {
                            xPos = addresses[0];
                            yPos = addresses[1];
                            zPos = addresses[2];
                            yawAng = addresses[3];
                            pitchAng = addresses[4];
                            rollAng = addresses[5];
                            playerFov = addresses[6];
                            Header = addresses[7];
                            BuildTag = addresses[8];
                            LoadedMap = addresses[9];
                            locationTargetBase = addresses[10];
                            targetBaseOffset = addresses[11];
                            offsetMagic = addresses[12];
                            difference = addresses[13];
                            theaterTime = addresses[14];
                            gameFramerate = addresses[15];

                            GetModules();

                            Console.WriteLine("Loaded: " + selectedFileName);

                            Process[] processes = Process.GetProcessesByName(selectedProcessName);
                            updateModules.Text = $"{selectedProcessName + " (" + processes[0].Id + ")\n" + selectedFileName} (click again to change plugins)";
                            isHeaderLoaded = true;

                            unlockUI();

                            headerCheck = new Thread(async () => { await CheckForMapHeader(); });
                            headerCheck.Start();
                        }
                        else
                        {
                            MessageBox.Show("The selected JSON file does not contain the expected number of elements.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading JSON file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The selected JSON file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Buttons

        private async void SaveAndSmooth_Button_Click(object sender, EventArgs e)
        {
            SaveMasterFrames();

            //await ApplySmoothOnPath();
        }


        private void importPath_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string json = File.ReadAllText(openFileDialog.FileName);
                    List<KeypointExports> keyPoints = JsonConvert.DeserializeObject<List<KeypointExports>>(json);

                    keyframeDataGridView.Rows.Clear();

                    foreach (KeypointExports keypoint in keyPoints)
                    {
                        AddKeyPointRow(keypoint.xPos, keypoint.yPos, keypoint.zPos, keypoint.yawAng, keypoint.pitchAng, keypoint.rollAng, keypoint.playerFov, keypoint.transitionTime);
                    }

                    MessageBox.Show("Key points imported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error importing key points: " + ex.Message);
                }
            }
        }

        private void exportPath_Button_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveMasterFrames();
                    string json = JsonConvert.SerializeObject(masterFrames.Select(kp => new KeypointExports
                    {
                        xPos = kp.Item1,
                        yPos = kp.Item2,
                        zPos = kp.Item3,
                        yawAng = kp.Item4,
                        pitchAng = kp.Item5,
                        rollAng = kp.Item6,
                        playerFov = kp.Item7,
                        transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[masterFrames.IndexOf(kp)].Cells["Transition Time"].Value)
                    }));
                    File.WriteAllText(saveFileDialog.FileName, json);
                    MessageBox.Show("Key points exported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting key points: " + ex.Message);
                }
            }
        }

        private void deleteKey_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                keyframeDataGridView.Rows.RemoveAt(keyframeDataGridView.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void resetCameraRotation_Button_Click(object sender, EventArgs e)
        {
            memory.WriteMemory(yawAng, "Float", "0");
            memory.WriteMemory(pitchAng, "Float", "0");
            memory.WriteMemory(rollAng, "Float", "0");
        }

        private void teleportToSelection_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                float x = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["X"].Value);
                float y = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Y"].Value);
                float z = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Z"].Value);
                float yaw = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Yaw"].Value);
                float pitch = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Pitch"].Value);
                float roll = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Roll"].Value);
                float fov = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["FOV"].Value);

                memory.WriteMemory(xPos, "float", $"{x}");
                memory.WriteMemory(yPos, "float", $"{y}");
                memory.WriteMemory(zPos, "float", $"{z}");
                memory.WriteMemory(yawAng, "float", $"{yaw}");
                memory.WriteMemory(pitchAng, "float", $"{pitch}");
                memory.WriteMemory(rollAng, "float", $"{roll}");
                memory.WriteMemory(playerFov, "float", $"{fov}");
            }
            else
            {
                Console.WriteLine("Please select a row to teleport to!");
            }
        }

        private void replaceCurrent_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                byte[] CameraXBytes = memory.ReadBytes(xPos, 4);
                float x = (float)Math.Round(BitConverter.ToSingle(CameraXBytes, 0), 7);
                byte[] CameraYBytes = memory.ReadBytes(yPos, 4);
                float y = (float)Math.Round(BitConverter.ToSingle(CameraYBytes, 0), 7);
                byte[] CameraZBytes = memory.ReadBytes(zPos, 4);
                float z = (float)Math.Round(BitConverter.ToSingle(CameraZBytes, 0), 7);
                byte[] CameraYawBytes = memory.ReadBytes(yawAng, 4);
                float yaw = (float)Math.Round(BitConverter.ToSingle(CameraYawBytes, 0), 7);
                byte[] CameraPitchBytes = memory.ReadBytes(pitchAng, 4);
                float pitch = (float)Math.Round(BitConverter.ToSingle(CameraPitchBytes, 0), 7);
                byte[] CameraRollBytes = memory.ReadBytes(rollAng, 4);
                float roll = (float)Math.Round(BitConverter.ToSingle(CameraRollBytes, 0), 7);
                float fov = memory.ReadFloat(playerFov);
                float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value);

                keyframeDataGridView.Rows[selectedIndex].Cells["X"].Value = x;
                keyframeDataGridView.Rows[selectedIndex].Cells["Y"].Value = y;
                keyframeDataGridView.Rows[selectedIndex].Cells["Z"].Value = z;
                keyframeDataGridView.Rows[selectedIndex].Cells["Yaw"].Value = yaw;
                keyframeDataGridView.Rows[selectedIndex].Cells["Pitch"].Value = pitch;
                keyframeDataGridView.Rows[selectedIndex].Cells["Roll"].Value = roll;
                keyframeDataGridView.Rows[selectedIndex].Cells["FOV"].Value = fov;
                keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value = transitionTime;
            }
            else
            {
                MessageBox.Show("Please select a row to replace.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void teleportCamera_Button_Click(object sender, EventArgs e)
        {
            memory.WriteMemory(xPos, "float", teleportX.Text.ToString());
            memory.WriteMemory(yPos, "float", teleportY.Text.ToString());
            memory.WriteMemory(zPos, "float", teleportZ.Text.ToString());
        }

        private void updateModules_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("MCCWinStore-Win64-Shipping");
            if (processes.Length == 0)
            {
                processes = Process.GetProcessesByName("MCC-Win64-Shipping");
            }

            if (processes.Length > 0)
            {
                string selectedProcessName = $"{processes[0].ProcessName}";
                ProcessSelected(selectedProcessName);
            }
            else
            {
                using (var processDialog = new ProcessSelectionDialog())
                {
                    if (processDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedProcessName = processDialog.SelectedProcess;
                        ProcessSelected(selectedProcessName);

                        if (selectedProcessName != null)
                        {
                            Console.WriteLine("Process: " + selectedProcessName.ToString());
                        }
                    }
                }
            }
        }

        private void clearList_Button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all rows from the list?\n(You are going lose everything!)", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

            if (result == DialogResult.Yes)
            {
                keyframeDataGridView.Rows.Clear();
            }
        }

        private void teleportToPlayer_Button_Click(object sender, EventArgs e)
        {
            memory.WriteMemory(xPos, "float", "100000.0");
            memory.WriteMemory(yPos, "float", "100000.0");
            memory.WriteMemory(zPos, "float", "100000.0");
        }

        private void dupeSelection_Button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                float x = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["X"].Value);
                float y = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Y"].Value);
                float z = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Z"].Value);
                float yaw = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Yaw"].Value);
                float pitch = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Pitch"].Value);
                float roll = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Roll"].Value);
                float fov = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["FOV"].Value);
                float transitionTime = Convert.ToSingle(keyframeDataGridView.Rows[selectedIndex].Cells["Transition Time"].Value);

                AddKeyPointRow(x, y, z, yaw, pitch, roll, fov, transitionTime);
            }
            else
            {
                MessageBox.Show("Please select a row to duplicate.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sortUp_button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                if (selectedIndex > 0)
                {
                    DataGridViewRow selectedRow = keyframeDataGridView.Rows[selectedIndex];
                    keyframeDataGridView.Rows.Remove(selectedRow);
                    keyframeDataGridView.Rows.Insert(selectedIndex - 1, selectedRow);
                    keyframeDataGridView.Rows[selectedIndex - 1].Selected = true;
                }
                else
                {
                    MessageBox.Show("The selected row is already at the top.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to sort up.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sortDown_button_Click(object sender, EventArgs e)
        {
            if (keyframeDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = keyframeDataGridView.SelectedRows[0].Index;

                if (selectedIndex < keyframeDataGridView.Rows.Count - 1)
                {
                    DataGridViewRow selectedRow = keyframeDataGridView.Rows[selectedIndex];
                    keyframeDataGridView.Rows.Remove(selectedRow);
                    keyframeDataGridView.Rows.Insert(selectedIndex + 1, selectedRow);
                    keyframeDataGridView.Rows[selectedIndex + 1].Selected = true;
                }
                else
                {
                    MessageBox.Show("The selected row is already at the bottom.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to sort down.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pathStart_checkbox_CheckedChanged(object sender, EventArgs e)
        {

            if (isHeaderLoaded == true)
            {
                if (pathStart_checkbox.Checked == true)
                {
                    MoveCamera();
                }
                else if (pathStart_checkbox.Checked == false)
                {
                    pathStart_checkbox.Checked = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("You might want to load a map first!", "Warning", MessageBoxButtons.OK);
                pathStart_checkbox.Checked = false;
            }
        }

        private void importPathWithOffset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Warning!\n\nYou will lose any unsaved progress.\nAre you sure you want to import a new existing path?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

            if (result == DialogResult.Yes)
            {
                float currentX = memory.ReadFloat(xPos);
                float currentY = memory.ReadFloat(yPos);
                float currentZ = memory.ReadFloat(zPos);

                OpenFileDialog openFileDialog = new OpenFileDialog();
                string programDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string subfolderPath = Path.Combine(programDirectory, "paths");
                openFileDialog.InitialDirectory = subfolderPath;
                openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string json = File.ReadAllText(openFileDialog.FileName);
                        List<KeypointExports> keyPoints = JsonConvert.DeserializeObject<List<KeypointExports>>(json);

                        keyframeDataGridView.Rows.Clear();

                        float offsetX = currentX - keyPoints[0].xPos;
                        float offsetY = currentY - keyPoints[0].yPos;
                        float offsetZ = currentZ - keyPoints[0].zPos;

                        foreach (KeypointExports keypoint in keyPoints)
                        {
                            float newX = keypoint.xPos + offsetX;
                            float newY = keypoint.yPos + offsetY;
                            float newZ = keypoint.zPos + offsetZ;

                            AddKeyPointRow(newX, newY, newZ, keypoint.yawAng, keypoint.pitchAng, keypoint.rollAng, keypoint.playerFov, keypoint.transitionTime);
                        }

                        MessageBox.Show("Key points imported with offset successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error importing key points with offset: " + ex.Message);
                    }
                }
            }
        }

        List<Tuple<float, float, float, float, float, float, float>> masterFrames = new List<Tuple<float, float, float, float, float, float, float>>();

        private void SaveMasterFrames()
        {
            if (isHeaderLoaded == false)
            {
                return;
            }

            masterFrames.Clear();

            foreach (DataGridViewRow row in keyframeDataGridView.Rows)
            {
                if (row.IsNewRow)
                    continue;

                float x = Convert.ToSingle(row.Cells["X"].Value);
                float y = Convert.ToSingle(row.Cells["Y"].Value);
                float z = Convert.ToSingle(row.Cells["Z"].Value);
                float yaw = Convert.ToSingle(row.Cells["Yaw"].Value);
                float pitch = Convert.ToSingle(row.Cells["Pitch"].Value);
                float roll = Convert.ToSingle(row.Cells["Roll"].Value);
                float fov = Convert.ToSingle(row.Cells["FOV"].Value);

                masterFrames.Add(new Tuple<float, float, float, float, float, float, float>(x, y, z, yaw, pitch, roll, fov));
            }
        }

        private void startPath_Button_Click(object sender, EventArgs e)
        {
            cameraThread = new Thread(async () => { await MoveCamera(); });

            cameraThread.Start();
        }

        private void AddKey_Button_Click(object sender, EventArgs e)
        {
            addKey();
        }

        private void useTheaterTime_CheckedChanged(object sender, EventArgs e)
        {
            if (useTheaterTime.Checked == true && isCheckingTime == false)
            {
                if (messageShown == false && keyframeDataGridView.Rows.Count > 0)
                {
                    MessageBox.Show("You have keyframes created!\n\nYou'll need to manually adjust their times under the 'Transition Times' column.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    messageShown = true;
                }

                startFromSelection_checkbox.Checked = false;
                startFromSelection_checkbox.Enabled = false;
                isCheckingTime = true;
                checkTheaterTime = new Thread(async () => { await TimeCheck(); });
                checkTheaterTime.Start();
            }
            if (useTheaterTime.Checked == false)
            {
                startFromSelection_checkbox.Enabled = true;
            }
        }

        public async Task TimeCheck()
        {
            while (true)
            {
                if (useTheaterTime.Checked == false)
                {
                    isCheckingTime = false;
                    return;
                }

                float time = memory.ReadFloat(theaterTime);
                TimeSpan timeSpan = TimeSpan.FromSeconds(time);
                string formattedTime = timeSpan.ToString(@"hh\:mm\:ss");

                if (CurrentTimeTextbox.InvokeRequired)
                {
                    CurrentTimeTextbox.Invoke((MethodInvoker)delegate
                    {
                        CurrentTimeTextbox.Text = formattedTime;
                    });
                }
                else
                {
                    CurrentTimeTextbox.Text = formattedTime;
                }

                await Task.Delay(1);
            }
        }
        #endregion
    }
}
