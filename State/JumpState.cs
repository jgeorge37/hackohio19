using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackGame.State;

namespace HackGame.States.Avatar_States
{
    class JumpState : IActionState
    {
        private static JumpState instance = null;

        public IActionState prevState;

        private JumpState(){

        }

        public static JumpState Instance{
          get{
            if (instance == null){
              instance = new JumpState();
            }
            return instance;
          }
        }

        public IActionState Jump(){
          return this;
        }

        public IActionState Fall(){
          return this;
        }

        public void Update(){
          throw new NotImplementedException();
        }

        public IActionState Exit(){
          throw new NotImplementedException();
        }
    }
}
