using UnityEngine;
using System.Collections;
using Boo.Lang;

public class ActiveObjects : MonoBehaviour
{

    private static bool ObjActive;
    private static bool NewActive;
    public int AttackMenu = 0;

    private void Update()
    {
        ObjActive = ToggleColor.SelectActive;
        NewActive = ToggleNewAttack.SelectNew;



        if (ObjActive == false)
        {

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {

            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }

    }
}