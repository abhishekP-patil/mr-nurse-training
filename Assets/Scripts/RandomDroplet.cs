using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDroplet : MonoBehaviour
{
    public ParticleSystem droplet;
    public AudioSource audioSource;
    public float minTime = 2f;
    public float maxTime = 5f;

    private float timer;
    private float timeToTrigger;

    void Start()
    {
        if (droplet == null)
        {
            droplet = GetComponent<ParticleSystem>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        ResetTimer();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToTrigger)
        {
            droplet.Play();
            audioSource.Play();
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        timer = 0f;
        timeToTrigger = Random.Range(minTime, maxTime);
    }
}
