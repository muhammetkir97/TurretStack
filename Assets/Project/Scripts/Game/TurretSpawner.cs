using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    private bool spawnStatus = false;
    private float spawnTime = 0;
    private int spawnPointCount = 0;
    private int spawnTurretCount = 0;
    [SerializeField] private Transform turretParent;
    [SerializeField] private Transform mapParent;

    void Start()
    {
        spawnPointCount = transform.childCount;
        spawnTurretCount = turretParent.childCount;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetSpawnStatus()
    {
        return spawnStatus;
    }

    public void SetSpawnStatus(bool status)
    {
        spawnStatus = status;
        if(spawnStatus)
        {
            spawnTime = Globals.GetSpawnTime();
            InvokeRepeating("SpawnTurret",0,spawnTime);
        }
    }


    void SpawnTurret()
    {
        int randomPosition = Random.Range(0,spawnPointCount);
        int randomTurret = Random.Range(0,spawnTurretCount);

        GameObject selectedTurret = turretParent.GetChild(randomTurret).gameObject;

        GameObject cloneTurret = Instantiate(selectedTurret,transform.GetChild(randomPosition).position,selectedTurret.transform.rotation);
        cloneTurret.transform.SetParent(mapParent);

        cloneTurret.GetComponent<GunBlock>().Init((GunType)Random.Range(0,2));

    }
}
