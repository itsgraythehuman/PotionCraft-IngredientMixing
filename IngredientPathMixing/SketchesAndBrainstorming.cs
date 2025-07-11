using PotionCraft.ObjectBased.RecipeMap.Path;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IngredientMixing
{
	internal class SketchesAndBrainstorming
	{


		// Currently just an outline of how to merge paths together
		public List<Vector3> SketchMerge(List<IngredientPath> pathList)
		{
			/*
			 * This shit is gonna require a custom implimentation of the method:
			 *		void EvenlySpacedPointsPath.CalculateEvenlySpacedPoints
			 *			(float grindStatus, float spacing, bool addEdgePoints = true, bool addToCache = false)
			 * Not looking forward to that :/
			 */

			float segmentLength = 0; // This is the target length for each segment, is given by a variable somewhere, idk
			float segmentPercent = 0; // The expected percentage of the total length each segment should be

			float approxLength = 0; // The length of the merged path calculated on the first pass
			float approxSegments = 0; // (May convert to int) Number of segments used in the first pass

			List<float> percentList = new List<float>(pathList.Count); // This is the list of percents that each path is at
			float leadingPercent = 0f; // Basicly just Max(percentList) (here as pseudocode for readability)
			float trailingPercent = 0f; // Basicly just Min(percentList) (here as pseudocode for readability)

			float deviationPercent = 0; // This is the maximum allowed difference between the leading and trailing percent

			List<Vector3> currentLengths = new List<Vector3>(pathList.Count); // This is the list of current lengths of each path
			List<Vector3> totalLengths = new List<Vector3>(pathList.Count); // This is the list of total lengths of each path (here as pseudocode for readability)

			List<Vector3> pointList = new List<Vector3>(pathList.Count); // A list version of the (Vector2) point used in CalculateEvenlySpacedPoints
			List<Vector3> previousPoints = new List<Vector3>(pathList.Count); // A list version of the (Vector2) previousPoint used in alculateEvenlySpacedPoints

			List<Vector3> mergedPath = new List<Vector3>(); // The final merged path to be returned

			/* 
			 * ----- Description of the Single Path Implementation -----
			 * 
			 * When implementing the CalculateEvenlySpacedPoints method, the main trouble will be when making the equivilant to the forEach loop:
			 *		path.ForEach(delegate (CubicBezierCurve curve) { ... }
			 *		
			 * Within it there is a for loop that itterates along each point of the path, which effectively reads as:
			 *		for (int currentSegment = 0; currentSegment <= [ numSegments := 10 * curve.length ]; currentSegment++) { ... }
			 * 
			 * In said for loop, the next point is first guessed by:
			 *		Vector2 point = curve.GetPoint( parametricParameter: currentSegment / numSegments );
			 * 
			 * Because this is only dealing with a single path, the only requirement is that each sengment has a [ length <= segmentLength ], this is
			 * achieved by lerping from the previous point to the current point in incriments of segmentLength, and adding all of them to evenlySpacedPoints
			 * 
			 */

			/*
			 * ----- Key Differences Between Single and Multi -----
			 * 
			 * Single Path Conditions:
			 *		// Prevents any one segment from being too long
			 *		(previousPoint - point).magnitude < spacing
			 * 
			 * Multi Path Conditions:
			 *		// Prevents any one segment of the total path from being too long
			 *		(pointList.sum - previousPoints.sum).magnitude < segmentLength
			 *		
			 *		// Prevents any one path's segments from being too long
			 *		(pointList[pathIndex] - previousPoints[pathIndex]).magnitude < segmentLength
			 *		
			 *		// Prevents any one path's segments from being a larger percentage of the path's total length than allowed
			 *		(pointList[pathIndex] - previousPoints[pathIndex]).magnitude < approxPercent * totalLengths[pathIndex]
			 *		
			 *		// Prevents the different paths from being too far apart in their progression
			 *		leadingPercent - trailingPercent < deviationPercent
			 *		
			 *		
			 *		
			 * 
			 */

			/*
			 * ----- Description of the Multi Path Implementation -----
			 * 
			 * 
			 * 
			 * 
			 * 
			 */






			return mergedPath;
		}
	}
}
