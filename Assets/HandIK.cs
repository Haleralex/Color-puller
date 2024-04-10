using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIK : MonoBehaviour
{
    [SerializeField] private Transform mainBody;
    [SerializeField] private Transform forRotate;
    public float angleZ = -30f;
    public float weight = 0;
    void LateUpdate()
    {
        forRotate.Rotate(Vector3.forward, Mathf.Lerp(0, angleZ, weight));
    }
}