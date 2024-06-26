using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
            sprite = Texture2D.FromFile(graphicsDevice, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), imagePath));
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
            this.imagePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), imagePath);
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
            for (int x = 0; x < (dimensions.X * tileDimensions.X) / tileDimensions.X; x++) {
                for (int y = 0; y < (dimensions.Y * tileDimensions.Y) / tileDimensions.Y; y++) {
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
                    list.Add(new Tile(new Sprite(sheet, new Vector2(16, 16), new Rectangle(x, y, (int)tileDimensions.X, (int)tileDimensions.Y))));
                    //list.Add(new Sprite(sheet, new Vector2(16, 16), new Rectangle(x, y-1, (int)tileDimensions.X, (int)tileDimensions.Y)));
                }
            }
            return list;
        }
    }

    public class SpriteStack : GameObject
    {
        public List<Sprite> spritesInStack;
        //the initial rotation for a sprite stack is for direction.
        //i.e a sprite stack consisting of all sprites facing west would have
        //an initial rotation of 180
        float spread, initialRotation;
        int scale;

        public SpriteStack(SpriteSheet spriteSheet, float rotation, float initialRotation, float spread = 2, int scale = 1) : base("Sprite Stack", new Vector3(0,0,0))
        {
            this.spritesInStack = spriteSheet.SliceSheet();
            this.rotation = rotation;
            this.spread = spread;
            this.scale = scale;
            this.initialRotation = initialRotation;
        }

        public void Rotate(float rotation)
        {
            this.rotation += rotation;
        }

        public Vector3 GetDirection()
        {
            return direction;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            this.position.X = position.X;
            this.position.Y = position.Y;
            //changing direction based on rotation
            direction = new Vector3((float)Math.Cos(MathHelper.ToRadians(initialRotation) - rotation), -(float)Math.Sin(MathHelper.ToRadians(initialRotation) - rotation), 0);
            //spriteBatch.Draw(spritesInStack[2].sprite, new Vector2(position.X, position.Y), spritesInStack[2].rect, Color.White);
            for (int i = 0; i < spritesInStack.Count; i++)
            {
                spriteBatch.Draw(spritesInStack[i].sprite, new Vector2(position.X, position.Y - (i * spread)), spritesInStack[i].rect, Color.White, rotation, new Vector2(spritesInStack[i].rect.Width / 2, spritesInStack[i].rect.Height / 2), scale, SpriteEffects.None, 0);
            }
        }
    }
}