using INF.GamePlay;
using Unity.Entities;
using Unity.Transforms;
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
            dstManager.AddBuffer<UnitAction>(entity);
        }
    }
}