using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private TurretSpawner turretSpawner;

    void Start()
    {
        turretSpawner.SetSpawnStatus(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
