using Unity.Entities;
using Unity.Mathematics;

namespace OceanFlocks
{
    public struct TownRandom : IComponentData
    {
        public Random Value;
    }
}
