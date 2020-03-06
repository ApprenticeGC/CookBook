namespace GiantCroissant.FllowCodeMonkey.FindTargetInUnityECS
{
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;

    public class FindTargetSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<Unit>().ForEach((Entity uniEntity, Translation unitTranslation) =>
            {
                //
                var unitPosition = unitTranslation.Value;
                
                var closestTargetEntity = Entity.Null;
                var closestTargetPosition = float3.zero;
                Entities.WithAll<Target>().ForEach((Entity targetEntity, Translation targetTranslation) =>
                {
                    //
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
                })
                    .WithoutBurst()
                    .Run();

                if (closestTargetEntity != Entity.Null)
                {
                    Debug.DrawLine(unitPosition, closestTargetPosition);
                }
            })
                .WithoutBurst()
                .Run();
        }
    }
}