namespace Commute.UI
{
    using Commute.Extensions;
    using Commute.Localisation;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    /// <summary>
    /// A bar displayed at the top of the screen.
    /// </summary>
    internal class TopBar
    {
        /// <summary>
        /// The width of the bar.
        /// </summary>
        private const int barWidth = 294;

        /// <summary>
        /// The background colour.
        /// </summary>
        private static Color backgroundColour = new Color(64, 103, 161);

        /// <summary>
        /// The position where the size is displayed.
        /// </summary>
        private static Vector2 sizePosition = new Vector2(175, 20);

        /// <summary>
        /// The position where points are displayed.
        /// </summary>
        private static Vector2 pointsPosition = new Vector2(850, 20);

        /// <summary>
        /// The background.
        /// </summary>
        private readonly Rectangle background;

        /// <summary>
        /// A separator.
        /// </summary>
        private readonly Rectangle separator;

        /// <summary>
        /// The outline of the size bar.
        /// </summary>
        private readonly Rectangle sizeBarOutline;

        /// <summary>
        /// The outline of the points bar.
        /// </summary>
        private readonly Rectangle pointsBarOutline;

        /// <summary>
        /// The font.
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        /// A list of labels and their positions.
        /// </summary>
        private readonly List<KeyValuePair<string, Vector2>> labels;

        /// <summary>
        /// The size bar.
        /// </summary>
        private Rectangle sizeBar;

        /// <summary>
        /// The points bar.
        /// </summary>
        private Rectangle pointsBar;

        /// <summary>
        /// The colour of the points bar.
        /// </summary>
        private Color pointsBarColour;

        /// <summary>
        /// The points.
        /// </summary>
        private string points;

        /// <summary>
        /// The size.
        /// </summary>
        private string size;

        /// <summary>
        /// Create a new top bar.
        /// </summary>
        public TopBar()
        {
            background = new Rectangle(0, 0, GameManager.UiResolutionWidth, 130);

            separator = new Rectangle(0, 130, GameManager.UiResolutionWidth, 10);

            font = GameManager.LoadFont("CounterFont");

            points = "0";
            size = "1";

            pointsBarColour = Color.White;

            // Create the outlines at specific positions
            sizeBarOutline = new Rectangle(250, 20, 300, 45);

            pointsBarOutline = new Rectangle(1100, 20, 300, 45);

            // Create bars inside the outlines
            sizeBar = new Rectangle(253, 23, 0, 42);

            pointsBar = new Rectangle(1103, 23, 0, 42);

            // Set labels at specific positions
            labels = new List<KeyValuePair<string, Vector2>>
            {
                new KeyValuePair<string, Vector2>(StringLibrary.GetString("Size"), new Vector2(30, 20)),
                new KeyValuePair<string, Vector2>(StringLibrary.GetString("Points"), new Vector2(690, 20))
            };
        }

        /// <summary>
        /// Update the top bar.
        /// </summary>
        /// <param name="points">The points value.</param>
        /// <param name="scale">The scale value.</param>
        /// <param name="fishToNextScale">The number of fish required to the next scale.</param>
        /// <param name="doublePoints">The double points timer.</param>
        /// <param name="bubblesBurst">The number of bubbles burst.</param>
        public void Update(int points, int scale, int fishToNextScale, double doublePoints, int bubblesBurst)
        {
            // Update text values
            this.points = points.ToString();
            size = scale.ToString();

            // Change the size bar's width depending on how many fish have been eaten
            sizeBar.Width = (int)(barWidth * ((Scales.GetFishToEat(scale) - fishToNextScale) / (float)Scales.GetFishToEat(scale)));

            // Change the points bar colour and width depending on if double points is active 
            if (doublePoints > 0)
            {
                pointsBarColour = Color.Gold;
                pointsBar.Width = (int)(barWidth * (doublePoints / 10000));
            }
            else
            {
                pointsBarColour = Color.White;
                pointsBar.Width = (int)(barWidth * (bubblesBurst / (float)10));
            }
        }

        /// <summary>
        /// Draw the bar.
        /// </summary>
        public void Draw()
        {
            GameManager.SpriteBatch.FillRectangle(background, backgroundColour);
            GameManager.SpriteBatch.FillRectangle(separator, Color.White);

            GameManager.SpriteBatch.DrawString(font, points, pointsPosition, Color.White);
            GameManager.SpriteBatch.DrawString(font, size, sizePosition, Color.White);

            GameManager.SpriteBatch.DrawRectangle(sizeBarOutline, Color.White, 3);
            GameManager.SpriteBatch.DrawRectangle(pointsBarOutline, Color.White, 3);

            GameManager.SpriteBatch.FillRectangle(sizeBar, Color.White);
            GameManager.SpriteBatch.FillRectangle(pointsBar, pointsBarColour);

            labels.ForEach(l => GameManager.SpriteBatch.DrawString(font, l.Key, l.Value, Color.White));
        }
    }
}
