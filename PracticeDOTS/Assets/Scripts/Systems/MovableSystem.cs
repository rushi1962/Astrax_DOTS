using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MovableSystem : SystemBase
{
    protected override void OnUpdate()
    {


        var dt = Time.DeltaTime;
        
        Entities.ForEach((Entity e,ref Translation translation, in Movable mov, in Rotation rotation) => {
            translation.Value += mov.direction*mov.speed*dt;   
            if(HasComponent<Rotatable>(e))
            {
                translation.Value.z = 0f;
            }
           
        }).Schedule();
    }
}
