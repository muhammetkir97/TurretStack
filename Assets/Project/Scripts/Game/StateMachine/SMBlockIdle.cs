using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMBlockIdle : State
{
    Transform currentTransform;
    public SMBlockIdle(StateMachineController controller) : base(controller)
    {
    }

    public override void OnStateEnter()
    {
        currentTransform = controller.GetCurrentObject().transform;
        currentTransform.gameObject.layer = Globals.gunBlockIdleLayer;
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        Vector3 pos = currentTransform.position;
        pos.y = 0.5f + Mathf.PingPong(Time.time * 0.8f, 0.7f);

        currentTransform.position = pos;
        currentTransform.Rotate(Vector3.up * Time.deltaTime * 50,Space.World);
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
