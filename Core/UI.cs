using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpriteFontPlus;
using System.IO;

namespace Sorcery.Core
{
	public class UI
	{
		public UI()
		{
		}
	}

	public class Text
	{
		public SpriteFont font;
		string fontName;
		GraphicsDevice graphicsDevice;
		public int fontSize;


        public Text(string fontName, GraphicsDevice graphicsDevice, int fontSize = 24)
		{
			this.fontName = fontName;
			this.graphicsDevice = graphicsDevice;
			this.fontSize = fontSize;
			Load();
		}

		public SpriteFont Load()
		{
            var fontBakeResult = TtfFontBaker.Bake(File.ReadAllBytes(@"Content/Assets/Fonts/" + fontName + ".ttf"),

                fontSize,
                1024,
                1024,
                new[]
                {
                    CharacterRange.BasicLatin,
                    CharacterRange.Latin1Supplement,
                    CharacterRange.LatinExtendedA,
                    CharacterRange.Cyrillic
                });
            font = fontBakeResult.CreateSpriteFont(graphicsDevice);
            return font;

			
		}

		public void Draw(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
		{
			spriteBatch.DrawString(font, text, position, color);
		}
	}
}

