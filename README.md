# Asset Bundle after runtime

This project contains ability to build asset bundles after after runtime and also load. This can be useful when creating textures on runtime and then later they need to be converted into asset bundles.

Build scene allows developer to trigger build of asset bundle. Enter play mode first, the script will generate texture. Then play mode automatically stops and triggers editor script which builds asset bundle.

Load scene allows developer to load asset bundle created inside Build scene. Simply hit play and if asset bundle is built before, texture should display on screen. Developer then can load or unload asset bundle while in play mode by using B or U keys.

This project was created for tutorial https://www.youtube.com/watch?v=3uwzxDvXKOU and refactored later as well.
