using BepInEx;
using BepInEx.Logging;
using PotionCraft.InputSystem;
using PotionCraft.ManagersSystem.Cursor;
using PotionCraft.ManagersSystem;
using PotionCraft.ScriptableObjects.Ingredient;
using PotionCraft.Settings;
using System;
using System.Linq;
using UnityEngine;
using PotionCraft.ObjectBased.Stack;
using System.Collections.Generic;
using ElixerSpace;


namespace IngredientMixing
{
	[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
	public class IngredientMixing : BaseUnityPlugin
	{
		public const string PLUGIN_GUID = "game.itsgraytheguman.potioncraft.ingredientmixing";
		public const string PLUGIN_NAME = "Ingredient Mixing";
		public const string PLUGIN_VERSION = "1.0.0.0";


		private DebugHelper debug;


		public void Awake()
		{
			/*
             * Harmony harmony = new Harmony(PLUGIN_GUID);
             * 
             * MethodInfo original = AccessTools.Method(typeof( [Game Class] ), [Original Method] );
             * MethodInfo patch = AccessTools.Method(typeof( [Mod Class] ), [Patched Method] );
             * 
             * harmony.Patch(original, new HarmonyMethod(patch));
             */


			debug = new DebugHelper();
		}

		public void Update()
		{


			debug.Update();
		}

	}


	public class DebugHelper
	{
		private ManualLogSource debugLog;


		public DebugHelper()
		{
			debugLog = BepInEx.Logging.Logger.CreateLogSource("DebugLog");
			debugLog.LogInfo("DebugLog created");
		}


		public void Update()
		{
			if (KeyboardKey.Get(KeyCode.Backslash).State == State.JustDowned)
			{
				String logText = "\nSpacingGraphics : Length : CommonPoints : Ratio : Segments : SegmentMax : SegmentMin : Name\n";

				foreach (Ingredient ingredient in Ingredient.allIngredients)
				{
					logText += string.Concat
					(
						"\n", ingredient.path.GetSpacingGraphics(),
						"\t", ingredient.path.Length,
						"\t", ingredient.path.EvenlySpacedPointsCommon.points.Length,
						"\t", (ingredient.path.Length / ingredient.path.EvenlySpacedPointsCommon.SegmentsLength.Count).ToString(),
						"\t", ingredient.path.EvenlySpacedPointsCommon.SegmentsLength.Count,
						"\t", ingredient.path.EvenlySpacedPointsCommon.SegmentsLength.Max(),
						"\t", ingredient.path.EvenlySpacedPointsCommon.SegmentsLength.Min(),
						"\t", ingredient.name.ToString()
					);

				}

				debugLog.LogInfo("\n" + logText + "\n");
			}

			if (KeyboardKey.Get(KeyCode.Equals).State == State.JustDowned)
			{
				CursorManagerSettings asset = Settings<CursorManagerSettings>.Asset;
				Vector2 vector = Managers.Input.controlsProvider.CurrentMouseWorldPosition + asset.pivotOffset;


				List<Stack> stacks = new List<Stack>
				{
					Stack.SpawnNewItemStack(vector, Ingredient.GetByName("Firebell"), Managers.Player.InventoryPanel),
					Stack.SpawnNewItemStack(vector, Ingredient.GetByName("Windbloom"), Managers.Player.InventoryPanel)
				};

				Elixer elixer = new Elixer(stacks);


			}

		}


	}
}
