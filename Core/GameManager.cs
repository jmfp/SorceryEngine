using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sorcery.Scenes;

namespace Sorcery.Core
{
    //The Game Manager class saves and offers data for the game as a whole for quick access
    public class GameManager
    {
        public List<Scene> allScenes;
        public Scene currentScene;
    }
}