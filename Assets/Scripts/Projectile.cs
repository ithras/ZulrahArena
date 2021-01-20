using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { Ranged, Magic, Venom, Minion }
public class Projectile : MonoBehaviour
{
    public static event System.Action<PlayerStats, PlayerStats, Projectile> OnProjectileHit;
    public float speed = 60f;
    public bool seekTarget;
    public Transform shootingPoint;
    public Vector3 shootingDir;
    public Vector3 direction;
    public PlayerStats stats;
    public ProjectileType Type;
    private Transform target;
    public int damage;

    void Start()
    {
        if (Type == ProjectileType.Magic)
            damage = Random.Range(0, stats.MagicMaxHit + 1);

        else if (Type == ProjectileType.Ranged)
            damage = Random.Range(0, stats.RangedMaxHit + 1);

        Type = stats.atkType;

        shootingDir = shootingPoint.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && seekTarget)
        {
            Destroy(gameObject);
            return;
        }

        float distanceThisFrame = speed * Time.deltaTime;

        if (!seekTarget)
        {
            transform.Translate(shootingDir * distanceThisFrame, Space.World);
            return;
        }

        direction = target.position - transform.position;

        if (direction.magnitude <= distanceThisFrame)
        {
            Action(target);
            return;
        }
        

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void Action(Transform target)
    {
        PlayerStats targetStats = target.GetComponent<PlayerStats>();
        OnProjectileHit.Invoke(stats, targetStats, this);
        
        Destroy(gameObject);
    }

    public void Seek(Transform target)
    {
        this.target = target;
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
