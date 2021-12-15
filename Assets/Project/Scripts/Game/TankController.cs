using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{

    private float baseTurnSpeed;
    private float baseSpeed;
    private float smoothValue = 2;
    private float currentSmoothValue;
    private float currentSpeedRatio;
    private float smoothSpeedRatio;
    private Vector2 movementVector;


    private int gunBlockCount = 0;
    [SerializeField] private Transform gunStackParent;
    private List<GunBlock> gunList;
    [SerializeField] private ParticleSystem[] motorSmokes;
    private bool motorIsActive = false;
    private bool lastMotorStatus = false;

    private float stackSpacing = 0.9f;

    private float health = 1f;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image damageBar;

    void Awake()
    {
        gunList = new List<GunBlock>();


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) setDamage(0.1f);
    }

    void FixedUpdate()
    {
        currentSmoothValue = smoothValue;
        if(movementVector.magnitude == 0)
        {
            smoothSpeedRatio /= 2f;
        }
        //currentSmoothValue *= 5f;
        smoothSpeedRatio = Mathf.Lerp(smoothSpeedRatio,currentSpeedRatio,Time.deltaTime * currentSmoothValue);
        Move();

        damageBar.fillAmount = Mathf.Lerp(damageBar.fillAmount,health,Time.deltaTime * 3);
    }

    void LateUpdate()
    {
        
    }

    void Move()
    {
        Vector3 lookRotation = new Vector3(-movementVector.x,0,-movementVector.y);

        if(movementVector.magnitude > 0.1f)
        {
            
            Quaternion lookQuaternion = Quaternion.LookRotation(lookRotation,Vector3.up) * Quaternion.Euler(0,90,0);
            transform.rotation = Quaternion.Lerp(transform.rotation,lookQuaternion,Time.deltaTime * baseTurnSpeed);
            transform.Translate(Vector3.right * (baseSpeed * currentSpeedRatio),Space.Self);
            motorIsActive = true;
        }
        else
        {
            motorIsActive = false;
        }


            if(motorIsActive && !lastMotorStatus)
            {
                foreach(ParticleSystem smoke in motorSmokes)
                {
                    smoke.Play();
                }
                lastMotorStatus = true;
                
                Debug.Log("smoke start");
            }
            else if(!motorIsActive && lastMotorStatus)
            {
                lastMotorStatus = false;
                foreach(ParticleSystem smoke in motorSmokes)
                {
                    smoke.Stop();
                }
                Debug.Log("smoke stop");
            }
        



        GunStackBend(currentSpeedRatio - smoothSpeedRatio,transform.right);
    }

    void GunStackBend(float magnitude,Vector3 bendDirection)
    {
        Vector3 endPoint = getGunStackPosition();

        for(int i=0; i<gunBlockCount; i++)
        {
            Vector3 bend = new Vector3(-0.2f * (i+1) * magnitude, i * stackSpacing, 0);
            gunList[i].transform.localPosition = bend;
            gunList[i].transform.localRotation = Quaternion.Euler(0,0,(i+1) * 4 * magnitude);
        }

    }

    public void SetBaseTurnSpeed(float speed)
    {
        baseTurnSpeed = speed;
    }

    public void SetBaseSpeed(float speed)
    {
        baseSpeed = speed;
    }

    void SetCurrentSpeed(float speed)
    {
        currentSpeedRatio = speed;
    }

    public void SetMovementVector(Vector2 movement)
    {
        SetCurrentSpeed(movement.magnitude);
        movementVector = movement;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if(collision.transform.tag == "GunBlock")
        {
            GunBlock gunBlock = collision.transform.GetComponent<GunBlock>();
            gunBlock.ClaimBlock(transform.gameObject);
            gunBlock.transform.parent = gunStackParent;
            iTween.MoveTo(gunBlock.gameObject,iTween.Hash("position",getGunStackPosition(),"time",0.4f,"islocal",true,"easetype",iTween.EaseType.easeInOutQuad,"movetopath",false,"path",getGunBlockClaimPath(gunBlock.transform.localPosition).ToArray()));
            iTween.RotateTo(gunBlock.gameObject,iTween.Hash("rotation",Vector3.zero,"time",0.4f,"islocal",true,"easetype",iTween.EaseType.linear));
            gunBlockCount++;
            gunList.Add(gunBlock);
        }
    }

    Vector3 getGunStackPosition()
    {
        Vector3 pos = new Vector3(0,stackSpacing * gunBlockCount,0);
        return pos;
    }

    List<Vector3> getGunBlockClaimPath(Vector3 firstPos)
    {
        List<Vector3> pathValues = new List<Vector3>();
        
        pathValues.Add(firstPos);
        float xVal = Mathf.Sign(Random.Range(-10,10)) * Random.Range(1,3);
        float yVal = (getGunStackPosition().y - firstPos.y) / 2;
        float zVal = Mathf.Sign(Random.Range(-10,10)) * Random.Range(1,3);
        pathValues.Add(new Vector3(xVal, yVal, zVal));
        
        pathValues.Add(getGunStackPosition());
        return pathValues;
    }

    public void Shoot()
    {
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        foreach(GunBlock gun in gunList)
        {
            yield return new WaitForEndOfFrame();
            gun.Shoot();
        }
    }

    public bool setDamage(float damage)
    {
        bool isDead = false;

        health -= damage;
        healthBar.fillAmount = health;
        return isDead;
    }
}
