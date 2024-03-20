
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player;
    private NavMeshAgent agent;
    public float angleOffset = 45.0f;
    [SerializeField] private float timer = 5;
    private float bulletTime;
    public GameObject enemyBullet;
    public Transform spawnpoint;
    public float enemySpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
        // Calculate the direction vector towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Calculate the target rotation with the offset angle
        Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, angleOffset, 0);

        // Interpolate between current rotation and target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
        ShootAtPlayer();

    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * enemySpeed);
        bulletRig.AddForce(bulletRig.transform.right * -300);

    }


}