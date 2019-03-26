using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO
/// Fix camera's positioning so when player is loaded in,
/// the camera does not snap back to its previous position,
/// but instead saves at that position the camera is at
/// </summary>
[DisallowMultipleComponent]
public class FPCamera : MonoBehaviour
{

    Vector2 mouseLook;
    Vector2 smoothV;

    private float Sensitivity => 2.0f;
    [Range(0, 10)]
    public float smoothing = 2.0f;

    public Transform Player => transform.parent;

    private bool InTab => Input.GetKey(KeyCode.Mouse2) || Input.GetKey(KeyCode.Tab);

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!InTab)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.Locked;
            //var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            var md = new Vector2();
            md.x += Input.GetAxisRaw("Mouse X");
            md.y += Input.GetAxisRaw("Mouse Y");
            md = Vector2.Scale(md, new Vector2(Sensitivity * smoothing, Sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook += smoothV;

            mouseLook.y = Mathf.Clamp(mouseLook.y, -50, 70);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            Player.rotation = Quaternion.AngleAxis(mouseLook.x, Player.up);
        }
        else
        {
            if (Cursor.lockState != CursorLockMode.None)
                Cursor.lockState = CursorLockMode.None;
        }
    }

}
