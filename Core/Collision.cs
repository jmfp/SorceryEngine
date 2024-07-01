using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
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
        public Rectangle rect;
        Vector2 offset = new Vector2(0, 0);

        public SquareCollider(Texture2D texture, Vector2 offset, string name = "SquareCollider")
        {
            this.rect = new Rectangle(0, 0, texture.Width - (int)Math.Round(offset.X), texture.Height - (int)Math.Round(offset.Y));
            this.name = name;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Primitives2D.DrawRectangle(spriteBatch, rect, Color.LightSeaGreen);
        }

        public void Update()
        {
            rect.X = (int)parent.position.X;
            rect.Y = (int)parent.position.Y;
        }
    }
}