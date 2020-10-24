using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FlorenciaShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public float speed = 10f;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject projectileGO = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
            projectileGO.GetComponent<Projectile>().shootingPoint = shootingPoint;
        }
    }
}
