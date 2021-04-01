using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform planetCoG;
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float view_speed = -10f;

    private Rigidbody _rigi;
    private float h_axis;
    private float v_axis;
    private float mouse_x;
    private float mouse_y;

    private void Start()
    {
        _rigi = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        h_axis = Input.GetAxis("Horizontal");
        v_axis = Input.GetAxis("Vertical");
        mouse_x = Input.GetAxis("Mouse X");
        mouse_y = Input.GetAxis("Mouse Y");
    }

    private void FixedUpdate()
    {
        _rigi.AddForce((planetCoG.position - transform.position).normalized * 9.81f);
        _rigi.AddForce(speed * Vector3.forward * v_axis * Time.fixedDeltaTime);
        _rigi.AddForce(speed * Vector3.right * h_axis * Time.fixedDeltaTime);
        Vector3 upward = transform.position - planetCoG.position;
        Vector3 lookAt = Vector3.Cross(upward, -transform.right) + upward;
        _rigi.transform.LookAt(lookAt, upward);

        cam.transform.Rotate(Vector3.right * view_speed * mouse_y * Time.fixedDeltaTime, Space.Self);
    }
}
