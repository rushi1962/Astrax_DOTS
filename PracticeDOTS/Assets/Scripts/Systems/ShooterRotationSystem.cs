using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class ShooterRotationSystem : SystemBase
{
    protected override void OnUpdate()
    {
        
        var playerEntity = EntityManager.CreateEntityQuery(typeof(Player)).GetSingletonEntity();       
        var mousePosition = (float3)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        var playerPosition = GetComponent<Translation>(playerEntity);
        var shooterPosition = math.normalize(mousePosition - playerPosition.Value) * 0.45f;
        shooterPosition.z = 0f;
        Entities.ForEach((ref Translation translation, in Shooter shooter) => {
            translation.Value = playerPosition.Value + shooterPosition;
        }).Schedule();
        
    }
}
