using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public Controller gamecontrol;

    private int stage = 1;
    private Text stagetext;

    public void AddStage()
    {
        stage += 1;
        stagetext.text = "Stage " + stage.ToString(); 
    }

    public void ResetStage()
    {
        stage = 1;
        stagetext.text = "Stage " + stage.ToString();
    }

    // Use this for initialization
    void Start () {
        stagetext = this.GetComponent<Text>();
	}

}
