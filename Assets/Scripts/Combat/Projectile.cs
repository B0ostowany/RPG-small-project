using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

public class Projectile : MonoBehaviour
{
    Health target = null;
    GameObject instigator = null;
    float damage = 0f;
    [SerializeField] float speed = 5f;
    [SerializeField] bool isHoming = false;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] float maxLifeTime = 10f;
    [SerializeField] GameObject[] destroyOnHit = null;
    [SerializeField] float lifeAfterImpact = 2f;
    private void Start()
    {
        if(!isHoming)
        {
            transform.LookAt(GetAimLocation());
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(target ==null) return;
        if (isHoming&&!target.IsDead()) { transform.LookAt(GetAimLocation()); }

        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

    public void SetTarget(Health target,GameObject instigator, float damage)
    {
        this.target = target;
        this.damage = damage;
        this.instigator = instigator;

        Destroy(gameObject,maxLifeTime);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;
        if (target.IsDead()) return;
        target.TakeDamage(instigator,damage);
        speed = 0;
        if (hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(),transform.rotation);
        }
        
        foreach(GameObject toDestroy in destroyOnHit)
        {
            Destroy(toDestroy);
        }

        Destroy(gameObject);
    }
}
