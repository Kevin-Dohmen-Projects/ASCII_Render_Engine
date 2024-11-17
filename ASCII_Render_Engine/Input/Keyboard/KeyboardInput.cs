using ASCII_Render_Engine.Input.Keyboard.Factories;
using ASCII_Render_Engine.Input.Keyboard.KeyboardInputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Input.Keyboard
{
    public class KeyboardInput
    {
        IKeyboardInput keyboardInput;

        public KeyboardInput()
        {
            keyboardInput = KeyboardInputFactory.CreateKeyboardInput();
        }

        public bool IsKeyPressed(Keys key)
        {
            return keyboardInput.IsKeyPressed(key);
        }
    }
}
