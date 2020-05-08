using Unity.Entities;

namespace INF.GamePlay
{
#pragma warning disable 0649
    struct Player : IComponentData
    {
    }

    struct Authority : IComponentData
    {
    }

    struct InputReceiver : IComponentData
    {
    }

    [InternalBufferCapacity(16)]
    public struct UnitAction : IBufferElementData
    {
        public enum eUnitAction
        {
            MoveForward,
            StopMoveForward,
            MoveRight,
            StopMoveRight
        }

        public eUnitAction action;
        public float parameter;
    }
#pragma warning restore 0649
}