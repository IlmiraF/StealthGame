using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderScript : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] GameObject gObject;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            if (Mathf.Abs(gObject.transform.position.x - enemy.transform.position.x) > distance || Mathf.Abs(gObject.transform.position.z - enemy.transform.position.z) > distance)
            {
                enemy.layer = LayerMask.NameToLayer("Default");
            }
            else
            {
                enemy.layer = LayerMask.NameToLayer("Scanning");
            }
        }
    }
}
