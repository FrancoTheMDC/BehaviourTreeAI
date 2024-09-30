using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public delegate Status Tick(); // one loop around the action
    public Tick ProcessMethod;

    public Leaf() { }

    public Leaf(string n, Tick ptrMethod)
    {
        name = n;
        ProcessMethod = ptrMethod;
    }

    public override Status Process()
    {
        if (ProcessMethod != null)
            return ProcessMethod();

        return Status.FAILURE;
    }
}
