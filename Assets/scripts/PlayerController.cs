using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    [Header("이동 및 점프 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("바닥 체크 설정")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    private void Awake()
    {
        // Rigidbody2D 컴포넌트 연결
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Update()
    {
        // 좌우 이동 처리 (= 사용)
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 바닥 감지 (OverlapCircle 사용)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    // Input System: Move 액션이 발생할 때 호출
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }

    // Input System: Jump 액션이 발생할 때 호출
    public void OnJump(InputValue value)
    {
        // 버튼을 눌렀을 때(isPressed) 그리고 바닥일 때만 점프
        if (value.isPressed && isGrounded)
        {
            // 점프 전 수직 속도를 0으로 초기화해서 일정한 점프 높이 유지
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}