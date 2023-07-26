using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using UnityEngine;

namespace OceanFlocks
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnFishiesSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //entity needs to exist to find the singleton in onupdate
            state.RequireForUpdate<TownProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //code after this will still run once, but this spawning system is turned off starting the next frame
            state.Enabled = false;
            
            var townEntity = SystemAPI.GetSingletonEntity<TownProperties>();
            var town = SystemAPI.GetAspect<TownAspect>(townEntity);
           
            //An entity command buffer (ECB) stores a queue of thread-safe commands which you can add to and later play back.
            //used for structural changes (creating or destroying entity, adding or removing components, setting a shared component value)
            //temp = dispose in the same frame that it's allocated
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            for( var i=0; i< town.numberFishieResidents; i++)
            {
                var newFishie = ecb.Instantiate(town.fishieEntity);
                var newFishieTransform = town.GetRandomFishieTransform();
                //float fishSpeed = town.GetRandomSpeed();
                //Debug.Log("speed for fish" + i + " " + fSpeed);
                var newFishiePos = new FishiePosBufferElement { Value = newFishieTransform.Position };
                ecb.SetComponent(newFishie, newFishieTransform);
                //ecb.AddBuffer<FishiePosBufferElement>(townEntity)
                   // .Add(new FishiePosBufferElement { Value = newFishieTransform.Position });
                
                //ecb.SetComponent(newFishie, new FishieProperties{ fSpeed = fishSpeed });
            }

            ecb.Playback(state.EntityManager);        
        }
    }
}
