using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    // Use this for initialization
    public AudioSource background;
    public AudioSource screech;
    public AudioSource gargle;

    void Start()
    {
        background.Play(0);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            background.Stop();
            gargle.Play(0);
            Invoke("Screech", 2);
        }
        
    }

    void Screech()
    {
        gargle.Stop();
        screech.Play(0);
    }
}
