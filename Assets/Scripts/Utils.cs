using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Monotheist
{

    public class Utils
    {
        public static Interactable SearchInteractable(Vector3 origin, float searchRange, string tag)
		{
			//Search for the acceptable objects around
			Collider2D[] colliders = Physics2D.OverlapCircleAll(origin, searchRange);

			List<Interactable> _targetList = new List<Interactable>();

			foreach (Collider2D collider in colliders)
			{
				if (collider.tag == tag)
				{
					_targetList.Add(collider.GetComponent<Interactable>());
				}
			}

			//From the around objects, go to the nearest
			Interactable target = SearchInteractableFromList(origin, searchRange, _targetList);
			return target;
		}

		public static Interactable SearchInteractableFromList(Vector3 origin, float searchRange, List<Interactable> interactableList)
		{
			Interactable target = NullInteractable.Instance;

			float minDistance = searchRange;

			foreach (Interactable inter in interactableList)
			{
				float distance = Vector2.Distance(inter.transform.position, origin);

				if (distance < minDistance)
				{
					minDistance = distance;
					target = inter;
				}
			}

			Assert.IsNotNull(target);
			return target;
		}
    }
}
