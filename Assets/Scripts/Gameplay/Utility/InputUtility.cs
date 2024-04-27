using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class InputUtility {
    public static float GetVerticalMoveInput()
    {
        return Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
    }

    public static float GetHorizontalMoveInput()
    {
        return Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
    }

    public static float GetMouseScrollWheelInput()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }
}