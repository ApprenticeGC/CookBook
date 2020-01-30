namespace GiantCroissant.LearnECS.HelloCube001
{
    using Unity.Entities;
    using Unity.Jobs;
    using Unity.Mathematics;
    using Unity.Transforms;

    public class RotationSpeedSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var deltaTime = Time.DeltaTime;
            
            var jobHandle = Entities
                .WithName("RotationSpeedSystem")
                .ForEach((ref Rotation rotation, in RotationSpeed rotationSpeed) =>
                {
                    var speed = rotationSpeed.radiansPerSecond * deltaTime;
                    rotation.Value = math.mul(
                        math.normalize(rotation.Value),
                        quaternion.AxisAngle(math.up(), speed));
                })
                .Schedule(inputDeps);

            return jobHandle;
        }
    }
}