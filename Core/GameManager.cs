using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sorcery.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sorcery.Core
{
    //The Game Manager class saves and offers data for the game as a whole for quick access
    public class GameManager
    {
        public List<Scene> allScenes;
        public Scene currentScene;
        public Vector3 playerPosition;
        //the layer the player is rendered on
        public int playerRenderLayer = 3;
        //The tilemap editor
        //public TileMapEditor tileMapEditor = new TileMapEditor();
    }
}