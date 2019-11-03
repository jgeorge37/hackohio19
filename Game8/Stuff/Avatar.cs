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

namespace Game8.Stuff
{
    class Avatar : ICollidable
    {
        bool isJumping;
        int heightJumped;
        int maxJumpHeight;
        double Scale;
        Animation Travel;
        Animation CurrentAnimation;
        bool isDead;
        Texture2D texture;
        public int PlayerPoints { get; set; }
        public int PlayerCoconuts { get; set; }

        public Avatar(GraphicsDevice graphicsDevice, Texture2D tx, double scale)
        {
            isJumping = false;
            heightJumped = 0;
            maxJumpHeight = 170;
            isDead = false;
            texture = tx;
            Scale = scale;
            PlayerPoints = 0;
            PlayerCoconuts = 0;

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
            Travel.AddFrame(new Rectangle(3, 40, 30, 30), TimeSpan.FromSeconds(.08));
            Travel.AddFrame(new Rectangle(170, 40, 40, 30), TimeSpan.FromSeconds(.08));


        }

        public Rectangle BoundingBox => new Rectangle(30, 348 - (int)(texture.Width * Scale) - heightJumped, (int)(texture.Width * Scale), (int)(texture.Height * Scale));
        public bool HasResponse => true;
        public void Jump()
        {
            isJumping = true;
        }

        public void Update(GameTime gametime)
        {
            if (isJumping)
            {
                if (heightJumped < maxJumpHeight)
                {
                    heightJumped = heightJumped + 7;
                }
                else
                {
                    isJumping = false;
                }
            }
            else if (!isJumping && heightJumped > 0)
            {
                heightJumped = heightJumped - 5;
            }

            CurrentAnimation = Travel;
            CurrentAnimation.Update(gametime);
            if((int)gametime.TotalGameTime.TotalMilliseconds % 100 == 0)
            {
                this.PlayerPoints = PlayerPoints + 10;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.BoundingBox, Color.White);
        }

        public void CollisionResponse(bool isItem)
        {
            if (!isItem)
            {
                isDead = true;
            }
            else
            {
                this.PlayerCoconuts =  this.PlayerCoconuts + 1;
            }
        }
        public bool IsDead()
        {
            return isDead;
        }
    }
}
