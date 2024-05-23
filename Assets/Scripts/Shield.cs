using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shield;
    public GameObject shieldUI;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(this.gameObject.tag);
            shield.SetActive(true);
            GameObject foundObject = GameObject.Find("PowerUpContainerDoubleShine");
            shieldUI.SetActive(true);
            Destroy(foundObject);

        }
    }

}
