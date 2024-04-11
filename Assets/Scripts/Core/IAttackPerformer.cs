using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackPerformer
{
    void Perform(AttackInstruction moveInstruction);
}
