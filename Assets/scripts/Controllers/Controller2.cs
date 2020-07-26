using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2 : MonoBehaviour, IControlPlayers
{
    public bool isassigned { get; set; }
    public int Index { get; set; }
    public bool anyButtonDown()
    {
        return X_Button();
    }
    public Vector3 getDirection()
    {
        return new Vector3(Horizontal(), 0, Vertical());
    }
    public void SetIndex(int index)
    {
        Index = index;
    }

    public bool X_Button()
    {
        bool X_Button = Input.GetKeyUp(KeyCode.Joystick2Button1);
        return X_Button;
    }
    public bool Hold_X_Button()
    {
        bool X_Hold = Input.GetKey(KeyCode.Joystick2Button1);
        return X_Hold;
    }
    public bool Circle_Button()
    {
        bool Circle = Input.GetKeyUp(KeyCode.Joystick2Button2);
        return Circle;
    }
    public bool Hold_Circle_Button()
    {
        bool CircleHold = Input.GetKey(KeyCode.Joystick2Button2);
        return CircleHold;
    }
    public bool Square_Button()
    {
        bool Square = Input.GetKeyUp(KeyCode.Joystick2Button3);
        return Square;
    }
    public bool Start_Button()
    {
        bool Circle = Input.GetKeyDown(KeyCode.Joystick2Button9) || Input.GetKey(KeyCode.Escape);
        return Circle;
    }
    public float Horizontal()
    {
        float horizontal = Input.GetAxis("Horizontal2");
        return horizontal;
    }

    public float Vertical()
    {
        float vertical = Input.GetAxis("Vertical2");
        return vertical;
    }

    public bool Left_Button()
    {
        bool Left = Input.GetKeyDown(KeyCode.Joystick2Button6);
        return Left;
    }

    public bool Right_Button()
    {
        bool right = Input.GetKeyDown(KeyCode.Joystick2Button7);
        return right;
    }
}
