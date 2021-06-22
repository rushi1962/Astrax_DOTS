using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class AsteroidAuthoring : MonoBehaviour, IConvertGameObjectToEntity,IDeclareReferencedPrefabs
{
    public int level;
    public GameObject childAsteroid;
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new Movable() { direction = new float3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0),speed= UnityEngine.Random.Range(0f, 5f) });
        dstManager.AddComponentData(entity, new Rotatable() { rotateSpeed=UnityEngine.Random.Range(0f,5f),rotateDir= new float3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)) });
        dstManager.AddComponentData(entity, new AsteroidChildren() { childrenA=conversionSystem.GetPrimaryEntity(childAsteroid)});
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(childAsteroid);
    }
}
