﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "BULLET")
        {
            ShowEffect(collision);
            Destroy(collision.gameObject);
        }
    }

    void ShowEffect(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);

        GameObject spark = Instantiate(sparkEffect, contact.point + (-contact.normal * 0.05f), rot);
        spark.transform.SetParent(this.transform);
    }
}
