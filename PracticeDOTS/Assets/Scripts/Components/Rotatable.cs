using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;


public struct Rotatable : IComponentData
{
    
    public float rotateSpeed;
    public float3 rotateDir;
}
