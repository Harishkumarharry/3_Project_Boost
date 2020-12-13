using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsTrust = 100f;
    [SerializeField] float MainTrust = 100f;

    Rigidbody rigidBody;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Trust();
        Rotate();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Ok");
                break;
            default:
                print("Dead");
                break;
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        
        float rotationTrustperFrame = rcsTrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationTrustperFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationTrustperFrame);
        }
        rigidBody.freezeRotation = false;
    }

    private void Trust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float mainTrustperFrame = MainTrust * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * mainTrustperFrame);
            if (!audiosource.isPlaying) //if we will not keep then the audio will keep on playing.
            {
                audiosource.Play();
            }
        }
        else
        {
            audiosource.Stop();
        }
    }
}
