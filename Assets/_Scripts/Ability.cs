using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool OnCooldown { get; private set; } // булевая переменная, которая отвечает на вопрос "На Перезарядке?"
    [field: SerializeField] public float CooldownTime { get; private set; } = 2; // время перезарядки
    protected Transform _playerTransform; // поле, которое будет хранить в себе ссылку на игрока (на его Transform)

    public void Use(Transform transform) // метод, который использует абилку
    {
        if (!OnCooldown) // проверка не на кд ли абилка
        {
            _playerTransform = transform;    // присваиваем из параметра ссылку на игрока
            Logic();                         // вызываем метод, который отвечает за логику абилки
            OnCooldown = true;               // на кулдауне = тру)
            StartCoroutine(StartCooldown()); // запускаем корутину для того чтобы просчитать время перезарядки
        }
    }

    protected virtual void Logic() // определяем метод, чтобы потом можно было его поменять у наследников логику (полиморфизм называется)
    {
        
    }

    private IEnumerator StartCooldown() // корутина)
    {
        yield return new WaitForSecondsRealtime(CooldownTime); // ждём реальное время CooldownTime секунд (сейчас это 2, но можно поменять в инспекторе)
        OnCooldown = false; // ставм, что уже не на кулдауне (эта строка выполнится только после того как пройдёт CooldownTime секунд)
    }
}
