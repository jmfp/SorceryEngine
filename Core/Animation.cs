using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sorcery.Core
{
    public class Animation
    {
        public Texture2D[] animationFrames;
        int counter, activeFrame;

        public void Animate(int speed)
        {
            counter++;
            if(counter >= speed)
            {
                activeFrame++;
                if(activeFrame > animationFrames.Length - 1)
                {
                    activeFrame = 0;
                }
            }
        }
    }
}