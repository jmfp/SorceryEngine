using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using Sorcery.Core;

namespace Sorcery
{
    public class Game3D : GameWindow
    {
        public int width, height;
        //testing primitives
        Triangle3D testTri;
        public Game3D(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default){
            //center screen on monitor
            this.CenterWindow(new Vector2i(width, height));
            this.width = width;
            this.height = height;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            //loading a triangle
            testTri = new Triangle3D();
            testTri.Setup();
            GL.AttachShader(testTri.shaderProgram, testTri.vertexShader);
            GL.AttachShader(testTri.shaderProgram, testTri.fragmentShader);
            GL.LinkProgram(testTri.shaderProgram);

            //delete shaders
            GL.DeleteShader(testTri.vertexShader);
            GL.DeleteShader(testTri.fragmentShader);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.DeleteBuffer(testTri.vbo);
            GL.DeleteVertexArray(testTri.vao);
            GL.DeleteProgram(testTri.shaderProgram);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //called every frame, used for drawing onto the screen

            GL.ClearColor(0f, 0.3f, 1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //draw here

            GL.UseProgram(testTri.shaderProgram);
            GL.BindVertexArray(testTri.vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }
    }

    
}