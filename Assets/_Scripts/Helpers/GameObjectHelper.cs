using Assets._Scripts.Abstract;
using Assets._Scripts.Pieces;
using UnityEngine;

namespace Assets._Scripts.Helpers
{
    internal static class GameObjectHelper
    {
        internal static void AddAsChildrenTo(this GameObject children, GameObject parent)
        {
            children.transform.parent = parent.transform;
            children.transform.position = parent.transform.position;
        }
    }
}
