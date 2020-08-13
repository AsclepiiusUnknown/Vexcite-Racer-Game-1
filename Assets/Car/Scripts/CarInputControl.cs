using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CarControllerVex))]
public class CarInputControl : MonoBehaviour
{
    private CarControllerVex vexCar; // the car controller we want to use
    InputMaster input;
    Vector2 moveInput;
    Vector3 rotationInput;
    float handbrake;
    bool isTurbo = false;
    string lastTriggered;

    #region Setup
    private void Awake()
    {
        #region Variable Setup
        vexCar = GetComponent<CarControllerVex>(); // get the car controller
        input = new InputMaster(); //Set the Input Master
        #endregion

        #region Input Action Setup
        //* Handbrake
        input.Player.Handbrake.performed += ctx => HandbrakeChange(ctx.ReadValue<float>());
        input.Player.Handbrake.canceled += ctx => HandbrakeChange(ctx.ReadValue<float>());

        //* Turbo
        input.Player.Turbo.performed += ctx => TurboChange(true);
        input.Player.Turbo.canceled += ctx => TurboChange(false);

        //* Acceleration
        input.Player.Accelerate.performed += ctx => SpeedChange(ctx.ReadValue<float>());
        input.Player.Accelerate.canceled += ctx => SpeedChange(ctx.ReadValue<float>());

        //* Reverse
        input.Player.Reverse.performed += ctx => SpeedChange(-ctx.ReadValue<float>());
        input.Player.Reverse.canceled += ctx => SpeedChange(-ctx.ReadValue<float>());

        //* Steering
        input.Player.Steering.performed += ctx => SteerChange(ctx.ReadValue<Vector2>());
        input.Player.Steering.canceled += ctx => SteerChange(ctx.ReadValue<Vector2>());

        //* Rotation
        input.Player.RotateCar.performed += ctx => RotateCar(ctx.ReadValue<Vector2>());
        input.Player.RotateCar.canceled += ctx => RotateCar(ctx.ReadValue<Vector2>());
        #endregion
    }

    #region Input Enabling
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    #endregion
    #endregion

    #region Driving
    void SpeedChange(float speed)
    {
        if (speed > 0)
            lastTriggered = "Accel";
        else if (speed < 0)
            lastTriggered = "Brake";

        moveInput.y = speed;
    }
    #endregion

    #region Steering
    void SteerChange(Vector2 dir)
    {
        moveInput.x = dir.x;
    }
    #endregion

    #region Rotation
    void RotateCar(Vector2 dir)
    {
        rotationInput.x = 0;
        rotationInput.x = dir.x;
        rotationInput.z = dir.y;
    }
    #endregion

    #region Handbrake
    void HandbrakeChange(float value)
    {
        handbrake = value * vexCar.handbrakeMultiplier;
    }
    #endregion

    #region Turbo
    void TurboChange(bool goTurbo)
    {
        isTurbo = goTurbo;
        ApplyMovements();
    }
    #endregion

    private void FixedUpdate()
    {
        if (moveInput.y > 0 && lastTriggered == "Brake")
        {
            SpeedChange(moveInput.y);
        }

        ApplyMovements();
    }

    void ApplyMovements()
    {
        vexCar.Move(moveInput.x, moveInput.y, moveInput.y, handbrake, isTurbo);

        vexCar.Rotate(rotationInput);
    }
}