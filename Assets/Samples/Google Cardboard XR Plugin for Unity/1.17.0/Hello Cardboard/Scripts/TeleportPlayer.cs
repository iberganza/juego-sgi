using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementStatus { 
    Still,
    StartTeleport,
    Walking
} 

public class TeleportPlayer : MonoBehaviour
{
    Rigidbody rigidbody;
    private float? startTimer;
    public float deltaTime = 2f;
    private GameObject lastHit;

    private Vector3 playerStartPosition;
    private Vector3 playerFinalPosition;

    [SerializeField]
    private float speed = 0.6f;                   //	Spped for opening and closing the door

    private MovementStatus status = MovementStatus.Still;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); 

        playerStartPosition = this.transform.position;
        playerFinalPosition = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        TeleportHoldMouse();
        
    }
    private void TeleportHoldMouse()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {     
            if (hitInfo.collider.gameObject.GetComponent<TeleportBase>() != null)
            {
                lastHit = hitInfo.collider.gameObject;
                if (Time.time - startTimer >= deltaTime)
                {
                    SetPlayerTransformPosition(hitInfo);
                    if (status == MovementStatus.StartTeleport)
                    {
                        if (status != MovementStatus.Walking)
                        {
                            StartCoroutine("WalkToTeleporter");
                        }
                    }
                }
                else
                {
                    if(startTimer == null)
                    {
                        startTimer = Time.time;
                    }
                    HighlightTeleporter(hitInfo);
                }
            }else
            {
                startTimer = null;
                if (lastHit != null)
                {
                    lastHit.GetComponent<Renderer>().material.color = Color.white;
                    lastHit = null;
                }
            }
        }else
        {
            startTimer = null;
        }
    }
    
    private void SetPlayerTransformPosition(RaycastHit hitInfo)
    {
        playerFinalPosition = new Vector3(hitInfo.collider.gameObject.transform.position.x, 
                                                        playerStartPosition.y, 
                                                        hitInfo.collider.gameObject.transform.position.z);
        status = MovementStatus.StartTeleport;
    }

    private void HighlightTeleporter(RaycastHit hitInfo)
    {
        hitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    private void DimTeleporter(RaycastHit hitInfo)
    {
        hitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    IEnumerator WalkToTeleporter()
    {
        status = MovementStatus.Walking;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;

            this.transform.localPosition = Vector3.Slerp(playerStartPosition, playerFinalPosition, t);
            yield return null;
        }

        playerStartPosition = playerFinalPosition;
        status = MovementStatus.Still;

    }
}
