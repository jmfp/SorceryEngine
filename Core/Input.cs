using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sorcery.Core{
    public class Input : Component{
        public Vector2 movementVector = new Vector2(0, 0);
        public float maxSpeed;
        public float minSpeed;
        public bool isPressed;
        //directions for animator in bools for north, east, south, westt
        public bool[] directions = new bool[4];

        public Input(float maxSpeed, string name="Input") {
            //this.movementVector = movementVector;
            this.maxSpeed = maxSpeed;
            //setting minSpeed to maxSpeed on instantiation so that only if minSpeed < maxSpeed will the player speed up during input
            this.minSpeed = maxSpeed;
            this.name = name;
        }

        public void TopDown8(){
            isPressed = false;
            movementVector = new Vector2(0, 0);
            if(Keyboard.GetState().IsKeyDown(Keys.A)) {
                isPressed = true;
                movementVector.X -= maxSpeed;
                //moving west
                directions[3] = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D)) {
                isPressed = true;
                movementVector.X += maxSpeed;
                //moving east
                directions[1] = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.W)) {
                isPressed = true;
                movementVector.Y -= maxSpeed;
                //moving north
                directions[0] = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S)) {
                isPressed = true;
                movementVector.Y += maxSpeed;
                //moving south
                directions[2] = true;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.A) && isPressed) {
                directions[3] = false;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.D) && isPressed) {
                directions[1] = false;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.W) && isPressed) {
                directions[0] = false;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.S) && isPressed) {
                directions[2] = false;
            }

            parent.position.X += movementVector.X;
            parent.position.Y += movementVector.Y;
        }

        public void TopDown4(){
            isPressed = false;
            movementVector = new Vector2(0, 0);
            if(Keyboard.GetState().IsKeyDown(Keys.A) && !isPressed) {
                isPressed = true;
                movementVector.X -= maxSpeed;
                //moving west
                directions[3] = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D) && !isPressed) {
                isPressed = true;
                movementVector.X += maxSpeed;
                //moving east
                directions[1] = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.W) && !isPressed) {
                isPressed = true;
                movementVector.Y -= maxSpeed;
                //moving north
                directions[0] = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S) && !isPressed) {
                isPressed = true;
                movementVector.Y += maxSpeed;
                //moving south
                directions[2] = true;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.A) && isPressed) {
                directions[3] = false;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.D) && isPressed) {
                directions[1] = false;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.W) && isPressed) {
                directions[0] = false;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.S) && isPressed) {
                directions[2] = false;
            }

            parent.position.X += movementVector.X;
            parent.position.Y += movementVector.Y;
        }

        public void TopDownDrive(float rotationVelocity=3, float linearVelocity=4)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                parent.rotation -= MathHelper.ToRadians(rotationVelocity);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                parent.rotation += MathHelper.ToRadians(rotationVelocity);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                parent.position -= parent.direction * linearVelocity;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                parent.position += parent.direction * linearVelocity;
            }
        }
    }

    

    //makes game objects clickable, has nothing to do with UI
    public interface IClickable{
        public void Click();
    }
}
