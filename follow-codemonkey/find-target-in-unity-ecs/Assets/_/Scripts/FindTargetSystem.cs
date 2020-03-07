namespace GiantCroissant.FllowCodeMonkey.FindTargetInUnityECS
{
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;

    public class FindTargetSystem : SystemBase
    {
        private EntityCommandBufferSystem _entityCommandBufferSystem;
        private EntityQuery _targetQuery;

        protected override void OnCreate()
        {
            base.OnCreate();

            _entityCommandBufferSystem = World.GetOrCreateSystem<EntityCommandBufferSystem>();
            
            var query = new EntityQueryDesc
            {
                All = new ComponentType[] { typeof(Target), typeof(Translation) }
            };

            _targetQuery = GetEntityQuery(query);
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer();
            
            Entities
                .WithNone<HasTarget>()
                .WithAll<Unit>().ForEach((Entity unitEntity, ref Translation unitTranslation) =>
            {
                //
                var unitPosition = unitTranslation.Value;
                
                var closestTargetEntity = Entity.Null;
                var closestTargetPosition = float3.zero;

                var targetEntities = _targetQuery.ToEntityArray(Allocator.TempJob);
                var targetTranslations = _targetQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
                {
                    for (var i = 0; i < targetEntities.Length; ++i)
                    {
                        var targetEntity = targetEntities[i];
                        var targetTranslation = targetTranslations[i];
                        
                        if (closestTargetEntity == Entity.Null)
                        {
                            closestTargetEntity = targetEntity;
                            closestTargetPosition = targetTranslation.Value;
                        }
                        else
                        {
                            var comparedTargetDistance = math.distance(unitPosition, targetTranslation.Value);
                            var cachedTargetDistance = math.distance(unitPosition, closestTargetPosition);
                            if (comparedTargetDistance < cachedTargetDistance)
                            {
                                closestTargetEntity = targetEntity;
                                closestTargetPosition = targetTranslation.Value;
                            }
                        }
                    }
                }

                if (closestTargetEntity != Entity.Null)
                {
                    Debug.Log($"closest: {closestTargetEntity}");
                    // Debug.DrawLine(unitPosition, closestTargetPosition);
                    commandBuffer.AddComponent<HasTarget>(unitEntity, new HasTarget
                    {
                        targetEntity = closestTargetEntity
                    });
                }

                targetEntities.Dispose();
                targetTranslations.Dispose();
            })
                .WithoutBurst()
                .Run();
        }
    }
}