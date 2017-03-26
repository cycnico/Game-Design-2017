using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public ParticleSystem dieparticle;
    public AudioSource killaudio;
    public Controller controller;

    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    private PolygonCollider2D shape;
    private ParticleSystem die;

    // Use this for initialization
    void Start () {
        sprite = this.GetComponent<SpriteRenderer>();
        rigid = this.GetComponent<Rigidbody2D>();
        shape = this.GetComponent<PolygonCollider2D>();
        die = Instantiate(dieparticle);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude < 2.0f)
            return;
        sprite.enabled = false;
        rigid.isKinematic = true;
        shape.enabled = false;
        killaudio.Play();
        die.transform.position = this.transform.position;
        die.gameObject.SetActive(true);
        controller.enemydead();
    }
}
