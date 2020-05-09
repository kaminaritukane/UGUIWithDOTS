using INF.GamePlay;
using Unity.Entities;
using UnityEngine;

namespace INF.UI
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(UISystemGroup))]
    public class DockingUISystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _ecb;
        protected override void OnCreate()
        {
            base.OnCreate();

            _ecb = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var ecb = _ecb.CreateCommandBuffer().ToConcurrent();

            Entities.ForEach((
                Entity entity,
                int entityInQueryIndex,
                in Docking docking,
                in DockingChanged dockingChanged) =>
            {
                Debug.Log($"Docking = {docking.dockable}");

                ecb.RemoveComponent<DockingChanged>(entityInQueryIndex, entity);
            }).Schedule();

            _ecb.AddJobHandleForProducer(this.Dependency);
        }
    }
}