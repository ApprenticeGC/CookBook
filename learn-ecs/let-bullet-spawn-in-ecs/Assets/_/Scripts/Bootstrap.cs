namespace GiantCroissant.LearnECS.LetBulletSpawnInECS
{
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;

    public struct Bullet : IComponentData
    {
        public float MoveSpeed;
    }

    public struct Shooting : IComponentData
    {
        public Entity Bullet;
    }

    public struct ShootingStyle : IComponentData
    {
        public float IntervalMax;
        public float CountDown;
    }

    public struct Enemy : IComponentData
    {
    }

    public struct TimedAlive : IComponentData
    {
        public float MaxAliveTime;
        public float CountDown;
    }
    
    public struct ToBeRemoved : IComponentData
    {
        
    }

    public class EnemyShootingSystem : SystemBase
    {
        private BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;

        private EntityQuery _enemyQuery;

        protected override void OnCreate()
        {
            base.OnCreate();

            var queryDesc = new EntityQueryDesc
            {
                All = new ComponentType[] {typeof(Shooting), typeof(Enemy)}
            };

            _enemyQuery = GetEntityQuery(queryDesc);

            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            // var entities = _enemyQuery.ToEntityArray(Allocator.Temp);
            // entities.Dispose();

            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
            var deltaTime = Time.DeltaTime;

            Entities
                .ForEach((Entity entity, int entityInQueryIndex, ref Shooting shooting, ref ShootingStyle shootingStyle) =>
                {
                    var elapsedTime = shootingStyle.CountDown + deltaTime;

                    shootingStyle.CountDown = elapsedTime;

                    if (elapsedTime >= shootingStyle.IntervalMax)
                    {
                        shootingStyle.CountDown = 0;
                        
                        var bulletEntity = commandBuffer.Instantiate(entityInQueryIndex, shooting.Bullet);

                        var bulletPosition = new Translation
                        {
                            Value = float3.zero
                        };
                    
                        commandBuffer.SetComponent(entityInQueryIndex, bulletEntity, bulletPosition);
                    }
                    
                    commandBuffer.SetComponent(entityInQueryIndex, entity, shootingStyle);

                })
                .Schedule();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }

    public class BulletMoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            
            Entities
                .ForEach((Entity entity, int entityInQueryIndex, ref Translation translation, ref Bullet bullet) =>
                {
                    //
                    translation.Value.y += deltaTime * bullet.MoveSpeed;
                })
                .Schedule();
        }
    }

    public class RecordCountDownSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _entityCommandBuffer;

        protected override void OnCreate()
        {
            base.OnCreate();

            _entityCommandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBuffer.CreateCommandBuffer().ToConcurrent();
            var deltaTime = Time.DeltaTime;

            Entities
                .ForEach((Entity entity, int entityInQueryIndex, ref TimedAlive timedAlive) =>
                {
                    var elapsedTime = timedAlive.CountDown + deltaTime;

                    timedAlive.CountDown = elapsedTime;

                    if (timedAlive.CountDown >= timedAlive.MaxAliveTime)
                    {
                        commandBuffer.AddComponent<ToBeRemoved>(entityInQueryIndex, entity);
                    }
                    
                    commandBuffer.SetComponent(entityInQueryIndex, entity, timedAlive);
                    
                })
                .Schedule();
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
    }

    public class CleanToBeRemovedSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _entityCommandBuffer;

        protected override void OnCreate()
        {
            base.OnCreate();

            _entityCommandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }
        
        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBuffer.CreateCommandBuffer().ToConcurrent();

            Entities
                .ForEach((Entity entity, int entityInQueryIndex, ref ToBeRemoved toBeRemoved) =>
                {
                    commandBuffer.DestroyEntity(entityInQueryIndex, entity);
                })
                .Schedule();
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
    }
    
    public class Bootstrap : MonoBehaviour
    {
        
    }
}
