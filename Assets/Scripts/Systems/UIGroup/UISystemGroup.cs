using Unity.Entities;

namespace INF.UI
{
    public class UISystemGroup : ComponentSystemGroup
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            AddSystemToUpdateList(World.CreateSystem<DockingUISystem>());
        }
    }
}