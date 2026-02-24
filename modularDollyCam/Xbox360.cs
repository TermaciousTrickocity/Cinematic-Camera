using System;
using System.Security.Cryptography.Xml;

#if XBOX360
namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        public async Task ConnectToConsole()
        {
            try
            {
                Console.WriteLine($"Awaiting connection...");
                XDK.Connect();

                if (XDK.activeConnection)
                {
                    XDK.xbCon.DebugTarget.Go(out stopped);
                    Console.WriteLine("Connected successfully! - (" + XDK.xbCon.Name.ToString() + ")");

                    /* temporary, will make a ui or auto-loader / sigscanner later */

                    uint address = address = Convert.ToUInt32(Xbox360BaseAddress.Text);
                    uint x = address;
                    uint y = (address + 0x4);
                    uint z = (address + 0x8);
                    uint pitch = (address + 0xC);
                    uint yaw = (address + 0x10);
                    uint roll = (address + 0x14);

                    xPos = x.ToString();
                    yPos = y.ToString();
                    zPos = z.ToString();
                    yawAng = yaw.ToString();
                    pitchAng = pitch.ToString();
                    rollAng = roll.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
#endif