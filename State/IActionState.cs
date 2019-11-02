using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackGame.State;

namespace HackGame.States.Avatar_States
{
    interface IActionState : IState
    {
        IActionState MoveRight();
        IActionState Jump();
        IActionState Exit();
    }
}
