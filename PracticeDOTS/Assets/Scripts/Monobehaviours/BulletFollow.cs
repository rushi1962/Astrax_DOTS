using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
public class BulletFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Entity bulletEntity;
    private Vector3 target;

    // Update is called once per frame
    void Update()
    {
        var em = World.DefaultGameObjectInjectionWorld.EntityManager;
        if (em.Exists(bulletEntity))
        {
            
            target = em.GetComponentData<Translation>(bulletEntity).Value;
        }
       else
        {
            Destroy(gameObject);
        }
       
       
        if(target!=null)
        {
            transform.position = target;
        }
    }
}
