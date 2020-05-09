using INF.GamePlay;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace INF.UI
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(UISystemGroup))]
    public class DockingUISystem : SystemBase
    {
        public delegate void DockableChnagedDelegate(bool dockable);
        public event DockableChnagedDelegate OnDockableChangedEvent;

        private EndSimulationEntityCommandBufferSystem _ecb;
        protected override void OnCreate()
        {
            base.OnCreate();

            _ecb = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var ecb = _ecb.CreateCommandBuffer();

            NativeArray<bool> dockingChangedArray = new NativeArray<bool>(1, Allocator.Temp);

            Entities.ForEach((
                Entity entity,
                int entityInQueryIndex,
                in Player player,
                in Authority authority,
                in Docking docking,
                in DockingChanged dockingChanged) =>
            {
                Debug.Log($"Dockable = {docking.dockable}");

                dockingChangedArray[0] = docking.dockable;

                ecb.RemoveComponent<DockingChanged>(entity);
            }).Run();

            _ecb.AddJobHandleForProducer(this.Dependency);

            OnDockableChangedEvent?.Invoke(dockingChangedArray[0]);

            dockingChangedArray.Dispose();
        }
    }
}