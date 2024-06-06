using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;
using ImGuiNET;
using Sorcery.Core;
using Sorcery.Scenes;

namespace Sorcery;

public class Editor : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    bool show_native_examples = true;

    public Scene currentScene = new Scene();

    //testing textures
    Texture2D testTexture;

    ImGuiRenderer GuiRenderer;
    // Create a new render target
    RenderTarget2D renderTarget;

    public Editor()
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
            this.Window.Title = "Sorcery";

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
            currentScene.gameObjects.Add(new GameObject("New Object", new Vector3(0, 0, 0)));
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
        ImGui.Begin("Inspector");
        for (int i = 0; i<currentScene.gameObjects.Count; i++){
            //ImGui.Text(currentScene.gameObjects[i].name);
            ImGui.Selectable(currentScene.gameObjects[i].name);
        }
        //ImGui.SliderFloat("Float Slider", ref sliderVal, 0, 100)
        ImGui.Begin("Project");
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
        GraphicsDevice.SetRenderTarget(renderTarget);
        GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
        // Draw the scene
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(testTexture, new Vector2(100, 100), Color.White);
        _spriteBatch.End();
        // Drop the render target
        GraphicsDevice.SetRenderTarget(null);
    }
}
