using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game8.Commands;
using Game8.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game8
{
    class Avatar : ICollidable
    {
        bool isJumping;
        int heightJumped;
        int maxJumpHeight;
        Animation Travel;
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

            Travel = new Animation();
            Travel.AddFrame(new Rectangle(170, 40, 40, 30), TimeSpan.FromSeconds(.08));
            Travel.AddFrame(new Rectangle(100, 40, 30, 30), TimeSpan.FromSeconds(.08));
            Travel.AddFrame(new Rectangle(67, 40, 30, 30), TimeSpan.FromSeconds(.08));
            Travel.AddFrame(new Rectangle(35, 40, 30, 30), TimeSpan.FromSeconds(.08));
						Travel.AddFrame(new Rectangle (3, 40, 30, 30), TimeSpan.FromSeconds (.08));
						Travel.AddFrame(new Rectangle(170, 40, 40, 30), TimeSpan.FromSeconds(.08));


        }

        public Rectangle BoundingBox => new Rectangle();
        public bool isMoving => true;

        public void Jump()
        {
            if (!isJumping)
            {

            }
            isJumping = true;
        }

        public void Update(GameTime gametime)
        {
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
            }
            else if (!isJumping && heightJumped > 0)
            {
                heightJumped = heightJumped - 10;
            }

            CurrentAnimation = JumpUp;
            CurrentAnimation.Update(gametime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void CollisionResponse(bool isItem)
        {

        }
    }
}
