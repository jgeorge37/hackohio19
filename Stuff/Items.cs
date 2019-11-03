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
        public Items()
        {

        }
        public Rectangle BoundingBox => new Rectangle();
        public bool HasResponse => true;
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void Update(GameTime gameTime)
        {

        }
        public void CollisionResponse(bool isItem)
        {

        }
    }
}
