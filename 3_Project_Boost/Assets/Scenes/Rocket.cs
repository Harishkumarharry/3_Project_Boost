using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody; //Getting components added in unity to handle in code
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
        Thrust();//To Move the Rocket in upward direction
        Rotate();//To rotate the ship in either cloclwise
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
        
        float rotationTrustperFrame = rcsThrust * Time.deltaTime;
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

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float mainTrustperFrame = mainThrust * Time.deltaTime;
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
