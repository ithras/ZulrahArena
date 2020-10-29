using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZulrahMovement : MonoBehaviour
{
    public Transform Florencia;
    public CharacterStats stats;
    int MoveSpeed = 2;
    int MaxDist = 10;
    int MinDist = 0;
    int AttackOfDeath = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Florencia = gameObject.GetComponent<CharacterController>();
        stats = gameObject.GetComponent<ZulrahHealth>().stats;
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
    
    void OnCollisionEnter(Collision collision)
    {
        FlorenciaHealth fh = gameObject.GetComponent<FlorenciaHealth>();
        fh.stats.TakeDamage(AttackOfDeath);
    }
}
