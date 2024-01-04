using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camera
{
    public partial class CameraForm
    {
        public async Task UpdateModules()
        {
            try
            {
                if (memory == null) return;
                if (memory.theProc == null) return;

                memory.theProc.Refresh();
                memory.modules.Clear();

                foreach (ProcessModule Module in memory.theProc.Modules)
                {
                    if (!string.IsNullOrEmpty(Module.ModuleName) && !memory.modules.ContainsKey(Module.ModuleName))
                        memory.modules.Add(Module.ModuleName, Module.BaseAddress);
                }

                await DisableButton();
                await Data();
            }
            catch
            {
                if (modulesUpdated == false) { modulesUpdated = true; MessageBox.Show("Cant update modules, Is the Game still Running?."); }
            }
        }

        public async Task<bool> UpdateTagStatus()
        {
            string tags_Loaded = memory.ReadString("haloreach.dll+0x25BDAB0", "");
            bool tags_Loaded_Status = tags_Loaded != "mainmenu";

            if (!tagscurrentlyloaded && tags_Loaded_Status) //Check if tags are loaded if not load them
            {
                tagscurrentlyloaded = true;
                // Do shit when map loads
            }
            else if (tagscurrentlyloaded && !tags_Loaded_Status)
            {
                tagscurrentlyloaded = false;
                //do shit when map unloads
            }

            return tags_Loaded_Status;
        }

        public async Task DisableButton()
        {
            updateModules.Enabled = false;

            await Task.Delay(1000);

            updateModules.Enabled = true;
        }
    }
}
