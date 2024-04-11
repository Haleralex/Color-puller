using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private StarterAssetsInputs input;
    
    /*void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (!input.attack)
            return;
        
        player.Attack();
    }*/
}
