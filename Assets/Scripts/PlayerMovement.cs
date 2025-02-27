using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool turnLeft, turnRight;
    public float speed = 7.0f;
    private CharacterController myCharacterController;
    public float mouseMovementSpeed = 5.0f;
    public float movementRange = 5f;

    // Jump variables - VALEURS AMÉLIORÉES
    public float jumpHeight = 3.0f;               // Augmenté considérablement pour des sauts plus hauts
    public float gravity = -18.0f;                // Gravité plus forte pour un meilleur contrôle
    private Vector3 verticalVelocity = Vector3.zero;
    private bool isJumping = false;
    private Vector3 jumpMomentum = Vector3.zero;  // Nouvel élan horizontal pendant le saut
    public float jumpForwardBoost = 1.5f;         // Boost de vitesse pendant le saut

    private bool isGrounded = true;
    public bool canMove = true;
    private Animator animator;

    // Add a small delay after rotation before allowing jumps
    private float rotationCooldown = 0f;
    private float rotationCooldownDuration = 0.1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        myCharacterController = GetComponent<CharacterController>();
        
        // Désactive root motion pour que l'animation ne contrôle pas le mouvement
        if (animator != null)
        {
            animator.applyRootMotion = false;
        }
    }

    void Update()
    {
        if (canMove)
        {
            // Update rotation cooldown
            if (rotationCooldown > 0)
            {
                rotationCooldown -= Time.deltaTime;
            }

            // Check ground state - use a raycast for more reliable ground detection
            isGrounded = CheckGrounded();

            // Handle rotation
            turnLeft = Input.GetKeyDown(KeyCode.A);
            turnRight = Input.GetKeyDown(KeyCode.D);

            if (turnLeft)
            {
                transform.Rotate(new Vector3(0f, -90f, 0f));
                rotationCooldown = rotationCooldownDuration;
            }
            else if (turnRight)
            {
                transform.Rotate(new Vector3(0f, 90f, 0f));
                rotationCooldown = rotationCooldownDuration;
            }

            // Apply gravity and handle jumping
            if (isGrounded)
            {
                // Reset vertical velocity when grounded
                verticalVelocity.y = -0.5f; // Small negative value to keep grounded
                
                // Réinitialiser l'élan horizontal quand on touche le sol
                jumpMomentum = Vector3.zero;

                if (isJumping)
                {
                    isJumping = false;
                    // Reset jump animation if needed
                }

                // Check for jump input (only if not in rotation cooldown)
                if (Input.GetKeyDown(KeyCode.Space) && rotationCooldown <= 0)
                {
                    // Formula for jump velocity: v = sqrt(-2 * gravity * jumpHeight)
                    verticalVelocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
                    
                    // Ajouter un élan horizontal supplémentaire pour franchir les obstacles
                    jumpMomentum = transform.forward * speed * jumpForwardBoost;
                    
                    isJumping = true;
                    if (animator != null)
                    {
                        animator.SetTrigger("Jump");
                    }
                }
            }
            else
            {
                // Apply gravity - Gravité différente pour montée et descente
                if (verticalVelocity.y > 0)
                {
                    // Pendant la phase ascendante du saut
                    verticalVelocity.y += gravity * 0.8f * Time.deltaTime; // Gravité réduite pour monter plus haut
                }
                else
                {
                    // Pendant la phase descendante du saut
                    verticalVelocity.y += gravity * 1.2f * Time.deltaTime; // Gravité plus forte pour retomber plus vite
                }
            }

            // Apply vertical movement (gravity/jump)
            myCharacterController.Move(verticalVelocity * Time.deltaTime);

            // Handle forward movement - MODIFIÉ POUR UTILISER L'ÉLAN DU SAUT
            Vector3 forwardMovement;
            if (isJumping && !isGrounded && jumpMomentum.magnitude > 0)
            {
                // Utiliser l'élan du saut pendant qu'on est en l'air
                forwardMovement = jumpMomentum * Time.deltaTime;
            }
            else
            {
                // Mouvement normal
                forwardMovement = transform.forward * speed * Time.deltaTime;
            }
            myCharacterController.Move(forwardMovement);

            // Handle mouse-based side-to-side movement
            float mouseX = Input.mousePosition.x;
            float normalizedX = (mouseX / Screen.width) * 2 - 1; // Convert to -1 to 1
            float targetSideMovement = normalizedX * movementRange;

            // Calculate movement perpendicular to forward direction
            Vector3 rightDirection = transform.right;
            Vector3 targetSidePosition = transform.position + rightDirection * targetSideMovement;

            // Apply side movement
            Vector3 currentPos = transform.position;
            Vector3 newPosition = Vector3.Lerp(currentPos, targetSidePosition, Time.deltaTime * mouseMovementSpeed);

            // Calculate only the side movement component
            Vector3 sideMovementOnly = newPosition - currentPos;
            Vector3 projectedOnRight = Vector3.Project(sideMovementOnly, rightDirection);

            // Apply only the side movement
            myCharacterController.Move(projectedOnRight);
        }
    }

    // More reliable ground check using a raycast
    private bool CheckGrounded()
    {
        // First check the character controller's built-in ground detection
        if (myCharacterController.isGrounded)
        {
            return true;
        }

        // Add a raycast for additional ground detection - DISTANCE AUGMENTÉE
        float rayLength = 0.2f;  // Augmenté pour une meilleure détection
        Vector3 rayStart = transform.position + myCharacterController.center;

        // Adjust the ray start position to be at the bottom of the character controller
        rayStart.y -= myCharacterController.height / 2;

        // Create a small offset to cast rays from the edges as well
        float radius = myCharacterController.radius * 0.9f;

        // Cast rays from center and around the edges
        if (Physics.Raycast(rayStart, Vector3.down, rayLength))
            return true;

        if (Physics.Raycast(rayStart + (transform.forward * radius), Vector3.down, rayLength))
            return true;

        if (Physics.Raycast(rayStart + (-transform.forward * radius), Vector3.down, rayLength))
            return true;

        if (Physics.Raycast(rayStart + (transform.right * radius), Vector3.down, rayLength))
            return true;

        if (Physics.Raycast(rayStart + (-transform.right * radius), Vector3.down, rayLength))
            return true;

        return false;
    }
}