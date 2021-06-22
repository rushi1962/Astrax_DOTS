using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class ShooterSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var playerEntity = EntityManager.CreateEntityQuery(typeof(Player)).GetSingletonEntity();
        if(!GetComponent<Player>(playerEntity).invincible&&!GetComponent<Player>(playerEntity).dead && Input.GetMouseButtonDown(0))
        {
            var shooterEntity = EntityManager.CreateEntityQuery(typeof(Shooter)).GetSingletonEntity();
            var shooterComponet = GetComponent<Shooter>(shooterEntity);
            var trans = GetComponent<Translation>(shooterEntity);
            var newBullet=EntityManager.Instantiate(shooterComponet.bulletEntity);
            EntityManager.SetComponentData(newBullet, trans);
            var bulletMovComponent = GetComponent<Movable>(newBullet);
            var mousePosition = (float3)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            bulletMovComponent.direction = math.normalize(mousePosition-trans.Value);
            EntityManager.SetComponentData(newBullet, bulletMovComponent);
           
        }
        
    }
}
