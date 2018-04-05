using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLCarAgent : Agent {

    public GameObject path;
    private Vector3 startPosition;
    private Quaternion startQuaternion;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    public static bool checkbool = false;
    //[Header("Sensors")]
    //public float sensorLength = 0f;
    //public Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
    //public float frontSideSensorPosition = 0.2f;
    //public float frontSensorAngle = 30f;

    void Start () {
        startPosition = gameObject.transform.position;
        startQuaternion = gameObject.transform.rotation;
        
    }



    public override void CollectObservations()
    {
        //AddVectorObs((path.transform.position - gameObject.transform.position));
        AddVectorObs(wheelFL.steerAngle);
        AddVectorObs(wheelFR.steerAngle);
        //AddVectorObs(wheelFL.motorTorque);
        //AddVectorObs(wheelFR.motorTorque);
        SetTextObs("Testing " + gameObject.GetInstanceID());
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //print(vectorAction[0]);

        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            if (vectorAction[0] == 1)
            {
                wheelFL.steerAngle = 45f;
                wheelFR.steerAngle = 45f;
            }
            else if (vectorAction[0] == -1)
            {
                wheelFL.steerAngle = -45f;
                wheelFR.steerAngle = -45f;
            }
            else
            {
                wheelFL.steerAngle = 0;
                wheelFR.steerAngle = 0;
            }

            //if (vectorAction[1] == 1)
            //{
            //    wheelFL.motorTorque += 10f;
            //    wheelFR.motorTorque += 10f;
            //}
            //else if (vectorAction[1] == -1)
            //{
            //    wheelFL.motorTorque -= 10f;
            //    wheelFR.motorTorque -= 10f;
            //}
            //else
            //{
            //    wheelFL.motorTorque = 0;
            //    wheelFR.motorTorque = 0;
            //}

            SetReward(0.1f);

        }
        if(checkbool)
        {
            Done();
            SetReward(-1f);
        }
        wheelFL.motorTorque = 50f;
        wheelFR.motorTorque = 50f;
    }
    //private bool Sensors()
    //{
    //    RaycastHit hit;
    //    Vector3 sensorStartPos = transform.position;
    //    sensorStartPos += transform.forward * frontSensorPosition.z;
    //    sensorStartPos += transform.up * frontSensorPosition.y;

    //    //front right sensor
    //    sensorStartPos += transform.right * frontSideSensorPosition;
    //    if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
    //    {
    //            Debug.DrawLine(sensorStartPos, hit.point);
    //    }

    //    //front right angle sensor
    //    else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
    //    {
    //            Debug.DrawLine(sensorStartPos, hit.point);
    //    }

    //    //front left sensor
    //    sensorStartPos -= transform.right * frontSideSensorPosition * 2;
    //    if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
    //    {
    //            Debug.DrawLine(sensorStartPos, hit.point);
    //    }

    //    //front left angle sensor
    //    else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
    //    {
    //            Debug.DrawLine(sensorStartPos, hit.point);
    //    }
    //    return (false);

    //}

    public override void AgentReset()
    {
        gameObject.transform.position = new Vector3(1.2f, 10, -2);
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        wheelFL.steerAngle = 0;
        wheelFR.steerAngle = 0;
        wheelFL.motorTorque = 0;
        wheelFR.motorTorque = 0;
        checkbool = false;
    }

}
