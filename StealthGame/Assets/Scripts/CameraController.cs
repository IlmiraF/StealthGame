using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float followSpeed = 3;
    public Vector3 followOffset;
    public Vector3 topDownOffset;
    public float lookSpeed = 5;


    private void Start()
    {
        followOffset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        if(PlayerController.IsHidden)
        {
            TopDownMode();
        }
        else
        {
            FollowMode();
        }
    }

    void FollowMode()
    {
        Vector3 newPosition = target.transform.position - target.transform.forward + target.transform.TransformVector(followOffset);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, target.transform.forward, lookSpeed * Time.deltaTime);
    }

    void TopDownMode()
    {
        Vector3 newPosition = target.transform.position + topDownOffset;
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, Vector3.down, lookSpeed * Time.deltaTime);
    }
}
