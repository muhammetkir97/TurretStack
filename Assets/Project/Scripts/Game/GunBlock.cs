using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Cannon,
    Rocket
}

public class GunBlock : MonoBehaviour
{
    private GameObject claimedTank;

    private StateMachineController stateMachine;
    private SMBlockIdle stateBlockIdle;
    private SMBlockClaimed stateBlockClaimed;

    private bool readyForFire = false;

    private Transform ammoParent;
    private Transform   muzzlesParent;
    private Transform gunParent;

    private GunType gunType;
    private bool isInit = false;

    void Start()
    {

    }

    public void Init(GunType gun)
    {
        gunParent = transform.GetChild(0);
        ammoParent = transform.GetChild(1);
        muzzlesParent = transform.GetChild(2);
        stateMachine = new StateMachineController();
        stateMachine.InitStateMachine(gameObject);
        stateBlockIdle = new SMBlockIdle(stateMachine);
        stateBlockClaimed = new SMBlockClaimed(stateMachine);   

        gunType = gun;
        stateMachine.SetState(stateBlockIdle);

        switch(gun)
        {
            case GunType.Cannon:
                EnableGunModel(0);
                break;

            case GunType.Rocket:
                EnableGunModel(1);
                break;
        }
        isInit = true;
    }



    void EnableGunModel(int selectedModel)
    {
        for(int i=0; i<gunParent.childCount; i++)
        {
            gunParent.GetChild(i).gameObject.SetActive(i == selectedModel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isInit) stateMachine.UpdateState();
    }

    public void ClaimBlock(GameObject target)
    {
        claimedTank = target;
        stateMachine.SetState(stateBlockClaimed);
        Invoke("enableShooting",1f);
    }

    void enableShooting()
    {
        readyForFire = true;
    }

    public bool getShootingStatus()
    {
        return readyForFire;
    }

    public void Shoot()
    {
        if(readyForFire)
        {
            GetMuzzle().Play();
            GameObject ammo = GetAmmo();
            GameObject cloneAmmo = Instantiate(ammo,ammo.transform.position,ammo.transform.rotation);
            cloneAmmo.SetActive(true);
            Rigidbody ammoRb = cloneAmmo.AddComponent<Rigidbody>();
            ammoRb.AddForce(cloneAmmo.transform.right * 50,ForceMode.Impulse);
            iTween.PunchScale(gameObject,Vector3.one * 2.5f,0.1f);
            
            /*
            cloneAmmo.GetComponent<SphereCollider>().enabled = true;
            cloneAmmo.GetComponent<MeshRenderer>().enabled = true;
            */
        }
    }

    ParticleSystem GetMuzzle()
    {
        ParticleSystem muzzle = muzzlesParent.GetChild(0).GetComponent<ParticleSystem>();
        switch(gunType)
        {
            case GunType.Cannon:
                muzzle = muzzlesParent.GetChild(0).GetComponent<ParticleSystem>();
                break;

            case GunType.Rocket:
                muzzle = muzzlesParent.GetChild(1).GetComponent<ParticleSystem>();
                break;
        }

        return muzzle;
    }

    GameObject GetAmmo()
    {
        GameObject ammo = ammoParent.GetChild(0).gameObject;
        switch(gunType)
        {
            case GunType.Cannon:
                ammo = ammoParent.GetChild(0).gameObject;
                break;

            case GunType.Rocket:
                ammo = ammoParent.GetChild(1).gameObject;
                break;
        }

        return ammo;
    }
}
