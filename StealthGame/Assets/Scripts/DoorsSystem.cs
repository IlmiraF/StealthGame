using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsSystem : MonoBehaviour
{
    private bool isOpen = false;
    [SerializeField]
    private GameObject door;

    void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            isOpen = !isOpen;
            //Vector3 open = 
            //door.transform.position = new Vector3(door.transform.position.x + 50, door.transform.position.y, door.transform.position.z);
            Destroy(door);
        }
    }

    private void Update()
    {
        if (isOpen)
        {
            OpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOpen = false;
        }
    }
}
