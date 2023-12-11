using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum InputType
{
    Tap,
    UpSwipe,
    DownSwipe,
    LeftSwipe,
    RightSwipe
}



public class CameraCont : MonoBehaviour
{
    // Start is called before the first frame update

    Touch touch = new Touch();
    private Vector2 worldStartPoint;

    [SerializeField]
    private float tapThreshhold = 50f;

    Vector2 startPosition = Vector2.zero;
    Vector2 endPosition = Vector2.zero;
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if finger is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("Touched the UI");
            }

        }

        // only work with one touch
        if (Input.touchCount == 1)
        {
            Touch currentTouch = Input.GetTouch(0);

            if (currentTouch.phase == TouchPhase.Began)
            {
                this.worldStartPoint = this.getWorldPoint(currentTouch.position);
            }

            if (currentTouch.phase == TouchPhase.Moved)
            {
                Vector2 worldDelta = this.getWorldPoint(currentTouch.position) - this.worldStartPoint;

                Camera.main.transform.Translate(
                    -worldDelta.x,
                    -worldDelta.y,
                    0
                );
            }
        }

    }
    private Vector2 getWorldPoint(Vector2 screenPoint)
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit);
        return hit.point;
    }
}
