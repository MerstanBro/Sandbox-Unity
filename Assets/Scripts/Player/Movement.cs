using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public SpriteRenderer spriteRenderer;

    public Sprite[] walkingNorth;
    public Sprite[] walkingSouth;
    public Sprite[] walkingEast;
    public Sprite[] walkingWest;

    public Sprite[] idleNorth;
    public Sprite[] idleSouth;
    public Sprite[] idleEast;
    public Sprite[] idleWest;

    private Vector2 input;
    private Vector2 lastDirection;
    private float frameTimer;
    private int currentFrame;

    public float frameRate = 0.1f; // 10 FPS
    private bool isMoving;

    void Update()
    {
        // Movement Input
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Only allow 4-directional input (no diagonal)
        if (Mathf.Abs(input.x) > 0)
            input.y = 0;

        isMoving = input != Vector2.zero;

        if (isMoving)
            lastDirection = input;

        // Move the character
        transform.position += (Vector3)(input.normalized * moveSpeed * Time.deltaTime);

        // Animate
        Animate();
    }

    void Animate()
    {
        Sprite[] currentAnimation = null;

        // Determine direction
        if (lastDirection.y > 0)       currentAnimation = isMoving ? walkingNorth : idleNorth;
        else if (lastDirection.y < 0)  currentAnimation = isMoving ? walkingSouth : idleSouth;
        else if (lastDirection.x > 0)  currentAnimation = isMoving ? walkingEast  : idleEast;
        else if (lastDirection.x < 0)  currentAnimation = isMoving ? walkingWest  : idleWest;

        if (currentAnimation == null || currentAnimation.Length == 0)
            return;

        // Frame cycling
        frameTimer += Time.deltaTime;
        if (frameTimer >= frameRate)
        {
            frameTimer = 0f;
            currentFrame = (currentFrame + 1) % currentAnimation.Length;
            spriteRenderer.sprite = currentAnimation[currentFrame];
        }
    }
}
