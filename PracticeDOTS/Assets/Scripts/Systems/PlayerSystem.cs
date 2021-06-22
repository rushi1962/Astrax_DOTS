using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class PlayerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float varX = Input.GetAxis("Horizontal");
        float varY = Input.GetAxis("Vertical");
       
        Entities.ForEach((ref Translation translation,ref Movable movable,in Player player) => {

            movable.direction = new float3(varX, varY,0f );
        }).Schedule();
        Entities.ForEach((ref Translation translation, ref Movable movable, in Shooter shooter) => {

            movable.direction = new float3(varX, varY, 0f);
        }).Schedule();
    }
}
