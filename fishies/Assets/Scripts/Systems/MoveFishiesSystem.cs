using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

namespace OceanFlocks
{
    [BurstCompile]
    public partial struct MoveFishiesSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //use a entity query to get the town entity to access the buffer of current positions
            
            var DeltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, fishie) in
                    SystemAPI.Query<RefRW<LocalTransform>>()
                    .WithEntityAccess())
            {
                var pos = transform.ValueRO.Position;

                //var v1 = alignment(pos);
                //var v2 = cohesion (pos);
                //var v3 = seperation(pos);
                //pos += v1+v2+v3;
                //transform.ValueRWPosition += pos;
                //transform.ValueRW.Position = (transform.ValueRO.Position + pos) * 3 * DeltaTime;
                transform.ValueRW.Position += math.up() * 3 * DeltaTime;
            }
        }

        [BurstCompile]
        public float3 alignment(float3 Position)
        {
            return Position;
        }

        [BurstCompile]
        public float3 cohesion(float3 Position)
        {
            return Position;
        }

        [BurstCompile]
        public float3 seperation(float3 Position)
        {
            return Position;
        }
    }
}


/*
namespace OceanFlocks
{
    [BurstCompile]
    public partial struct MoveFishiesSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var DeltaTime = SystemAPI.Time.DeltaTime;
            new MoveFishiesJob
            {
                deltaTime = DeltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct MoveFishiesJob : IJobEntity
    {
        public float deltaTime;

        [BurstCompile]
        private void Execute(FishieMoveAspect fishie)
        {
            fishie.Move(deltaTime);
        }
    }
}
*/