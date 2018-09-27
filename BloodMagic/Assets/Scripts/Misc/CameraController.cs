using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    public GameObject oldTarget = null;
    GameObject bumper = null;

    public float smoothSpeed = 0.125f;

    // Use this for initialization
    void Start()
    {
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        Vector3 finalPosition = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        transform.position = finalPosition;

        if(bumper != null)
        {

            if (Vector3.Distance(transform.position, player.transform.position) > 2.5f)
            {
                Invoke("SetTarget", 2);
                bumper = null;
            }
        }
    }

    public void setTarget(GameObject target)
    {
        Debug.Log("Im changing");
        oldTarget = player;
        player = target;
        bumper = player;
    }

    public void SetTarget()
    {
        Debug.Log("Im new");
        player = oldTarget;
        oldTarget = null;
    }
}
