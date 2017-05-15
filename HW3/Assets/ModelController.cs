using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Hit;
using DG.Tweening;

public class ModelController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject UnityChan;
    public GameObject Skeleton;
    public TransformGesture ZoomGesture;
    public FlickGesture flick;
    public TapGesture singleTap;
    public TapGesture doubleTap;
    public Animator UnityChanAnimator;
    public Animator SkeletonAnimator;
    public float maxZ;
    public float minZ;
    public float thresold;

    private GameObject Display;
    private Animator animator;
    private float temp;
    private int model;
    private Vector3 tempGyro;

    void Start()
    {
        Input.gyro.enabled = true;
        model = 1;
        Display = UnityChan;
        animator = UnityChanAnimator;

        singleTap.Tapped += (object sender, System.EventArgs e) =>
        {
            animator.SetTrigger("damage");
        };

        doubleTap.Tapped += (object sender, System.EventArgs e) =>
        {
            animator.SetTrigger("jump");
        };

        flick.Flicked += (object sender, System.EventArgs e) =>
        {
            if (model == 1)
            {
                UnityChan.SetActive(false);
                Display = Skeleton;
                animator = SkeletonAnimator;
                Skeleton.SetActive(true);
                model = 2;
            } else if (model == 2)
            {
                Skeleton.SetActive(false);
                Display = UnityChan;
                animator = UnityChanAnimator;
                UnityChan.SetActive(true);
                model = 1;
            }
        };

        ZoomGesture.TransformStarted += (object sender, System.EventArgs e) => { };

        ZoomGesture.Transformed += (object sender, System.EventArgs e) =>
        {
            if (ZoomGesture.DeltaScale > 1.0)
            {
                mainCamera.transform.position += new Vector3(0, 0, 0.1f);
                if (mainCamera.transform.position.z >= maxZ) {
                    mainCamera.transform.position -= new Vector3(0, 0, 0.1f);
                }
            }

            if (ZoomGesture.DeltaScale < 1.0)
            {
                mainCamera.transform.position -= new Vector3(0, 0, 0.1f);
                if (mainCamera.transform.position.z <= minZ)
                {
                    mainCamera.transform.position += new Vector3(0, 0, 0.1f);
                }
            }
        };

        ZoomGesture.TransformCompleted += (object sender, System.EventArgs e) => { };

    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.gyro.userAcceleration);
        if ((tempGyro- Input.gyro.userAcceleration).magnitude > thresold)
        {
            animator.SetTrigger("fall");
        }
        tempGyro = Input.gyro.userAcceleration;

        if (Input.touches.Length == 1)
        {
            if(temp > Input.touches[0].position.x)
            {
                Display.transform.Rotate(new Vector3(0, 1.3f, 0));
            }

            if (temp < Input.touches[0].position.x)
            {
                Display.transform.Rotate(new Vector3(0, -1.3f, 0));
            }
            temp = Input.touches[0].position.x;
        }
    }
}
