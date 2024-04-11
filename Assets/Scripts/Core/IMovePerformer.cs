using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovePerformer
{
    void Perform(MoveInstruction moveInstruction);
}
