using System;
using UnityEngine;

public class CarControllerVex : MonoBehaviour
{
    #region Variables
    // * //
    #region General
    [Header("General")]
    public DrivetrainType drivetrainType = DrivetrainType.FourWheelDrive;
    public UnitType unitType;
    public float downforce = 100f;
    #endregion

    #region Gears
    [Header("Gears")]
    public int NoOfGears = 5; //Starts at 0 so its actually 6 gears if we put 5
    //*PRIVATE//
    private int m_GearNum;
    private float m_GearFactor;
    #endregion

    #region Wheels
    [Header("Wheels")]
    public WheelCollider[] m_WheelColliders = new WheelCollider[4];
    public GameObject[] m_WheelMeshes = new GameObject[4];
    public WheelEffects[] m_WheelEffects = new WheelEffects[4];
    //*PRIVATE//
    private Quaternion[] m_WheelMeshLocalRotations;
    #endregion

    #region Steering
    public WheelSteerType wheelSteerType = WheelSteerType.FrontWheelsTurn;
    [Header("Steering")]
    [Tooltip("The maximum angle that the player can rotate the wheels to turn.")]
    public float m_MaximumSteerAngle;
    [Tooltip("0 is completely modified, 1 is the raw input from the controller/keyboard. This doesn't change the time to steer but rather the max steering angle (multiplied).")]
    public float steeringSensetivity = .5f;
    [Tooltip("The higher the number the faster the wheels will increase thier steering angle. 0 wont turn the wheels at all.")]
    public float steerIntensity = 25;
    [Tooltip("The higher the number the faster the wheels will reset to no steer angle. 0 wont reset the wheels at all.")]
    public float steerResetIntensity = 10;
    [Range(0, 50), Tooltip("0 doesnt affect the steering delata time at all, 50 just about cancels the use of time. The higher the number the faster the steering will respond but it will also nullify the smoothing effect.")]
    public float steerTimeEffector = 50;
    [Range(0, 1), Tooltip("0 is raw physics, 1 the car will grip in the direction it is facing.")]
    public float m_SteerHelper;
    [Range(0, 1), Tooltip("0 is no traction control, 1 is full interference.")]
    public float m_TractionControl;
    public SteerSmoothPoint[] steerSmoothPoints;
    public float slowSteeringEffector = 1;

    //*PRIVATE//
    private float m_SteerAngle;
    #endregion

    #region Acceleration
    [Header("Acceleration")]
    public float m_FullTorqueOverAllWheels;
    public float m_Topspeed = 200;
    public float m_RevRangeBoundary = 1f;
    #endregion

    #region Reversing & Braking
    [Header("Reversing & Braking")]
    public float m_ReverseTorque;
    public float m_BrakeTorque;
    public float m_MaxHandbrakeTorque;
    public float handbrakeMultiplier = 2;
    //*PRIVATE//
    private const float k_ReversingThreshold = 0.01f;
    private float defaultStiffness;
    #endregion

    #region Misc.
    [Header("Misc.")]
    public Vector3 m_CentreOfMassOffset;
    public float m_SlipLimit;
    //*PRIVATE//
    private Vector3 m_Prevpos, m_Pos;
    private float m_OldRotation;
    private float m_CurrentTorque;
    private Rigidbody m_Rigidbody;
    #endregion

    #region Special Access
    //*PRIVATE//
    public bool Skidding { get; private set; }
    public float BrakeInput { get; private set; }
    public float CurrentSteerAngle { get { return m_SteerAngle; } }
    public float CurrentSpeed { get { return m_Rigidbody.velocity.magnitude * 2.23693629f; } }
    public float MaxSpeed { get { return m_Topspeed; } }
    public float Revs { get; private set; }
    public float AccelInput { get; private set; }
    #endregion

    public float turboAmount = 50;

    #endregion


    // Use this for initialization
    private void Start()
    {
        m_WheelMeshLocalRotations = new Quaternion[4];
        for (int i = 0; i < 4; i++)
        {
            m_WheelMeshLocalRotations[i] = m_WheelMeshes[i].transform.localRotation;
        }
        m_WheelColliders[0].attachedRigidbody.centerOfMass = m_CentreOfMassOffset;

        m_MaxHandbrakeTorque = int.MaxValue;

        m_Rigidbody = GetComponent<Rigidbody>();
        m_CurrentTorque = m_FullTorqueOverAllWheels - (m_TractionControl * m_FullTorqueOverAllWheels);

        defaultStiffness = m_WheelColliders[2].sidewaysFriction.stiffness;
    }


    private void GearChanging()
    {
        float f = Mathf.Abs(CurrentSpeed / MaxSpeed);
        float upgearlimit = (1 / (float)NoOfGears) * (m_GearNum + 1);
        float downgearlimit = (1 / (float)NoOfGears) * m_GearNum;

        if (m_GearNum > 0 && f < downgearlimit)
        {
            m_GearNum--;
        }

        if (f > upgearlimit && (m_GearNum < (NoOfGears - 1)))
        {
            m_GearNum++;
        }
    }


    // simple function to add a curved bias towards 1 for a value in the 0-1 range
    private static float CurveFactor(float factor)
    {
        return 1 - (1 - factor) * (1 - factor);
    }


    // unclamped version of Lerp, to allow value to exceed the from-to range
    private static float ULerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }


    private void CalculateGearFactor()
    {
        float f = (1 / (float)NoOfGears);
        // gear factor is a normalised representation of the current speed within the current gear's range of speeds.
        // We smooth towards the 'target' gear factor, so that revs don't instantly snap up or down when changing gear.
        var targetGearFactor = Mathf.InverseLerp(f * m_GearNum, f * (m_GearNum + 1), Mathf.Abs(CurrentSpeed / MaxSpeed));
        m_GearFactor = Mathf.Lerp(m_GearFactor, targetGearFactor, Time.deltaTime * 5f);
    }


    private void CalculateRevs()
    {
        // calculate engine revs (for display / sound)
        // (this is done in retrospect - revs are not used in force/power calculations)
        CalculateGearFactor();
        var gearNumFactor = m_GearNum / (float)NoOfGears;
        var revsRangeMin = ULerp(0f, m_RevRangeBoundary, CurveFactor(gearNumFactor));
        var revsRangeMax = ULerp(m_RevRangeBoundary, 1f, gearNumFactor);
        Revs = ULerp(revsRangeMin, revsRangeMax, m_GearFactor);
    }


    public void Move(float steering, float accel, float footbrake, float handbrake, bool isTurbo)
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 position;
            m_WheelColliders[i].GetWorldPose(out position, out quat);
            m_WheelMeshes[i].transform.position = position;
            m_WheelMeshes[i].transform.rotation = quat;
        }

        //clamp input values
        steering = Mathf.Clamp(steering, -1, 1);
        AccelInput = accel = Mathf.Clamp(accel, 0, 1);
        BrakeInput = footbrake = -1 * Mathf.Clamp(footbrake, -1, 0);
        handbrake = Mathf.Clamp(handbrake, 0, 1);

        accel = (isTurbo) ? accel + turboAmount : accel;

        #region Steering
        // * //
        #region Fast Magnitude //!Not implemented currently (work on functionality for smoothing between these points)
        float[] fastMag = new float[steerSmoothPoints.Length];
        int currentRvIndex = 0;

        for (int i = 0; i < steerSmoothPoints.Length; i++)
        {
            fastMag[i] = m_Rigidbody.velocity.magnitude;
            if (fastMag[i] > steerSmoothPoints[i].triggerMagnitude && handbrake < .1f)
            {
                fastMag[i] *= steerSmoothPoints[i].magEffector;
                currentRvIndex = i;
            }
            else
            {
                fastMag[i] = 1;
            }
        }
        #endregion

        #region Slow Magnitude
        float slowMag = m_Rigidbody.velocity.magnitude;
        if (slowMag < 10)
        {
            slowMag = slowSteeringEffector;
        }
        else
        {
            slowMag = 1;
        }
        #endregion


        float anglePct = 100 / (m_MaximumSteerAngle / m_SteerAngle);
        float minWaitTime = 1;

        if (m_SteerAngle == 0)
        {
            anglePct = 100 / m_MaximumSteerAngle;
            print("");
        }
        anglePct = Mathf.Abs(anglePct); //Make our number positive if its negaitve
        anglePct = 100 - anglePct;
        anglePct = Mathf.Clamp(anglePct, minWaitTime, m_MaximumSteerAngle); //Clamp between min and max to reduce initial wait time and avoid extremities

        m_SteerAngle = (m_SteerAngle + (steering * Time.deltaTime * anglePct * steerIntensity));
        m_SteerAngle = Mathf.Clamp(m_SteerAngle, -m_MaximumSteerAngle * slowMag, m_MaximumSteerAngle * slowMag);

        if (Mathf.Abs(steering) < 0.03f)
        {
            m_SteerAngle = m_SteerAngle - (m_SteerAngle * steerResetIntensity * Time.deltaTime);
        }

        switch (wheelSteerType)
        {
            case WheelSteerType.FrontWheelsTurn:
                m_WheelColliders[0].steerAngle = m_SteerAngle;
                m_WheelColliders[1].steerAngle = m_SteerAngle;
                break;
            case WheelSteerType.RearWheelsTurn:
                m_WheelColliders[2].steerAngle = m_SteerAngle;
                m_WheelColliders[3].steerAngle = m_SteerAngle;
                break;
            case WheelSteerType.FourWheelsTurn:
                m_WheelColliders[0].steerAngle = m_SteerAngle;
                m_WheelColliders[1].steerAngle = m_SteerAngle;
                m_WheelColliders[2].steerAngle = m_SteerAngle / 2;
                m_WheelColliders[3].steerAngle = m_SteerAngle / 2;
                break;
            default:
                print("**ERROR: Steering had to default to Front Wheels Turn**");
                m_WheelColliders[0].steerAngle = m_SteerAngle;
                m_WheelColliders[1].steerAngle = m_SteerAngle;
                break;
        }
        #endregion

        SteerHelper();
        ApplyDrive(accel, footbrake);
        CapSpeed();

        //Set the handbrake.
        //Assuming that wheels 2 and 3 are the rear wheels.
        if (handbrake > 0f)
        {
            var hbTorque = handbrake * m_MaxHandbrakeTorque;
            m_WheelColliders[2].brakeTorque = hbTorque;
            m_WheelColliders[3].brakeTorque = hbTorque;

            // m_WheelColliders[2].sidewaysFriction.stiffness = defaultStiffness;
        }
        else
        {

        }


        CalculateRevs();
        GearChanging();

        AddDownForce();
        CheckForWheelSpin();
        TractionControl();
    }


    private void CapSpeed()
    {
        float speed = m_Rigidbody.velocity.magnitude;
        switch (unitType)
        {
            case UnitType.MPH:

                speed *= 2.23693629f;
                if (speed > m_Topspeed)
                    m_Rigidbody.velocity = (m_Topspeed / 2.23693629f) * m_Rigidbody.velocity.normalized;
                break;

            case UnitType.KPH:
                speed *= 3.6f;
                if (speed > m_Topspeed)
                    m_Rigidbody.velocity = (m_Topspeed / 3.6f) * m_Rigidbody.velocity.normalized;
                break;
        }
    }


    private void ApplyDrive(float accel, float footbrake)
    {
        float thrustTorque;
        switch (drivetrainType)
        {
            case DrivetrainType.FourWheelDrive:
                thrustTorque = accel * (m_CurrentTorque / 4f);
                for (int i = 0; i < 4; i++)
                {
                    m_WheelColliders[i].motorTorque = thrustTorque;
                }
                break;

            case DrivetrainType.FrontWheelDrive:
                thrustTorque = accel * (m_CurrentTorque / 2f);
                m_WheelColliders[0].motorTorque = m_WheelColliders[1].motorTorque = thrustTorque;
                break;

            case DrivetrainType.RearWheelDrive:
                thrustTorque = accel * (m_CurrentTorque / 2f);
                m_WheelColliders[2].motorTorque = m_WheelColliders[3].motorTorque = thrustTorque;
                break;

        }

        for (int i = 0; i < 4; i++)
        {
            if (CurrentSpeed > 5 && Vector3.Angle(transform.forward, m_Rigidbody.velocity) < 50f)
            {
                m_WheelColliders[i].brakeTorque = m_BrakeTorque * footbrake;
            }
            else if (footbrake > 0)
            {
                m_WheelColliders[i].brakeTorque = 0f;
                m_WheelColliders[i].motorTorque = -m_ReverseTorque * footbrake;
            }
        }
    }


    private void SteerHelper()
    {
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelhit;
            m_WheelColliders[i].GetGroundHit(out wheelhit);
            if (wheelhit.normal == Vector3.zero)
                return; // wheels arent on the ground so dont realign the rigidbody velocity
        }

        // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
        if (Mathf.Abs(m_OldRotation - transform.eulerAngles.y) < 10f)
        {
            var turnadjust = (transform.eulerAngles.y - m_OldRotation) * m_SteerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
            m_Rigidbody.velocity = velRotation * m_Rigidbody.velocity;
        }
        m_OldRotation = transform.eulerAngles.y;
    }


    // this is used to add more grip in relation to speed
    private void AddDownForce()
    {
        m_WheelColliders[0].attachedRigidbody.AddForce(-transform.up * downforce *
                                                     m_WheelColliders[0].attachedRigidbody.velocity.magnitude);
    }


    // checks if the wheels are spinning and is so does three things
    // 1) emits particles
    // 2) plays tiure skidding sounds
    // 3) leaves skidmarks on the ground
    // these effects are controlled through the WheelEffects class
    private void CheckForWheelSpin()
    {
        // loop through all wheels
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelHit;
            m_WheelColliders[i].GetGroundHit(out wheelHit);

            // is the tire slipping above the given threshhold
            if (Mathf.Abs(wheelHit.forwardSlip) >= m_SlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) >= m_SlipLimit)
            {
                m_WheelEffects[i].EmitTyreSmoke();

                // avoiding all four tires screeching at the same time
                // if they do it can lead to some strange audio artefacts
                if (!AnySkidSoundPlaying())
                {
                    m_WheelEffects[i].PlayAudio();
                }
                continue;
            }

            // if it wasnt slipping stop all the audio
            if (m_WheelEffects[i].PlayingAudio)
            {
                m_WheelEffects[i].StopAudio();
            }
            // end the trail generation
            m_WheelEffects[i].EndSkidTrail();
        }
    }

    // crude traction control that reduces the power to wheel if the car is wheel spinning too much
    private void TractionControl()
    {
        WheelHit wheelHit;
        switch (drivetrainType)
        {
            case DrivetrainType.FourWheelDrive:
                // loop through all wheels
                for (int i = 0; i < 4; i++)
                {
                    m_WheelColliders[i].GetGroundHit(out wheelHit);

                    AdjustTorque(wheelHit.forwardSlip);
                }
                break;

            case DrivetrainType.RearWheelDrive:
                m_WheelColliders[2].GetGroundHit(out wheelHit);
                AdjustTorque(wheelHit.forwardSlip);

                m_WheelColliders[3].GetGroundHit(out wheelHit);
                AdjustTorque(wheelHit.forwardSlip);
                break;

            case DrivetrainType.FrontWheelDrive:
                m_WheelColliders[0].GetGroundHit(out wheelHit);
                AdjustTorque(wheelHit.forwardSlip);

                m_WheelColliders[1].GetGroundHit(out wheelHit);
                AdjustTorque(wheelHit.forwardSlip);
                break;
        }
    }


    private void AdjustTorque(float forwardSlip)
    {
        if (forwardSlip >= m_SlipLimit && m_CurrentTorque >= 0)
        {
            m_CurrentTorque -= 10 * m_TractionControl;
        }
        else
        {
            m_CurrentTorque += 10 * m_TractionControl;
            if (m_CurrentTorque > m_FullTorqueOverAllWheels)
            {
                m_CurrentTorque = m_FullTorqueOverAllWheels;
            }
        }
    }


    private bool AnySkidSoundPlaying()
    {
        for (int i = 0; i < 4; i++)
        {
            if (m_WheelEffects[i].PlayingAudio)
            {
                return true;
            }
        }
        return false;
    }
}

public enum DrivetrainType
{
    FrontWheelDrive,
    RearWheelDrive,
    FourWheelDrive
}

public enum WheelSteerType
{
    FrontWheelsTurn,
    RearWheelsTurn,
    FourWheelsTurn
}

public enum UnitType
{
    MPH,
    KPH
}

[System.Serializable]
public struct SteerSmoothPoint
{
    public float triggerMagnitude;
    public float magEffector;
}