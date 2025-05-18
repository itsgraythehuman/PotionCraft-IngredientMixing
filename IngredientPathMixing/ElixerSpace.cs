using PathSystem;
using PotionCraft.ObjectBased;
using PotionCraft.ObjectBased.RecipeMap.Path;
using PotionCraft.ObjectBased.Stack;
using PotionCraft.ScriptableObjects;
using PotionCraft.ScriptableObjects.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ElixerSpace
{
	// Needs to extend InventoryItem because the ItemFromInventory extensions InventoryItem objects
	public class Elixer		// : InventoryItem
	{
		public List<Stack> IngredientStacks { get; private set; }
		public ElixerPath Path { get; private set; }


		public Elixer(List<Stack> stacks)
		{
			IngredientStacks = stacks;

			CalculatePath();
		}

		/*
		public Elixer(Ingredient ingredient)
			: this(ingredients: new List<Ingredient> { ingredient }) { }
		public Elixer(Ingredient first, Ingredient second)
			: this(ingredients: new List<Ingredient> { first, second }) { }
		public Elixer(Elixer elixer)
			: this(elixers: new List<Elixer> { elixer }) { }
		public Elixer(Elixer first, Elixer second)
			: this(elixers: new List<Elixer> { first, second }) { }
		public Elixer(Elixer elixer, Ingredient ingredient)
			: this(elixers: new List<Elixer> { elixer }, ingredients: new List<Ingredient> { ingredient }) { }
		public Elixer(Elixer elixer, List<Ingredient> ingredients)
			: this(elixers: new List<Elixer> { elixer }, ingredients: ingredients) { }
		public Elixer(List<Elixer> elixers, Ingredient ingredient)
			: this(elixers: elixers, ingredients: new List<Ingredient> { ingredient }) { }
		public Elixer(List<Elixer> elixers)
			: this(ingredients: new List<Ingredient>(elixers.SelectMany(elixer => elixer.Ingredients).ToList())) { }
		public Elixer(List<Elixer> elixers, List<Ingredient> ingredients)
			: this(ingredients: new List<Ingredient>(elixers.SelectMany(elixer => elixer.Ingredients).Concat(ingredients).ToList())) { }
		*/


		private void CalculatePath()
		{
			Path = new ElixerPath(this);
		}

	}


	public class ElixerPath		// : IngredientPath
	{
		public const float PATH_SPACING = 0.05f;    // The target length for each segment, basicly [ Length / Segments ]
		public const float RECIPROCAL_SCALE = 1f;   // When set to 1f the paths ADD, when set to 2f the paths AVERAGE


		// Common is whole thing
		// Main is only the grinded portion
		// Grinded is what still needs to be grinded
		private List<IngredientPath> IngredientPaths => Stacks.Select(stack => stack.Ingredient.path).ToList();
		private List<Stack> Stacks => parentElixer.IngredientStacks;
		public EvenlySpacedPoints MixedPath { get; private set; }


		public ElixerPath(Elixer elixer)
		{
			this.parentElixer = elixer;

			CalculatePath();
		}


		private void CalculatePath(bool isRecalc = false)
		{
			float simpleLength = isRecalc ? MixedPath.Length : IngredientPaths.Sum(path => path.EvenlySpacedPointsMain.Length);
			float simpleSegments = simpleLength / PATH_SPACING;


			// This is a simple version that is assuming that the standard spacing calculation is goo enough
			foreach (Stack stack in Stacks)
			{
				IngredientPath path = stack.Ingredient.path;
				stack.Ingredient.path.CalculateEvenlySpacedPoints(
					grindStatus: path.grindedPathStartsFrom + Mathf.Clamp01(stack.overallGrindStatus) * (1f - path.grindedPathStartsFrom),
					spacing: path.EvenlySpacedPointsMain.Length / simpleSegments);
			}

			

			// This is assuming that each path has the same number of points, and each path's points are evenly distributed
			List<List<Vector3>> listOfLists = IngredientPaths.Select(path
				=> path.EvenlySpacedPointsMain.points.ToList()).ToList();

			List<Vector3> zipedList = listOfLists.Aggregate((first, second)
				=> first.Zip(second, (one, two) => one + two).ToList());


			MixedPath = new EvenlySpacedPoints(zipedList);


			if (!isRecalc)
				CalculatePath(true);

		}


		private Elixer parentElixer;

		
	}



/*

	// This is a skeleton class for if I want the physical elixer item to extend PhysicalItemFromInventory
	//  - PhysicalItemFromInventory has child classes for things like potions, ingredients, and salts
	public class ElixerItem : PhysicalItemFromInventory
	{
		public Elixer Elixer { get; private set; }


		// TODO: still needs done


		// These are inhereted methods and I simply can't be bothered to do them right now
		public override Collider2D[] GetCollidersToCheckSurfaceStuck() => new Collider2D[] { this.mainCollider };
		public override Bounds GetItemBoundsForLedge() => throw new System.NotImplementedException();
		public override void VacuumItem(bool isPrimaryGrab, bool forceMassModifier = false, bool forceAltModifier = false)
			=> throw new System.NotImplementedException();
		// End of inherited methods
	}


	// This is a skeleton class for if I want the phisical elixer item to extend Stack
	//  - Stack is the class used to represent the physical ingredients
	public class ElixerStack : Stack
	{
		public Elixer Elixer { get; private set; }


		// TODO: still needs done


	}


	// There is a class called FlexibleIngredientItem, but it's like, never used????????


	*/


}
