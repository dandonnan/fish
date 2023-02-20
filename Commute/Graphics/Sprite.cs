namespace Commute.Graphics
{
    using Commute.Extensions;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    /// <summary>
    /// A standard sprite, with no animation.
    /// </summary>
    internal class Sprite : AbstractSprite
    {
        /// <summary>
        /// The sprite's single frame.
        /// </summary>
        private readonly Frame frame;

        /// <summary>
        /// The frame's source rectangle on the texture.
        /// </summary>
        private readonly Rectangle sourceRectangle;

        /// <summary>
        /// An offset from the sprite's depth.
        /// </summary>
        private float depthOffset;

        /// <summary>
        /// Create a new sprite from a texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public Sprite(string texture)
            : base()
        {
            // Load the texture and set the sprite's width and height to match the texture
            this.texture = GameManager.LoadTexture(texture);
            frame = new Frame(0, 0, this.texture.Width, this.texture.Height);
            frames = new List<Frame> { frame };

            depthOffset = 0;
            sourceRectangle = frame.ToRectangle();
            effects = SpriteEffects.None;
        }

        /// <summary>
        /// Create a new sprite from a texture, using a frame to determine which part of
        /// the texture makes up the sprite.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="frame">The frame.</param>
        public Sprite(Texture2D texture, Frame frame)
            : base()
        {
            this.texture = texture;
            this.frame = frame;
            frames = new List<Frame> { frame };
            depthOffset = 0;
            sourceRectangle = frame.ToRectangle();
            effects = SpriteEffects.None;
        }

        /// <summary>
        /// Create a sprite as a copy of an existing sprite.
        /// </summary>
        /// <param name="sprite">The existing sprite.</param>
        public Sprite(Sprite sprite)
        {
            // Set parameters from the original sprite
            texture = sprite.texture;
            frame = sprite.frame.Copy();
            rotation = sprite.rotation;
            scale = sprite.scale;
            origin = sprite.origin;
            depthOffset = sprite.depthOffset;
            sourceRectangle = sprite.sourceRectangle;
            effects = SpriteEffects.None;

            position = Vector2.Zero;
            colour = Color.White;
        }

        /// <summary>
        /// Get the sprite's width.
        /// </summary>
        /// <returns>The width.</returns>
        public override int GetWidth()
        {
            // Get the width of the frame and multiply by the scale
            return (int)(frame.Width * scale.X);
        }

        /// <summary>
        /// Get the sprite's height.
        /// </summary>
        /// <returns>The height.</returns>
        public override int GetHeight()
        {
            // Get the height of the frame and multiply by the scale
            return (int)(frame.Height * scale.Y);
        }

        /// <summary>
        /// Set the sprite's depth.
        /// </summary>
        /// <param name="depth">The new depth.</param>
        public override void SetDepth(float depth)
        {
            // Take into account the offset
            this.depth = depth + depthOffset;

            // Make sure the depth does not go over the max
            if (this.depth > 1)
            {
                this.depth = 1;
            }
        }

        /// <summary>
        /// Update the sprite.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            // Not required as there are no animations
        }

        /// <summary>
        /// Draw the sprite.
        /// </summary>
        public override void Draw()
        {
            GameManager.SpriteBatch.Draw(texture, position, sourceRectangle,
                colour, rotation, origin, scale, effects, depth);
        }
    }
}
