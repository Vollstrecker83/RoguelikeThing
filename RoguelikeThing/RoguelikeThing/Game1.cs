using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoguelikeThing
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region Member Variables
        Viewport viewport;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        static Player player;
        static Terrain map;
        #endregion

        #region Accessors/Mutators
        static public Player Player { get { return player; } }
        public static Terrain Map { get { return map; } }
        public Viewport GetViewport { get { return viewport; } }


        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            viewport = graphics.GraphicsDevice.Viewport;
            camera = Camera.GetCamera;
            TerrainManager.TileSize = new Point(64, 64);
            TerrainManager.MapList.Add(1, new Terrain(1));
            bool gotMap = TerrainManager.MapList.TryGetValue(1, out map);
            if (!gotMap)
                throw new Exception("Failed to retrieve map in Game1::Initialize()");
            player = new Player();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player.ObjectTexture = Content.Load<Texture2D>("shittyPlayer");

            bool gotMap = TerrainManager.MapList.TryGetValue(1, out map);

            if (!gotMap)
                throw new Exception("Failed to retrieve map from Terrain Manager in LoadContent!");

            foreach (Tile tempTile in map.TileSet)
            {
                switch (tempTile.FloorType)
                {
                    case Terrain.GroundType.Dirt:
                        tempTile.ObjectTexture = Content.Load<Texture2D>("shittyDirt");
                        break;
                    case Terrain.GroundType.Floor:
                        tempTile.ObjectTexture = Content.Load<Texture2D>("shittyFloor");
                        break;
                    case Terrain.GroundType.Water:
                        tempTile.ObjectTexture = Content.Load<Texture2D>("shittyWater");
                        break;
                    default:
                        System.Console.WriteLine("Failed to assign a texture for tile at " + tempTile.DrawPosition.X + ", "
                            + tempTile.DrawPosition.Y + "!");
                        break;
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            InputController.GetInputController.GiveTime(gameTime);
            Camera.GetCamera.GiveTime(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cameraTransform);
            spriteBatch.Begin();

            // Draw the terrain first, as it is the "bottom" layer
            foreach (Tile tempTile in map.TileSet)
            {
                Vector2 origin = new Vector2(0, 0);
                spriteBatch.Draw(tempTile.ObjectTexture, tempTile.DrawRectangle, null, Color.White, 0, origin, tempTile.TileEffects, 0.0f);
            }

            spriteBatch.Draw(player.ObjectTexture, player.DrawRectangle, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public virtual void GiveTime(GameTime gameTime)
        { }
    }
}
