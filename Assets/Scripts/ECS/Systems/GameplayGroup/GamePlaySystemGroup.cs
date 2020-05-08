using Unity.Entities;

namespace INF.GamePlay
{
    public class GamePlaySystemGroup : ComponentSystemGroup
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            AddSystemToUpdateList(World.CreateSystem<InputSystem>());
            AddSystemToUpdateList(World.CreateSystem<UnitActionSystem>());
            AddSystemToUpdateList(World.CreateSystem<DockingSystem>());
        }
    }
}