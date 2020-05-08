using INF.GamePlay;
using Unity.Entities;

namespace INF.UI
{
    [UpdateAfter(typeof(GamePlaySystemGroup))]
    public class UISystemGroup : ComponentSystemGroup
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            AddSystemToUpdateList(World.CreateSystem<DockingUISystem>());
        }
    }
}