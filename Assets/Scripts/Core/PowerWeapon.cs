using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PowerWeapon : MonoBehaviour, IAttackPerformer
{
    [SerializeField] private Rigidbody rbs;
    [SerializeField] private Transform startPoint;
    public void Perform(AttackInstruction instruction)
    {
        rbs.transform.SetPositionAndRotation(startPoint.position, Quaternion.identity);
        rbs.velocity = Vector3.zero;
        
        rbs.AddForce(instruction.direction*100);
    }
}
