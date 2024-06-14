using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sorcery;


namespace Sorcery.Core
{
    public class Camera2D
    {
        public Matrix Transform { get; private set; }

        public void Follow(GameObject target)
        {
            int width = Editor.screenWidth / 2;
            int height = Editor.screenHeight / 2;
            var position = Matrix.CreateTranslation(-target.position.X - (target.Rectangle.Width / 2), -target.position.Y - (target.Rectangle.Height / 2), 0);
            var offset = Matrix.CreateTranslation(width, height, 0);
            Transform = position * offset;
            //Transform = Matrix.CreateTranslation(-target.position.X - (target.Rectangle.Width / 2), -target.position.Y - (target.Rectangle.Height / 2), 0);
        }
    }
}