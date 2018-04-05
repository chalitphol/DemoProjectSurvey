using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollision : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("wall"))
        {
            print(MLCarAgent.checkbool);
            MLCarAgent.checkbool = true;
        }
    }
}
