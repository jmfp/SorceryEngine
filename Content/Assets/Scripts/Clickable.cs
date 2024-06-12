using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sorcery.Core;

namespace Sorcery.Content.Assets.Scripts
{
    public class Clickable : IClickable
    {
        public string test= "Worked";

        public void Click(){
            Console.WriteLine(test);
        }


    }
}