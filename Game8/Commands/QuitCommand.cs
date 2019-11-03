using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game8.Commands
{
    class QuitCommand : ICommand
    {
        private Game1 receiver;

        public QuitCommand(Game1 game1)
        {
            this.receiver = game1;
        }

        public void Execute()
        {
            receiver.Exit();
        }
    }
}
