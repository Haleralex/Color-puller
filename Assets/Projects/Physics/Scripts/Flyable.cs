using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Flyable : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    public event Action<Flyable> NowOnGoodPos;

    public bool onGoodDistance = false;
    private Vector3 randomVec;
    private int randomAngle;
    public void AddPullingForce(Vector3 pos, float power = 1, float PowerOnGoodDistance = 1, float goodDistance = 1)
    {
        var force = (pos - transform.position);
        var magnitude = force.magnitude;
        var normalizedForce = force.normalized;

        if (onGoodDistance)
        {
            NowOnGoodPos?.Invoke(this);
        }
        else
        {
            if (magnitude > goodDistance)
            {
                rigidBody.AddForce(power * normalizedForce / magnitude, ForceMode.Force);
            }
            else
            {
                rigidBody.mass = 0.1f;
                onGoodDistance = true;
                randomAngle = UnityEngine.Random.Range(-90, 90);
            }
        }
    }
}
