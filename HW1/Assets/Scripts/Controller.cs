using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public Stonecontroller stone;
    public StageManager stagemanager;
    public LifeManager lifemanager;
    public GameObject Prestage1;
    public GameObject Prestage2;
    public Text gameover;
    public Text complete;
    public Text finish;

    private GameObject stage1;
    private GameObject stage2;
    private GameObject temp;
    private int stage;
    private int NumofEnemy;
    private bool Stage1Complete;

    // Use this for initialization
    void Start () {
        stage = 1;
        NumofEnemy = 1;
        Stage1Complete = false;
        temp = Instantiate(Prestage1);
        stage1 = temp;
        stage1.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            ResetGame();

        if (Input.GetKeyDown(KeyCode.Return) && stage ==1 && Stage1Complete == true)
            GotoStage2();
    }

    public void enemydead()
    {
        NumofEnemy -= 1;
        if(NumofEnemy == 0)
            StageComplete();
    }

    public void Gameover()
    {
        gameover.gameObject.SetActive(true);
    }

    private void ResetGame()
    {
        if (stage == 1)
        {
            Destroy(stage1);
        }
        if (stage == 2)
        {
            Destroy(stage2);
        }
        temp = Instantiate(Prestage1);
        stage1 = temp;
        stage1.SetActive(true);
        stone.ResetStone();
        stagemanager.ResetStage();
        lifemanager.Resetlife();
        stage = 1;
        NumofEnemy = 1;
        Stage1Complete = false;
        gameover.gameObject.SetActive(false);
        complete.gameObject.SetActive(false);
        finish.gameObject.SetActive(false);
    }

    private void StageComplete()
    {
        if (stage == 1)
        {
            stone.Pause();
            complete.gameObject.SetActive(true);
            Stage1Complete = true;
        }

        if (stage == 2)
        {
            stone.Pause();
            finish.gameObject.SetActive(true);
        }
    }

    private void GotoStage2()
    {
        stage += 1;
        NumofEnemy = 3;
        Destroy(stage1);
        temp = Instantiate(Prestage2);
        stage2 = temp;
        stage2.SetActive(true);
        stone.ResetStone();
        complete.gameObject.SetActive(false);
        stagemanager.AddStage();
        lifemanager.Resetlife();
    }

}
