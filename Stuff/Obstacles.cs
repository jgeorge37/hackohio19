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
        public Obstacles(Texture2D tx)
        {
          texture = tx;
        }
        public Rectangle BoundingBox => new Rectangle();
        public bool HasResponse => false;
        public void Draw(SpriteBatch spriteBatch)
        {
          spriteBatch.Draw(texture, new Rectangle(30, 200, 30, 40), Color.White);
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
