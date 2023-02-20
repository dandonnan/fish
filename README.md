# There's Always a Bigger Fish
There's Always a Bigger Fish is a PC and Mobile game released in 2022 that was developed
in C# using the MonoGame framework.

## How This Works
The solution contains 4 projects - a game library, and 3 projects that make use of this
library that are deployable to different platforms (each is called Commute because the
game was originally going to be about a character going to work before it changed to
be about fish).

Each platform project - GL, Android and iOS - has a MainGame.cs which is used as an
entry-point. There they create an instance of an implementation of IPlatform for their
specific platform, then pass it through to the GameManager in the library project which
then takes care of everything else.

That means that the library itself doesn't have to have different logic for platform
specific stuff as it knows that any implementation of IPlatform will handle any drastic
differences. Although the only real difference is the save data method is changed slightly
to avoid access issues on mobile, as achievements were never implemented.

While each platform project also has a Content.mgcb file, these go unused as their .csproj
files point to the Content.mgcb in the library instead.

## Projects

### Game Library (Commute)
All the main code for the game is contained in here, in relevant namespaces. GameManager.cs
and Scenes/MainScene.cs are the two main big blocks of code, with the GameManager setting up
a render target to draw the game to, and MainScene containing most of the game logic.

Since it was a small game, there is an AudioLibrary and SpriteLibrary which do hard-coded
loads of assets, and a StringLibrary that uses a set of DefaultStrings which are also
hard-coded. Setting up a content pipeline seemed like overkill.

### OpenGL Project (CommuteGL)
This should build on Windows provided that all packages get resolved (MonoGame might need
to be installed, but it should just work automatically by restoring NuGet packages).

Mac has been untested, but it might complain about fonts (it shouldn't do given the fonts
are just Arial).

### Android Project (CommuteAndroid)
Some private keys for AdMob have been removed. Setting a correct key in AndroidManifest.xml
and AdManager.cs (both in the Android project), and having the Android SDK installed should
allow building and deploying to Android.

### iOS Project (CommuteiOS)
This is untested, but should hopefully build provided that you follow the bajillion steps
needed to deploy to iOS (if using Windows then remote accessing a Mac, signing using XCode
and having a license to deploy to an iOS device).