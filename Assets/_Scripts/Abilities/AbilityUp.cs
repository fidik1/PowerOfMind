using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUp : Ability
{
    [SerializeField] private float _forceVertical = 15; // насколько сильно будет толкать вверх игрока
    [SerializeField] private float _forceHorizontal = 3; // насколько сильно будет толкать вбок игрока 
    [SerializeField] private GameObject _trail; // префаб трейла(след от игрока)
    [SerializeField] private Vector3 _trailOffset; // отступ трейла, чтобы его поставить по центру игрока

    private float _verticalVelocity; // текущая сила вертикального толчка
    private float _horizontalVelocity; // текущая сила горизонтального толчка

    private GameObject _trailObject; // поле, которое будет хранить ссылку на созданный объект трейла
    private bool _hasTrail; // есть ли трейл?

    protected override void Logic() // переопределяем метод из родительского класса Ability и меняем ему логику под этот класс
    {
        // квадратный корень из H * -2 * G = сколько скорости необходимо для достижения желаемой высоты
        _verticalVelocity = Mathf.Sqrt(_forceVertical * -2f * -9.81f); // в общем математические приколы, чтобы просчитать силу вертикального толчка
        _horizontalVelocity =  _forceHorizontal; // тут просто присваиваем переменной стандартную горизонтальную силу, чтобы потом просчитывать её в методе Update
        
        _trailObject = Instantiate(_trail, _playerTransform); // создаём из префаба трейл и присваиваем его в поле _trailObject
        _trailObject.transform.localPosition += _trailOffset; // добавляем отступ 
        _hasTrail = true; // имеет трейл = тру
    }

    private void Update() // вызывается каждый кадр
    {
        if (_verticalVelocity > 0) // проверяем не закончилась ли сила вертикального толчка
        {
            _playerTransform.GetComponent<CharacterController>().Move(new Vector3(_horizontalVelocity, _verticalVelocity, 0) * Time.deltaTime); // используем метод Move у CharacterController на игроке, чтобы
                                                                                                                                                // передать ему наши переменные со скоростями (_horizontalVelocity, _verticalVelocity)
                                                                                                                                                // и умножаем на Time.deltaTime для плавности
            _verticalVelocity += -9.81f * Time.deltaTime * 2; // так как из физики знаем, что сила притяжения равна -9.81f мы просто отнимаем от текущей вертикальной скорости -9.81 умноженную на плавность и на 2, чтобы просчитывалось в два раза быстрее
            _horizontalVelocity -= Time.deltaTime * 2; // тут такой же прикол, только уже без силы притяжения, потому что это горизонтальная скорость, так тоже норм
        }
        else // если закончилась сила
        {
            if (_hasTrail) // есть ли трейл
            {
                _trailObject.transform.SetParent(transform); // меняем ему родителя на сцене, чтобы он больше не следовал за игроком
                Invoke(nameof(DestroyTrail), 2); // удаляем объект трейла со сцены через 2 секунды
                _hasTrail = false; // имеет трейл = фолс
            }
        }
    }

    private void DestroyTrail() 
    {
        Destroy(_trailObject); // удаляем объект трейла
    }
}
