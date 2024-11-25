using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
                return new UniversalKeyboardInput();
            }
            else
            {
                return new UniversalKeyboardInput();
            }
        }
    }
}
