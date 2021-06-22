using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class TODSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
       
        Entities.ForEach((Entity e,ref Translation translation, ref TODDots todDots) => {
            todDots.delay=todDots.delay-dt;
            if(todDots.delay<0)
            {
                EntityManager.DestroyEntity(e);
            }
          
        }).WithStructuralChanges().Run();
    }
}
