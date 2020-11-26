using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { Ranged, Magic }
public class Projectile : MonoBehaviour
{
    public float speed = 60f;
    private Transform target;
    public bool seekTarget;
    public Transform shootingPoint;
    public Vector3 shootingDir;
    public Vector3 direction;
    private int damage;
    public PlayerStats stats;
    public ProjectileType Type;

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

    public virtual void Action(Transform target)
    {
        PlayerStats targetStats = target.GetComponent<PlayerStats>();
        float accuracy = -1;
        if(Type == ProjectileType.Ranged)
        {
            if (stats.RangedAtkRoll > targetStats.RangedDefenceRoll)
                accuracy = 1 - ((targetStats.RangedDefenceRoll + 2) / (2 * (stats.RangedAtkRoll + 1)));

            else if (targetStats.RangedDefenceRoll > stats.RangedAtkRoll)
                accuracy = stats.RangedAtkRoll / (2 * (targetStats.RangedDefenceRoll+1));
        }

        float random = Random.Range(0f, 1f);

        if( accuracy >= random )
            target.GetComponent<PlayerStats>().TakeDamage(damage, Type);
        
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
