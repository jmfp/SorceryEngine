using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Sorcery.Core;
using Sorcery.Scenes;
using OpenTK.Graphics.OpenGL4;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;

namespace Sorcery
{
	public class Game2D : Game
	{
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        bool show_native_examples = true, isResizing;

        //listing fields for selected component
        public FieldInfo[] fields;

        public Scene currentScene = new Scene();
        //Game Manager controls quite a bit of logic under the hood
        public GameManager gameManager = new GameManager();
        TileMapEditor tileMapEditor;
        public Input input;
        public static int screenWidth, screenHeight;
        public Game2D()
		{
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        void OnResize(object sender, EventArgs e)
        {
            if (!isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
            {
                isResizing = true;
                //CalculateRenderDestination();
                isResizing = false;
            }
        }

    }
}

