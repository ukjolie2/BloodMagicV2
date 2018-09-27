using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour
{
    public Animator animator;
    public GameObject enemy;
    public float yRotate = 0;
    public float zRotate = 0;


	// Use this for initialization
	void Start () {
		
	}

    Quaternion rotation;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update () {

     
        yRotate = enemy.transform.rotation.eulerAngles.y;
        zRotate = Mathf.Abs(enemy.transform.rotation.eulerAngles.z);
        animator.SetFloat("Rotation", zRotate);

        if (yRotate > 0)
        {
            animator.SetBool("Flip", true);
        }
        else
        {
            animator.SetBool("Flip", false);

        }


    }
}
