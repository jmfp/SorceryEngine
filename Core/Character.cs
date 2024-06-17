using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sorcery.Core
{
    //a class for all characters in a game
    public class Character : GameObject, IInteractable
    {
        public string firstName, middleName, lastName;
        public Rectangle rect;

        public Character(string name, Vector3 position): base(name, position){
            this.name = name;
            this.position = position;
        }

        //interactable logic
        public void Do(){
            Console.WriteLine("interacted");
        }
    }
}