using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class PlayerFollow : MonoBehaviour
{
    public Entity player;
    
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        EntityManager em = World.DefaultGameObjectInjectionWorld.EntityManager;
        player = em.CreateEntityQuery(typeof(Player)).GetSingletonEntity();
        if(em.Exists(player))
        {
            transform.position = em.GetComponentData<Translation>(player).Value;
        }        
    }
   
}
