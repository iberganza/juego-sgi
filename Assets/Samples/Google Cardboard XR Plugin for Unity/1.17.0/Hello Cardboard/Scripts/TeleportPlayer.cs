using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    Rigidbody rigidbody;
    private float? startTimer;
    public float deltaTime = 2f;
    private GameObject lastHit;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); 
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
                }else
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
        this.transform.position = new Vector3(hitInfo.collider.gameObject.transform.position.x, 
                                                        this.transform.position.y, 
                                                        hitInfo.collider.gameObject.transform.position.z);
    }

    private void HighlightTeleporter(RaycastHit hitInfo)
    {
        hitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    private void DimTeleporter(RaycastHit hitInfo)
    {
        hitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
