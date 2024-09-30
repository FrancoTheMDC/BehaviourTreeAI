using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheifBehavior : MonoBehaviour
{
    BehaviourTree tree;
    public GameObject paint;
    public GameObject van;
    public GameObject frontDoor;
    public GameObject backDoor;

    NavMeshAgent agent;

    public enum ActionState { IDLE, WORKING };
    ActionState state = ActionState.IDLE;
    Node.Status treeStatus = Node.Status.RUNNING;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Sequence steal = new Sequence("Steal Paint");

        Leaf goToPaint = new Leaf("Go to Paint", GoToPaint);
        Leaf goToVan = new Leaf("Go to Van", GoToVan);
        Leaf goToFrontDoor = new Leaf("Go to Front Door", GoToFrontDoor);
        Leaf goToBackDoor = new Leaf("Go to Back Door", GoToBackDoor);

        tree.AddChild(steal);

        steal.AddChild(goToBackDoor);
        steal.AddChild(goToPaint);
        steal.AddChild(goToFrontDoor);
        steal.AddChild(goToVan);

        tree.PrintTree();
        //tree.Process();

        //agent.SetDestination(paint.transform.position);
    }

    public Node.Status GoToPaint()
    {
        //agent.SetDestination(paint.transform.position);
        //return Node.Status.SUCCESS;
        return GoToLocation(paint.transform.position);
    }

    public Node.Status GoToVan()
    {
        //agent.SetDestination(van.transform.position);
        //return Node.Status.SUCCESS;
        return GoToLocation(van.transform.position);
    }

    public Node.Status GoToFrontDoor()
    {
        return GoToLocation(frontDoor.transform.position);
    }

    public Node.Status GoToBackDoor()
    {
        return GoToLocation(backDoor.transform.position);
    }

    Node.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);

        if (state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }
        else if (Vector3.Distance(agent.pathEndPosition, destination) >= 2)
        {
            state = ActionState.IDLE;
            return Node.Status.FAILURE;
        }
        else if (distanceToTarget < 2)
        {
            state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }

        return Node.Status.RUNNING;
    }

    // Update is called once per frame
    void Update()
    {
        if(treeStatus == Node.Status.RUNNING)
        {
            treeStatus = tree.Process();
        }
    }
}
