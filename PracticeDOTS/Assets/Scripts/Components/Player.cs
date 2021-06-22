using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Player : IComponentData
{
    public bool dead, invincible;
    public float respawnTime;
}
