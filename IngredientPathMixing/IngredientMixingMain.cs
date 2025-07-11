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
using System.Security.Permissions;
using System.Security;

using Logger = BepInEx.Logging.Logger;


// Idk if this will be useful, but it is here for now
#pragma warning disable CS0618
[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]


namespace IngredientMixing
{
	[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
	public class IngredientMixingMain : BaseUnityPlugin
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
			debugLog = Logger.CreateLogSource("ElixerLog");
			debugLog.LogInfo("Elixer Debug Log created");
		}


		public void Update()
		{
			string logText = string.Empty;


			if (KeyboardKey.Get(KeyCode.Backslash).State == State.JustDowned)
			{
				logText = "\nSpacingGraphics : Length : CommonPoints : Ratio : Segments : SegmentMax : SegmentMin : Name\n";

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
				logText = "\nLength : MainPoints : Ratio : Segments : SegmentMax : SegmentMin : Name\n";


				CursorManagerSettings asset = Settings<CursorManagerSettings>.Asset;
				Vector2 vector = Managers.Input.controlsProvider.CurrentMouseWorldPosition + asset.pivotOffset;

				List<Stack> stacks = new List<Stack>
				{
					Stack.SpawnNewItemStack(vector, Ingredient.GetByName("Firebell"), Managers.Player.InventoryPanel),
					Stack.SpawnNewItemStack(vector, Ingredient.GetByName("Terraria"), Managers.Player.InventoryPanel)
				};

				stacks.ForEach(stack => stack.overallGrindStatus = 1f);


				logText += string.Concat
				(
					"\n", "\n",
					"\n", stacks[0].Ingredient.path.EvenlySpacedPointsMain.Length,
					"\t", stacks[0].Ingredient.path.EvenlySpacedPointsMain.points.Length,
					"\t", (stacks[0].Ingredient.path.EvenlySpacedPointsMain.Length / stacks[0].Ingredient.path.EvenlySpacedPointsMain.SegmentsLength.Count).ToString(),
					"\t", stacks[0].Ingredient.path.EvenlySpacedPointsMain.SegmentsLength.Count,
					"\t", stacks[0].Ingredient.path.EvenlySpacedPointsMain.SegmentsLength.Max(),
					"\t", stacks[0].Ingredient.path.EvenlySpacedPointsMain.SegmentsLength.Min(),
					"\t", "Firebell",
					"\n", "\n"
				);

				foreach (Vector3 point in stacks[0].Ingredient.path.EvenlySpacedPointsMain.points)
				{
					logText += string.Concat
					(
						"\n", point.ToString()
					);
				}


				logText += string.Concat
				(
					"\n", "\n",
					"\n", stacks[1].Ingredient.path.EvenlySpacedPointsMain.Length,
					"\t", stacks[1].Ingredient.path.EvenlySpacedPointsMain.points.Length,
					"\t", (stacks[1].Ingredient.path.EvenlySpacedPointsMain.Length / stacks[1].Ingredient.path.EvenlySpacedPointsMain.SegmentsLength.Count).ToString(),
					"\t", stacks[1].Ingredient.path.EvenlySpacedPointsMain.SegmentsLength.Count,
					"\t", stacks[1].Ingredient.path.EvenlySpacedPointsMain.SegmentsLength.Max(),
					"\t", stacks[1].Ingredient.path.EvenlySpacedPointsMain.SegmentsLength.Min(),
					"\t", "Terraria",
					"\n", "\n"
				);

				foreach (Vector3 point in stacks[1].Ingredient.path.EvenlySpacedPointsMain.points)
				{
					logText += string.Concat
					(
						"\n", point.ToString()
					);
				}


				Elixer elixer = new Elixer(stacks);

				logText += string.Concat
				(
					"\n", "\n",
					"\n", elixer.Path.MixedPath.Length,
					"\t", elixer.Path.MixedPath.points.Length,
					"\t", (elixer.Path.MixedPath.Length / elixer.Path.MixedPath.SegmentsLength.Count).ToString(),
					"\t", elixer.Path.MixedPath.SegmentsLength.Count,
					"\t", elixer.Path.MixedPath.SegmentsLength.Max(),
					"\t", elixer.Path.MixedPath.SegmentsLength.Min(),
					"\t", "Elixer (Firebell + Terraria)",
					"\n", "\n"
				);

				foreach (Vector3 point in elixer.Path.MixedPath.points)
				{
					logText += string.Concat
					(
						"\n", point.ToString()
					);
				}


				debugLog.LogInfo("\n" + logText + "\n");
			}

		}


	}
}
