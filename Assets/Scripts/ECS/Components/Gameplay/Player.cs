using Unity.Entities;
using Unity.Mathematics;

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

    struct MoveAbility : IComponentData
    {
        public float linearSpeed;
    }

    struct Movement : IComponentData
    {
        public float3 linearVelocity;
    }

    struct Docking : IComponentData
    {
        public bool dockable;
    }
    struct DockingChanged : IComponentData
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