using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProgrammingAssignment5
{
    /// <summary>
    /// An animated explosion object
    /// </summary>
    public class Explosion
    {
        #region Fields

        // object location
        Rectangle drawRectangle;

        // animation strip info
        Texture2D strip;
        int frameWidth;
        int frameHeight;

        // hard-coded animation info. There are better ways to do this,
        // we just don't know enough to use them yet
        const int FRAMES_PER_ROW = 3;
        const int NUM_ROWS = 3;
        const int NUM_FRAMES = 9;

        // fields used to track and draw animations
        Rectangle sourceRectangle;
        int currentFrame;
        const int FRAME_TIME = 10;
        int elapsedFrameTime = 0;

        // playing or not
        bool playing = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new explosion object that immediately starts playing
        /// </summary>
        /// <param name="strip">the sprite strip for the explosion</param>
        /// <param name="x">the x location of the center of the explosion</param>
        /// <param name="y">the y location of the center of the explosion</param>
        public Explosion(Texture2D strip, int x, int y)
        {
            this.strip = strip;

            // calculate frame size
            frameWidth = strip.Width / FRAMES_PER_ROW;
            frameHeight = strip.Height / NUM_ROWS;

            // set initial draw and source rectangles
            drawRectangle = new Rectangle(x - frameWidth / 2, y - frameWidth / 2,
                frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);

            // start playing the animation
            Play();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets whether or not the explosion animation is currently playing
        /// </summary>
        public bool Playing
        {
            get { return playing; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the explosion. This only has an effect if the explosion animation is playing
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime)
        {
            if (playing)
            {
                // check for advancing animation frame
                elapsedFrameTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedFrameTime > FRAME_TIME)
                {
                    // reset frame timer
                    elapsedFrameTime = 0;

                    // advance the animation
                    if (currentFrame < NUM_FRAMES - 1)
                    {
                        currentFrame++;
                        SetSourceRectangleLocation(currentFrame);
                    }
                    else
                    {
                        // reached the end of the animation
                        playing = false;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the explosion. This only has an effect if the explosion animation is playing
        /// </summary>
        /// <param name="spriteBatch">the spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (playing)
            {
                spriteBatch.Draw(strip, drawRectangle, sourceRectangle, Color.White);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Starts playing the animation for the explosion
        /// </summary>
        private void Play()
        {
            // reset tracking values
            playing = true;
            elapsedFrameTime = 0;
            currentFrame = 0;

            // set source rectangle
            SetSourceRectangleLocation(currentFrame);
        }

        /// <summary>
        /// Sets the source rectangle location to correspond with the given frame
        /// </summary>
        /// <param name="frameNumber">the frame number</param>
        private void SetSourceRectangleLocation(int frameNumber)
        {
            // calculate X and Y based on frame number
            sourceRectangle.X = (frameNumber % FRAMES_PER_ROW) * frameWidth;
            sourceRectangle.Y = (frameNumber / FRAMES_PER_ROW) * frameHeight;
        }

        #endregion

    }
}