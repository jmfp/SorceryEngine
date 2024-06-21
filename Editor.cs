using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;
using System.Collections.Generic;
using System;
using ImGuiNET;
using Sorcery.Core;
using Sorcery.Scenes;
using System.Reflection;

namespace Sorcery;

public class Editor : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    bool show_native_examples = true, isResizing;

    //listing fields for selected component
    public FieldInfo[] fields;

    public Scene currentScene = new Scene();
    //Game Manager controls quite a bit of logic under the hood
    public GameManager gameManager = new GameManager();
    TileMapEditor tileMapEditor;
    public Input input;
    public static int screenWidth, screenHeight;
    Vector2 mousePosition;
    bool leftClick, rightClick;
    //tteting spritesheets
    List<Sprite> sprites;

    GameObject selectedObject;

    //testing camera
    Camera2D camera;

    //testing textures
    Texture2D testTexture;

    ImGuiRenderer GuiRenderer;
    // Create a new render target
    RenderTarget2D renderTarget;
    Rectangle renderDestination;

    public Editor()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnResize;
    }

    void OnResize(object sender, EventArgs e){
        if (!isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0){
            isResizing = true;
            CalculateRenderDestination();
            isResizing = false;
        }
    }

    protected override void Initialize()
    {
            screenWidth = _graphics.PreferredBackBufferWidth;
            screenHeight = _graphics.PreferredBackBufferHeight;
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
            currentScene.gameObjects.Add(new GameObject("New Object", new System.Numerics.Vector3(0, 0, 0)));
            currentScene.gameObjects.Add(new GameObject("Demon", new System.Numerics.Vector3(0, 0, 0)));
            currentScene.gameObjects.Add(new GameObject("Tilemap", new System.Numerics.Vector3(0, 0, 0)));
            currentScene.gameObjects.Add(new GameObject("Game Manager", new System.Numerics.Vector3(0, 0, 0)));
            currentScene.gameObjects.Add(new GameObject("UI Canvas", new System.Numerics.Vector3(0, 0, 0)));
            SpriteRenderer spriteRenderer = new SpriteRenderer("Content/Assets/Sprites/demon.png", GraphicsDevice);
            spriteRenderer.scale = new Vector2(2, 2);
            currentScene.gameObjects[1].AddComponent(spriteRenderer);
            currentScene.gameObjects[1].AddComponent(new Input(1));

            //testing spritesheets
            SpriteSheet testSheet = new SpriteSheet("test", "Content/Assets/Sprites/techplat1.png", GraphicsDevice, new Vector2(48, 64), new Vector2(16, 16));
            sprites = testSheet.SliceSheet();
            input = currentScene.gameObjects[1].GetComponent<Input>() as Input;

            //testing camera
            camera = new Camera2D();

            //testing tilemaps
            TileMap tileMap= new TileMap("Tilemap");
            currentScene.gameObjects[2].AddComponent(tileMap);
            tileMapEditor = new TileMapEditor(currentScene.gameObjects[2].GetComponent<TileMap>() as TileMap);
            currentScene.tiles = testSheet.SliceToTile();
            tileMapEditor.SelectTile(currentScene.tiles[0]);
            Console.WriteLine(currentScene.tiles.Count);
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

        input.TopDown8();
        //currentScene.gameObjects[1].position += new Vector3(input.movementVector.X, input.movementVector.Y, 0);
        //getting mouse position
        mousePosition = Mouse.GetState().Position.ToVector2();
        Vector2 relativeMousePos = Vector2.Transform(mousePosition, Matrix.Invert(camera.Transform));
        //Console.WriteLine(relativeMousePos);
        if (Mouse.GetState().LeftButton == ButtonState.Pressed && !leftClick){
            leftClick = true;
        }
        if (Mouse.GetState().RightButton == ButtonState.Pressed && !rightClick){
            rightClick = true;
        }
        if (Mouse.GetState().LeftButton != ButtonState.Pressed){
            leftClick = false;
        }
        if (Mouse.GetState().RightButton != ButtonState.Pressed){
            rightClick = false;
        }
        //testing out tilemap editor
        tileMapEditor.Update(relativeMousePos, leftClick, rightClick);
        camera.Follow(currentScene.gameObjects[1]);

        

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
        ImGui.Image(GuiRenderer.BindTexture(renderTarget), new System.Numerics.Vector2(renderDestination.Width, renderDestination.Height));
        ImGui.Begin("Scene");
        for (int i = 0; i<currentScene.gameObjects.Count; i++){
            //ImGui.Text(currentScene.gameObjects[i].name);
            if(ImGui.Button(currentScene.gameObjects[i].name)){
                selectedObject = currentScene.gameObjects[i];
            }
        }
        
        if(ImGui.Button("Add Empty Object")){
            currentScene.gameObjects.Add(new GameObject("Empty Game Object", new System.Numerics.Vector3(0, 0, 0)));
        };
        //ImGui.SliderFloat("Float Slider", ref sliderVal, 0, 100)
        ImGui.Begin("Project");
        ImGui.Begin("Inspector");
        if (selectedObject != null){
            ImGui.Text(selectedObject.name);
            ImGui.InputFloat("Position X", ref selectedObject.position.X);
            ImGui.InputFloat("Position Y", ref selectedObject.position.Y);
            ImGui.InputFloat("Position Z", ref selectedObject.position.Z);
            if(selectedObject.components.Count > 0){
                //list all atttached components
                for (int i = 0; i < selectedObject.components.Count; i++){
                    if(ImGui.Button($"{selectedObject.components[i].name}")){
                        var fieldValues = selectedObject.components[i].GetType().
                            GetFields();
                        fields = fieldValues;
                        //Console.WriteLine(fields[1].FieldType);
                    }
                }
            }

            if (fields != null && fields.Length > 0)
            {
                for (int x = 0; x < fields.Length; x++)
                {
                    if (fields[x].FieldType == typeof(float))
                    {
                        ImGui.InputFloat(fields[x].Name, ref selectedObject.position.X);
                    }
                    if (fields[x].FieldType == typeof(string))
                    {
                        ImGui.Text(fields[x].Name);
                    }
                }
            }
            //if(ImGui.Button("Add Component"))
            //{
            //    selectedObject.components.Add()
            //}
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

    public void CalculateRenderDestination(){
        Point size = GraphicsDevice.Viewport.Bounds.Size;

        //scale on x axis
        float scaleX = size.X / renderTarget.Width;
        //y axis
        float scaleY = size.Y / renderTarget.Height;

        float scale = Math.Min(scaleX, scaleY);
        renderDestination.Width = (int)(renderTarget.Width * scale);
        renderDestination.Height = (int)(renderTarget.Height * scale);
        renderDestination.X = (size.X - renderDestination.Width) / 2;
        renderDestination.X = (size.Y - renderDestination.Height) / 2;
    }

    protected void DrawSceneToTexture(RenderTarget2D renderTarget)
    {
        // Set the render target
        GraphicsDevice.SetRenderTarget(renderTarget);
        GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
        // Draw the scene
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix: camera.Transform, samplerState: SamplerState.PointClamp);
        //draw tilemap
        TileMap tMap = currentScene.gameObjects[2].GetComponent<TileMap>() as TileMap;
        tMap.Draw(_spriteBatch);
        _spriteBatch.Draw(testTexture, new Vector2(100, 100), Color.White);
        sprites[1].Draw(_spriteBatch, new Vector2(116, 116));
        SpriteRenderer ren = currentScene.gameObjects[1].GetComponent<SpriteRenderer>() as SpriteRenderer;
        ren.Draw(_spriteBatch);
        //currentScene.gameObjects[1].components[0].Draw(_spriteBatch);
        _spriteBatch.End();
        // Drop the render target
        GraphicsDevice.SetRenderTarget(null);
    }
}
