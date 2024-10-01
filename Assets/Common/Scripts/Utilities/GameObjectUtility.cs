using UnityEngine;

namespace Common.Scripts.Utilities
{
    public abstract class GameObjectUtility
    {
        public static float Distance2dOfTwoGameObject(GameObject goA, GameObject goB)
        {
            return Vector2.Distance(goA.transform.position, goB.transform.position);
        }
    }
}
