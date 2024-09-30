using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence(string n)
    {
        name = n;
    }

    public override Status Process()
    {
        //loop through its childre nand run their process method
        //no loop necessary as it's live in the Update method

        Status childStatus = children[currentChild].Process();

        if(childStatus == Status.RUNNING || childStatus == Status.FAILURE)
        {
            return childStatus;
        }

        currentChild++;

        if(currentChild >= children.Count) //made it to end of sequence of actions?
        {
            currentChild = 0;
            return Status.SUCCESS;
        }

        return Status.RUNNING;

    }
}
