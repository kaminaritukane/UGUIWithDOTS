using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace INF.GamePlay
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(GamePlaySystemGroup))]

    public class DockingSystem : SystemBase
    {
        private const float DOCKING_DISTANCE_CHECK_FREQUANCY = 1f;
        private float m_updateInterval = 1f / DOCKING_DISTANCE_CHECK_FREQUANCY;
        private float m_updateCD = 0f;

        private EntityQuery spaceStationQuery;

        private EndSimulationEntityCommandBufferSystem _ecb;

        private struct DockingStationInfo
        {
            public float3 position;
            public float range;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            _ecb = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

            spaceStationQuery = GetEntityQuery(
                ComponentType.ReadOnly<SpaceStation>(),
                ComponentType.ReadOnly<Translation>());
        }

        protected override void OnUpdate()
        {
            // AI system should update in a lower frequancy
            if (m_updateCD <= 0.0f)
            {
                m_updateCD += m_updateInterval;

                CheckInDockingRange();
            }

            m_updateCD -= Time.DeltaTime;
        }

        private void CheckInDockingRange()
        {
            var count = spaceStationQuery.CalculateEntityCount();
            var stationPosArray = new NativeArray<DockingStationInfo>(count, Allocator.TempJob);

            var ecb = _ecb.CreateCommandBuffer().ToConcurrent();

            // Get all station's position
            Entities
                .WithStoreEntityQueryInField(ref spaceStationQuery)
                .ForEach((int entityInQueryIndex,
                in SpaceStation station,
                in Translation stationTrans) =>
            {
                stationPosArray[entityInQueryIndex] = new DockingStationInfo { 
                    position = stationTrans.Value,
                    range = station.Range
                };
            })
            .Schedule();

            Entities.ForEach((
                Entity entity,
                int entityInQueryIndex,
                ref Docking docking,
                in Player player,
                in Authority auth,
                in Translation trans) =>
            {
                var playerPos = trans.Value;

                bool isDockable = false;
                for (int i = 0; i < stationPosArray.Length; ++i)
                {
                    var stationPos = stationPosArray[i].position;
                    var stationRange = stationPosArray[i].range;
                    if (math.distancesq(playerPos, stationPos) < stationRange * stationRange)
                    {
                        isDockable = true;
                        break;
                    }
                }

                if ( docking.dockable != isDockable )
                {
                    docking.dockable = isDockable;
                    ecb.AddComponent<DockingChanged>(entityInQueryIndex, entity);
                }
            })
            .WithDeallocateOnJobCompletion(stationPosArray)
            .Schedule();

            _ecb.AddJobHandleForProducer(this.Dependency);
        }
    }
}