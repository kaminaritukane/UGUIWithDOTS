using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace INF.GamePlay
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(GamePlaySystemGroup))]

    public class DockingSystem : SystemBase
    {
        private const float DOCKING_DISTANCE_CHECK_FREQUANCY = 1f;
        private float m_updateInterval = 1f / DOCKING_DISTANCE_CHECK_FREQUANCY;
        private float m_updateCD = 0f;
        private bool m_wasInRange = false;

        protected override void OnUpdate()
        {
            // AI system should update in a lower frequancy
            if (m_updateCD <= 0.0f)
            {
                m_updateCD += m_updateInterval;

                var isInRange = CheckInDockingRange();
                if ( isInRange != m_wasInRange )
                {
                    m_wasInRange = isInRange;

                    SendInRangeChangedEvent();
                }
            }

            m_updateCD -= Time.DeltaTime;
        }

        private void SendInRangeChangedEvent()
        {
            throw new NotImplementedException();
        }

        private bool CheckInDockingRange()
        {
            bool isInRange = false;

            float3 playerPos = float3.zero;

            Entities.ForEach((in Player player, 
                in Authority auth,
                in Translation trans) =>
            {
                playerPos = trans.Value;
            }).Run();

            // Should be only one Docking station around?
            Entities.ForEach((in SpaceStation station,
                in Translation stationTrans) =>
            {
                if (math.distancesq(playerPos, stationTrans.Value) < station.Range * station.Range)
                {
                    isInRange = true;
                }
            }).Run();

            return isInRange;
        }
    }
}