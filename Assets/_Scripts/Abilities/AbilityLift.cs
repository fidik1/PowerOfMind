using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLift : Ability
{
    [SerializeField] private float _deltaY;
    [SerializeField] private float _speed = 2f;        // Скорость движения лифта
    [SerializeField] private float _timeToWait = 2f;   // Время ожидания на конечной точке
    [SerializeField] private Transform _objectLift;    // Ссылка на объект лифта

    private float _startYPosition;                     // Стартовая позиция лифта по Y координате
    private bool _isMovingUp;                          // Двигается ли вверх?
    private bool _isUsed;                              // Использовали?
    private bool _isPaused;                            // На паузе?

    private void Start()
    {
        _startYPosition = _objectLift.position.y; // Сохраняем стартовую позицию лифта в поле _startYPosition;
    }

    protected override void Logic() // Переопределяем метод из родительского класса Ability и меняем ему логику под этот класс
    {
        if (!_isUsed) // Если не использован
        {
            _isUsed = true; 
            _isMovingUp = true;
        }
    }

    private void Update()
    {
        if (_isUsed && !_isPaused) // Проверяем если использован и не на паузе
        {
            // Проверяем, двигается ли лифт вверх или вниз
            if (_isMovingUp)
            {
                // Если лифт еще не достиг целевой точки
                if (_objectLift.position.y < _startYPosition + _deltaY)
                {
                    // Поднимаем лифт к целевой точке
                    _objectLift.Translate(Vector3.up * _speed * Time.deltaTime); // Вызываем метод Translate для перемещения _objectLift вверх со скоростью _speed и умножаем на Time.deltaTime для плавности
                }
                else
                {
                    // Достижение целевой точки, ждем некоторое время и вызываем метод через _timeToWait секунд и меняем направление движения лифта
                    _isPaused = true;
                    Invoke(nameof(ChangeDirection), _timeToWait);
                }
            }
            else
            {
                // Если лифт еще не достиг целевой точки
                if (_objectLift.position.y > _startYPosition)
                {
                    // Опускаем лифт обратно к исходной точке
                    _objectLift.Translate(Vector3.down * _speed * Time.deltaTime);
                }
                else
                {
                    // Лифт вернулся в исходное положение, ставим _isUsed = false и лифт готов к использованию ещё раз
                    _isUsed = false;
                }
            }
        }
    }

    private void ChangeDirection() // Метод, который меняет направление движения лифта на обратный
    {
        _isPaused = false;
        _isMovingUp = !_isMovingUp; // _isMovingUp равняется значению наоборот, то есть если был false, то станет true, если был true, то станет false
    }
}
