using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using UnityEngine;
using Unity.Transforms;

namespace OceanFlocks
{
    [BurstCompile]
    public partial struct GetFishiePosSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //entity needs to exist to find the singleton in onupdate
            state.RequireForUpdate<FishieProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            
            var DeltaTime = SystemAPI.Time.DeltaTime;

            /*foreach (DynamicBuffer<FishiePosBufferElement> allPos in SystemAPI.Query<DynamicBuffer<FishiePosBufferElement>>())
            {
                allPos.Clear();
                foreach (var transform in SystemAPI.Query<RefRO<LocalTransform>>())
                    { 
                        allPos.Add(new FishiePosBufferElement {Value = transform.ValueRO.Position});
                    }
            }*/

            var DeltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new GetFishiePosJob
            {
                deltaTime = DeltaTime
            }.ScheduleParallel();
            
            //An entity command buffer (ECB) stores a queue of thread-safe commands which you can add to and later play back.
            //used for structural changes (creating or destroying entity, adding or removing components, setting a shared component value)
            //temp = dispose in the same frame that it's allocated
            /*
            public EntityCommandBuffer.ParallelWriter ecb;
            var townEntity = SystemAPI.GetSingletonEntity<FishiePosBufferElement>();

            foreach (var (transform, fishie) in
                    SystemAPI.Query<RefRW<LocalTransform>>()
                    .WithEntityAccess())
            {
                var pos = transform.ValueRO.Position;
                ecb.AppendToBuffer(townEntity, pos);
            }
            */

        }

        [BurstCompile]
        public partial struct GetFishiePosJob : IJobEntity
        {
            public float deltaTime;

            [BurstCompile]
            private void Execute(TownAspect town)
            {
                foreach (DynamicBuffer<FishiePosBufferElement> allPos in SystemAPI.Query<DynamicBuffer<FishiePosBufferElement>>())
                {
                allPos.Clear();
                foreach (var transform in SystemAPI.Query<RefRO<LocalTransform>>())
                    { 
                        allPos.Add(new FishiePosBufferElement {Value = transform.ValueRO.Position});
                    }
                }
            }
        }
    }
}
