namespace ASCII_Render_Engine.Input.Keyboard.KeyboardInputs;

public class UniversalKeyboardInput : IKeyboardInput
{
    private ConsoleKeyInfo keySample;
    private DateTime lastSample;

    public bool IsKeyPressed(Keys key)
    {
        ConsoleKey cKey = ConsoleKeyConverter.ConvertKeyToConsoleKey(key);

        if (DateTime.Now - lastSample > TimeSpan.FromMilliseconds(50))
        {
            if (!Console.KeyAvailable)
            {
                return false;
            }
            keySample = Console.ReadKey(true);
            lastSample = DateTime.Now;
        }
        return keySample.Key == cKey;
    }

    private static class ConsoleKeyConverter
    {
        public static ConsoleKey ConvertKeyToConsoleKey(Keys key)
        {
            return key switch
            {
                Keys.None => ConsoleKey.None,
                Keys.Enter => ConsoleKey.Enter,
                Keys.Escape => ConsoleKey.Escape,
                Keys.Space => ConsoleKey.Spacebar,
                Keys.Backspace => ConsoleKey.Backspace,
                Keys.Tab => ConsoleKey.Tab,
                Keys.LeftArrow => ConsoleKey.LeftArrow,
                Keys.RightArrow => ConsoleKey.RightArrow,
                Keys.UpArrow => ConsoleKey.UpArrow,
                Keys.DownArrow => ConsoleKey.DownArrow,
                Keys.A => ConsoleKey.A,
                Keys.B => ConsoleKey.B,
                Keys.C => ConsoleKey.C,
                Keys.D => ConsoleKey.D,
                Keys.E => ConsoleKey.E,
                Keys.F => ConsoleKey.F,
                Keys.G => ConsoleKey.G,
                Keys.H => ConsoleKey.H,
                Keys.I => ConsoleKey.I,
                Keys.J => ConsoleKey.J,
                Keys.K => ConsoleKey.K,
                Keys.L => ConsoleKey.L,
                Keys.M => ConsoleKey.M,
                Keys.N => ConsoleKey.N,
                Keys.O => ConsoleKey.O,
                Keys.P => ConsoleKey.P,
                Keys.Q => ConsoleKey.Q,
                Keys.R => ConsoleKey.R,
                Keys.S => ConsoleKey.S,
                Keys.T => ConsoleKey.T,
                Keys.U => ConsoleKey.U,
                Keys.V => ConsoleKey.V,
                Keys.W => ConsoleKey.W,
                Keys.X => ConsoleKey.X,
                Keys.Y => ConsoleKey.Y,
                Keys.Z => ConsoleKey.Z,
                Keys.Num0 => ConsoleKey.D0,
                Keys.Num1 => ConsoleKey.D1,
                Keys.Num2 => ConsoleKey.D2,
                Keys.Num3 => ConsoleKey.D3,
                Keys.Num4 => ConsoleKey.D4,
                Keys.Num5 => ConsoleKey.D5,
                Keys.Num6 => ConsoleKey.D6,
                Keys.Num7 => ConsoleKey.D7,
                Keys.Num8 => ConsoleKey.D8,
                Keys.Num9 => ConsoleKey.D9,
                Keys.F1 => ConsoleKey.F1,
                Keys.F2 => ConsoleKey.F2,
                Keys.F3 => ConsoleKey.F3,
                Keys.F4 => ConsoleKey.F4,
                Keys.F5 => ConsoleKey.F5,
                Keys.F6 => ConsoleKey.F6,
                Keys.F7 => ConsoleKey.F7,
                Keys.F8 => ConsoleKey.F8,
                Keys.F9 => ConsoleKey.F9,
                Keys.F10 => ConsoleKey.F10,
                Keys.F11 => ConsoleKey.F11,
                Keys.F12 => ConsoleKey.F12,
                _ => ConsoleKey.NoName,
            };
        }
    }
}
