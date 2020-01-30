namespace GiantCroissant.LearnECS.HelloCube002
{
    using Unity.Entities;

    [System.Serializable]
    public struct RotationSpeed : IComponentData
    {
        public float radiansPerSecond;
    }
}