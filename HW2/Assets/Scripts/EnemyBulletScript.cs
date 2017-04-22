using UnityEngine;
using System.Collections;

public class EnemyBulletScript : MonoBehaviour
{

    public float FlyingSpeed;
    public float LifeTime;
    public float damageValue;
    public GameObject explosion;
    public AudioSource bulletAudio;

    public void InitAndShoot(Vector3 Direction)
    {
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.velocity = Direction * FlyingSpeed;
        Invoke("KillYourself", LifeTime);
    }

    public void KillYourself()
    {
        GameObject.Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        //other.gameObject.SendMessage("Hit", damageValue);
        other.transform.GetChild(1).GetChild(0).SendMessage("Hit", damageValue);
        explosion.gameObject.transform.parent = null;
        explosion.gameObject.SetActive(true);
        bulletAudio.pitch = Random.Range(0.8f, 1);

        KillYourself();
    }

}
