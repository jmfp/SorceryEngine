using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sorcery.Core
{
    public class TileMap
    {
        //represents the tilemap with
        public Dictionary<Tile, Dictionary<Vector2, int>> tileMap;

        public TileMap (Dictionary<Tile, Dictionary<Vector2, int>> tileMap){
            this.tileMap = tileMap;
        }
    }

    public class Tile{
        public Texture2D image;
        public Vector2 position;
    }

    public class RuleTile : Tile{
        public Dictionary<string, Vector2> directions;
    }
}