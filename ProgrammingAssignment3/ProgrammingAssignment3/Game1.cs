﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProgrammingAssignment3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWidth = 800;
        const int WindowHeight = 600;

        Random rand = new Random();
        Vector2 centerLocation = new Vector2(
            WindowWidth / 2, WindowHeight / 2);

        // STUDENTS: declare variables for 3 rock sprites
        Texture2D greenRockSprite;
        Texture2D magentaRockSprite;
        Texture2D whiteRockSprite;

        // STUDENTS: declare variables for 3 rocks
        Rock greenRock;
        Rock magentaRock;
        Rock whiteRock;

        // delay support
        const int TotalDelayMilliseconds = 1000;
        int elapsedDelayMilliseconds = 0;

        // random velocity support
        const float BaseSpeed = 0.15f;
        Vector2 upLeft = new Vector2(-BaseSpeed, -BaseSpeed);
        Vector2 upRight = new Vector2(BaseSpeed, -BaseSpeed);
        Vector2 downRight = new Vector2(BaseSpeed, BaseSpeed);
        Vector2 downLeft = new Vector2(-BaseSpeed, BaseSpeed);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // change resolution
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
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

            // STUDENTS: Load content for 3 sprites
            greenRockSprite = Content.Load<Texture2D>(@"graphics\greenrock");
            magentaRockSprite = Content.Load<Texture2D>(@"graphics\magentarock");
            whiteRockSprite = Content.Load<Texture2D>(@"graphics\whiterock");

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
                Exit();

            // STUDENTS: update rocks
            if (greenRock != null)
            {
                greenRock.Update(gameTime);
            }

            if (magentaRock != null)
            {
                magentaRock.Update(gameTime);
            }

            if (whiteRock != null)
            {
                whiteRock.Update(gameTime);    
            }

            // update timer
            elapsedDelayMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedDelayMilliseconds >= TotalDelayMilliseconds)
            {
                // STUDENTS: timer expired, so spawn new rock if fewer than 3 rocks in window
                // Call the GetRandomRock method to do this
                if (greenRock == null)
                {
                    greenRock = GetRandomRock();
                }
                else if (magentaRock == null)
                {
                    magentaRock = GetRandomRock();
                }
                else if (whiteRock == null)
                {
                    whiteRock = GetRandomRock();
                }

                // restart timer
                elapsedDelayMilliseconds = 0;
            }

            // STUDENTS: Check each rock to see if it's outside the window. If so
            // spawn a new random rock for it by calling the GetRandomRock method
            // Caution: Only check the property if the variable isn't null
            if (greenRock != null && greenRock.OutsideWindow == true)
            {
                greenRock = GetRandomRock();
            }

            if (magentaRock != null && magentaRock.OutsideWindow == true)
            {
                magentaRock = GetRandomRock();
            }

            if (whiteRock != null && whiteRock.OutsideWindow == true)
            {
                whiteRock = GetRandomRock();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // STUDENTS: draw rocks
            spriteBatch.Begin();

            if (greenRock != null)
            {
                greenRock.Draw(spriteBatch);
            }

            if (magentaRock != null)
            {
                magentaRock.Draw(spriteBatch);
            }

            if (whiteRock != null)
            {
                whiteRock.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets a rock with a random sprite and velocity
        /// </summary>
        /// <returns>the rock</returns>
        private Rock GetRandomRock()
        {
            // STUDENTS: Uncomment and complete the code below to randomly pick a rock sprite by calling the GetRandomSprite method
            Texture2D sprite = GetRandomSprite();

            // STUDENTS: Uncomment and complete the code below to randomly pick a velocity by calling the GetRandomVelocity method
            Vector2 velocity = GetRandomVelocity();

            // STUDENTS: After completing the two lines of code above, delete the following two lines of code
            // They're only included so the code I provided to you compiles
            //Texture2D sprite = null;
            //Vector2 velocity = Vector2.Zero;

            // return a new rock, centered in the window, with the random sprite and velocity
            return new Rock(sprite, centerLocation, velocity, WindowWidth, WindowHeight);
        }

        /// <summary>
        /// Gets a random sprite
        /// </summary>
        /// <returns>the sprite</returns>
        private Texture2D GetRandomSprite()
        {
            // STUDENTS: Uncommment and modify the code below as appropriate to return 
            // a random sprite
            int spriteNumber = rand.Next(0, 3);
            if (spriteNumber == 0)
            {
                return greenRockSprite;
            }
            else if (spriteNumber == 1)
            {
                return magentaRockSprite;
            }
            else
            {
                return whiteRockSprite;
            }

            // STUDENTS: After completing the code above, delete the following line of code
            // It's only included so the code I provided to you compiles
            //return null;
        }

        /// <summary>
        /// Gets a random velocity
        /// </summary>
        /// <returns>the velocity</returns>
        private Vector2 GetRandomVelocity()
        {
            // STUDENTS: Uncommment and modify the code below as appropriate to return 
            // a random velocity
            int velocityNumber = rand.Next(0, 4);
            if (velocityNumber == 0)
            {
                return upLeft;
            }
            else if (velocityNumber == 1)
            {
                return upRight;
            }
            else if (velocityNumber == 2)
            {
                return downRight;
            }
            else
            {
                return downLeft;
            }

            // STUDENTS: After completing the code above, delete the following line of code
            // It's only included so the code I provided to you compiles
            //return Vector2.Zero;
        }
    }
}
