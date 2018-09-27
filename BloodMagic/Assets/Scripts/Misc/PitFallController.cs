using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFallController : MonoBehaviour {

    public float minSpeed = 10f;
    private GameObject victim;
    private Vector2 victimScale;
    private Vector2 victimPosition;
    private Vector2 victimDirection;
    private int frameNum = 0;
    private Vector2 frameZeroPos;
    private Vector2 frameOnePos;
    private bool fall = false;

    void Update()
    {
        if (victim != null && (victim.tag == "Player" || victim.tag == "Enemy"))
        {
            Vector2 v = Vector2.zero;
            if (frameNum == 0)
            {
                frameZeroPos = victim.transform.position;
            }
            else if (frameNum == 1)
            {
                frameOnePos = victim.transform.position;
            }
            else if (frameNum == 2)
            {
                v = ((frameOnePos - frameZeroPos) / Time.deltaTime);
                if (v.magnitude < minSpeed)
                    fall = true;
                else
                {
                    victim = null;
                    fall = false;
                    frameNum = -1;
                }
                Debug.Log("Speedy speed" + v.magnitude);
            }
            frameNum++;
            if (fall)
            {
                victim.transform.localScale = Vector2.Lerp(victim.transform.localScale, Vector2.zero, 5f * Time.deltaTime);
                if (victim.transform.localScale.x < .5f)
                {
                    if (victim.tag != "Player")
                    {
                        Destroy(victim);
                    }
                    else
                    {
                        Transform[] pitSpawnPoints = transform.GetComponentsInChildren<Transform>();
                        float smallestDist = int.MaxValue;
                        Vector3 closestSpawn = new Vector3(0, int.MaxValue);
                        for (int i = 0; i < pitSpawnPoints.Length; i++)
                        {
                            float tempDist = Vector2.Distance(pitSpawnPoints[i].position, closestSpawn);
                            if (tempDist <= smallestDist)
                            {
                                smallestDist = tempDist;
                                closestSpawn = pitSpawnPoints[i].position;
                            }
                        }
                        victim.transform.position = closestSpawn;
                        victim.transform.localScale = new Vector3(2, 2, 0);
                    }
                    victim = null;
                    fall = false;
                    frameNum = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        victim = collision.gameObject;
        victimPosition = victim.transform.position;
        victimDirection = victim.transform.eulerAngles;
    }


}
