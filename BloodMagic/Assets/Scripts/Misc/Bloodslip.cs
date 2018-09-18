using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodslip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    
    /**
     * if (MultiInput.GetAxis("LeftJoystickX", "", name) < 0)
                {
                    if (slide)
                    {
                        if (velocity.x > 0)
                        {
                            velocity.x += -walkSpeed* 0.002f;
                        }
                        else
                        {
                            velocity.x += -walkSpeed* 0.11f;
                        }
                    }
                }
                else if (MultiInput.GetAxis("LeftJoystickX", "", name) > 0)
                {
                    if (slide)
                    {
                        if (velocity.x < 0)
                        {
                            velocity.x += walkSpeed* 0.002f;
                        }
                        else
                        {
                            velocity.x += walkSpeed* 0.11f;
                        }
                    }
                }
                else
                {
                    if (slide)
                    {
                        velocity.x += velocity.x* 0.05f;
                    }
                }
     **/
}
