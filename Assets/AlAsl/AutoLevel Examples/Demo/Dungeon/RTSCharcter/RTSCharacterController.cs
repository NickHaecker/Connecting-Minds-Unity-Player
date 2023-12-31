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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out var hit))
        //    MoveTo(hit.point);
        //}

        Touch touch = new Touch();
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endPosition = touch.position;

                    if (Vector2.Distance(startPosition, endPosition) > 5f)
                    {

                    }
                    else
                    {
                            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
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
