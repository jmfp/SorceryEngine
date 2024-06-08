using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Sorcery.Core{
    public class SpriteRenderer : Component{
        public string imagePath;
        public Texture2D sprite;
        //base dimensions of image in pixels
        public int imageDimensions;
        public Vector2 scale = new Vector2(1, 1);
        Vector2 position;
        public SpriteRenderer(string imagePath, GraphicsDevice graphicsDevice, string name="Sprite Renderer", int imageDimensions = 16){
            this.imageDimensions = imageDimensions;
            this.imagePath = imagePath;
            this.name = name;
            sprite = Texture2D.FromFile(graphicsDevice, imagePath);
        }

        public virtual void Draw(SpriteBatch spriteBatch){
            position = new Vector2(parent.position.X, parent.position.Y);
            spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, (int)scale.X * imageDimensions, (int)scale.Y * imageDimensions), Color.White);
        }
    }
}