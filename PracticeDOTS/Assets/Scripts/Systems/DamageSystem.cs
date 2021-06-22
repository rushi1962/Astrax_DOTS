using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class DamageSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
        Entities
            .WithNone<Kill>()
            .ForEach((DynamicBuffer<CollisionBuffer> collisions) => {
                
            for(int i=0;i<collisions.Length;i++)
            {
                if(HasComponent<Rotatable>(collisions[i].entity)&&HasComponent<OnKill>(collisions[i].entity))
                {
                        var onKill=GetComponent<OnKill>(collisions[i].entity);
                        onKill.killed = true;
                        EntityManager.SetComponentData(collisions[i].entity, onKill);
                }
            }
           
        }).WithStructuralChanges().Run();
        Entities
            .WithNone<Kill>()
            .ForEach((DynamicBuffer<CollisionBuffer> collisions) => {

                for (int i = 0; i < collisions.Length; i++)
                {
                    if (HasComponent<Bullet>(collisions[i].entity) && HasComponent<OnKill>(collisions[i].entity))
                    {
                        var onKill = GetComponent<OnKill>(collisions[i].entity);
                        onKill.killed = true;
                        EntityManager.SetComponentData(collisions[i].entity, onKill);
                    }
                }

            }).WithStructuralChanges().Run();
        Entities.ForEach((DynamicBuffer<CollisionBuffer> collisions) => {

                    
                    for(int i=0;i<collisions.Length;i++)
                    {
                         if (HasComponent<Player>(collisions[i].entity)&& !GetComponent<Player>(collisions[i].entity).invincible)
                         {
                            if(GameManager.gm.lives>0)
                            {
                                GameManager.gm.lives--;
                                GameManager.gm.ChangeText();
                                GameManager.gm.invincibleTime = GetComponent<Player>(collisions[i].entity).respawnTime;
                                var particles = Resources.Load<GameObject>("ShipTeleport");
                                var newParticles = GameObject.Instantiate(particles);
                                newParticles.transform.position = GetComponent<Translation>(collisions[i].entity).Value;
                                var trans = GetComponent<Translation>(collisions[i].entity);
                                trans.Value.z = -30f;
                                var player = GetComponent<Player>(collisions[i].entity);
                                player.invincible = true;
                                EntityManager.SetComponentData(collisions[i].entity, trans);
                                EntityManager.SetComponentData(collisions[i].entity, player);

                            }
                            else
                            {
                                GameManager.gm.gameOver = true;
                                GameManager.gm.GameOver();
                                var particles = Resources.Load<GameObject>("ShipTeleport");
                                var newParticles = GameObject.Instantiate(particles);
                                newParticles.transform.position = GetComponent<Translation>(collisions[i].entity).Value;
                                var trans = GetComponent<Translation>(collisions[i].entity);
                                trans.Value.z = -30f;
                                var player = GetComponent<Player>(collisions[i].entity);
                                player.invincible = true;
                                EntityManager.SetComponentData(collisions[i].entity, trans);
                                EntityManager.SetComponentData(collisions[i].entity, player);
                    }
                            

                          }
                
                    }
        }).WithStructuralChanges().Run();
        var playerEntity = EntityManager.CreateEntityQuery(typeof(Player)).GetSingletonEntity();
        if(GetComponent<Player>(playerEntity).invincible&&!GameManager.gm.gameOver)
        {
            GameManager.gm.invincibleTime -= dt;
            if(GameManager.gm.invincibleTime<0f)
            {
                var trans=GetComponent<Translation>(playerEntity);
                bool gotPoint = true;
                float3 newSpot;
                do
                {
                    newSpot = new float3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(-9f, 9f), 0f);
                    var asteroids = EntityManager.GetAllEntities();
                    for(int i=0;i<asteroids.Length;i++)
                    {
                        if(HasComponent<Rotatable>(asteroids[i]))
                        {
                            var position = GetComponent<Translation>(asteroids[i]);
                            if(math.distance(position.Value,newSpot)<5f)
                            {
                                gotPoint = false;
                                break;
                            }
                            gotPoint = true;
                        }
                    }

                } while (!gotPoint);
                trans.Value = newSpot;
                var player = GetComponent<Player>(playerEntity);
                player.invincible = false;
                EntityManager.SetComponentData(playerEntity, trans);
                EntityManager.SetComponentData(playerEntity, player);
                var particles = Resources.Load<GameObject>("ShipTeleport");
                var newParticles = GameObject.Instantiate(particles);
                newParticles.transform.position = newSpot;
            }
        }
        Entities.ForEach((Entity e,in OnKill onKill) => {
            if(onKill.killed)
            EntityManager.AddComponentData(e, new Kill());
        
        }).WithStructuralChanges().Run();
        Entities.ForEach((Entity e,in Kill kill) => {
            if(HasComponent<Rotatable>(e))
            {
                var particles=Resources.Load<GameObject>("AsteroidParticles");
                var newParticles = GameObject.Instantiate(particles);
                newParticles.transform.position = GetComponent<Translation>(e).Value;
                if(GetComponent<OnKill>(e).level >0)
                {
                    var childComponent = GetComponent<AsteroidChildren>(e);
                    var newChild=EntityManager.Instantiate(childComponent.childrenA);
                    EntityManager.SetComponentData(newChild, GetComponent<Translation>(e));
                }
                
            }
            
            EntityManager.DestroyEntity(e);
        }).WithStructuralChanges().Run();
    }
}
