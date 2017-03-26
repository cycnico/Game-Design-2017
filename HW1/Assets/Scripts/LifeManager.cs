using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LifeManager : MonoBehaviour {

    public Controller gamecontrol;
    public Stonecontroller stonecontrol;

    private int life;
    private Text lifetext;

    public void decreaselife()
    {
        if (life >= 1)
        {
            life -= 1;
            lifetext.text = "Life " + life.ToString();
            if(life == 0)
            {
                gamecontrol.Gameover();
            } else {
                stonecontrol.ResetStone();
            }
        }
    }

    public void Resetlife()
    {
        life = 3;
        lifetext.text = "Life " + life.ToString();
    }

    public int Getlife()
    {
        return life;
    }

    // Use this for initialization
    void Start () {
        life = 3;
        lifetext = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
