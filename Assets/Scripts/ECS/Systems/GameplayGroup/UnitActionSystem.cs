using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace INF.GamePlay
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(GamePlaySystemGroup))]
    [UpdateAfter(typeof(InputSystem))]
    class UnitActionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                ref DynamicBuffer<UnitAction> actions,
                ref Movement move,
                in MoveAbility moveAbility,
                in Rotation rot) =>
            {
                var nActions = actions.Length;
                for( int i=0; i<nActions; ++i)
                {
                    var act = actions[i];
                    switch (act.action)
                    {
                        case UnitAction.eUnitAction.MoveForward:
                            {
                                var fwd = math.mul(rot.Value, new float3(0, 0, 1));
                                move.linearVelocity += fwd * act.parameter * moveAbility.linearSpeed;
                            }
                            break;
                        case UnitAction.eUnitAction.StopMoveForward:
                            {
                                var fwd = math.mul(rot.Value, new float3(0, 0, 1));
                                move.linearVelocity -= fwd * act.parameter * moveAbility.linearSpeed;
                            }
                            break;
                        case UnitAction.eUnitAction.MoveRight:
                            {
                                var rgt = math.mul(rot.Value, new float3(1, 0, 0));
                                move.linearVelocity += rgt * act.parameter * moveAbility.linearSpeed;
                            }
                            break;
                        case UnitAction.eUnitAction.StopMoveRight:
                            {
                                var rgt = math.mul(rot.Value, new float3(1, 0, 0));
                                move.linearVelocity -= rgt * act.parameter * moveAbility.linearSpeed;
                            }
                            break;
                    }
                }

                actions.Clear();

            }).ScheduleParallel();
        }
    }
}
