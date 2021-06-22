using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class RotationSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
        Entities.ForEach((ref Translation translation, ref Rotation rotation,in Rotatable rotRef) => {
            rotation.Value = math.mul(rotation.Value.value, quaternion.RotateX(rotRef.rotateSpeed * rotRef.rotateDir.x*dt));
            rotation.Value = math.mul(rotation.Value.value, quaternion.RotateY(rotRef.rotateSpeed * rotRef.rotateDir.y * dt));
            rotation.Value = math.mul(rotation.Value.value, quaternion.RotateZ(rotRef.rotateSpeed * rotRef.rotateDir.z * dt));

        }).Schedule();
    }
}
