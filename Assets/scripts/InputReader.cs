using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputSystem_Actions _playerInputActions;

    // Храним вектор движения, чтобы другие скрипты могли его считать
    public Vector2 MoveVector { get; private set; }

    private void Awake()
    {
        // Создаём экземпляр сгенерированного класса
        _playerInputActions = new InputSystem_Actions();

        // Подписываемся на событие изменения вектора Movement
        _playerInputActions.Player.Move.performed += OnMovePerformed;
        _playerInputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnEnable()
    {
        // Включаем карту действий
        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        // Отключаем карту действий
        _playerInputActions.Player.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // context.ReadValue<Vector2>() возвращает текущий вектор WASD
        MoveVector = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Когда пользователь отпустил кнопки, вектор = (0,0)
        MoveVector = Vector2.zero;
    }
    public Vector2 GetMousePosition()
    {
        // Читаем позицию мыши в экранных координатах
        return Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;
    }
}