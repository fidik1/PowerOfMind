using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool OnCooldown { get; private set; } // ������� ����������, ������� �������� �� ������ "�� �����������?"
    [field: SerializeField] public float CooldownTime { get; private set; } = 2; // ����� �����������
    protected Transform _playerTransform; // ����, ������� ����� ������� � ���� ������ �� ������ (�� ��� Transform)

    public void Use(Transform transform) // �����, ������� ���������� ������
    {
        if (!OnCooldown) // �������� �� �� �� �� ������
        {
            _playerTransform = transform;    // ����������� �� ��������� ������ �� ������
            Logic();                         // �������� �����, ������� �������� �� ������ ������
            OnCooldown = true;               // �� �������� = ���)
            StartCoroutine(StartCooldown()); // ��������� �������� ��� ���� ����� ���������� ����� �����������
        }
    }

    protected virtual void Logic() // ���������� �����, ����� ����� ����� ���� ��� �������� � ����������� ������ (����������� ����������)
    {
        
    }

    private IEnumerator StartCooldown() // ��������)
    {
        yield return new WaitForSecondsRealtime(CooldownTime); // ��� �������� ����� CooldownTime ������ (������ ��� 2, �� ����� �������� � ����������)
        OnCooldown = false; // �����, ��� ��� �� �� �������� (��� ������ ���������� ������ ����� ���� ��� ������ CooldownTime ������)
    }
}
