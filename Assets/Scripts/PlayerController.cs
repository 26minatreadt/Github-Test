using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player animation
    private Animator playerAnim;
    //player movement
    private Rigidbody playerRb;
    public float jumpForce;
    public bool isOnGround = true;
    public float gravityModifier;
    //player particles
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    //conditions for game over
    public bool gameOver = false;
    //Game Audio
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    //initializing Components for the script
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); 
        playerAnim = GetComponent<Animator>(); 
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>(); 
    }

    //player Jump and player animation
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        playerAnim.SetTrigger("Jump_trig"); 
    }

    //Code for when the player collides with another tag
    private void OnCollisionEnter(Collision collision)
    {
        //Ground Check For Jumping and particle affect
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        //Collsiion script causing Game Over to occur

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //the game over sequence and particles for it
            explosionParticle.Play();
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
