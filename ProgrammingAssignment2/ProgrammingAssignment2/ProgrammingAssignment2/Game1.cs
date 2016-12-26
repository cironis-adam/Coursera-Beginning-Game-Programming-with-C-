using System;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProgrammingAssignment2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int WindowWidth = 800;
        const int WindowHeight = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Variables for three sprites
        Texture2D superman;
        Texture2D batman;
        Texture2D spiderman;

        // Variables for x and y speeds
        int xSpeed = 10;
        int ySpeed = 5;

        // Used to handle generating random values
        Random rand = new Random();
        const int ChangeDelayTime = 1000;
        int elapsedTime = 0;

        // used to keep track of current sprite and location
        Texture2D currentSprite;
        Rectangle drawRectangle = new Rectangle();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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
            // Creates a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load sprites for heroes
            superman = Content.Load<Texture2D>(@"graphics\superman");
            batman = Content.Load<Texture2D>(@"graphics\batman");
            spiderman = Content.Load<Texture2D>(@"graphics\spiderman");

            // Set the currentSprite variable to one of your sprite variables
            currentSprite = superman;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime > ChangeDelayTime)
            {
                elapsedTime = 0;

                // Generate a random number between 0 and 2 inclusive using the rand field I provided
                int spriteNumber = rand.Next(0, 3);

                // Sets current sprite
                if (spriteNumber == 0)
                {
                    currentSprite = superman;
                }
                else if (spriteNumber == 1)
                {
                    currentSprite = batman;
                }
                else if (spriteNumber == 2)
                {
                    currentSprite = spiderman;
                }

                // Sets the drawRectangle.Width and drawRectangle.Height to match the width and height of currentSprite
                drawRectangle.Width = currentSprite.Width;
                drawRectangle.Height = currentSprite.Height;

                // Centers the draw rectangle in the window
                drawRectangle.X = (WindowWidth / 2) - (currentSprite.Width / 2);
                drawRectangle.Y = (WindowHeight / 2) - (currentSprite.Height / 2);

                // Gnerates random numbers  between -4 and 4 inclusive for the x and y speed 
                xSpeed = rand.Next(-4, 5);
                ySpeed = rand.Next(-4, 5);

            }

            // Moves the drawRectangle by the x speed and the y speed
            drawRectangle.X += xSpeed;
            drawRectangle.Y += ySpeed;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draws currentSprite
            spriteBatch.Begin();
            spriteBatch.Draw(currentSprite, drawRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}