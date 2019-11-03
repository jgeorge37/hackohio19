using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game8
{
    public interface ICollidable
    {
        Rectangle BoundingBox { get; }

        void CollisionResponse(bool isItem);
        bool HasResponse { get; }
    }
}
