# IngredientPathMixing Mod

# KEEP IN MIND THAT THIS IS NOWHERE NEAR FINISHED

## Running the mod

If you just want the mod, just take the *IngredientPathMixing.dll* and throw it in the game


## Using the code

If you're wanting to read/edit this code, first off good luck.

But secondly, you'll need to copy over some .dll's from the game files to get this to work.
I would just upload these, but that's kind of illegal to do, so you'll have to do it yourself.

### Setting up the references

In the project's root folder (same place as this README.md file) create a new folder, I called mine "Libraries" but you can call it whatever you want.
Copy the following .dll's from the game files into that folder.

From the *Potion Craft\BepInEx\core* folder, copy over:
	
	0Harmony.dll
	BepInEx.dll
	BepInEx.Harmony.dll

From the *Potion Craft\Potion Craft_Data\Managed* folder, copy over:
	netstandard.dll
	PotionCraft.Core.dll
	PotionCraft.DataBaseSystem.dll
	PotionCraft.InputSystem.dll
	PotionCraft.ManagerSystem.dll
	PotionCraft.Scripts.dll
	PotionCraft.Settings.dll
	UnityEngine.dll
	UnityEngine.CoreModule.dll
	UnityEngine.Physics2DModule.dll

There's a 50/50 on whether I'll end up using the following .dll's, but if you run into issues, try copying them over as well:
	PotionCraft.DebugInfoCollectorSystem.dll
	PotionCraft.ErrorCatcher.dll
	PotionCraft.SceneLoader.dll

### Post-build events

The project has some post build events set up, these are only there because I am lazy and don't feel like copying over the .dll's manually every time I build the project.
Just delete them, they will almost certainly not work for your machine and throw errors when you build the project.




There's probably a lot more that I need to add to this, but I am very lazy and simply can't be bothered to do it right now.