using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sorcery.Core
{
    public class Collision2D
    {
        public bool CollisionCheck(Rectangle rect1, Rectangle rect2){
            return rect1.Intersects(rect2);
            //if(rect1.Intersects(rect2)){
            //    return true;
            //}
            //return false;
        }
    }
}