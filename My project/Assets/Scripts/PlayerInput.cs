using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Camera cam;

    private InputAction mousePosDetect;
    private InputAction mouseClick;
    private Action<InputAction.CallbackContext> MouseMove;
    private Vector2 mousePos;

    private void Start()
    {
        cam = Camera.main;

        MouseMove = ctx => mousePos = ctx.ReadValue<Vector2>();

        mousePosDetect = InputSystem.actions.FindAction("MousePosition");
        mouseClick = InputSystem.actions.FindAction("MouseClick");

        mousePosDetect.performed += MouseMove;
    }

    private void OnDestroy()
    {
        mousePosDetect.performed -= MouseMove;
    }

    private void Update()
    {
        Ray mouseRay = cam.ScreenPointToRay(mousePos);

        Physics.Raycast(mouseRay, out RaycastHit hit);

        MissionNode node = null;

        //  this is probably not a good way to do things but ui is being weird
        //  with the raycast
        if(hit.transform)
            node = hit.transform.GetComponent<MissionNode>();

        if (node)
        {
            node.HoverInfo();
            if (mouseClick.WasPressedThisFrame())
            {
                node.ClickSelect();
            }
        }
    }
}
