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
        bool charizardCanFly;
        int heightJumped;
        int maxJumpHeight;
        int velocity;
        double Scale;
        Animation Travel;
        Animation CurrentAnimation;
        bool isDead;
        Texture2D texture1;
        Texture2D texture2;
        Texture2D texture3;
        Texture2D currentTexture;
        public int PlayerPoints { get; set; }
        public int PlayerCoconuts { get; set; }

        public Avatar(GraphicsDevice graphicsDevice, Texture2D tx1, Texture2D tx2, Texture2D tx3, double scale)
        {
            isJumping = false;
            //charizardIsFlying = false;
            heightJumped = 0;
            maxJumpHeight = 170;
            isDead = false;
            texture1 = tx1;
            currentTexture = texture1;
            texture2 = tx2;
            texture3 = tx3;
            Scale = scale;
            PlayerPoints = 0;
            PlayerCoconuts = 0;
            velocity = 0;

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

        public Rectangle BoundingBox => new Rectangle(30, 348 - (int)(currentTexture.Height * Scale) - heightJumped, (int)(currentTexture.Width * Scale), (int)(currentTexture.Height * Scale));
        public bool HasResponse => true;
        public void Jump()
        {
            isJumping = true;
        }
        public void Fly(bool up)
        {
            if (up)
            {
                this.heightJumped = heightJumped - 5;
            }
            else
            {
                this.heightJumped = heightJumped + 5;
            }
        }
        public void Update(GameTime gametime)
        {
            if (charizardCanFly && isJumping)
            {
                Fly()
            }
            else
            {
                if (isJumping)
                {
                    if (heightJumped < maxJumpHeight)
                    {
                        velocity = 7;
                    }
                    else
                    {
                        isJumping = false;
                    }
                }
                else if (!isJumping && heightJumped > 0)
                {
                    velocity = - 5;
                }
            }
            CurrentAnimation = Travel;
            CurrentAnimation.Update(gametime);
            if ((int)gametime.TotalGameTime.TotalMilliseconds % 100 == 0)
            {
                this.PlayerPoints = PlayerPoints + 10;
            }
            heightJumped = heightJumped + velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentTexture, this.BoundingBox, Color.White);
        }

        public void attemptUpgrade()
        {
            if (currentTexture == texture1 && this.PlayerCoconuts >= 20)
            {
                currentTexture = texture2;
            }
            else if (currentTexture == texture2 && this.PlayerCoconuts >= 30)
            {
                currentTexture = texture3;
                charizardCanFly = true;
            }
        }

        public void CollisionResponse(bool isItem)
        {
            if (!isItem)
            {
                isDead = true;
            }
            else
            {
                this.PlayerCoconuts = this.PlayerCoconuts + 1;
                this.attemptUpgrade();

            }
        }
        public bool IsDead()
        {
            return isDead;
        }
    }
}
