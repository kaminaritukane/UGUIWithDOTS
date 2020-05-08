using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace INF.GamePlay
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(GamePlaySystemGroup))]
    [UpdateAfter(typeof(InputSystem))]
    class UnitActionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref DynamicBuffer<UnitAction> actions,
                ref Translation trans)=>
            {
                var nActions = actions.Length;
                for( int i=0; i<nActions; ++i)
                {
                    var act = actions[i];
                    switch (act.action)
                    {
                        case UnitAction.eUnitAction.MoveForward:
                            {
                                trans.Value += new float3(1, 0, 0);
                            }
                            break;
                    }
                }

                actions.Clear();

            }).ScheduleParallel();
        }
    }
}
