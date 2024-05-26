using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject gP;
    private Renderer renderer;
    public Material material; // Material que quieres cambiar
    public Color originalColor;
    public Color grapColor = Color.green; // Nuevo color que quieres aplicar cuando el booleano sea true
    public Color impulseColor = Color.red; // Nuevo color que quieres aplicar cuando el booleano sea true
    private GrapplingScript scriptA; // Referencia al script donde está el booleano

    void Start()
    {
        // Obtener la referencia al ScriptA
        scriptA = gP.GetComponent<GrapplingScript>();

        renderer = gP.GetComponent<Renderer>();

        material = renderer.material;

        originalColor = material.color;
        if (scriptA == null)
        {
            Debug.LogError("No se pudo encontrar el componente ScriptA adjunto al gP.");
        }
    }

    void Update()
    {
        if (scriptA != null)
        {
            // Verifica si el booleano es true en ScriptA
            if (scriptA.canGrapple)
            {
                // Cambia el color del material
                material.color = grapColor;
                if (scriptA.canImpulse)
                {
                    material.color = impulseColor;
                }
                else 
                {
                    material.color = grapColor;
                }
            }
            else
            {
                material.color = originalColor;
            }
        }
    }
}
