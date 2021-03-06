﻿using System;
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
        int Sky;

        public Obstacles(Texture2D tx, double x, double scale, int sky)
        {
            delayTime = x;
            texture = tx;
            currentPosition = 800;
            Scale = scale;
            Sky = sky;

        }
        public Rectangle BoundingBox => new Rectangle(currentPosition, 348 - (int)(texture.Height *Scale) - 250*Sky, (int)(texture.Width* Scale), (int)(texture.Height * Scale));
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
                currentPosition = currentPosition - 6;
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
