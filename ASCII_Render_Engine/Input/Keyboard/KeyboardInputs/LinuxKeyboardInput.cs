using System.Runtime.InteropServices;

namespace ASCII_Render_Engine.Input.Keyboard.KeyboardInputs;

public class LinuxKeyboardInput : IKeyboardInput, IDisposable
{
    private nint device;

    [DllImport("libevdev.so.2")]
    private static extern nint libevdev_new();

    [DllImport("libevdev.so.2")]
    private static extern int libevdev_set_fd(nint dev, int fd);

    [DllImport("libevdev.so.2")]
    private static extern int libevdev_next_event(nint dev, int flags, out InputEvent ev);

    [DllImport("libevdev.so.2")]
    private static extern int libevdev_grab(nint dev, int grab);

    [DllImport("libevdev.so.2")]
    private static extern void libevdev_free(nint dev);

    [StructLayout(LayoutKind.Sequential)]
    private struct InputEvent
    {
        public timeval time;
        public ushort type;
        public ushort code;
        public int value;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct timeval
    {
        public long tv_sec;
        public long tv_usec;
    }

    private const int EV_KEY = 0x01;
    private const int EV_SYN = 0x00;
    private const int LIBEVDEV_READ_FLAG_NORMAL = 0x00;
    private const int LIBEVDEV_GRAB = 1;

    public LinuxKeyboardInput(string devicePath)
    {
        device = libevdev_new();
        int fd = open(devicePath, 0);
        if (fd < 0)
        {
            throw new InvalidOperationException("Failed to open device.");
        }

        if (libevdev_set_fd(device, fd) != 0)
        {
            throw new InvalidOperationException("Failed to set device file descriptor.");
        }

        libevdev_grab(device, LIBEVDEV_GRAB);
    }

    public bool IsKeyPressed(Keys key)
    {
        InputEvent ev;
        while (libevdev_next_event(device, LIBEVDEV_READ_FLAG_NORMAL, out ev) == 0)
        {
            if (ev.type == EV_KEY && ev.code == ConvertKeyToEvdevCode(key) && ev.value == 1)
            {
                return true;
            }
        }
        return false;
    }

    private static int ConvertKeyToEvdevCode(Keys key)
    {
        return key switch
        {
            Keys.None => 0,
            Keys.Enter => 28,
            Keys.Escape => 1,
            Keys.Space => 57,
            Keys.Backspace => 14,
            Keys.Tab => 15,
            Keys.CapsLock => 58,
            Keys.Shift => 42, // or 54 for right shift
            Keys.Control => 29, // or 97 for right control
            Keys.Alt => 56, // or 100 for right alt
            Keys.LeftArrow => 105,
            Keys.RightArrow => 106,
            Keys.UpArrow => 103,
            Keys.DownArrow => 108,
            Keys.A => 30,
            Keys.B => 48,
            Keys.C => 46,
            Keys.D => 32,
            Keys.E => 18,
            Keys.F => 33,
            Keys.G => 34,
            Keys.H => 35,
            Keys.I => 23,
            Keys.J => 36,
            Keys.K => 37,
            Keys.L => 38,
            Keys.M => 50,
            Keys.N => 49,
            Keys.O => 24,
            Keys.P => 25,
            Keys.Q => 16,
            Keys.R => 19,
            Keys.S => 31,
            Keys.T => 20,
            Keys.U => 22,
            Keys.V => 47,
            Keys.W => 17,
            Keys.X => 45,
            Keys.Y => 21,
            Keys.Z => 44,
            Keys.Num0 => 11,
            Keys.Num1 => 2,
            Keys.Num2 => 3,
            Keys.Num3 => 4,
            Keys.Num4 => 5,
            Keys.Num5 => 6,
            Keys.Num6 => 7,
            Keys.Num7 => 8,
            Keys.Num8 => 9,
            Keys.Num9 => 10,
            Keys.F1 => 59,
            Keys.F2 => 60,
            Keys.F3 => 61,
            Keys.F4 => 62,
            Keys.F5 => 63,
            Keys.F6 => 64,
            Keys.F7 => 65,
            Keys.F8 => 66,
            Keys.F9 => 67,
            Keys.F10 => 68,
            Keys.F11 => 87,
            Keys.F12 => 88,
            _ => 0
        };
    }

    [DllImport("libc", SetLastError = true)]
    private static extern int open(string pathname, int flags);

    [DllImport("libc", SetLastError = true)]
    private static extern int close(int fd);

    public void Dispose()
    {
        if (device != nint.Zero)
        {
            libevdev_free(device);
            device = nint.Zero;
        }
    }
}
