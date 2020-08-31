using UnityEngine;

namespace Assets._Scripts.Helpers
{
    internal static class GameObjectHelper
    {
        internal static void AddAsChildrenTo(this GameObject children, GameObject parent)
        {
            children.transform.SetParent(parent.transform);
            children.transform.position = parent.transform.position;
        }

        internal static void AddAsChildrenTo(this GameObject children, GameObject parent, Vector3 childrenOffset)
        {
            children.transform.parent = parent.transform;
            children.transform.position = childrenOffset;
        }

        internal static T GetComponentFromRayCast<T>() where T : Component
        {
            return GetGameObjectFromRayCast()?.GetComponent<T>();
        }

        private static GameObject GetGameObjectFromRayCast()
        {
            Ray rayFromCam = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(rayFromCam, out RaycastHit rayHit))
            {
                return null;
            }

            return rayHit.collider.gameObject;
        }
    }
}
