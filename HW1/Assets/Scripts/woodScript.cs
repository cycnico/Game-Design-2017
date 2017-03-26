using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodScript : MonoBehaviour {

    public AudioSource woodSound;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude < 3f)
            return;
        if(woodSound.isPlaying==false)
            woodSound.Play();
    }

}
