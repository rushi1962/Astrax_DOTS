using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
public class TurretRotation : MonoBehaviour
{
    private Camera mainCamera;
    Vector3 mousePosition;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var em = World.DefaultGameObjectInjectionWorld.EntityManager;
        var playerEntity = em.CreateEntityQuery(typeof(Player)).GetSingletonEntity();
        if(!em.GetComponentData<Player>(playerEntity).invincible)
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.rotation = Quaternion.LookRotation(mousePosition - transform.position, transform.up);
        }
        
    }
}
