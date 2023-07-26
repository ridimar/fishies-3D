using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace OceanFlocks
{
    public readonly partial struct TownAspect : IAspect
    {
        //entity associated with aspect
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _transform;
        private LocalTransform Transform => _transform.ValueRO;

        private readonly RefRO<TownProperties> _townProperties;
        private readonly RefRW<TownRandom> _townRandom;

        //to spawn
        public int numberFishieResidents => _townProperties.ValueRO.numberFishieResidents;
        public Entity fishieEntity => _townProperties.ValueRO.fishiePrefab;

        //public float minSpeed => _townProperties.ValueRO.minSpeed;
        //public float maxSpeed => _townProperties.ValueRO.maxSpeed;

       
        public LocalTransform GetRandomFishieTransform()
        {
           return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale(0.5f)
            };
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            
            randomPosition = _townRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            return randomPosition;
        }
        
        private float3 MinCorner => Transform.Position - HalfDimensions;
        private float3 MaxCorner => Transform.Position + HalfDimensions;
        
        private float3 HalfDimensions => new()
        {
            x = _townProperties.ValueRO.townDimensions.x * 0.5f,
            y = _townProperties.ValueRO.townDimensions.y * 0.5f,
            z = _townProperties.ValueRO.townDimensions.z * 0.5f
        };

        private quaternion GetRandomRotation() => quaternion.RotateX(_townRandom.ValueRW.Value.NextFloat(-0.8f, 0.8f));

        private float GetRandomScale(float min) => _townRandom.ValueRW.Value.NextFloat(min, 2f);

        //public float GetRandomSpeed() => _townRandom.ValueRW.Value.NextFloat(minSpeed, maxSpeed);
        
    }
}
