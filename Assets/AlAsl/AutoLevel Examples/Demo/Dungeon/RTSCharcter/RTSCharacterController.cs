using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent), typeof(CharacterController), typeof(Animator))]
public class RTSCharacterController : MonoBehaviour
{
    public GameObject markerPrefab;
    [Space]
    public float Speed = 5f;
    public float TurnSpeed = 5f;
    [Space]
    public string idleTrigger = "Idle";
    public string WalkTrigger = "Run";


    Vector2 startPosition = Vector2.zero;
    Vector2 endPosition = Vector2.zero;

    [SerializeField] CameraController camcont;

    [HideInInspector]
    public Animator animator;
    private NavMeshAgent navAgent;
    private CharacterController controller;
    private GameObject marker;

    public bool isNavAgentMoving { get; private set; }

    private int idleId;
    private int walkId;

    [SerializeField]
    private CinemachineFreeLook freeLookCamera;
    [SerializeField]
    private float swipeHeightChange = 5f;
    [SerializeField]
    private float tapThreshhold = 50f;
    [SerializeField]
    private int currentRigIndex = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navAgent.speed = Speed;
        navAgent.angularSpeed = TurnSpeed;

        marker = Instantiate(markerPrefab);
        marker.SetActive(false);

        idleId = Animator.StringToHash(idleTrigger);
        walkId = Animator.StringToHash(WalkTrigger);



    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out var hit))
        //    MoveTo(hit.point);
        //}

        Touch touch = new Touch();
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endPosition = touch.position;

                    // Differenz der y-Koordinaten berechnen
                    float deltaY = endPosition.y - startPosition.y;
                    float deltaX = endPosition.x - startPosition.x;

                    if (Vector2.Distance(startPosition, endPosition) > 10f)
                    {


                        if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX) && Mathf.Abs(deltaY) > 10f)
                        {
                            // Überprüfe, ob der Wisch nach oben oder unten ging
                            if (deltaY > 0)
                            {
                                // Swipe Up
                                Debug.Log("Swipe Up");
                                //ChangeCameraHeight(swipeHeightChange);
                                SwitchToNextRig();
                            }
                            else
                            {
                                // Swipe Down
                                Debug.Log("Swipe Down");
                                //ChangeCameraHeight(-swipeHeightChange);
                                SwitchToPreviousRig();
                            }
                        }
                    }
                    else
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
                            MoveTo(hit.point);
                    }

                    break;
                default:
                    break;
            }
        }

        if (isNavAgentMoving)
        {
            if (!navAgent.pathPending && (navAgent.isPathStale || navAgent.isStopped
                || Mathf.Abs(navAgent.remainingDistance - navAgent.stoppingDistance) <= Speed * Time.deltaTime * 2f))
            {
                StopNavAgent();
            }
        }
    }

    //void ChangeCameraHeight(float heightChange)
    //{
    //    // Zugriff auf den OrbitalTransposer der FreeLookCamera
    //    //CinemachineOrbitalTransposer transposer = freeLookCamera.GetRig(0).GetCinemachineComponent<CinemachineOrbitalTransposer>();

    //    // Aktuelle Höhe ändern
    //    //freeLookCamera.GetRig(0).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_FollowOffset.y += heightChange;
    //    freeLookCamera.m_Orbits.
    //}
    void SwitchToNextRig()
    {


        // Rotiere durch die Liste der Rigs
        currentRigIndex = (currentRigIndex + 1) % freeLookCamera.m_Orbits.Length;

        // Setze das aktuelle Rig
        UpdateCameraRig(currentRigIndex);
    }

    void SwitchToPreviousRig()
    {

        // Rotiere durch die Liste der Rigs rückwärts
        currentRigIndex = (currentRigIndex - 1 + freeLookCamera.m_Orbits.Length) % freeLookCamera.m_Orbits.Length;

        // Setze das aktuelle Rig
        UpdateCameraRig(currentRigIndex);
    }

    void UpdateCameraRig(int rigIndex)
    {


        // Kopiere die Orbits-Liste, um sie zu bearbeiten
        var orbits = freeLookCamera.m_Orbits;

        // Setze das aktuelle Rig
        freeLookCamera.m_Orbits = RotateOrbits(orbits, rigIndex);
    }
    CinemachineFreeLook.Orbit[] RotateOrbits(CinemachineFreeLook.Orbit[] orbits, int startIndex)
    {
        // Rotiere die Orbits in der Liste, um beim nächsten Rig zu beginnen
        CinemachineFreeLook.Orbit[] rotatedOrbits = new CinemachineFreeLook.Orbit[orbits.Length];
        for (int i = 0; i < orbits.Length; i++)
        {
            rotatedOrbits[i] = orbits[(startIndex + i) % orbits.Length];
        }

        return rotatedOrbits;
    }

    public void MoveTo(Vector3 point)
    {
        navAgent.SetDestination(point);
        marker.SetActive(true);
        marker.transform.position = point;
        animator.ResetTrigger(idleId);
        animator.SetTrigger(walkId);
        isNavAgentMoving = true;
    }

    void StopNavAgent()
    {
        if (navAgent.enabled)
            navAgent.ResetPath();
        marker.SetActive(false);
        animator.ResetTrigger(walkId);
        animator.SetTrigger(idleId);
        isNavAgentMoving = false;
    }

    private Vector2 getWorldPoint(Vector2 screenPoint)
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit);
        return hit.point;
    }
}
