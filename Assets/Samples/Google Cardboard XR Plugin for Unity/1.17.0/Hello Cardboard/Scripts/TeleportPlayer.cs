using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    Rigidbody rigidbody;
    private float? startTimer;
    public float deltaTime = 2f;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {     
            Debug.Log("Raycast");
            if (hitInfo.collider.gameObject.GetComponent<TeleportBase>() != null)
            {
                
                if (Time.time - startTimer >= deltaTime)
                {
                    SetPlayerTransformPosition(hitInfo);
                }else
                {
                    if(startTimer == null)
                    {
                        HighlightTeleporter(hitInfo);
                        Debug.Log("Start");
                        startTimer = Time.time;
                    }
                    DimTeleporter(hitInfo);
                }
            }else
            {
                startTimer = null;
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
