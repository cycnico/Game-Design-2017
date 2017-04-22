using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FireGunManager : MonoBehaviour
{
    public GameObject Fire;
    public ParticleSystem FireSpray;
    public float damageValue;

    private AudioSource FireShootSound;
    private bool onFire = false;

    public void Start()
    {
        onFire = false;
        FireShootSound = this.GetComponent<AudioSource>();
    }

    public void TryToTriggerGun()
    {
        if (!onFire)
        {
            onFire = true;
            FireShootSound.Stop();
            FireShootSound.pitch = Random.Range(0.8f, 1);
            FireShootSound.Play();
            Fire.SetActive(true);
        }
    }

    public void StopFire()
    {
        if (onFire)
        {
            onFire = false;
            FireShootSound.Stop();
            Fire.SetActive(false);
        }
    }

}