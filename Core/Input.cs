using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sorcery.Core{
    public class Input : Component{
        public Vector2 movementVector = new Vector2(0, 0);
        public float maxSpeed;
        public float minSpeed;
        public bool isPresed;
        public Input(float maxSpeed) {
            //this.movementVector = movementVector;
            this.maxSpeed = maxSpeed;
            //setting minSpeed to maxSpeed on instantiation so that only if minSpeed < maxSpeed will the player speed up during input
            this.minSpeed = maxSpeed;
        }

        public void TopDown8(KeyboardState state){
            if(state.IsKeyDown(Keys.A) && !isPresed) {
                isPresed = true;
                movementVector.X -= maxSpeed;
            }
            if(state.IsKeyDown(Keys.D) && !isPresed) {
                isPresed = true;
                movementVector.X = 1f;
            }
            if(state.IsKeyDown(Keys.W) && !isPresed) {
                isPresed = true;
                movementVector.Y = -1f;
            }
            if(state.IsKeyDown(Keys.S) && !isPresed) {
                isPresed = true;
                movementVector.Y = 1f;
            }
            if(state.IsKeyUp(Keys.A)) {
                isPresed = false;
                movementVector.X = 0f;
            }
            if(state.IsKeyUp(Keys.D)) {
                isPresed = false;
                movementVector.X = 0f;
            }
            if(state.IsKeyUp(Keys.W)) {
                isPresed = false;
                movementVector.Y = 0f;
            }
            if(state.IsKeyUp(Keys.S)) {
                isPresed = false;
                movementVector.Y = 0f;
            }

            parent.position.X += movementVector.X * maxSpeed;
            parent.position.Y += movementVector.Y * maxSpeed;
        }
    }

    //makes game objects clickable, has nothing to do with UI
    public interface IClickable{
        public void Click();
    }
}