using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int maxHealth = 500;
    [SerializeField] int health = 500;

    [Header("Projectile")]
    [SerializeField] bool isLock = true;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 1f;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip laserFireClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField][Range(0, 1)] float laserFireClipVolume = 0.7f;
    [SerializeField][Range(0,1)] float deathClipVolume = 0.7f;

    Level sceneLoader;

    float xMin, xMax;
    float yMin, yMax;

    Coroutine firingCoroutine;
    PersistThis scenery;

    //Camera gameCamera = Camera.main;
    
    void Start()
    {
        
        SetUpMoveBoundaries();
        scenery = FindObjectOfType<PersistThis>();
    }
        
        
    void Update()
    {
        Move();
        //if (!isLock)
        //{
            Fire();
        //}
        //StartCoroutine(FireContinuously());
       
    }

    public void LockFire(bool isLock)
    {

        this.isLock = isLock;
                
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            firingCoroutine = StartCoroutine(FireContinuously());
            //ClearObjectOffScreen(laser);

        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.transform.parent = scenery.gameObject.transform;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            PlayLaserAudio();
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void PlayLaserAudio()
    {
        laserFireClip = laserPrefab.GetComponent<DamageDealer>().GetLaserFireAudio();
        AudioSource.PlayClipAtPoint(laserFireClip, Camera.main.transform.position, laserFireClipVolume);
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        //multiplying by Time.deltaTime makes this frame independent
        
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin,xMax) ;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin,yMax) ;
        
        transform.position = new Vector2(newXPos, newYPos);        
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 1)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 1)).y - padding;
    }
    private void ClearObjectOffScreen(GameObject gameObject)
    {
        Camera mainCamera = Camera.main;

        float xCamMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float xCamMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float yCamMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        float yCamMax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
           
        Debug.Log(xCamMin + "," + xCamMax + "," + yCamMin + "," + yCamMax);
        Debug.Log(gameObject.transform.position.x + "," + gameObject.transform.position.y);
        if (gameObject.transform.position.x > xCamMax|| gameObject.transform.position.x < xCamMin)
        {
            Destroy(gameObject);
        }
        else if (gameObject.transform.position.y > yCamMax || gameObject.transform.position.y < yCamMin)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer)
        {
            return;
        }
        ProcessHit(other, damageDealer);
    }
    private void ProcessHit(Collider2D other, DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        Destroy(other.gameObject);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayDeathAnimation();
        PlayDeathAudio();
        CallDestroyHealthText();
        gameObject.SetActive(false);
        LoadGameOverScene();
    }

    private void PlayDeathAnimation()
    {
        GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, durationOfExplosion);
    }

    private void PlayDeathAudio()
    {
        AudioSource.PlayClipAtPoint(deathClip, transform.position, deathClipVolume);
    }

    public void LoadGameOverScene()
    {
        sceneLoader = FindObjectOfType<Level>();
        sceneLoader.LoadGameOver();
    }
    public int GetHealth()
    {
        return health;
    }
    private void CallDestroyHealthText()
    {
        gameObject.GetComponent<HealthDisplay>().DestroyHealthText();
    }
    public void ResetHealth()
    {
        health = maxHealth;
    }
}

