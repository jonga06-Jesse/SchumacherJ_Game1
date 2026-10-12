using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction MoveAction; // Used to guide the player via input from the keyboard
    public Collision collision; // Used to check for collision with enemies

    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    public GameObject cannon; // Weapon 1 (Input 0) Shoot bullets at enemies
    public GameObject shield; // Weapon 2 (Input 1) Collide with enemies to kill them
    public GameObject sword; // Weapon 3 (Input 2) Invincibility
    public GameObject bullet;
    public Transform shotSpawn; // Bullet spawnpoint

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        MoveAction.Enable();
        cannon.SetActive(false);
        sword.SetActive(false);
        shield.SetActive(false);
    }

    // NOTE: This method is currently broken as it doesn't kill enemies. The cause of the issue is currently unknown.
    void attack() // Attack when colliding
    {
        if (collision.gameObject.tag == "Enemy" && sword.activeInHierarchy == true)
        {
            Destroy(collision.gameObject);
        }
    }

    void shoot() // Fires bullets while cannon is equipped
    {
        GameObject g = Instantiate(bullet, shotSpawn.position, shotSpawn.rotation) as GameObject;
        Destroy(g, 1.5f);
    }

    void Update()
    {
        if (Keyboard.current.digit0Key.wasPressedThisFrame) // Equip cannon, unequip everything else
        {
            cannon.SetActive(true);
            sword.SetActive(false);
            shield.SetActive(false);
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame) // Equip sword, unequip everything else
        {
            cannon.SetActive(false);
            sword.SetActive(true);
            shield.SetActive(false);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame) // Equip shield, unequip everything else
        {
            cannon.SetActive(false);
            sword.SetActive(false);
            shield.SetActive(true);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame) // Unequip everything
        {
            cannon.SetActive(false);
            sword.SetActive(false);
            shield.SetActive(false);
        }

        if (sword.activeInHierarchy) // Call the shoot method
        {
            attack();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && cannon.activeInHierarchy) // Call the shoot method
        {
            shoot();
        }
    }

    void FixedUpdate()
    {
        var pos = MoveAction.ReadValue<Vector2>();

        float horizontal = pos.x;
        float vertical = pos.y;

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Rigidbody.MoveRotation(m_Rotation);
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * walkSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) // Kill player when colliding with an enemy without a sword or shield
    {
        if (collision.gameObject.CompareTag("Enemy") && (shield.activeInHierarchy == false && sword.activeInHierarchy == false))
        {
            // Destroy the current object
            Destroy(gameObject);
        }
    }
}