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
				Animation JumpUp;
				Animation CurrentAnimation;

		    public Avatar(GraphicsDevice graphicsDevice)
		    {
						isJumping = false;
        		heightJumped = 0;
            maxJumpHeight = 100;

						/*
						THIS IS STUFF FROM THE EXAMPLE THAT IDK HOW TO DEAL WITH

						if (characterSheetTexture == null)
					  {
					      using (var stream = TitleContainer.OpenStream ("Content/charactersheet.png"))
					      {
					          characterSheetTexture = Texture2D.FromStream (graphicsDevice, stream);
					      }
					  }
						*/

						JumpUp = new Animation();
						JumpUp.AddFrame(new Rectangle (50, 100, 20, 20), TimeSpan.FromSeconds (.2));
						JumpUp.AddFrame(new Rectangle (50, 90, 20, 20), TimeSpan.FromSeconds (.2));
						JumpUp.AddFrame(new Rectangle (50, 80, 20, 20), TimeSpan.FromSeconds (.2));
						JumpUp.AddFrame(new Rectangle (50, 70, 20, 20), TimeSpan.FromSeconds (.1));
						JumpUp.AddFrame(new Rectangle (50, 85, 20, 20), TimeSpan.FromSeconds (.2));
						JumpUp.AddFrame(new Rectangle (50, 100, 20, 20), TimeSpan.FromSeconds (.2));

        }

				public void Jump(){
					if(!isJumping){

					}
					isJumping = true;
				}

				public void Update(GameTime gametime){
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

					CurrentAnimation = JumpUp;
					CurrentAnimation.Update(gametime);
				}

				public void Draw(SpriteBatch spriteBatch){
					
				}


    }
}
