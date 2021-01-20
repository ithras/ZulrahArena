using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ZulrahShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public float speed = 10f;
    public GameObject projectilePrefab;
    private ZulrahBehaviour zulrah;

    // Start is called before the first frame update
    void Start()
    {
        zulrah = GetComponent<ZulrahBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(ProjectileType.Minion);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Shoot(ProjectileType.Venom);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            zulrah.MagicStats();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            zulrah.RangeStats();
        }

    }

    void Shoot(ProjectileType type)
    {
        GameObject projectileGO = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
        Projectile proj = projectileGO.GetComponent<Projectile>();
        proj.Type = type;
        proj.shootingPoint = shootingPoint;
    }
}
