﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgrammingAssignment3
{
    /// <summary>
    /// A rock
    /// </summary>
    public class Rock
    {
        #region Fields

        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;

        // moving support
        Vector2 velocity;

        // window containment support
        int windowWidth;
        int windowHeight;
        bool outsideWindow = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite for the rock</param>
        /// <param name="location">location of the center of the rock</param>
        /// <param name="velocity">velocity of the rock</param>
        /// <param name="windowWidth">window width</param>
        /// <param name="windowHeight">window height</param>
        public Rock(Texture2D sprite, Vector2 location, Vector2 velocity,
            int windowWidth, int windowHeight)
        {
            // save window dimensions
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            // save sprite and set draw rectangle
            this.sprite = sprite;
            drawRectangle = new Rectangle((int)location.X - sprite.Width / 2,
                (int)location.Y - sprite.Height / 2, sprite.Width, sprite.Height);

            // save velocity
            this.velocity = velocity;
        }

        public Rock()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Sets the rock's velocity
        /// </summary>
        public Vector2 Velocity
        {
            set
            {
                velocity.X = value.X;
                velocity.Y = value.Y;
            }
        }

        /// <summary>
        /// Gets whether or not the rock is outside the window
        /// </summary>
        public bool OutsideWindow
        {
            get { return outsideWindow; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the rock
        /// </summary>
        /// <param name="gameTime">game time</param>
        public void Update(GameTime gameTime)
        {
            // STUDENTS: Only update the rock if it's inside the window
            if (!OutsideWindow)
            {

                // STUDENTS: Update the rock's location
                drawRectangle.X += (int) (velocity.X*gameTime.ElapsedGameTime.Milliseconds);
                drawRectangle.Y += (int) (velocity.Y*gameTime.ElapsedGameTime.Milliseconds);

                // STUDENTS: Set outsideWindow to true if the rock is outside the window
            }
            else
            {
                outsideWindow = true;
            }

            outsideWindow = drawRectangle.Right < 0 || 
                            drawRectangle.Left > windowWidth || 
                            drawRectangle.Bottom < 0 ||
                            drawRectangle.Top > windowHeight;

        }

        /// <summary>
        /// Draws the rock
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // STUDENTS: Only draw the rock if it's inside the window
            if (!OutsideWindow)
            {
                // STUDENTS: Draw the rock
                spriteBatch.Draw(sprite, drawRectangle, Color.White);
                // Caution: Don't include spriteBatch.Begin or spriteBatch.End here
            }

        }

        #endregion
    }
}
