using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMBlockClaimed : State
{
    Transform currentTransform;

    public SMBlockClaimed(StateMachineController controller) : base(controller)
    {
    }

    public override void OnStateEnter()
    {
        currentTransform = controller.GetCurrentObject().transform;
        currentTransform.gameObject.layer = Globals.gunBlockClaimedLayer;

    }


    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {

    }


    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
