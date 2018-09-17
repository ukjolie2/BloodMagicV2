using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColor : MonoBehaviour {

    private UnityEngine.UI.Toggle toggle;
    public Color ActiveColor = Color.white;
    public Color UnactiveColor = Color.gray;
    public bool usingController = false;
    public int AttackNumber = 0;
    public enum Dpad { None, Right, Left, Up, Down }
    private bool flag = true;
    private Dpad control = Dpad.None;
    private int PressNumber = 0;

    private void Start()
    {
        toggle = GetComponent<UnityEngine.UI.Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // little tricky to use Dpad Axis as a button 

        private void PadControl()
        {
            if (Input.GetAxis("DpadX") == 0.0)
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
            yield return new WaitForSeconds(0.15f); // delay it as you wish 
            if (value == Dpad.Right)   //** go right
            {
                PressNumber = 3;
            }
            if (value == Dpad.Left)  //** go left
            {
                PressNumber = 2;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PressNumber = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PressNumber = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PressNumber = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PressNumber = 4;
        }

        if (PressNumber == AttackNumber)
        {
            if (toggle.isOn == true)
            {
                toggle.isOn = false;
            }
            else
            {
                toggle.isOn = true;
            }
            PressNumber = 0;

        }
        PadControl();
    }

}