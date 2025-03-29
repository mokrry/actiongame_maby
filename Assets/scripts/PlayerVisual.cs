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
            RotatePlayer();
        }
    }

    private void RotatePlayer()
    {
        if (playerController.IsWatchingLeft)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}