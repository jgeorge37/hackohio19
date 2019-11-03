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
    class Obstacles : ICollidable
    {
        Texture2D texture;
        double delayTime;
        double vanishTime;
        bool waiting;
        int currentPosition;
        double Scale;

        public Obstacles(Texture2D tx, double x, double scale)
        {
            delayTime = x;
            texture = tx;
            currentPosition = 800;
            Scale = scale;

        }
        public Rectangle BoundingBox => new Rectangle(currentPosition, 267 - (int)(texture.Width *Scale), (int)(texture.Width* Scale), (int)(texture.Height * Scale));
        public bool HasResponse => false;
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.BoundingBox, Color.White);

        }
        public void Update(GameTime gameTime)
        {
            // IDK IF THERE ARE BOOLEANS I SHOULD cCHECK
            if (currentPosition > -(int)texture.Width*Scale)
            {
                currentPosition = currentPosition - 4;
            }
            else
            {
                if (waiting && gameTime.TotalGameTime.TotalSeconds - vanishTime > delayTime)
                {
                    waiting = false; // ur time is NOW
                    currentPosition = 800;
                }
                else if (!waiting)
                {
                    waiting = true;
                    vanishTime = gameTime.TotalGameTime.TotalSeconds;
                }
            }
        }
        public void CollisionResponse(bool isItem)
        {
            throw new NotImplementedException();
        }
    }
}
