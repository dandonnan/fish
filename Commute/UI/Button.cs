namespace Commute.UI
{
    using Commute.Graphics;
    using Commute.Input;
    using Microsoft.Xna.Framework;
    using System;

    /// <summary>
    /// A UI button.
    /// </summary>
    internal class Button
    {
        /// <summary>
        /// The sprite to use as an icon.
        /// </summary>
        protected readonly Sprite icon;

        /// <summary>
        /// The sprite to use as a highlight.
        /// </summary>
        protected readonly Sprite highlight;

        /// <summary>
        /// The action to perform when selecting the button.
        /// </summary>
        protected readonly Action onSelected;

        /// <summary>
        /// The button's bounds.
        /// </summary>
        protected readonly Rectangle bounds;

        /// <summary>
        /// Whether the button is highlighted.
        /// </summary>
        protected bool highlighted;

        /// <summary>
        /// Create a button.
        /// </summary>
        /// <param name="icon">The sprite to use for the button.</param>
        /// <param name="position">The position.</param>
        /// <param name="onSelected">The action when selected.</param>
        /// <param name="highlight">The sprite to use as a highlight.</param>
        public Button(Sprite icon, Vector2 position, Action onSelected, Sprite highlight = null)
        {
            this.icon = icon;
            this.icon.SetPosition(position);

            this.onSelected = onSelected;

            this.highlight = highlight;

            // If there is a highlight, set the position
            if (highlight != null)
            {
                this.highlight.SetPosition(position);
            }
            
            highlighted = false;

            // Set the bounds based on the position and sprite's size
            bounds = new Rectangle((int)position.X, (int)position.Y, icon.GetWidth(), icon.GetHeight());
        }

        /// <summary>
        /// Highlight the button.
        /// </summary>
        /// <param name="highlight">Whether to highlight the button.</param>
        public void Highlight(bool highlight = true)
        {
            highlighted = highlight;
        }

        /// <summary>
        /// Select the button.
        /// </summary>
        public void Select()
        {
            // Call the selected action
            onSelected?.Invoke();
        }

        /// <summary>
        /// Whether the button is being hovered over.
        /// </summary>
        /// <returns>true if hovered, false if not.</returns>
        public bool IsHovered()
        {
            return bounds.Contains(InputManager.GetTouchPosition());
        }

        /// <summary>
        /// Whether the button is being touched.
        /// </summary>
        /// <returns>true if touched, false if not.</returns>
        public bool IsTouched()
        {
            return IsHovered() && InputManager.IsTouched();
        }

        /// <summary>
        /// Update the button.
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Draw the button.
        /// </summary>
        public virtual void Draw()
        {
            icon.Draw();

            if (highlighted)
            {
                highlight.Draw();
            }
        }
    }
}
