namespace GiantCroissant.FllowCodeMonkey.FindTargetInUnityECS
{
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [UpdateAfter(typeof(FindTargetSystem))]
    public class UnitMoveToTargetSystem : SystemBase
    {
        private EntityCommandBufferSystem _entityCommandBufferSystem;

        protected override void OnCreate()
        {
            base.OnCreate();

            _entityCommandBufferSystem = World.GetOrCreateSystem<EntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer();

            Entities.ForEach((Entity unitEntity, ref HasTarget hasTarget, ref Translation translation) =>
                {
                    var targetExist = EntityManager.Exists(hasTarget.targetEntity);
                    if (targetExist)
                    {
                        // Debug.Log($"has target");
                        var targetTranslation = EntityManager.GetComponentData<Translation>(hasTarget.targetEntity);
                        var targetDirection = math.normalize(targetTranslation.Value - translation.Value);
                        // var moveSpeed = 10.0f;
                        var moveSpeed = Random.Range(2.0f, 10.0f);

                        translation.Value += targetDirection * moveSpeed * Time.DeltaTime;

                        if (math.distance(translation.Value, targetTranslation.Value) < 0.2f)
                        {
                            Debug.Log($"move close to {hasTarget.targetEntity}");
                            commandBuffer.DestroyEntity(hasTarget.targetEntity);
                            commandBuffer.RemoveComponent<HasTarget>(unitEntity);
                        }
                    }
                    else
                    {
                        commandBuffer.RemoveComponent<HasTarget>(unitEntity);
                    }
                })
                .WithoutBurst()
                .Run();
        }
    }
}
