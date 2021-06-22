using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class ShooterAuthoring : MonoBehaviour, IConvertGameObjectToEntity,IDeclareReferencedPrefabs
{


    [SerializeField] GameObject bulletPrefab;
    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Bullet");
    }
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity,new Shooter() { bulletEntity=conversionSystem.GetPrimaryEntity(bulletPrefab)});        
        
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(bulletPrefab);
    }
}
