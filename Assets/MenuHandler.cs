using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    Rigidbody rigidbody;
    private float? startTimer;
    public float deltaTime = 2f;
    private GameObject lastHit;


    [SerializeField]
    private float speed = 0.6f;                   //	Spped for opening and closing the door

    private MovementStatus status = MovementStatus.Still;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        SelectObject();
    }

    private void SelectObject()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject.GetComponent<Interactable>() != null)
            {
                lastHit = hitInfo.collider.gameObject;
                if (Time.time - startTimer >= deltaTime)
                {
                    hitInfo.transform.GetComponent<Interactable>().Interact();
                }
                else
                {
                    if (startTimer == null)
                    {
                        startTimer = Time.time;
                    }

                }
            }
            else
            {
                startTimer = null;
                if (lastHit != null)
                {
                    lastHit = null;
                }
            }
        }
        else
        {
            startTimer = null;
        }
    }
}
