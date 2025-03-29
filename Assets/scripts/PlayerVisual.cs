using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private const string IS_RUNNING = "IsRuning"; // Пусть совпадает с параметром в Animator

    private Animator _animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Если не задали playerController через инспектор, 
        // попробуем найти его на родительском объекте
        if (playerController == null)
        {
            playerController = GetComponentInParent<PlayerController>();
        }
    }

    private void Update()
    {
        if (playerController != null)
        {
            // Передаём флаг анимации
            _animator.SetBool(IS_RUNNING, playerController.IsRunning);

            // Вызываем поворот (отражение) спрайта
            AdjustPlayerFacingDirection();
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector2 mousePos = playerController._inputReader.GetMousePosition();
        Vector2 playerPos = playerController.GetPlayerScreenPosition();

        // Если мышь левее игрока — отражаем спрайт
        if (mousePos.x < playerPos.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}