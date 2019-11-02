using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game8.Sprites
{
    class SceneryBatch
    {
        private SpriteBatch obstacleBatch;
        private Dictionary<Texture2D, int> obstacle;
        private int groundHeight;

        public SceneryBatch()
        {
            groundHeight = 400;
        }
        public void AddObstacle(int xSpot, Texture2D item)
        {
            obstacle.Add(item, xSpot);
        }
        public void Begin()
        {
            obstacleBatch.Begin();
        }
        public void Draw()
        {
            foreach(Texture2D tex in obstacle.Keys)
            {
                obstacleBatch.Draw(tex, new Vector2(obstacle[tex], groundHeight));
            }
        }
        public void Update()
        {

        }
        public void End()
        {
            obstacleBatch.End();
        }
    }
}
