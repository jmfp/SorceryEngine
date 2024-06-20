using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;
using System.Collections.Generic;
using System;
using ImGuiNET;
using Sorcery.Core;
using Sorcery.Scenes;
using OpenTK.Graphics.OpenGL4;

namespace Sorcery;

public class Editor3D : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    bool show_native_examples = true;

    public Scene currentScene = new Scene();
    public Input input;

    GameObject selectedObject;

    //testing textures
    Texture2D testTexture;

    ImGuiRenderer GuiRenderer;
    // Create a new render target
    RenderTarget2D renderTarget;

    public Editor3D()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        //Window.ClientSizeChanged += OnResize;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
            this.Window.Title = "Sorcery 3D";

            GuiRenderer = new ImGuiRenderer(this);
            GuiRenderer.RebuildFontAtlas();
            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;
            renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
            //dockspace_id = ImGui.GetID("MyDockspace");
            //scene management
            //currentScene.gameObjects.Add(new GameObject("New Object", new System.Numerics.Vector3(0, 0, 0)));
            //currentScene.gameObjects.Add(new GameObject("Demon", new System.Numerics.Vector3(0, 0, 0)));
            //currentScene.gameObjects.Add(new GameObject("Tilemap", new System.Numerics.Vector3(0, 0, 0)));
            //currentScene.gameObjects.Add(new GameObject("Game Manager", new System.Numerics.Vector3(0, 0, 0)));
            //currentScene.gameObjects.Add(new GameObject("UI Canvas", new System.Numerics.Vector3(0, 0, 0)));
            //SpriteRenderer spriteRenderer = new SpriteRenderer("Content/Assets/Sprites/demon.png", GraphicsDevice);
            //spriteRenderer.scale = new Vector2(2, 2);
            //currentScene.gameObjects[1].AddComponent(spriteRenderer);
            //currentScene.gameObjects[1].AddComponent(new Input(5));
            //input = currentScene.gameObjects[1].GetComponent<Input>() as Input;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        testTexture = Texture2D.FromFile(GraphicsDevice, "Content/Assets/Sprites/demon.png");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        var kstate = Keyboard.GetState();

        //input.TopDown8(kstate);
        //currentScene.gameObjects[1].position += new Vector3(input.movementVector.X, input.movementVector.Y, 0);
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        DrawSceneToTexture(renderTarget);
        //GraphicsDevice.Clear(Color.CornflowerBlue);
        //spritebatch is a tool provided by monogame. This is an object that can be used for drawing
        //_spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix: camera.Transform, samplerState: SamplerState.PointClamp);
        // TODO: Add your drawing code here
        //_spriteBatch.End();
        // TODO: Add your drawing code here

        //ImGui Begin
        GuiRenderer.BeginLayout(gameTime);
        //creating dockspace to mave tools around
        //ImGui.DockSpace(1, new System.Numerics.Vector2(1920.0f, 1080.0f));
        //ImGui.Image(GraphicsDevice, new System.Numerics.Vector2(1920.0f, 1080.0f));
        ImGui.Begin("Game");
        ImGui.Image(GuiRenderer.BindTexture(renderTarget), new System.Numerics.Vector2(1920, 1080));
        ImGui.Begin("Scene");
        for (int i = 0; i<currentScene.gameObjects.Count; i++){
            //ImGui.Text(currentScene.gameObjects[i].name);
            if(ImGui.Button(currentScene.gameObjects[i].name)){
                selectedObject = currentScene.gameObjects[i];
            }
        }
        
        ImGui.Button("Add Empty Object");
        //ImGui.SliderFloat("Float Slider", ref sliderVal, 0, 100)
        ImGui.Begin("Project");
        ImGui.Begin("Inspector");
        if (selectedObject != null){
            ImGui.Text(selectedObject.name);
            ImGui.SliderFloat("Position X", ref selectedObject.position.X, 0, 10000);
            ImGui.SliderFloat("Position Y", ref selectedObject.position.Y, 0, 10000);
            ImGui.SliderFloat("Position Z", ref selectedObject.position.Z, 0, 10000);
            if(selectedObject.components.Count > 0){
                //list all atttached components
                for (int i = 0; i < selectedObject.components.Count; i++){
                    ImGui.Text($"{selectedObject.components[i].name}");
                }
            }
        }
        //ImGui End
        GuiRenderer.EndLayout();

        base.Draw(gameTime);
        //GetBufferData();
    }
    private void DrawImGuiNativeDemos()
    {
        ImGui.ShowDemoWindow();
    }

    void GetBufferData()
    {
        //GraphicsDevice.GetBackBufferData();
    }

    protected void DrawSceneToTexture(RenderTarget2D renderTarget)
    {
        // Set the render target
        //GraphicsDevice.SetRenderTarget(renderTarget);
        //GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
        // Draw the scene
        //GraphicsDevice.Clear(Color.CornflowerBlue);
        //draw here



        //_spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);
        //_spriteBatch.Draw(testTexture, new Vector2(100, 100), Color.White);
        //SpriteRenderer ren = currentScene.gameObjects[1].GetComponent<SpriteRenderer>() as SpriteRenderer;
        //ren.Draw(_spriteBatch);
        //currentScene.gameObjects[1].components[0].Draw(_spriteBatch);
        //_spriteBatch.End();
        // Drop the render target
        //GraphicsDevice.SetRenderTarget(null);
    }
}
