using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;

    [Header("Shooting")]
    float shotcounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileDirection = 0f;
    [SerializeField] bool extraShot = false;

    [Header("Sound/Video Effects")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip laserFireClip;
    [SerializeField] [Range(0, 1)] float laserFireClipVolume = 0.4f;
    [SerializeField] AudioClip deathClip;
    [SerializeField] [Range(0, 1)] float deathClipVolume = 1f;
    

    float xVelocity;
    float yVelocity;

    void Start()
    {
        shotcounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        xVelocity = GetSpeedXComponent(projectileDirection, projectileSpeed);
        yVelocity = GetSpeedYComponent(projectileDirection, projectileSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotcounter -= Time.deltaTime;
        if(shotcounter <= 0)
        {
            Fire();
            if (extraShot == true)
            {
                ExtraFire();
            }
            shotcounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        PlayLaserAudio();
        enemyLaser.transform.rotation = Quaternion.Euler(0, 0, projectileDirection);
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, -yVelocity);
    }

    private void PlayLaserAudio()
    {
        laserFireClip = laserPrefab.GetComponent<DamageDealer>().GetLaserFireAudio();
        AudioSource.PlayClipAtPoint(laserFireClip, transform.position, laserFireClipVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return; //if damage failed to have a reference just exit and not process the hit
        }
        ProcessHit(other, damageDealer);                     
    }

    private void ProcessHit(Collider2D other, DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.PlayLaserHitVFX();
        if (health <= 0)
        {
            Die();
            
        }
        Destroy(other.gameObject);
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        PlayDeathAnimation();
        PlayDeathAudio();
    }

    private void PlayDeathAudio()
    {
        AudioSource.PlayClipAtPoint(deathClip, transform.position, deathClipVolume);
    }

    private void PlayDeathAnimation()
    {
        GameObject explosionDeath = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosionDeath, durationOfExplosion);
    }

    private float GetSpeedXComponent(float projectileDirection, float projectileSpeed)
    {
        return projectileSpeed * Mathf.Sin(projectileDirection);
    }
    private float GetSpeedYComponent(float projectileDirection, float projectileSpeed)
    {
        return projectileSpeed * Mathf.Cos(projectileDirection);
    }
    private void ExtraFire()
    {
        GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        enemyLaser.transform.rotation = Quaternion.Euler(0, 0, -projectileDirection);
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(-xVelocity, -yVelocity);
    }
}
