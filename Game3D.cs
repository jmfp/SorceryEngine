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
using OpenTK.Windowing.GraphicsLibraryFramework;
using Sorcery.Core;

namespace Sorcery
{
    public class Game3D : GameWindow
    {
        public int width, height;
        float[] vertices = {
        -0.5f, -0.5f, 0.0f, //Bottom-left vertex
         0.5f, -0.5f, 0.0f, //Bottom-right vertex
         0.0f,  0.5f, 0.0f  //Top vertex
        };
        int VertexBufferObject;
        Shader vertShader, fragShader;
        //testing primitives
        Triangle3D testTri;
        public Game3D(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) {
            //center screen on monitor
            this.CenterWindow(new Vector2i(width, height));
            this.width = width;
            this.height = height;
        }
        //public Game3D(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default){
        //    //center screen on monitor
        //    this.CenterWindow(new Vector2i(width, height));
        //    this.width = width;
        //    this.height = height;
        //}

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
            //shaders
            vertShader = new Shader("Content/Shaders/Default.vert", ShaderType.VertexShader);
            fragShader = new Shader("Content/Shaders/Default.frag", ShaderType.FragmentShader);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            //vao
            int VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            vertShader.Use();
            fragShader.Use();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            //code here
            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            vertShader.Dispose();
            fragShader.Dispose();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //called every frame, used for drawing onto the screen
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            //polling input
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }
    }

    
}