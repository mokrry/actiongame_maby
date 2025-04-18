using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rb;
    public InputReader _inputReader;
    private bool _isRunning;
    private bool _isWatchingLeft;

    // Свойство для чтения статуса бега
    public bool IsRunning => _isRunning;
    public bool IsWatchingLeft => _isWatchingLeft;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        // Пытаемся найти объект со скриптом InputReader в сцене (обязательно убедитесь, что он есть!)
        _inputReader = FindObjectOfType<InputReader>();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        // Получаем вектор движения из InputReader
        Vector2 movement = _inputReader != null ? _inputReader.MoveVector : Vector2.zero;

        // Применяем движение к физическому телу (Rigidbody2D)
        Vector2 newPosition = _rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);

        // Проверяем, движется ли игрок (если вектор не (0,0))
        if (movement.sqrMagnitude > 0.001f)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }
    public Vector2 GetPlayerScreenPosition()
    {
        // Проверяем, что основная камера существует, чтобы избежать ошибок.
        if (Camera.main != null)
        {
            return Camera.main.WorldToScreenPoint(transform.position);
        }
        return Vector2.zero;
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector2 mousePos = _inputReader.GetMousePosition();
        Vector2 playerPos = GetPlayerScreenPosition();

        // Если мышь левее игрока — отражаем спрайт
        if (mousePos.x < playerPos.x)
        {
            _isWatchingLeft = true;
        }
        else
        {
            _isWatchingLeft = false;
        }
    }
}