using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public LayerMask layer;
    public LayerMask zulrah;
    public NavMeshAgent agent;
    public Camera mainCam;
    public Transform shootingPoint;
    public GameObject projectilePrefab;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, zulrah))
            {
                Shoot(hit.collider.transform);
                //DO Something
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                agent.SetDestination(hit.point);
            }

        }

    }

    void Shoot(Transform target)
    {
        GameObject projectileGO = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectileGO.transform.parent = transform;

        if (projectile != null)
        {
            projectile.stats = GetComponent<PlayerStats>();
            projectile.seekTarget = true;
            projectile.Seek(target);
        }

    }
}
