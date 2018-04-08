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

    [Header("Sensors")]
    public float sensorLength = 3f;
    public Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
    public float frontSideSensorPosition = 0.2f;
    public float frontSensorAngle = 30f;

    void Start () {
        startPosition = gameObject.transform.position;
        startQuaternion = gameObject.transform.rotation;
        
    }



    public override void CollectObservations()
    {
        AddVectorObs(Sensors(1));
        AddVectorObs(Sensors(3));
        //AddVectorObs((path.transform.position - gameObject.transform.position));
        //AddVectorObs(this.transform.localPosition.x);
        //AddVectorObs(this.transform.localPosition.z);
        //AddVectorObs(wheelFL.motorTorque);
        //AddVectorObs(wheelFR.motorTorque);
        SetTextObs("Testing " + gameObject.GetInstanceID());
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        print("0: " + vectorAction[0]);
        print("1: " + vectorAction[1]);
        //print(textAction);

        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            float action = Mathf.Clamp(vectorAction[0], -1f, 1f);
            if (action > 0.3 || action < -0.3)
            {
                wheelFL.steerAngle = 45f * action;
                wheelFR.steerAngle = 45f * action;
                SetReward(0.1f);

            }
            else
            {
                wheelFL.steerAngle = 0;
                wheelFR.steerAngle = 0;
                SetReward(0.3f);

            }
            //float action = Mathf.InverseLerp(0f,1f, vectorAction[0]);
            //float action2 = Mathf.InverseLerp(0f, 1f, vectorAction[1]);

            //print(action);
            //print(action2);

            //if (action < 0.5 && action != 0)
            //{
            //    wheelFL.steerAngle = -45f * 1-action;
            //    wheelFR.steerAngle = -45f * 1-action;
            //}
            //else
            //{
            //    wheelFL.steerAngle = 0f;
            //    wheelFR.steerAngle = 0f;

            //}
            //if (action2 < 0.5 && action != 0)
            //{
            //    wheelFL.steerAngle = 45f * 1-action2;
            //    wheelFR.steerAngle = 45f * 1-action2;
            //}
            //else
            //{
            //    wheelFL.steerAngle = 0f;
            //    wheelFR.steerAngle = 0f;
            //}



        }
        if(checkbool)
        {
            Done();
            SetReward(-1f);
        }
        wheelFL.motorTorque = 50f;
        wheelFR.motorTorque = 50f;
    }

    private float Sensors(int select)
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorPosition.z;
        sensorStartPos += transform.up * frontSensorPosition.y;
        switch (select)
        {
            case 0:
                //front right sensor
                sensorStartPos += transform.right * frontSideSensorPosition;
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    return hit.distance;
                }
                else
                    return 99;
            case 1:
                //front right angle sensor
                if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    return (hit.distance);
                }
                else
                    return 99;
            case 2:
                //front left sensor
                sensorStartPos -= transform.right * frontSideSensorPosition * 2;
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    return (hit.distance);
                }
                else
                    return 99;
            case 3:
                //front left angle sensor
                if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    return hit.distance;
                }
                else
                    return 99;
        }
        return 99;
    }


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
