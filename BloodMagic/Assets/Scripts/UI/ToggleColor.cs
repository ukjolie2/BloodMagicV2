using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColor : MonoBehaviour {

    private UnityEngine.UI.Toggle toggle;
    public static bool SelectActive = true;
    public Color ActiveColor = Color.white;
    public Color UnactiveColor = Color.gray;
    public bool usingController = false;
    public int AttackNumber = 0;
    public enum Dpad { None, Right, Left, Up, Down }
    private bool flag = true;
    private Dpad control = Dpad.None;
    private int PressNumber = 0;
    public int HoldCount = 0;
    private bool holdFlag = false;

    private void Start()
    {
        toggle = GetComponent<UnityEngine.UI.Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // little tricky to use Dpad Axis as a button 

        private void PadControl()
        {
            if (Input.GetAxis("DpadX") <= 0.5 | Input.GetAxis("DpadY") <= 0.5)
            {
                control = Dpad.None;
                flag = true;
            }
            if (Input.GetAxis("DpadX") == 1f && flag)
            {
                StartCoroutine("DpadControl", Dpad.Right);
            }
            if (Input.GetAxis("DpadX") == -1f && flag)
            {
                StartCoroutine("DpadControl", Dpad.Left);
            }
            if (Input.GetAxis("DpadY") == 1f && flag)
            {
                StartCoroutine("DpadControl", Dpad.Up);
            }
            if (Input.GetAxis("DpadY") == -1f && flag)
            {
                StartCoroutine("DpadControl", Dpad.Down);

            }
        }

        // your methods can go nice and easy here ! 
        IEnumerator DpadControl(Dpad value)
        {
            flag = false;
            yield return new WaitForSeconds(.15f); // delay it as you wish 
            if (value == Dpad.Right)   //** go right
            {
                PressNumber = 3;
            }
            if (value == Dpad.Left)  //** go left
            {
                if (Input.GetAxis("DpadX") <= 0.5 | Input.GetAxis("DpadY") <= 0.5)
            {
                    if (holdFlag == false)
                    {
                        PressNumber = 2;
                        holdFlag = true;
                    }
            }
        }
            if (value == Dpad.Up)  //** go up
            {
                PressNumber = 1;
            }
            if (value == Dpad.Down) //** go down
            {
                PressNumber = 4;
            }

        StopCoroutine("DpadControl");
    }

    private void OnToggleValueChanged(bool isOn)
    {
        
        ColorBlock cb = toggle.colors;
        if (isOn)
        {
            cb.normalColor = ActiveColor;
            cb.highlightedColor = ActiveColor;
        }
        else
        {
            cb.normalColor = UnactiveColor;
            cb.highlightedColor = UnactiveColor;
        }
        toggle.colors = cb;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            PressNumber = 1;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (holdFlag == false)
            {
                PressNumber = 2;
                holdFlag = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            PressNumber = 3;
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            PressNumber = 4;
        }
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || 
            Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Alpha4) ||
            Input.GetAxis("DpadX") >= .5 || Input.GetAxis("DpadY") >= .5 ||
            Input.GetAxis("DpadX") <= -.5 || Input.GetAxis("DpadY") <= -.5)
        {
            HoldCount += 1;
        }
        else
        {
            HoldCount = 0;
            holdFlag = false;

        }

        if (HoldCount >= 50)
        {
            SelectActive = false;
        }

        if (PressNumber == AttackNumber)
        {
            if (holdFlag == false)
            {
                toggle.isOn = !toggle.isOn;
                PressNumber = 0;
            }
        }
        PadControl();
    }

}