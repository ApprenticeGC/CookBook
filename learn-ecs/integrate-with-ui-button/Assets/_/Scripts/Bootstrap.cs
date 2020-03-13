namespace GiantCroissant.LearnECS.IntegrateWithUiButton
{
    using System;
    using Unity.Entities;
    using UnityEngine;

    public enum EButtonKind
    {
        Add,
        Subtract
    }

    public struct HudEventBase : IComponentData
    {
    }
    
    public struct HpButtonPressed : IComponentData
    {
        public EButtonKind Kind;
        public int Value;
    }

    public struct Player : IComponentData
    {
    }
    
    public struct Status : IComponentData
    {
        public int TotalHp;
    }
    
    public struct AdjustStatus : IComponentData
    {
        public int HpValue;
    }

    public struct UpdateStatus : IComponentData
    {
        public int HpValue;
    }

    public struct UpdateHpHud : IComponentData
    {
        public int Value;
    }

    public class HudSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _entityCommandBufferSystem;
    
        private EntityQuery _statusQuery;

        protected override void OnCreate()
        {
            base.OnCreate();
            var queryDesc = new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    typeof(Player),
                    typeof(Status)
                }
            };

            _statusQuery = EntityManager.CreateEntityQuery(queryDesc);

            _entityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
            var statusEntity = _statusQuery.GetSingletonEntity();
            
            Entities
                .WithAll<HudEventBase>()
                .ForEach((Entity entity, int entityInQueryIndex, HpButtonPressed hpButtonPressed) =>
                {
                    var v = hpButtonPressed.Value;
                    if (hpButtonPressed.Kind == EButtonKind.Subtract)
                    {
                        v = -v;
                    }
                    
                    commandBuffer.AddComponent(entityInQueryIndex, statusEntity, new AdjustStatus
                    {
                        HpValue = v
                    });
                    
                    commandBuffer.RemoveComponent<HpButtonPressed>(entityInQueryIndex, entity);
                })
                .Schedule();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }

    public class PlayerStatusAdjustSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _entityCommandBufferSystem;
        private EntityQuery _statusQuery;

        protected override void OnCreate()
        {
            base.OnCreate();
            
            var queryDesc = new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    typeof(Player),
                    typeof(Status)
                }
            };

            _statusQuery = EntityManager.CreateEntityQuery(queryDesc);
            
            _entityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
            var statusEntity = _statusQuery.GetSingletonEntity();
            
            Entities
                .WithAll<Status>()
                .WithNone<UpdateStatus>()
                .ForEach((Entity entity, int entityInQueryIndex, AdjustStatus adjustStatus) =>
                {
                    commandBuffer.AddComponent(entityInQueryIndex, statusEntity, new UpdateStatus
                    {
                        HpValue = adjustStatus.HpValue
                    });
                    
                    commandBuffer.RemoveComponent<AdjustStatus>(entityInQueryIndex, entity);
                })
                .WithoutBurst()
                .Schedule();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }

    // Actual system that modifies the status value
    public class PlayerStatusUpdateSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _entityCommandBufferSystem;
        private EntityQuery _eventBaseQuery;

        protected override void OnCreate()
        {
            base.OnCreate();
            
            var queryDesc = new EntityQueryDesc
            {
                All = new ComponentType[] {typeof(HudEventBase)}
            };

            _eventBaseQuery = EntityManager.CreateEntityQuery(queryDesc);
            
            _entityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
            var eventBaseEntity = _eventBaseQuery.GetSingletonEntity();
            
            Entities
                .WithAll<Player>()
                .WithNone<AdjustStatus>()
                .ForEach((Entity entity, int entityInQueryIndex, ref Status status, ref UpdateStatus updateStatus) =>
                {
                    // Store the value for later use
                    status.TotalHp += updateStatus.HpValue;
                    
                    // Attach event use component to update UI
                    commandBuffer.AddComponent(entityInQueryIndex, eventBaseEntity, new UpdateHpHud
                    {
                        Value = status.TotalHp
                    });
                    
                    // After handing, remove the component(served as event)
                    commandBuffer.RemoveComponent<UpdateStatus>(entityInQueryIndex, entity);
                })
                .WithoutBurst()
                .Schedule();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }

    public class UpdateHudSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _entityCommandBufferSystem;

        private PlayerStatus _playerStatus;

        protected override void OnCreate()
        {
            base.OnCreate();

            _entityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

            _playerStatus = GameObject.FindObjectOfType<PlayerStatus>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer();
            
            Entities
                .WithAll<HudEventBase>()
                .WithNone<HpButtonPressed>()
                .ForEach((Entity entity, UpdateHpHud updateHpHud) =>
                {
                    _playerStatus.UpdateHpCount(updateHpHud.Value);
                    
                    //
                    commandBuffer.RemoveComponent<UpdateHpHud>(entity);
                })
                .WithoutBurst()
                .Run();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
    
    public class Bootstrap : MonoBehaviour
    {
        private EntityManager _entityManager;
        private EntityArchetype _hudEventBaseArchetype;
        private EntityArchetype _statusArchetype;

        private void Start()
        {
            //
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            // Crete event use entity for hud
            _hudEventBaseArchetype = _entityManager.CreateArchetype(
                typeof(HudEventBase));
            _entityManager.CreateEntity(_hudEventBaseArchetype);

            //
            _statusArchetype = _entityManager.CreateArchetype(
                typeof(Player),
                typeof(Status));
            _entityManager.CreateEntity(_statusArchetype);
        }
    }
}
