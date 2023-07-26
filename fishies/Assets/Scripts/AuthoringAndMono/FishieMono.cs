using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class FishieMono : MonoBehaviour
{
    public float fishSpeed;
}

public class FishieBaker : Baker<FishieMono>
{
    public override void Bake(FishieMono authoring)
    {
        var fishieEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(fishieEntity, new FishieProperties { fSpeed = authoring.fishSpeed });
    }
}
