using System.Runtime.InteropServices;
using ASCII_Render_Engine.Input.Keyboard.KeyboardInputs;

namespace ASCII_Render_Engine.Input.Keyboard.Factories
{
    public static class KeyboardInputFactory
    {
        public static IKeyboardInput CreateKeyboardInput()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsKeyboardInput();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                try
                {
                    return new LinuxKeyboardInput("/dev/input/event0");
                }
                catch (Exception e)
                {
                    throw new Exception("Linux Keyboard Input Device not supported, " + e);
                }
            }
            else
            {
                throw new PlatformNotSupportedException("Unsupported operating system.");
            }
        }
    }
}
