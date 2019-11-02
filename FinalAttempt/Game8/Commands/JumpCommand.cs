using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game8.Commands
{
    class JumpCommand : ICommand
    {
        private Game1 Receiver;
        public JumpCommand(Game1 game1)
        {
            this.Receiver = game1;
        }
        public void Execute()
        {

        }
    }
}
