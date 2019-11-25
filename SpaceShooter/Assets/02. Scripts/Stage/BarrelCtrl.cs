using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    private int hitCount = 0;
    private Rigidbody rb;

    public Mesh[] meshes;
    private MeshFilter meshFilter;

    public Texture[] textures;
    private MeshRenderer _renderer;

    public float expRadius = 10.0f;

    private AudioSource _audio;
    public AudioClip expSfx;

    public Shake shake;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        meshFilter = GetComponent<MeshFilter>();

        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];

        _audio = GetComponent<AudioSource>();

        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            if (++ hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);

        IndirectDamage(transform.position);

        int idx = Random.Range(0, meshes.Length);
        meshFilter.sharedMesh = meshes[idx];

        _audio.PlayOneShot(expSfx, 1.0f);

        StartCoroutine(shake.ShakeCamera(0.04f, 0.08f, 0.08f));
    }

    void IndirectDamage(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, expRadius, 1 << 8);

        foreach (var collider in colliders)
        {
            var _rb = collider.GetComponent<Rigidbody>();
            _rb.mass = 1.0f;
            _rb.AddExplosionForce(1200.0f, pos, expRadius, 1000.0f);
        }
    }
}
