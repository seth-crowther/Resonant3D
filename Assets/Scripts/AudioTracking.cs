using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTracking : MonoBehaviour
{
    public Transform mainCam;
    public Transform player;
    public float angleToSpeaker;

    private Vector3 cameraForward;
    private Vector3 dirToSpeaker;
    private AudioSource audioSource;

    private float range;
    private float playerDist;
    private float maxVolume = 0.1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        range = audioSource.maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        AdjustAudioPan();
        AdjustAudioAttenuation();
    }

    private void AdjustAudioPan()
    {
        cameraForward = new Vector3(mainCam.transform.forward.x, 0f, mainCam.transform.forward.z).normalized;
        dirToSpeaker = (new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(mainCam.transform.position.x, 0f, mainCam.transform.position.z)).normalized;

        angleToSpeaker = Vector3.SignedAngle(cameraForward, dirToSpeaker, Vector3.up) * Mathf.Deg2Rad;
        audioSource.panStereo = Mathf.Sin(angleToSpeaker);
    }

    private void AdjustAudioAttenuation()
    {
        playerDist = Vector3.Distance(transform.position, player.position);
        audioSource.volume = -(1 / range) * maxVolume * playerDist + maxVolume;
    }
}
