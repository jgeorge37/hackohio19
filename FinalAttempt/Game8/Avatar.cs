using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game8.Commands;

namespace Game8
{
    class Avatar
    {
		bool isJumping;
        int heightJumped;
        int maxJumpHeight;

        public Avatar()
        {
			isJumping = false;
            heightJumped = 0;
            maxJumpHeight = 100;

        }

		public void Jump(){
			if(!isJumping){

			}
			isJumping = true;
		}

		public void Update(){
            if (isJumping)
            {
                if (heightJumped < maxJumpHeight)
                {
                    heightJumped = heightJumped + 10;
                }
                else
                {
                    isJumping = false;
                }
            }else if(!isJumping && heightJumped > 0)
            {
                heightJumped = heightJumped - 10;
            }
		}


    }
}
