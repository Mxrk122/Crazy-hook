using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float angleOffset = 45.0f;
    [SerializeField] private float timer = 2;
    private float bulletTime;
    public GameObject enemyBullet;
    public Transform spawnpoint;
    public float enemySpeed;
    public float detectionRange = 100f; // Adjust this to your desired detection range
    public AudioClip shootSoundClip; // Sound clip to play when shooting
    public AudioSource audioSource; // Reference to AudioSource component

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If player is within detection range, move towards the player and shoot
        if (distanceToPlayer <= detectionRange)
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
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        // Calculate direction towards the player
        Vector3 directionToPlayer = (player.position - spawnpoint.transform.position).normalized;

        // Instantiate bullet
        GameObject bulletObj = Instantiate(enemyBullet, spawnpoint.transform.position, Quaternion.LookRotation(directionToPlayer)) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();

        // Add force in the direction towards the player
        bulletRig.AddForce(directionToPlayer * enemySpeed);
        // Play shoot sound clip
        if (shootSoundClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSoundClip);
            Debug.Log("Audio :)");
        }

    }
}
