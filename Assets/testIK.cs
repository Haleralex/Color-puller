using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testIK : MonoBehaviour
{
    [SerializeField] private Transform mainBody;
    [SerializeField] private Transform forRotate;
    public float angleZ = -30f;
    public float weight = 0;
    void LateUpdate()
    {
        //transform.LookAt(mainBody.position + mainBody.forward * 10);
        
        forRotate.Rotate(Vector3.forward, Mathf.Lerp(0, angleZ, weight));
    }
}