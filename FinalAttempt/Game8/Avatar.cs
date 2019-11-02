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

        public Avatar()
        {
						isJumping = false;
        }

				public void Jump(){
						if(!isJumping){

						}
						isJumping = true;
				}

				public void Update(){
					
				}


    }
}
