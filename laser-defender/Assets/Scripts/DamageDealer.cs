using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] AudioClip laserHitAudio;
    [SerializeField] AudioClip laserFireAudio;
    [SerializeField] GameObject laserHitVFX;
    [SerializeField] float durationOfHitVFX = 1f;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        PlayLaserHitVFX();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(laserHitAudio, transform.position);
        
    }
    public AudioClip GetLaserFireAudio()
    {
        return laserFireAudio;
    }
    public void PlayLaserHitVFX()
    {
        GameObject hitExplosion = Instantiate(laserHitVFX, transform.position, Quaternion.identity);
        Destroy(hitExplosion, durationOfHitVFX);
    }
}
