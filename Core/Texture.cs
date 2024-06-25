using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace Sorcery.Core
{
	public class Texture : Texture2D
	{
		GraphicsDevice graphicsDevice;
		Vector2 size;
		string imagePath;

        public Texture(string imagePath, GraphicsDevice graphicsDevice, Vector2 size) : base(graphicsDevice, (int)size.X, (int)size.Y)
		{
			this.graphicsDevice = graphicsDevice;
			this.size = size;
			this.imagePath = imagePath;
		}

		public Texture2D LoadTexture()
		{
			Texture2D newTexture = Texture2D.FromFile(graphicsDevice, Directory.GetCurrentDirectory() + "/" + imagePath);
			return newTexture;
		}
	}
}

