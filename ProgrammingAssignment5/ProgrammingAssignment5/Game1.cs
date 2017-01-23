using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeddyMineExplosion;

namespace ProgrammingAssignment5
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private const int WINDOW_WIDTH = 800;
        private const int WINDOW_HEIGHT = 600;

        Texture2D bearSprite;
        List<TeddyBear> bears;

        Texture2D mineSprite;
        List<Mine> mines;

        Texture2D explosionSprite;
        List<Explosion> explosions;

        Random rand = new Random();
        private ButtonState PreviousButtonState;

        float spawnDelayTimeMilliSeconds;
        int elapsedSpawnDelayMilliseconds;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Setting resolution
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Adding initialization logic
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            IsMouseVisible = true;

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

            // Loading game content
            bearSprite = Content.Load<Texture2D>(@"graphics/teddybear");
            bears = new List<TeddyBear>();

            mineSprite = Content.Load<Texture2D>(@"graphics/mine");
            mines = new List<Mine>();

            explosionSprite = Content.Load<Texture2D>(@"graphics/explosion");
            explosions = new List<Explosion>();

            spawnDelayTimeMilliSeconds = (float)(rand.Next(1000, 3000));
            elapsedSpawnDelayMilliseconds = 0;
            bears.Add(new TeddyBear(
                            bearSprite,
                            new Vector2((float)(rand.NextDouble() - 0.5), (float)(rand.NextDouble() - 0.5)),
                            WINDOW_WIDTH, WINDOW_HEIGHT));
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            // Adding mines
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Released && PreviousButtonState == ButtonState.Pressed)
            {
                mines.Add(new Mine(mineSprite, mouse.X, mouse.Y));
            }
            PreviousButtonState = mouse.LeftButton;

            // Update creates Bears every 1 up to 3 seconds
            elapsedSpawnDelayMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedSpawnDelayMilliseconds >= spawnDelayTimeMilliSeconds)
            {
                elapsedSpawnDelayMilliseconds = 0;
                spawnDelayTimeMilliSeconds = (float)(rand.Next(1000, 3000));
                bears.Add(new TeddyBear(
                            bearSprite,
                            new Vector2((float)(rand.NextDouble() - 0.5), (float)(rand.NextDouble() - 0.5)),
                            WINDOW_WIDTH, WINDOW_HEIGHT));
            }

            // Update Bears
            for (int i = 0; i < bears.Count; i++)
            {
                bears[i].Update(gameTime);
            }

            // Checking collisions and creating explosions
            for (int j = bears.Count - 1; j >= 0; j--)
            {
                for (int i = mines.Count - 1; i >= 0; i--)
                {
                    if (bears[j].CollisionRectangle.Intersects(mines[i].CollisionRectangle))
                    {
                        bears[j].Active = false;
                        mines[i].Active = false;
                        explosions.Add(new Explosion(explosionSprite,
                            mines[i].CollisionRectangle.X,
                            mines[i].CollisionRectangle.Y));

                        mines.RemoveAt(i);
                        bears.RemoveAt(j);
                        i = 0;
                    }
                }
            }

            // Updating explosions
            foreach (Explosion explosion in explosions)
            {
                explosion.Update(gameTime);
            }

            // Updating gameTime
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SteelBlue);

            spriteBatch.Begin();
            
            // Drawing Bears
            for (int i = 0; i < bears.Count; i++)
            {
                bears[i].Draw(spriteBatch);
            }

            // Drawing Mines
            for (int i = 0; i < mines.Count; i++)
            {
                mines[i].Draw(spriteBatch);
            }

            // Drawing Explosions
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
