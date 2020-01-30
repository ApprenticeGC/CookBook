namespace GiantCroissant.LearnECS.HelloCube002
{
    using Unity.Burst;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Jobs;
    using Unity.Mathematics;
    using Unity.Transforms;

    // This system updates all entities in the scene with both a RotationSpeed_IJobChunk and Rotation component.

    // ReSharper disable once InconsistentNaming
    public class RotationSpeedSystem : JobComponentSystem
    {
        private EntityQuery _group;

        protected override void OnCreate()
        {
            // Cached access to a set of ComponentData based on a specific query
            _group = GetEntityQuery(typeof(Rotation), ComponentType.ReadOnly<RotationSpeed>());
        }

        // Use the [BurstCompile] attribute to compile a job with Burst. You may see significant speed ups, so try it!
        [BurstCompile]
        struct RotationSpeedJob : IJobChunk
        {
            public float deltaTime;
            public ArchetypeChunkComponentType<Rotation> rotationType;
            [ReadOnly] public ArchetypeChunkComponentType<RotationSpeed> rotationSpeedType;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var chunkRotations = chunk.GetNativeArray(rotationType);
                var chunkRotationSpeeds = chunk.GetNativeArray(rotationSpeedType);
                for (var i = 0; i < chunk.Count; i++)
                {
                    var rotation = chunkRotations[i];
                    var rotationSpeed = chunkRotationSpeeds[i];

                    var speed = rotationSpeed.radiansPerSecond * deltaTime;

                    // Rotate something about its up vector at the speed given by RotationSpeed_IJobChunk.
                    chunkRotations[i] = new Rotation
                    {
                        Value = math.mul(math.normalize(rotation.Value),
                            quaternion.AxisAngle(math.up(), speed))
                    };
                }
            }
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            // Explicitly declare:
            // - Read-Write access to Rotation
            // - Read-Only access to RotationSpeed_IJobChunk
            var rotationType = GetArchetypeChunkComponentType<Rotation>();
            var rotationSpeedType = GetArchetypeChunkComponentType<RotationSpeed>(true);

            var job = new RotationSpeedJob()
            {
                rotationType = rotationType,
                rotationSpeedType = rotationSpeedType,
                deltaTime = Time.DeltaTime
            };

            return job.Schedule(_group, inputDependencies);
        }
    }
}