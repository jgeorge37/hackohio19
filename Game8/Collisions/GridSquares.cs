﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game8.Stuff;
using System.Diagnostics;

namespace Game8.Collisions
{
    class GridSquares
    {
        private ICollidable Avatar;
        private List<ICollidable> obstacles;
        private List<ICollidable> items;

        public GridSquares(Avatar avatar)
        {
            Avatar = avatar;
            obstacles = new List<ICollidable>();
            items = new List<ICollidable>();
        }
        public void AddCollidable(ICollidable collidable)
        {
            if (collidable.HasResponse)
            {
                items.Add(collidable);
            }
            else
            {
                obstacles.Add(collidable);
            }
        }
        public void HandleCollisions()
        {
            //check against all with responses
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].BoundingBox.Intersects(Avatar.BoundingBox))
                {
                    Avatar.CollisionResponse(true);
                    items[j].CollisionResponse(false);
                    //Debug.WriteLine("collide with item");
                    //items.RemoveAt(j);
                }
            }
            //check against all obstacles
            for (int j = 0; j < obstacles.Count; j++)
            {
                if (obstacles[j].BoundingBox.Intersects(Avatar.BoundingBox))
                {
                    Avatar.CollisionResponse(false);
                    //Debug.WriteLine("collide with obstacle");
                    //obstacles.RemoveAt(j);
                }
                for(int i = 0; i < items.Count; i++)
                {
                    if (obstacles[j].BoundingBox.Intersects(items[i].BoundingBox))
                    {
                        items[i].CollisionResponse(true);
                    }
                }
            }
        }
    }
}
