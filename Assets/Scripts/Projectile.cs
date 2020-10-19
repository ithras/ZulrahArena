using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 60f;
    private Transform target;
    public bool seekTarget;
    public Vector3 shootingDir;
    public Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
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
        ZulrahHealth zulrahHealth = target.GetComponent<ZulrahHealth>();

        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

        if (zulrahHealth != null)
        {
            //Do Something

            Destroy(gameObject);
        }
        else if (playerHealth != null)
        {
            //Do something

            Destroy(gameObject);
        }
    }

    public void Seek(Transform target)
    {
        this.target = target;
    }
}
