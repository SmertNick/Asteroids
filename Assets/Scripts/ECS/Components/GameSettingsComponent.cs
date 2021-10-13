using Unity.Entities;

namespace ECS.Components
{
    [GenerateAuthoringComponent]
    public struct GameSettingsComponent : IComponentData
    {
        public int numAsteroids;
        public float levelWidth;
        public float levelHeight;
        public float offset;
    }
}
