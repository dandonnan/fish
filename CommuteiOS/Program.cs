using System;
using Foundation;
using UIKit;

namespace CommuteiOS
{
    // Auto-generated when creating the iOS project.
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static MainGame game;

        internal static void RunGame()
        {
            game = new MainGame();
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}
