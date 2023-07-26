using Unity.Entities;
using Unity.Mathematics;

namespace OceanFlocks
{
   public struct TownProperties : IComponentData
   {
      public float3 townDimensions;
      public int numberFishieResidents;
      public Entity fishiePrefab;
      public float minSpeed;
      public float maxSpeed;
   }
}
