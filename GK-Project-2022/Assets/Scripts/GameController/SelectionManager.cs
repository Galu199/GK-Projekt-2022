using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private RaycastHit hit;
    public float maxReachLength = 1.0f;
    public bool elevatorButtonPressed = false;
    public bool elevatorPowerPressed = false;
    public Item clickedItem = null;
    public delegate void ClickAction();
    public static event ClickAction OnElevatorClick;
    public static event ClickAction OnPowerClick;
    public static event ClickAction OnItemClick;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(ray, out hit, maxReachLength))
            {
                var selection = hit.transform;
                //Debug.Log(selection.name);
                if (selection.GetComponent<ElevatorButton>() != null)
                {
                    elevatorButtonPressed = true;
                    if (OnElevatorClick != null) OnElevatorClick();
                }
                else
                if (selection.GetComponent<ElevatorPower>() != null)
                {
                    selection.GetComponent<Animator>().SetBool("TurnOn", true);
                    elevatorPowerPressed = true;
                    if (OnPowerClick != null) OnPowerClick();
                }
                else
                if (selection.GetComponent<Item>() != null)
                {
                    clickedItem = selection.GetComponent<Item>();
                    if (OnItemClick != null) OnItemClick();
                }
                else
                if (false)
                {

                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * maxReachLength, Color.yellow);
    }
}
