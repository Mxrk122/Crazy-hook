using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AttackRaycast();
        }
    }

    public float attackDistance = 5f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    public void Attack()
    {
        if(!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        // if(attackCount == 0)
        // {
        //     ChangeAnimationState(ATTACK1);
        //     attackCount++;
        // }
        // else
        // {
        //     ChangeAnimationState(ATTACK2);
        //     attackCount = 0;
        // }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }
    void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        { 
            //HitTarget(hit.point);

            // Accede al GameObject impactado
            GameObject objectHit = hit.transform.gameObject;
            // Llama a la función Destroy para destruir el GameObject
            Destroy(objectHit);

        } 
    }

    void HitTarget(Vector3 pos)
    {

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }
}
