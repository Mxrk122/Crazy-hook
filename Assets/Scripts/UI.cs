using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject grappleIcon;  // Referencia al objeto de UI del icono
    public GameObject impulseIcon;  // Referencia al objeto de UI del icono
    public GameObject attackIconOne;  // Referencia al objeto de UI del icono
    public GameObject attackIconTwo;  // Referencia al objeto de UI del icono
    private GrapplingScript grapplingScript;  // Referencia al script que contiene la propiedad booleana
    private PlayerAttack playerAttack;
    void Start()
    {
        // Obtener la referencia al GameController en el Start
        grapplingScript = FindObjectOfType<GrapplingScript>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    void Update()
    {
        if (grappleIcon != null && grapplingScript != null)
        {
            // Actualizar la visibilidad del icono en función de la propiedad booleana en el otro script
            grappleIcon.SetActive(grapplingScript.canGrapple);
        }

        if (impulseIcon != null && grapplingScript != null)
        {
            // Actualizar la visibilidad del icono en función de la propiedad booleana en el otro script
            impulseIcon.SetActive(grapplingScript.canImpulse);
        }

        if (attackIconOne != null && attackIconTwo != null && grapplingScript != null)
        {
            // Actualizar la visibilidad del icono en función de la propiedad booleana en el otro script
            attackIconOne.SetActive(playerAttack.canAttack);
            attackIconTwo.SetActive(playerAttack.canAttack);
        }
    }
}
