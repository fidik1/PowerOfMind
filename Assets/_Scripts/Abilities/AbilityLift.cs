using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLift : Ability
{
    [SerializeField] private float _deltaY;
    [SerializeField] private float _speed = 2f;        // �������� �������� �����
    [SerializeField] private float _timeToWait = 2f;   // ����� �������� �� �������� �����
    [SerializeField] private Transform _objectLift;    // ������ �� ������ �����

    private float _startYPosition;                     // ��������� ������� ����� �� Y ����������
    private bool _isMovingUp;                          // ��������� �� �����?
    private bool _isUsed;                              // ������������?
    private bool _isPaused;                            // �� �����?

    private void Start()
    {
        _startYPosition = _objectLift.position.y; // ��������� ��������� ������� ����� � ���� _startYPosition;
    }

    protected override void Logic() // �������������� ����� �� ������������� ������ Ability � ������ ��� ������ ��� ���� �����
    {
        if (!_isUsed) // ���� �� �����������
        {
            _isUsed = true; 
            _isMovingUp = true;
        }
    }

    private void Update()
    {
        if (_isUsed && !_isPaused) // ��������� ���� ����������� � �� �� �����
        {
            // ���������, ��������� �� ���� ����� ��� ����
            if (_isMovingUp)
            {
                // ���� ���� ��� �� ������ ������� �����
                if (_objectLift.position.y < _startYPosition + _deltaY)
                {
                    // ��������� ���� � ������� �����
                    _objectLift.Translate(Vector3.up * _speed * Time.deltaTime); // �������� ����� Translate ��� ����������� _objectLift ����� �� ��������� _speed � �������� �� Time.deltaTime ��� ���������
                }
                else
                {
                    // ���������� ������� �����, ���� ��������� ����� � �������� ����� ����� _timeToWait ������ � ������ ����������� �������� �����
                    _isPaused = true;
                    Invoke(nameof(ChangeDirection), _timeToWait);
                }
            }
            else
            {
                // ���� ���� ��� �� ������ ������� �����
                if (_objectLift.position.y > _startYPosition)
                {
                    // �������� ���� ������� � �������� �����
                    _objectLift.Translate(Vector3.down * _speed * Time.deltaTime);
                }
                else
                {
                    // ���� �������� � �������� ���������, ������ _isUsed = false � ���� ����� � ������������� ��� ���
                    _isUsed = false;
                }
            }
        }
    }

    private void ChangeDirection() // �����, ������� ������ ����������� �������� ����� �� ��������
    {
        _isPaused = false;
        _isMovingUp = !_isMovingUp; // _isMovingUp ��������� �������� ��������, �� ���� ���� ��� false, �� ������ true, ���� ��� true, �� ������ false
    }
}
