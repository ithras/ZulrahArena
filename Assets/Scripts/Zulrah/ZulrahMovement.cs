using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZulrahMovement : MonoBehaviour
{
    public Transform Florencia;
    int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 5;

    // Start is called before the first frame update
    void Start()
    {
        //zulrahController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Florencia);
 
        if (Vector3.Distance(transform.position, Florencia.position) >= MinDist)
        {
 
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            
            /*if (Vector3.Distance(transform.position, Florencia.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }*/
 
        }
    }
}
