using INF.GamePlay;
using Unity.Entities;

namespace INF.UI
{
    [UpdateAfter(typeof(GamePlaySystemGroup))]
    public class UISystemGroup : ComponentSystemGroup
    {
        public DockingUISystem DockingUISys { get; private set; }

        protected override void OnCreate()
        {
            base.OnCreate();

            DockingUISys = World.CreateSystem<DockingUISystem>();
            AddSystemToUpdateList(DockingUISys);
        }
    }
}