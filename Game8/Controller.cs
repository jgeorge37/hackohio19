using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game8.Commands;
using System.Diagnostics;

namespace Game8
{
    class Controller
    {
        KeyboardState now;
        KeyboardState past;
        Dictionary<Keys, ICommand> keys;
        public Controller()
        {
            past = Keyboard.GetState();
            keys = new Dictionary<Keys, ICommand>();
        }

        public void AddKey(Keys k, ICommand val)
        {
            keys.Add(k, val);
        }

        public void Update()
        {
            now = Keyboard.GetState();
            Keys[] keysPressed = now.GetPressedKeys();
            foreach (Keys key in keysPressed)
            {
                if (!past.IsKeyDown(key))
                {
                    if (keys.ContainsKey(key))
                    {
                        keys[key].Execute();
                    }
                    Debug.WriteLine(key.ToString() + " Pressed!");
                }

            }
            past = now;
        }
       
    }
}
