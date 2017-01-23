using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgrammingAssignment5
{
    /// <summary>
    /// A class for a teddy bear
    /// </summary>
    public class TeddyBear
    {
        #region Fields

        bool active = true;
        Random rand = new Random();

        // boundary support
        int windowWidth;
        int windowHeight;

        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;

        // velocity support
        Vector2 velocity = new Vector2(0, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a teddy bear with the given characteristics at a random
        /// location in the window
        /// </summary>
        /// <param name="sprite">the sprite for the teddy bear</param>
        /// <param name="velocity">the velocity vector for the teddy bear</param>
        /// <param name="windowWidth">the width of the game window</param>
        /// <param name="windowHeight">the height of the game window</param>
        public TeddyBear(Texture2D sprite, Vector2 velocity, int windowWidth, int windowHeight)
        {
            this.sprite = sprite;
            this.velocity = velocity;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            // build draw rectangle at random location
            int x = rand.Next(windowWidth - sprite.Width + 1);
            int y = rand.Next(windowHeight - sprite.Height + 1);
            drawRectangle = new Rectangle(x, y, sprite.Width, sprite.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets whether or not the teddy bear is active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// Gets the collision rectangle for the teddy bear
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the teddy bear's location, bouncing if necessary
        /// </summary>
        /// <param name="gameTime">game time</param>
        public void Update(GameTime gameTime)
        {
            if (active)
            {
                // move the teddy bear
                drawRectangle.X += (int)(velocity.X * gameTime.ElapsedGameTime.Milliseconds);
                drawRectangle.Y += (int)(velocity.Y * gameTime.ElapsedGameTime.Milliseconds);

                // bounce if necessary
                BounceTopBottom();
                BounceLeftRight();
            }
        }

        /// <summary>
        /// Draws the teddy bear
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.White);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Bounces the teddy bear off the top and bottom window borders if necessary
        /// </summary>
        private void BounceTopBottom()
        {
            if (drawRectangle.Y < 0)
            {
                // bounce off top
                drawRectangle.Y = 0;
                velocity.Y *= -1;
            }
            else if ((drawRectangle.Y + drawRectangle.Height) > windowHeight)
            {
                // bounce off bottom
                drawRectangle.Y = windowHeight - drawRectangle.Height;
                velocity.Y *= -1;
            }
        }
        /// <summary>
        /// Bounces the teddy bear off the left and right window borders if necessary
        /// </summary>
        private void BounceLeftRight()
        {
            if (drawRectangle.X < 0)
            {
                // bounc off left
                drawRectangle.X = 0;
                velocity.X *= -1;
            }
            else if ((drawRectangle.X + drawRectangle.Width) > windowWidth)
            {
                // bounce off right
                drawRectangle.X = windowWidth - drawRectangle.Width;
                velocity.X *= -1;
            }
        }

        #endregion
    }
}