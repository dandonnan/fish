namespace Commute.Graphics
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    /// <summary>
    /// An abstract sprite definition.
    /// </summary>
    internal abstract class AbstractSprite
    {
        /// <summary>
        /// The sprite's id.
        /// </summary>
        protected string id;

        /// <summary>
        /// The texture.
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// The position.
        /// </summary>
        protected Vector2 position;

        /// <summary>
        /// The rotation.
        /// </summary>
        protected float rotation;

        /// <summary>
        /// The scale.
        /// </summary>
        protected Vector2 scale;

        /// <summary>
        /// The origin point.
        /// </summary>
        protected Vector2 origin;

        /// <summary>
        /// The depth.
        /// </summary>
        protected float depth;

        /// <summary>
        /// The colour.
        /// </summary>
        protected Color colour;

        /// <summary>
        /// A list of frames for animation.
        /// </summary>
        protected List<Frame> frames;

        /// <summary>
        /// Sprite effects to apply.
        /// </summary>
        protected SpriteEffects effects;

        /// <summary>
        /// Create an abstract sprite.
        /// </summary>
        public AbstractSprite()
        {
            // Set values to defaults
            rotation = 0;
            depth = 0;
            scale = Vector2.One;
            origin = Vector2.Zero;
            colour = Color.White;
            effects = SpriteEffects.None;
        }

        /// <summary>
        /// Set the sprite's position.
        /// </summary>
        /// <param name="position">The position.</param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// Set the sprite's scale.
        /// </summary>
        /// <param name="scale">The scale in both axis.</param>
        public void SetScale(float scale)
        {
            this.scale.X = scale;
            this.scale.Y = scale;
        }

        /// <summary>
        /// Set the origin point to the center of the sprite.
        /// </summary>
        public void SetOriginToCenter()
        {
            origin = new Vector2(GetWidth() / 2, GetHeight() / 2);
        }

        /// <summary>
        /// Set the sprite's effects.
        /// </summary>
        /// <param name="effects">The effects.</param>
        public void SetEffects(SpriteEffects effects)
        {
            this.effects = effects;
        }

        /// <summary>
        /// Set the colour of the sprite.
        /// </summary>
        /// <param name="colour">The colour.</param>
        public void SetColour(Color colour)
        {
            this.colour = colour;
        }

        /// <summary>
        /// Set the depth.
        /// </summary>
        /// <param name="depth">The depth.</param>
        public virtual void SetDepth(float depth)
        {
            this.depth = depth;
        }

        /// <summary>
        /// Get the width of the sprite.
        /// </summary>
        /// <returns>The sprite's width.</returns>
        public abstract int GetWidth();

        /// <summary>
        /// Get the height of the sprite.
        /// </summary>
        /// <returns>The sprite's height.</returns>
        public abstract int GetHeight();

        /// <summary>
        /// Update the sprite.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draw the sprite.
        /// </summary>
        public abstract void Draw();
    }
}
