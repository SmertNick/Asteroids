using System;
using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS.Systems
{
    [UpdateBefore(typeof(MoveForwardSystem))]
    public class PlayerInputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Entities.ForEach((ref MoveData moveData, in InputData inputData, in AccelerationData accelerationData) =>
            {
                bool isRightKeyPressed = Input.GetKey(inputData.RightKey);
                bool isLeftKeyPressed = Input.GetKey(inputData.LeftKey);
                bool isUpKeyPressed = Input.GetKey(inputData.UpKey);
                bool isDownKeyPressed = Input.GetKey(inputData.DownKey);

                moveData.direction.x = Convert.ToInt32(isRightKeyPressed);
                moveData.direction.x -= Convert.ToInt32(isLeftKeyPressed);
                moveData.direction.z = Convert.ToInt32(isUpKeyPressed);
                moveData.direction.z -= Convert.ToInt32(isDownKeyPressed);

                // var speed = moveData.speed + deltaTime *
                //             (Convert.ToInt32(Input.GetKey(inputData.UpKey)) * accelerationData.acceleration -
                //              accelerationData.drag);
                //
                // moveData.speed = math.clamp(speed, 0f, accelerationData.maxSpeed);


            }).Run();
        }
    }
}
