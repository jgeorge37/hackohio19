using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackGame.State;

namespace HackGame.States.Avatar_States
{
    class MoveRightState : IActionState
    {
        private static MoveRightState instance = null;

        private MoveRightState(){

        }

        public static MoveRightState Instance{
          get{
            if(instance == null){
              instance = new MoveRightState();
            }
            return instance;
          }
        }

        public IActionState Jump(){
            JumpState.Instance.prevState = this;
            return JumpState.Instance;
        }

        public IActionState Fall(){
          return this;
        }

        public void Update(){
          throw new NotImplementedException();
        }

        public IActionState Exit(){
          return new IdlingState();
        }
    }
}
