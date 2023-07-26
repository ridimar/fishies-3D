using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace OceanFlocks
{
    public class TownMono : MonoBehaviour
    {
        public float3 townDimensions;
        public int numberFishieResidents;
        public GameObject fishiePrefab;
        public uint RandomSeed;
        public float minSpeed;
        public float maxSpeed;
    }

    public class TownBaker : Baker<TownMono>
    {
        public override void Bake(TownMono authoring)
        {
            var townEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(townEntity, new TownProperties
            {
                townDimensions=authoring.townDimensions,
                numberFishieResidents=authoring.numberFishieResidents,
                fishiePrefab=GetEntity(authoring.fishiePrefab, TransformUsageFlags.Dynamic),
                minSpeed = authoring.minSpeed,
                maxSpeed = authoring.maxSpeed
            });

            AddComponent(townEntity, new TownRandom{Value = Random.CreateFromIndex(authoring.RandomSeed)});

            AddBuffer<FishiePosBufferElement>(townEntity);
        }
    }
}