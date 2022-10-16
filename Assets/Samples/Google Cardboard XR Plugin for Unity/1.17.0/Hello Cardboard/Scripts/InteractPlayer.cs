using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPlayer : MonoBehaviour
{
    private new Transform camera;
    public float interactionDistance;
    // Start is called before the first frame update
    void Start()
    {
        camera = transform.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.DrawRay(camera.position, camera.forward * interactionDistance, Color.red);
        
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hitInfo, interactionDistance,LayerMask.GetMask("Interactable"))){
            hitInfo.transform.GetComponent<Interactable>().Interact();
        }
    }
}
