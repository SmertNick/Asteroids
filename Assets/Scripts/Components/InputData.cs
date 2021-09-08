using Unity.Entities;
using UnityEngine;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct InputData : IComponentData
    {
        public KeyCode UpKey;
        public KeyCode DownKey;
        public KeyCode LeftKey;
        public KeyCode RightKey;
    }
}
