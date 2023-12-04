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


    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;
    public Camera MainCamera;

    [HideInInspector]
    public Animator animator;
    private NavMeshAgent navAgent;
    private CharacterController controller;
    private GameObject marker;

    public bool isNavAgentMoving { get; private set; }

    private int idleId;
    private int walkId;
    

    [SerializeField]
    private float tapThreshhold = 50f;

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
        // Track a single touch as a direction control.
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen)
        {
            // Something that uses the chosen direction...
            //MainCamera.transform.position = new Vector3(direction.x, direction.y, Speed * Time.deltaTime);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
                MoveTo(direction);

        }*/

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out var hit))
            MoveTo(hit.point);
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
