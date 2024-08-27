﻿using System.Collections;
using UnityEngine;

public class TowerProjectile : MonoBehaviour {

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider coll;
    [SerializeField] private float moveForce;
    private Vector3 ogScale;

    void Awake() {
        ogScale = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine(IGrow());
    }

    public void Launch(Vector3 direction) {
        rb.AddForce(direction * moveForce);
    }

    void OnCollisionEnter(Collision coll) {
        if ((coll.collider is MeshCollider
            || coll.collider is BoxCollider)) {
            Destroy(rb);
            Destroy(this.coll);
            StartCoroutine(IEnd());
        }
    }

    private IEnumerator IGrow() {
        while (transform.localScale != ogScale) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, ogScale, Time.deltaTime * 5);
            yield return null;
        }
    }

    private IEnumerator IEnd() {
        while (transform.localScale != Vector3.zero) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * 5);
            yield return null;
        }
        Destroy(gameObject);
    }
}