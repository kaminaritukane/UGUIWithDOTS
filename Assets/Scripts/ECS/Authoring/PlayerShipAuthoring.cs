using INF.GamePlay;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace INF.Authoring
{

    public class PlayerShipAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent(entity, ComponentType.ReadOnly<Player>());
            dstManager.AddComponent(entity, ComponentType.ReadOnly<Authority>());
            dstManager.AddComponent(entity, ComponentType.ReadWrite<InputReceiver>());
            dstManager.AddComponentData(entity, new MoveAbility { linearSpeed = 1f });
            dstManager.AddComponentData(entity, new Movement { linearVelocity = float3.zero });
            dstManager.AddBuffer<UnitAction>(entity);
        }
    }
}