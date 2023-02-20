namespace Commute.UI
{
    using Commute.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    /// <summary>
    /// A button to use on the options screen.
    /// </summary>
    internal class OptionButton : Button
    {
        /// <summary>
        /// The offset of the left arrow.
        /// </summary>
        private static Vector2 leftArrowOffset = new Vector2(-75, 64);

        /// <summary>
        /// The offset of the right arrow.
        /// </summary>
        private static Vector2 rightArrowOffset = new Vector2(281, 64);

        /// <summary>
        /// The left arrow button.
        /// </summary>
        private readonly Button leftArrow;

        /// <summary>
        /// The right arrow button.
        /// </summary>
        private readonly Button rightArrow;

        /// <summary>
        /// The font to use for text.
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        /// A method to perform when the option changes.
        /// </summary>
        private readonly Func<int, string> updateMethod;

        /// <summary>
        /// The x co-ordinate.
        /// </summary>
        private readonly int x;

        /// <summary>
        /// The position of the option's value.
        /// </summary>
        private Vector2 valuePosition;

        /// <summary>
        /// The option's value.
        /// </summary>
        private string optionValue;

        /// <summary>
        /// Create an option button.
        /// </summary>
        /// <param name="icon">The sprite to use for the button.</param>
        /// <param name="position">The position.</param>
        /// <param name="updateMethod">The method to call when the value is updated.</param>
        /// <param name="highlight">The sprite to use as a highlight.</param>
        public OptionButton(Sprite icon, Vector2 position, Func<int, string> updateMethod, Sprite highlight = null)
            : base(icon, position, null, highlight)
        {
            font = GameManager.LoadFont("OptionsFont");

            x = (int)position.X;

            this.updateMethod = updateMethod;

            valuePosition = new Vector2(position.X, position.Y + 180);

            optionValue = updateMethod(0);

            CenterText();

            leftArrow = new Button(SpriteLibrary.GetSprite("ArrowLeft"), position + leftArrowOffset, null, SpriteLibrary.GetSprite("ArrowLeftHighlight"));
            rightArrow = new Button(SpriteLibrary.GetSprite("ArrowRight"), position + rightArrowOffset, null, SpriteLibrary.GetSprite("ArrowRightHighlight"));
        }

        /// <summary>
        /// Change the value.
        /// </summary>
        /// <param name="amount">The amount to change the value by.</param>
        public void ChangeValue(int amount)
        {
            optionValue = updateMethod(amount);

            CenterText();
        }

        /// <summary>
        /// Update the button.
        /// </summary>
        public override void Update()
        {
            // Change the value down when the left arrow is touched
            if (leftArrow.IsTouched())
            {
                ChangeValue(-1);
            }

            // Change the value up when the right arrow is touched
            if (rightArrow.IsTouched())
            {
                ChangeValue(1);
            }

            // Change the highlights on the arrow buttons
            if (leftArrow.IsHovered())
            {
                leftArrow.Highlight();
            }
            else
            {
                leftArrow.Highlight(false);
            }

            if (rightArrow.IsHovered())
            {
                rightArrow.Highlight();
            }
            else
            {
                rightArrow.Highlight(false);
            }
        }

        /// <summary>
        /// Draw the button.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            GameManager.SpriteBatch.DrawString(font, optionValue, valuePosition, Color.White);

            leftArrow.Draw();
            rightArrow.Draw();
        }

        /// <summary>
        /// Center the text that displays the value.
        /// </summary>
        private void CenterText()
        {
            valuePosition.X = x + ((icon.GetWidth() - font.MeasureString(optionValue).X) / 2);
        }
    }
}
