using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game8.Stuff;

namespace Game8.Commands
{
    class JumpCommand : ICommand
    {
        private Avatar avatar;
        public JumpCommand(Avatar avatar)
        {
            this.avatar = avatar;
        }
        public void Execute()
        {
            avatar.Jump();
        }
    }
}
