using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ZulrahShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public TrailRenderer trail;
    public float speed = 10f;
    private TrailRenderer trailObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hit, Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log("GEORGE YOU KILLED THE " + hit.collider.name);
            }
        }
    }

}
