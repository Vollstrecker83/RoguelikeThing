using System;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        Terrain map;
        double lastUpdate;
        int movementTimeLimit;
        bool moveAllowed;
        private static Point tileSize;
        #endregion

        #region Accessors/Mutators
        public Player Player { get => player; set => player = value; }
        protected static Point TileSize { get => tileSize; set => tileSize = value; }
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
            tileSize = new Point(64, 64);
            map = new Terrain(0);
            player = new Player(tileSize, map.MapSize);
            movementTimeLimit = 500;
            moveAllowed = true;

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

            // We need to have a movement rate limiter in place for free movement or people will just fly across the map.
            if (gameTime.TotalGameTime.TotalMilliseconds - lastUpdate >= movementTimeLimit && !moveAllowed)
            {
                moveAllowed = true;
            }

            // It seems like Update() runs once at least before Initialize(), so protect against null objects
            if (player != null)
            {
                if (moveAllowed)
                {
                    bool moved = player.ProcessPlayerMovement(player.GridPosition, Keyboard.GetState(), map);

                    if (moved)
                    {
                        // Note the time at which we processed movement, set movement flag to false
                        lastUpdate = gameTime.TotalGameTime.TotalMilliseconds;
                        moveAllowed = false;
                    }
                }
            }

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
    }
}
