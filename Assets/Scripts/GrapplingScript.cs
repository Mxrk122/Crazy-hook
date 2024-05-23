using UnityEngine;

public class GrapplingScript : MonoBehaviour
{

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public LayerMask whatIsGrappaleable2;
    private LayerMask combinedMask;

    public Transform gunTip, camera, player;
    public float maxDistance = 20f;
    private SpringJoint joint;
    public bool canGrapple; // Propiedad booleana que indica si se puede graplear o no
    public bool canImpulse; // Propiedad booleana que indica si se puede graplear o no

    // Impulso
    public float impulseForce = 50f;
    public float impulseDistance = 10f;
    private bool isDrawingImpulse; // Variable para rastrear si se está dibujando el gancho
    private float drawTimer = 0.5f; // Duración del tiempo de dibujo en segundos
    private float currentDrawTime; // Tiempo actual transcurrido de dibujo

    void Awake()
    {
        combinedMask = whatIsGrappleable | whatIsGrappaleable2;
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        // Assuming lr is your LineRenderer component
        lr.positionCount = 2; // Set the number of positions to 2
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartImpulse();
            StartDrawingImpulse();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopDrawingImpulse();
        }

        // Si se está dibujando el gancho, actualizar el temporizador
        if (isDrawingImpulse)
        {
            currentDrawTime += Time.deltaTime;

            // Si el temporizador alcanza la duración deseada, detener el dibujo del gancho
            if (currentDrawTime >= drawTimer)
            {
                StopDrawingImpulse();
            }
        }

        CheckCanGrapple();
        CheckCanImpulse();
    }

    // Verificar si el jugador puede graplear al objeto
    void CheckCanGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, combinedMask))
        {
            canGrapple = true; // El objeto es grappleable
            // Debug.Log("Se puede");
        }
        else
        {
            canGrapple = false; // El objeto no es grappleable
            // Debug.Log("No se puede");
        }
    }

    void CheckCanImpulse()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, impulseDistance, combinedMask))
        {
            canImpulse = true; // El objeto es grappleable
            // Debug.Log("Se puede");
        }
        else
        {
            canImpulse = false; // El objeto no es grappleable
            // Debug.Log("No se puede");
        }
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, combinedMask))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    // impulso con la cuerda
    void StartImpulse()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, impulseDistance, combinedMask))
        {
            grapplePoint = hit.point;

            // Calcula la dirección desde el jugador al punto de enganche
            Vector3 grappleDirection = (grapplePoint - player.position).normalized;

            // Aplica un impulso al jugador en la dirección del enganche
            player.GetComponent<Rigidbody>().AddForce(grappleDirection * impulseForce, ForceMode.Impulse);

            // Dibujar el gancho
            DrawGrip();
        }
    }

    void DrawGrip()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    // Función para comenzar a dibujar el gancho
    void StartDrawingImpulse()
    {
        isDrawingImpulse = true;
        currentDrawTime = 0f; // Reiniciar el temporizador
    }

    // Función para detener el dibujo del gancho
    void StopDrawingImpulse()
    {
        isDrawingImpulse = false;
        lr.positionCount = 0; // Borrar la línea del gancho
    }

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint || !lr) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        // Make sure lr has at least 2 positions
        if (lr.positionCount >= 2)
        {
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, currentGrapplePosition);
        }
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}