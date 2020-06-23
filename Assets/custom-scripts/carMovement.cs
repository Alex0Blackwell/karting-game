using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMovement : MonoBehaviour
{
  public float MotorForce, SteerForce, BrakeForce;
  public WheelCollider frontRight, frontLeft, backRight, backLeft;

  void Update() {
    float v = Input.GetAxis("Vertical") * MotorForce;
    float h = Input.GetAxis("Horizontal") * SteerForce;

    backLeft.motorTorque = backRight.motorTorque = -v;

    frontLeft.steerAngle = frontRight.steerAngle = h;

    if(Input.GetKey(KeyCode.Space)) {
      backLeft.brakeTorque = backRight.brakeTorque = BrakeForce;
    }
    if(Input.GetKeyUp(KeyCode.Space)) {
      backLeft.brakeTorque = backRight.brakeTorque = 0;
    }
    if(Input.GetAxis("Vertical") == 0) {
      backLeft.brakeTorque = backRight.brakeTorque = BrakeForce;
    } else {
      backLeft.brakeTorque = backRight.brakeTorque = 0;
    }
  }
}
