using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 100f;

    enum State { Alive, Dying, Transcending};
    State state = State.Alive;

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
        if(state == State.Alive)
        {
            Thrust();//To Move the Rocket in upward direction
            Rotate();//To rotate the ship in either cloclwise
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } // Ignores collision when state is dead

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadingNextLevel", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadingFirstLevel", 1f);
                break;
        }
    }

    private void LoadingNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadingFirstLevel()
    {
        SceneManager.LoadScene(0);
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
