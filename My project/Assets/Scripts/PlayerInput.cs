using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    private Camera cam;

    private InputAction mousePosDetect;
    private InputAction mouseClick;
    private InputAction switchView;
    private InputAction switchCaptain;
    private Action<InputAction.CallbackContext> MouseMove;
    private Vector2 mousePos;

    [SerializeField] private Transform popUpContainer;

    private void Start()
    {
        cam = Camera.main;

        MouseMove = ctx => mousePos = ctx.ReadValue<Vector2>();

        mousePosDetect = InputSystem.actions.FindAction("MousePosition");
        mouseClick = InputSystem.actions.FindAction("MouseClick");
        switchView = InputSystem.actions.FindAction("SwitchView");
        switchCaptain = InputSystem.actions.FindAction("SwitchCaptain");

        mousePosDetect.performed += MouseMove;
        switchView.performed += SwitchView;
        switchCaptain.performed += SwitchCaptain;

        instance = GetComponent<PlayerInput>();
    }

    private void SwitchCaptain(InputAction.CallbackContext ctx)
    {
        if(popUpContainer.childCount != 0)
        {
            return;
        }

        float direction = ctx.ReadValue<float>();

        if (direction < 0)
        {
            MissionSelector.instance.BeePrevIndex();
        }
        else
        {
            MissionSelector.instance.BeeNextIndex();
        }
    }

    private void SwitchView(InputAction.CallbackContext ctx)
    {
        if (popUpContainer.childCount != 0)
        {
            return;
        }

        float direction = ctx.ReadValue<float>();

        if(direction < 0)
        {
            MissionSelector.instance.TableAngle();
        }
        else
        {
            MissionSelector.instance.SquadAngle();
        }
    }

    private void OnDestroy()
    {
        mousePosDetect.performed -= MouseMove;
        switchView.performed -= SwitchView;
        switchCaptain.performed -= SwitchCaptain;
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

        if (node && !MissionSelector.instance.SquadSelect)
        {
            node.HoverInfo();
            if (mouseClick.WasPressedThisFrame())
            {
                node.ClickSelect();
            }
        }
    }
}
