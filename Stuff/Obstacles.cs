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
        public Obstacles()
        {

        }
        public Rectangle BoundingBox => new Rectangle();
        public bool HasResponse => false;
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void Update(GameTime gameTime)
        {

        }
        public void CollisionResponse(bool isItem)
        {
            throw new NotImplementedException();
        }
    }
}
