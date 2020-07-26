using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, IControlPlayers
{
    public bool isassigned { get; set; }
    public int Index { get; set; }

    public bool anyButtonDown()
    {
        return X_Button();
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    public Vector3 getDirection()
    {
        return new Vector3(Horizontal(), 0, Vertical()).normalized;
    }
    public bool X_Button()
    {
        bool X_BUtton = Input.GetKeyUp(KeyCode.Joystick1Button1) || Input.GetKeyUp(KeyCode.K);
        return X_BUtton;
    }
    public bool Hold_X_Button()
    {
        bool XHold = Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.K);

        return XHold;
    }
    public bool Circle_Button()
    {
        bool Circle = Input.GetKeyUp(KeyCode.Joystick1Button2) || Input.GetKeyUp(KeyCode.L);
        return Circle;
    }
    public bool Hold_Circle_Button()
    {
        bool CircleHold = Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.K);
        return CircleHold;
    }
    public bool Square_Button()
    {
        bool Square = Input.GetKeyUp(KeyCode.Joystick1Button3) || Input.GetKeyUp(KeyCode.J);
        return Square;
    }
    public bool Start_Button()
    {
        bool StartBut = Input.GetKeyDown(KeyCode.Joystick1Button9) || Input.GetKeyDown(KeyCode.Escape);
        return StartBut;
    }


    public float Horizontal()
    {
        float horizontal = Input.GetAxis("Horizontal");
        return horizontal;
    }

    public float Vertical()
    {
        float vertical = Input.GetAxis("Vertical");
        return vertical;
    }

    public bool Left_Button()
    {
        bool Left = Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.A);
        return Left;
    }

    public bool Right_Button()
    {
        bool right = Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.D);
        return right;
    }
}

public interface IControlPlayers
{
    Vector3 getDirection();
    bool isassigned { get; set; }
    int Index { get;  set; }
    bool anyButtonDown();

    void SetIndex(int index);

    bool X_Button();
    bool Hold_X_Button();
    bool Circle_Button();
    bool Hold_Circle_Button();
    bool Square_Button();
    bool Start_Button();
    float Horizontal();
    float Vertical();

    bool Left_Button();
    bool Right_Button();
}
