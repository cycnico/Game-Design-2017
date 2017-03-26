using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stonecontroller : MonoBehaviour {

    public float max;
    public GameObject Catapultfront;
    public GameObject Catapultback;
    public AudioSource launch;
    public LifeManager life;
    public float leftedge;
    public float rightedge;
    public float velocity;
    public float gravity;

    private Vector3 CatapultCenter;
    private LineRenderer Catapultlinefront;
    private LineRenderer Catapultlineback;
    private Ray r;
    private Rigidbody2D stone;
    private float StoneRadius = 0.62f;
    private int step;
    private bool pause;

    // Use this for initialization
    void Start ()
    {
        stone = this.GetComponent<Rigidbody2D>();
        step = 0;
        pause = false;
        CatapultCenter = (Catapultfront.transform.position + Catapultback.transform.position) / 2;
        stone.transform.position = CatapultCenter;
        stone.velocity = new Vector2(0,0);
        stone.freezeRotation = true;
        stone.isKinematic = true;
        stone.gravityScale = 0;
        r = new Ray(Catapultfront.transform.position, Vector3.zero);
        LineRendererSetup ();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(pause == true)
            return;

        if (stone.transform.position.x < leftedge || stone.transform.position.x > rightedge)
            DecreaseLife();

        if (step==1)
        {
            Catapultlinefront.enabled = false;
            Catapultlineback.enabled = false;
            if (stone.velocity.magnitude == 0) {
                stone.velocity = -(transform.position - CatapultCenter).normalized * velocity;
                stone.gravityScale = gravity;
            }
            launch.Play();
            step++;
        }

        if (step == 2)
            if(stone.velocity.magnitude < 0.25 && pause != true)
                DecreaseLife();

    }

    public void ResetStone()
    {
        Start();
    }


    public void Pause()
    {
        pause = true;
    }

    private void DecreaseLife()
    {
        life.decreaselife();
        if (life.Getlife() != 0)
            ResetStone();
    }

    void LineRendererSetup ()
    {
        Catapultlinefront = Catapultfront.GetComponent<LineRenderer>();
        Catapultlineback = Catapultback.GetComponent<LineRenderer>();
        Catapultlinefront.sortingLayerName = Catapultfront.GetComponent<SpriteRenderer>().sortingLayerName;
        Catapultlineback.sortingLayerName = Catapultback.GetComponent<SpriteRenderer>().sortingLayerName;
        Catapultlinefront.SetPosition(0, Catapultlinefront.transform.position);
        Catapultlineback.SetPosition(0, Catapultlineback.transform.position);
        Catapultlinefront.SetPosition(1, Catapultlinefront.transform.position);
        Catapultlineback.SetPosition(1, Catapultlineback.transform.position);
        Catapultlinefront.enabled = true;
        Catapultlineback.enabled = true;
    }

    void OnMouseUp ()
    {
        if (step == 0) {
            stone.freezeRotation = false;
            stone.isKinematic = false;
            step = 1;
        }
    }

    void OnMouseDrag()
    {
        if (step == 0) {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePoint.z = 0;
            Vector3 MouseToFront = MousePoint - CatapultCenter;
            float draglength = MouseToFront.magnitude;
            if (draglength > max)
                draglength = max;
            transform.position = CatapultCenter + MouseToFront.normalized * draglength;

            Vector2 CatapultToStone = transform.position - Catapultlinefront.transform.position;
            r.direction = CatapultToStone;
            Vector3 hold = r.GetPoint(CatapultToStone.magnitude + StoneRadius);
            Catapultlinefront.SetPosition(1, hold);
            Catapultlineback.SetPosition(1, hold);
        }
    }
}