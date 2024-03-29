using UnityEngine;

public class GrapplingScript : MonoBehaviour {

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    public float maxDistance = 20f;
    private SpringJoint joint;
     private bool canGrapple; // Propiedad booleana que indica si se puede graplear o no
     
     // Impulso
     public float impulseForce = 50f;
     public float impulseDistance = 10f;
     private bool isDrawingImpulse; // Variable para rastrear si se está dibujando el gancho
     private float drawTimer = 0.5f; // Duración del tiempo de dibujo en segundos
    private float currentDrawTime; // Tiempo actual transcurrido de dibujo

    void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Q)) {
            StartGrapple();
        }
        else if (Input.GetKeyUp(KeyCode.Q)) {
            StopGrapple();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            StartImpulse();
            StartDrawingImpulse();
        }
        else if (Input.GetKeyUp(KeyCode.F)) {
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
    }

    // Verificar si el jugador puede graplear al objeto
    void CheckCanGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            canGrapple = true; // El objeto es grappleable
            Debug.Log("Se puede");
        }
        else
        {
            canGrapple = false; // El objeto no es grappleable
            Debug.Log("No se puede");
        }
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
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
    void StartImpulse() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, impulseDistance, whatIsGrappleable)) {
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
    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}