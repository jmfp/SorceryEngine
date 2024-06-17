using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sorcery.Core
{
    public class Player : Character
    {
        public Player(string name, Vector3 position): base(name, position){
            this.name = name;
            this.position = position;
        }
    }
}