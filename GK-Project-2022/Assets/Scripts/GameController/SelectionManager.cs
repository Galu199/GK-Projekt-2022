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

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(ray, out hit, maxReachLength))
            {
                var selection = hit.transform;
                if (selection.GetComponent<ElevatorButton>() != null)
                {
                    elevatorButtonPressed = true;
                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * maxReachLength, Color.yellow);
    }
}
