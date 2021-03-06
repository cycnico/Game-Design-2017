﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Transform rotateYTransform;
    public Transform rotateXTransform;
    public Rigidbody rigidBody;
    public JumpSensor JumpSensor;
    public GunManager gunManager;
    public FireGunManager FiregunManager;
    public GameUIManager uiManager;
    public AudioSource runsound;
    public GameObject gun;
    public GameObject firegun;

    public float rotateSpeed;
    public float currentRotateX = 0;
    public float MoveSpeed;
    public float JumpSpeed;
    public int hp = 100;

    private Animator animatorController;
    private int Firemode = 0;
    float currentSpeed = 0;

    // Use this for initialization
    void Start()
    {
        animatorController = this.GetComponent<Animator>();
    }

    public void Hit(int value)
    {
        if (hp <= 0)
        {
            return;
        }

        hp -= value;
        uiManager.SetHP(hp);

        if (hp > 0)
        {
            uiManager.PlayHitAnimation();
        }
        else
        {
            currentSpeed = 0 ;
            runsound.Stop();
            animatorController.SetFloat("Speed", currentSpeed);
            uiManager.PlayerDiedAnimation();
            rigidBody.gameObject.GetComponent<Collider>().enabled = false;
            rigidBody.useGravity = false;
            rigidBody.velocity = Vector3.zero;
            this.enabled = false;
            rotateXTransform.transform.DOLocalRotate(new Vector3(-60, 0, 0), 0.5f);
            rotateYTransform.transform.DOLocalMoveY(-1.5f, 0.5f).SetRelative(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        if (Input.GetMouseButton(0))
        {
            if (Firemode == 0)
                gunManager.TryToTriggerGun();
            if (Firemode == 1)
                FiregunManager.TryToTriggerGun();
        }

        if (!Input.GetMouseButton(0))
        {
            FiregunManager.StopFire();
        }

        //決定鍵盤input的結果
        Vector3 movDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { movDirection.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movDirection.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { movDirection.x += 1; }
        if (Input.GetKey(KeyCode.A)) { movDirection.x -= 1; }
        movDirection = movDirection.normalized;

        //決定武器
        if (Input.GetKey(KeyCode.Alpha1)) {
            Firemode = 0;
            firegun.SetActive(false);
            gun.SetActive(true);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            Firemode = 1;
            gun.SetActive(false);
            firegun.SetActive(true);
        }

        //決定要給Animator的動畫參數
        if (movDirection.magnitude == 0 || !JumpSensor.IsCanJump()) { currentSpeed = 0; }
        else
        {
            if (movDirection.z < 0) { currentSpeed = -MoveSpeed; }
            else { currentSpeed = MoveSpeed; }
        }
        animatorController.SetFloat("Speed", currentSpeed);

        if (currentSpeed == 0)
            runsound.Stop();

        if (currentSpeed != 0)
            if(!runsound.isPlaying)
                runsound.Play();

        //轉換成世界座標的方向
        Vector3 worldSpaceDirection = movDirection.z * rotateYTransform.transform.forward +
                                      movDirection.x * rotateYTransform.transform.right;
        Vector3 velocity = rigidBody.velocity;
        velocity.x = worldSpaceDirection.x * MoveSpeed;
        velocity.z = worldSpaceDirection.z * MoveSpeed;

        if (Input.GetKey(KeyCode.Space) && JumpSensor.IsCanJump())
        {
            velocity.y = JumpSpeed;
        }

        rigidBody.velocity = velocity;

        //計算滑鼠
        rotateYTransform.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Horizontal"), 0) * rotateSpeed;
        currentRotateX += Input.GetAxis("Vertical") * rotateSpeed;

        if (currentRotateX > 90)
        {
            currentRotateX = 90;
        }
        else if (currentRotateX < -90)
        {
            currentRotateX = -90;
        }
        rotateXTransform.transform.localEulerAngles = new Vector3(-currentRotateX, 0, 0);

    }
}
