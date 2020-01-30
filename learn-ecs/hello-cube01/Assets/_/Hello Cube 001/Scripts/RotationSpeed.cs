namespace GiantCroissant.LearnECS.HelloCube001
{
    using Unity.Entities;

    [GenerateAuthoringComponent]
    public struct RotationSpeed : IComponentData
    {
        public float radiansPerSecond;
    }
}
