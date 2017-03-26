using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform stone;
    public Transform leftupedge;
    public Transform rightdownedge;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = this.transform.position;
        temp.x = stone.position.x;
        temp.x = Mathf.Clamp(temp.x, leftupedge.position.x, rightdownedge.position.x);
        temp.y = stone.position.y;
        temp.y = Mathf.Clamp(temp.y, rightdownedge.position.y, leftupedge.position.y);
        this.transform.position = temp;
	}
}
