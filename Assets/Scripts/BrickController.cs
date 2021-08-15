using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.collider.gameObject == this.gameObject)
                {
                    this.gameObject.GetComponent<RayfireRigid>().physics.useGravity = true;
                    this.gameObject.GetComponent<RayfireRigid>().Demolish();
                    
                }
            }
        }
    }
}
