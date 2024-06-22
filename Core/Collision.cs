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

    public class SquareCollider : Component
    {
        Rectangle rect;
        Vector2 offset = new Vector2(0, 0);

        public SquareCollider(Texture2D texture, Vector2 offset, string name = "SquareCollider")
        {
            this.rect = new Rectangle(0, 0, texture.Width - (int)Math.Round(offset.X), texture.Height - (int)Math.Round(offset.Y));
            this.name = name;
        }
    }
}