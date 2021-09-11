using System;
using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerInputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref MoveData moveData, in InputData inputData) =>
            {
                bool isRightKeyPressed = Input.GetKey(inputData.RightKey);
                bool isLeftKeyPressed = Input.GetKey(inputData.LeftKey);
                bool isUpKeyPressed = Input.GetKey(inputData.UpKey);
                bool isDownKeyPressed = Input.GetKey(inputData.DownKey);
            
                moveData.direction.x = Convert.ToInt32(isRightKeyPressed);
                moveData.direction.x -= Convert.ToInt32(isLeftKeyPressed);
                moveData.direction.z = Convert.ToInt32(isUpKeyPressed);
                moveData.direction.z -= Convert.ToInt32(isDownKeyPressed);
            
            }).Run();
        }
    }
}
