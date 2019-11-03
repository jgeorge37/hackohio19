using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game8.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game8.Stuff
{
    class Items : ICollidable
    {
        Texture2D texture;
        int delayTime;
        double vanishTime;
        bool waiting;
        int currentPosition;
        double Scale;
        bool taken;

        public Items(Texture2D tx, int x, double scale)
        {
            delayTime = x;
            texture = tx;
            currentPosition = 800;
            waiting = false;
            Scale = scale;
            taken = false;
        }
        public Rectangle BoundingBox => new Rectangle(currentPosition, 267 - (int)(texture.Height * Scale), (int)(texture.Width * Scale), (int)(texture.Height * Scale));
        public bool HasResponse => true;
        public bool isVisible => !taken;
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!taken)
            {
                spriteBatch.Draw(texture, this.BoundingBox, Color.White);
            }
        }
        public void Update(GameTime gameTime)
        {
            // IDK IF THERE ARE BOOLEANS I SHOULD cCHECK
            if (currentPosition > -texture.Width)
            {
                currentPosition = currentPosition - 6;
            }
            else
            {
                if (waiting && gameTime.TotalGameTime.TotalSeconds - vanishTime > delayTime)
                {
                    waiting = false;
                    currentPosition = 800;
                }
                else if (!waiting)
                {
                    waiting = true;
                    vanishTime = gameTime.TotalGameTime.TotalSeconds;
                    taken = false;
                }
            }
        }
        public void CollisionResponse(bool isObstacle)
        {
            if (isObstacle)
            {
                currentPosition = 800;
            }
            else
            {
                taken = true;
            }
        }
    }
}
