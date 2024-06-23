using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public class Sprite{
        public Texture2D sprite;
        public Rectangle rect;
        public Vector2 dimensions;

        public Sprite (Texture2D sprite, Vector2 dimensions, Rectangle rect){
            this.sprite = sprite;
            this.rect = rect;
            this.dimensions = dimensions;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position){
            spriteBatch.Draw(sprite, position, rect, Color.White);
        }
    }

    public class SpriteSheet{
        public string name, imagePath;
        public Vector2 dimensions, tileDimensions;
        public GraphicsDevice graphicsDevice;
        List<Rectangle> rects = new List<Rectangle>();
        public SpriteSheet(string name, string imagePath, GraphicsDevice graphicsDevice, Vector2 dimensions, Vector2 tileDimensions){
            this.name = name;
            this.imagePath = imagePath;
            this.dimensions = dimensions;
            this.tileDimensions = tileDimensions;
            this.graphicsDevice = graphicsDevice;
        }

        public Texture2D LoadSpriteSheet(){
            Texture2D sheet = Texture2D.FromFile(graphicsDevice, imagePath);
            return sheet;
        }

        public List<Sprite> SliceSheet(){
            Texture2D sheet = LoadSpriteSheet();
            List<Sprite> list = new List<Sprite>();
            for (int x = 0; x < dimensions.X / tileDimensions.X; x++) {
                for (int y = 0; y < dimensions.Y / tileDimensions.Y; y++) {
                    list.Add(new Sprite(sheet, new Vector2(16, 16), new Rectangle(x * (int)tileDimensions.X, y, (int)tileDimensions.X, (int)tileDimensions.Y)));
                }
            }
            return list;
        }

        public List<Tile> SliceToTile(){
            Texture2D sheet = LoadSpriteSheet();
            List<Tile> list = new List<Tile>();
            for (int x = 0; x < dimensions.X / tileDimensions.X; x++) {
                for (int y = 0; y < dimensions.Y / tileDimensions.Y; y++) {
                    list.Add(new Tile(new Sprite(sheet, new Vector2(16, 16), new Rectangle(x, y-1, (int)tileDimensions.X, (int)tileDimensions.Y))));
                    //list.Add(new Sprite(sheet, new Vector2(16, 16), new Rectangle(x, y-1, (int)tileDimensions.X, (int)tileDimensions.Y)));
                }
            }
            return list;
        }
    }

    public class SpriteStack
    {
        public List<Sprite> spritesInStack;
        float rotation;

        public SpriteStack(SpriteSheet spriteSheet, float rotation)
        {
            this.spritesInStack = spriteSheet.SliceSheet();
            this.rotation = rotation;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int spread = 1, int scale = 2)
        {
            //spriteBatch.Draw(spritesInStack[2].sprite, new Vector2(position.X, position.Y), spritesInStack[2].rect, Color.White);
            for (int i = 0; i < spritesInStack.Count; i++)
            {
                spriteBatch.Draw(spritesInStack[i].sprite, new Vector2(position.X, position.Y - (i * spread)), spritesInStack[i].rect, Color.White, rotation, new Vector2(position.X, position.Y), scale, SpriteEffects.None, 0);
            }
        }
    }
}