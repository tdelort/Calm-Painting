using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            LayerMask mask = 1 << 8;

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("Hit");
                DrawableSurface script = hit.collider.gameObject.GetComponent<DrawableSurface>();
                script.OnHit(hit.textureCoord);
            }
        }
    }
}
