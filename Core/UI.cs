using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
		SpriteFont font;
		string fontName;
		Game game;

		public Text(string fontName, Game game)
		{
			this.fontName = fontName;
			this.game = game;
			Load();
		}

		public SpriteFont Load()
		{
			font = game.Content.Load<SpriteFont>(fontName);
			return font;
		}

		public void Draw(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
		{
			spriteBatch.DrawString(font, text, position, color);
		}
	}
}

