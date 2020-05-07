using Unity.Entities;

namespace INF.GamePlay
{
    public class GamePlaySystemGroup : ComponentSystemGroup
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            AddSystemToUpdateList(World.CreateSystem<DockingSystem>());
        }
    }
}