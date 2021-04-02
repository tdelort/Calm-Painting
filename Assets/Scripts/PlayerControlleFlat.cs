using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlleFlat : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float acc = 4f;
    [SerializeField]
    private float view_speed = 10f;
    [SerializeField]
    private Camera cam;

    private Rigidbody _rigi;
    private float h_axis;
    private float v_axis;
    private float mouse_x;
    private float mouse_y;
    // Start is called before the first frame update
    void Start()
    {
        _rigi = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        h_axis = Input.GetAxis("Horizontal");
        v_axis = Input.GetAxis("Vertical");
        mouse_x += Input.GetAxis("Mouse X");
        mouse_y += Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void FixedUpdate()
    {
        _rigi.AddForce(acc * _rigi.transform.forward * v_axis * Time.fixedDeltaTime);
        _rigi.AddForce(acc * _rigi.transform.right * h_axis * Time.fixedDeltaTime);
        _rigi.velocity = Vector3.ClampMagnitude(_rigi.velocity, speed);

        //_rigi.transform.Rotate(Vector3.up * mouse_x * Time.fixedDeltaTime * view_speed, Space.Self);
        float roty = mouse_x * view_speed * Time.fixedDeltaTime;
        _rigi.transform.rotation = Quaternion.Euler(0, roty, 0);

        float rotx = mouse_y * view_speed * Time.fixedDeltaTime;
        rotx = Mathf.Clamp(rotx, -90, 90);
        Vector3 camRotation = cam.transform.rotation.eulerAngles;
        cam.transform.rotation = Quaternion.Euler(-rotx, camRotation.y, camRotation.z);

        if (Mathf.Abs(h_axis) < 0.1 && Mathf.Abs(v_axis) < 0.1)
            _rigi.velocity = new Vector3(_rigi.velocity.x * 0.9f, _rigi.velocity.y, _rigi.velocity.z * 0.9f);
    }
}
