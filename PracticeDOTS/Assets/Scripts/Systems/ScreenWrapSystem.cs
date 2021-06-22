using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class ScreenWrapSystem : SystemBase
{
    protected override void OnUpdate()
    {
        
        
        Entities
            .ForEach((Entity e,ref Translation translation, in Rotation rotation) => {
            if(translation.Value.x>18f)
            {
                Translation trans = new Translation();
                trans.Value = translation.Value;
                trans.Value.x = -17.8f;
                EntityManager.SetComponentData(e, trans);
            }
            if (translation.Value.x < -18f)
            {
                Translation trans = new Translation();
                trans.Value = translation.Value;
                trans.Value.x = 17.8f;
                EntityManager.SetComponentData(e, trans);
            }
            if (translation.Value.y > 9.5f)
            {
                Translation trans = new Translation();
                trans.Value = translation.Value;
                trans.Value.y = -9.2f;
                EntityManager.SetComponentData(e, trans);
            }
            if (translation.Value.y < -9.5f)
            {
                Translation trans = new Translation();
                trans.Value = translation.Value;
                trans.Value.y = 9.2f;
                EntityManager.SetComponentData(e, trans);
            }
        }).WithStructuralChanges().Run();
        
    }
}
