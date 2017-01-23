using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgrammingAssignment5
{
    /// <summary>
    /// A class for a mine
    /// </summary>
    public class Mine
    {
        #region Fields

        bool active = true;

        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a mine with the given characteristics
        /// </summary>
        /// <param name="sprite">the sprite for the min</param>
        /// <param name="x">the x location of the center of the mine</param>
        /// <param name="y">the y location of the center of the mine</param>
        public Mine(Texture2D sprite, int x, int y)
        {
            this.sprite = sprite;
            drawRectangle = new Rectangle(x - sprite.Width / 2, y - sprite.Height / 2, sprite.Width, sprite.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets whether or not the mine is active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// Gets the collision rectangle for the mine
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Draws the mine
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
    }
}