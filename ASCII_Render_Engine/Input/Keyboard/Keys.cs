namespace ASCII_Render_Engine.Input.Keyboard;

public enum Keys
{
    None,
    Enter,
    Escape,
    Space,
    Backspace,
    Tab,
    CapsLock,
    Shift,
    Control,
    Alt,
    LeftArrow,
    RightArrow,
    UpArrow,
    DownArrow,
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,
    Num0,
    Num1,
    Num2,
    Num3,
    Num4,
    Num5,
    Num6,
    Num7,
    Num8,
    Num9,
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12
}
internal static class KeyConverter
{
    public static Keys ConvertKey(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.Enter:
                return Keys.Enter;
            case ConsoleKey.Escape:
                return Keys.Escape;
            case ConsoleKey.Spacebar:
                return Keys.Space;
            case ConsoleKey.Backspace:
                return Keys.Backspace;
            case ConsoleKey.Tab:
                return Keys.Tab;
            case ConsoleKey.LeftArrow:
                return Keys.LeftArrow;
            case ConsoleKey.RightArrow:
                return Keys.RightArrow;
            case ConsoleKey.UpArrow:
                return Keys.UpArrow;
            case ConsoleKey.DownArrow:
                return Keys.DownArrow;
            case ConsoleKey.A:
                return Keys.A;
            case ConsoleKey.B:
                return Keys.B;
            case ConsoleKey.C:
                return Keys.C;
            case ConsoleKey.D:
                return Keys.D;
            case ConsoleKey.E:
                return Keys.E;
            case ConsoleKey.F:
                return Keys.F;
            case ConsoleKey.G:
                return Keys.G;
            case ConsoleKey.H:
                return Keys.H;
            case ConsoleKey.I:
                return Keys.I;
            case ConsoleKey.J:
                return Keys.J;
            case ConsoleKey.K:
                return Keys.K;
            case ConsoleKey.L:
                return Keys.L;
            case ConsoleKey.M:
                return Keys.M;
            case ConsoleKey.N:
                return Keys.N;
            case ConsoleKey.O:
                return Keys.O;
            case ConsoleKey.P:
                return Keys.P;
            case ConsoleKey.Q:
                return Keys.Q;
            case ConsoleKey.R:
                return Keys.R;
            case ConsoleKey.S:
                return Keys.S;
            case ConsoleKey.T:
                return Keys.T;
            case ConsoleKey.U:
                return Keys.U;
            case ConsoleKey.V:
                return Keys.V;
            case ConsoleKey.W:
                return Keys.W;
            case ConsoleKey.X:
                return Keys.X;
            case ConsoleKey.Y:
                return Keys.Y;
            case ConsoleKey.Z:
                return Keys.Z;
            case ConsoleKey.D0:
                return Keys.Num0;
            case ConsoleKey.D1:
                return Keys.Num1;
            case ConsoleKey.D2:
                return Keys.Num2;
            case ConsoleKey.D3:
                return Keys.Num3;
            case ConsoleKey.D4:
                return Keys.Num4;
            case ConsoleKey.D5:
                return Keys.Num5;
            case ConsoleKey.D6:
                return Keys.Num6;
            case ConsoleKey.D7:
                return Keys.Num7;
            case ConsoleKey.D8:
                return Keys.Num8;
            case ConsoleKey.D9:
                return Keys.Num9;
            case ConsoleKey.F1:
                return Keys.F1;
            case ConsoleKey.F2:
                return Keys.F2;
            case ConsoleKey.F3:
                return Keys.F3;
            case ConsoleKey.F4:
                return Keys.F4;
            case ConsoleKey.F5:
                return Keys.F5;
            case ConsoleKey.F6:
                return Keys.F6;
            case ConsoleKey.F7:
                return Keys.F7;
            case ConsoleKey.F8:
                return Keys.F8;
            case ConsoleKey.F9:
                return Keys.F9;
            case ConsoleKey.F10:
                return Keys.F10;
            case ConsoleKey.F11:
                return Keys.F11;
            case ConsoleKey.F12:
                return Keys.F12;
            default:
                return Keys.None;
        }
    }
    public static ConsoleKey ConvertKey(Keys key)
    {
        switch (key)
        {
            case Keys.Enter:
                return ConsoleKey.Enter;
            case Keys.Escape:
                return ConsoleKey.Escape;
            case Keys.Space:
                return ConsoleKey.Spacebar;
            case Keys.Backspace:
                return ConsoleKey.Backspace;
            case Keys.Tab:
                return ConsoleKey.Tab;
            case Keys.LeftArrow:
                return ConsoleKey.LeftArrow;
            case Keys.RightArrow:
                return ConsoleKey.RightArrow;
            case Keys.UpArrow:
                return ConsoleKey.UpArrow;
            case Keys.DownArrow:
                return ConsoleKey.DownArrow;
            case Keys.A:
                return ConsoleKey.A;
            case Keys.B:
                return ConsoleKey.B;
            case Keys.C:
                return ConsoleKey.C;
            case Keys.D:
                return ConsoleKey.D;
            case Keys.E:
                return ConsoleKey.E;
            case Keys.F:
                return ConsoleKey.F;
            case Keys.G:
                return ConsoleKey.G;
            case Keys.H:
                return ConsoleKey.H;
            case Keys.I:
                return ConsoleKey.I;
            case Keys.J:
                return ConsoleKey.J;
            case Keys.K:
                return ConsoleKey.K;
            case Keys.L:
                return ConsoleKey.L;
            case Keys.M:
                return ConsoleKey.M;
            case Keys.N:
                return ConsoleKey.N;
            case Keys.O:
                return ConsoleKey.O;
            case Keys.P:
                return ConsoleKey.P;
            case Keys.Q:
                return ConsoleKey.Q;
            case Keys.R:
                return ConsoleKey.R;
            case Keys.S:
                return ConsoleKey.S;
            case Keys.T:
                return ConsoleKey.T;
            case Keys.U:
                return ConsoleKey.U;
            case Keys.V:
                return ConsoleKey.V;
            case Keys.W:
                return ConsoleKey.W;
            case Keys.X:
                return ConsoleKey.X;
            case Keys.Y:
                return ConsoleKey.Y;
            case Keys.Z:
                return ConsoleKey.Z;
            case Keys.Num0:
                return ConsoleKey.D0;
            case Keys.Num1:
                return ConsoleKey.D1;
            case Keys.Num2:
                return ConsoleKey.D2;
            case Keys.Num3:
                return ConsoleKey.D3;
            case Keys.Num4:
                return ConsoleKey.D4;
            case Keys.Num5:
                return ConsoleKey.D5;
            case Keys.Num6:
                return ConsoleKey.D6;
            case Keys.Num7:
                return ConsoleKey.D7;
            case Keys.Num8:
                return ConsoleKey.D8;
            case Keys.Num9:
                return ConsoleKey.D9;
            case Keys.F1:
                return ConsoleKey.F1;
            case Keys.F2:
                return ConsoleKey.F2;
            case Keys.F3:
                return ConsoleKey.F3;
            case Keys.F4:
                return ConsoleKey.F4;
            case Keys.F5:
                return ConsoleKey.F5;
            case Keys.F6:
                return ConsoleKey.F6;
            case Keys.F7:
                return ConsoleKey.F7;
            case Keys.F8:
                return ConsoleKey.F8;
            case Keys.F9:
                return ConsoleKey.F9;
            case Keys.F10:
                return ConsoleKey.F10;
            case Keys.F11:
                return ConsoleKey.F11;
            case Keys.F12:
                return ConsoleKey.F12;
            default:
                return default;
        }
    }
}
