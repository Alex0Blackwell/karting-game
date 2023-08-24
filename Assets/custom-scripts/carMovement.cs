// credit: https://github.com/coderDarren/Unity3D-Cars

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMovement : MonoBehaviour
{
    public GameObject mobileUI;

#if UNITY_STANDALONE                            //Movement input for standalone platforms

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = -Input.GetAxis("Vertical");
    }

#endif

#if UNITY_ANDROID || UNITY_IOS                  //Movement input for mobile platforms


    public void LeftButton()
    {
        m_horizontalInput = -1f;
    }

    public void RightButton()
    {
        m_horizontalInput = 1f;
    }

    public void ForwardButton()
    {
        m_verticalInput = -1f;
    }

    public void ReverseButton()
    {
        m_verticalInput = 1f;
    }

    public void StopHorizontalMovement()
    {
        m_horizontalInput = 0f;
    }

    public void StopVerticalMovement()
    {
        m_verticalInput = 0f;
    }

    public void ResetCarPosition()
    {
        transform.position = startingPosition;
        transform.rotation = startingRotation;
    }

#endif

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = m_verticalInput * motorForce;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS // Enables Mobile UI

        mobileUI.SetActive(true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

#endif
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    private void FixedUpdate()
    {

#if UNITY_STANDALONE
        GetInput();
#endif

        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
    [HideInInspector]
    public float m_horizontalInput;
    [HideInInspector]
    public float m_verticalInput;
    [HideInInspector]
    public float m_steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 15;
    public float motorForce = 500;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    public Player player;
}