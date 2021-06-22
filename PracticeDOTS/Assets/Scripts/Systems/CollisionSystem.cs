using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
public class CollisionSystem : SystemBase
{
    public struct CollisionHandlingJob : ICollisionEventsJob
    {
        public BufferFromEntity<CollisionBuffer> collisions;
        public void Execute(CollisionEvent collisionEvent)
        {
            if (collisions.Exists(collisionEvent.EntityA))
            {
                collisions[collisionEvent.EntityA].Add(new CollisionBuffer() { entity = collisionEvent.EntityB });                
            }
            if (collisions.Exists(collisionEvent.EntityB))
            {
                collisions[collisionEvent.EntityB].Add(new CollisionBuffer() { entity = collisionEvent.EntityA });
            }
        }
    }
    protected override void OnUpdate()
    {

        var pw = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld;
        var sw = World.GetOrCreateSystem<StepPhysicsWorld>().Simulation;
        Entities.ForEach((DynamicBuffer<CollisionBuffer> collisionBuffers) => {
            collisionBuffers.Clear();

        }).Schedule();
        var colJobHandle = new CollisionHandlingJob() { collisions = GetBufferFromEntity<CollisionBuffer>() }.Schedule(sw, ref pw, this.Dependency);
        colJobHandle.Complete();
    }
}
