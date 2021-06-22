using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct OnKill : IComponentData
{
    public bool killed;
    public int level;
}
