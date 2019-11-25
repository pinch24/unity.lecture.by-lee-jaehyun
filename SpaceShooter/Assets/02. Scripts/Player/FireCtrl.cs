using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}

public class FireCtrl : MonoBehaviour
{
    public enum WeaponType
    {
        RIFLE = 0,
        SHOTGUN
    }

    public WeaponType currentWeapon = WeaponType.SHOTGUN;

    public GameObject bullet;
    public Transform firePos;
    public ParticleSystem cartridge;
    private ParticleSystem muzzleFlash;

    private AudioSource _audio;
    public PlayerSfx playerSfx;

    private Shake shake;
    
    void Start()
    {
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();

        _audio = GetComponent<AudioSource>();

        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Shot"))
        {
            Fire();
        }
    }

    void Fire()
    {
        StartCoroutine(shake.ShakeCamera(0.1f, 0.2f, 0.5f));

        Instantiate(bullet, firePos.position, firePos.rotation);

        cartridge.Play();
        muzzleFlash.Play();

        FireSfx();
    }

    void FireSfx()
    {
        var _sfx = playerSfx.fire[(int)currentWeapon];
        _audio.PlayOneShot(_sfx, 1.0f);
    }
}
