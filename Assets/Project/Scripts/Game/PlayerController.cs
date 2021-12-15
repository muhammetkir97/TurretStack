using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TankController tankController;

    void Start()
    {
        Application.targetFrameRate = 30;
        tankController.SetBaseSpeed(0.1f);
        tankController.SetBaseTurnSpeed(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(Input.GetKeyDown(KeyCode.Space)) Shoot();
    }

    void Movement()
    {
        float verticalValue = ControllerSystem.GetAxis("Vertical");
        float horizontalValue = ControllerSystem.GetAxis("Horizontal");
        Vector2 inputValues = new Vector2(horizontalValue, verticalValue);

        tankController.SetMovementVector(inputValues);
    }

    void Shoot()
    {
        tankController.Shoot();
    }
}
