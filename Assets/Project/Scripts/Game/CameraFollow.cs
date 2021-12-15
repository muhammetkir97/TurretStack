using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    private Vector3 followOffset;
    private float followSmooth = 5f;
    void Start()
    {
        followOffset = transform.position - targetObject.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Follow();
    }

    void LateUpdate()
    {
        
    }

    void Follow()
    {
        transform.position = Vector3.Lerp(transform.position,targetObject.position + followOffset,Time.deltaTime * followSmooth);
    }
}
