using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace OceanFlocks
{
    public readonly partial struct FishieMoveAspect : IAspect
    {
        //entity associated with aspect
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _fTransform;
        private LocalTransform Transform => _fTransform.ValueRO;

        private readonly RefRO<FishieProperties> _fishieProps;

        
        
        public void Move (float deltaTime)
        {
            /*
            float3 cohesionHeading = cohesionHeading.zero;
            float3 avoidanceHeading = avoidanceHeading.zero;
            float gSpeed = 0.01f
            float nDistance;
            int gSize = 0;

            //iterate over DynamicBuffer
                nDistance = math.distancesq(_fTransform.ValueRO.Position, positionBuffer[i]);
                if (nDistance<=maxNeighborDistance)
                {
                    cohesionHeading += positionBuffer[i];
                    gSize++;
                    if(nDistance <=minNeighborDistance)
                    {
                        avoidanceHeading += (_fTransform.ValueRO.Position - positionBuffer[i]);
                    }
                }
            */

            _fTransform.ValueRW.Position += math.up() * _fishieProps.ValueRO.fSpeed * deltaTime;

        }
    }
}