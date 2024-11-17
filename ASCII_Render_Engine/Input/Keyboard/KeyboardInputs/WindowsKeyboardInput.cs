using System.Runtime.InteropServices;

namespace ASCII_Render_Engine.Input.Keyboard.KeyboardInputs
{
    public class WindowsKeyboardInput : IKeyboardInput
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public bool IsKeyPressed(Keys key)
        {
            int vKey = VKeyConverter.ConvertKeyToVirtualKey(key);
            return (GetAsyncKeyState(vKey) & 0x8000) != 0;
        }

        private static class VKeyConverter
        {
            public static int ConvertKeyToVirtualKey(Keys key)
            {
                return key switch
                {
                    Keys.None => 0,
                    Keys.Enter => 0x0D,
                    Keys.Escape => 0x1B,
                    Keys.Space => 0x20,
                    Keys.Backspace => 0x08,
                    Keys.Tab => 0x09,
                    Keys.CapsLock => 0x14,
                    Keys.Shift => 0x10,
                    Keys.Control => 0x11,
                    Keys.Alt => 0x12,
                    Keys.LeftArrow => 0x25,
                    Keys.RightArrow => 0x27,
                    Keys.UpArrow => 0x26,
                    Keys.DownArrow => 0x28,
                    Keys.A => 0x41,
                    Keys.B => 0x42,
                    Keys.C => 0x43,
                    Keys.D => 0x44,
                    Keys.E => 0x45,
                    Keys.F => 0x46,
                    Keys.G => 0x47,
                    Keys.H => 0x48,
                    Keys.I => 0x49,
                    Keys.J => 0x4A,
                    Keys.K => 0x4B,
                    Keys.L => 0x4C,
                    Keys.M => 0x4D,
                    Keys.N => 0x4E,
                    Keys.O => 0x4F,
                    Keys.P => 0x50,
                    Keys.Q => 0x51,
                    Keys.R => 0x52,
                    Keys.S => 0x53,
                    Keys.T => 0x54,
                    Keys.U => 0x55,
                    Keys.V => 0x56,
                    Keys.W => 0x57,
                    Keys.X => 0x58,
                    Keys.Y => 0x59,
                    Keys.Z => 0x5A,
                    Keys.Num0 => 0x30,
                    Keys.Num1 => 0x31,
                    Keys.Num2 => 0x32,
                    Keys.Num3 => 0x33,
                    Keys.Num4 => 0x34,
                    Keys.Num5 => 0x35,
                    Keys.Num6 => 0x36,
                    Keys.Num7 => 0x37,
                    Keys.Num8 => 0x38,
                    Keys.Num9 => 0x39,
                    Keys.F1 => 0x70,
                    Keys.F2 => 0x71,
                    Keys.F3 => 0x72,
                    Keys.F4 => 0x73,
                    Keys.F5 => 0x74,
                    Keys.F6 => 0x75,
                    Keys.F7 => 0x76,
                    Keys.F8 => 0x77,
                    Keys.F9 => 0x78,
                    Keys.F10 => 0x79,
                    Keys.F11 => 0x7A,
                    Keys.F12 => 0x7B,
                    _ => 0
                };
            }
        }
    }
}
