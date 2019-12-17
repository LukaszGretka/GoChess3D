using UnityEngine;

namespace Assets._Scripts.Movement
{
    interface IBasicMovement
    {
        MovementType MovementType { get; }

        bool AbleToMoveBackward { get; }

        bool CheckIfMovePossible();
    }
}
