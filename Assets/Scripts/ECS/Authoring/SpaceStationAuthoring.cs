using INF.GamePlay;
using Unity.Entities;
using UnityEngine;

namespace INF.Authoring
{
    public class SpaceStationAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new SpaceStation {
                Range = 5f
            });
        }
    }
}