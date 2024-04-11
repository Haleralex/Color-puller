using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public abstract class Character : MonoBehaviour, IMovable, IAttack
{
    private CharacterStats characterStats;

    [Inject]
    private IAttackPerformer _attackPerformer;
    
    private IMovePerformer _movePerformer;
    
    public void Move(MoveInstruction instruction)
    {
        _movePerformer.Perform(instruction);
    }

    public void Attack(AttackInstruction instruction)
    {
        _attackPerformer.Perform(instruction);
    }
}