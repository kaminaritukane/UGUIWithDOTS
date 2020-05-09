using System;
using System.Text;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace INF.GamePlay
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(GamePlaySystemGroup))]
    [UpdateAfter(typeof(UnitActionSystem))]
    class MovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;

            Entities.ForEach((
                ref Translation tran,
                in Movement move) =>
            {
                tran.Value += move.linearVelocity * deltaTime;

            }).ScheduleParallel();
        }
    }
}
