using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUp : Ability
{
    [SerializeField] private float _forceVertical = 15; // ��������� ������ ����� ������� ����� ������
    [SerializeField] private float _forceHorizontal = 3; // ��������� ������ ����� ������� ���� ������ 
    [SerializeField] private GameObject _trail; // ������ ������(���� �� ������)
    [SerializeField] private Vector3 _trailOffset; // ������ ������, ����� ��� ��������� �� ������ ������

    private float _verticalVelocity; // ������� ���� ������������� ������
    private float _horizontalVelocity; // ������� ���� ��������������� ������

    private GameObject _trailObject; // ����, ������� ����� ������� ������ �� ��������� ������ ������
    private bool _hasTrail; // ���� �� �����?

    protected override void Logic() // �������������� ����� �� ������������� ������ Ability � ������ ��� ������ ��� ���� �����
    {
        // ���������� ������ �� H * -2 * G = ������� �������� ���������� ��� ���������� �������� ������
        _verticalVelocity = Mathf.Sqrt(_forceVertical * -2f * -9.81f); // � ����� �������������� �������, ����� ���������� ���� ������������� ������
        _horizontalVelocity =  _forceHorizontal; // ��� ������ ����������� ���������� ����������� �������������� ����, ����� ����� ������������ � � ������ Update
        
        _trailObject = Instantiate(_trail, _playerTransform); // ������ �� ������� ����� � ����������� ��� � ���� _trailObject
        _trailObject.transform.localPosition += _trailOffset; // ��������� ������ 
        _hasTrail = true; // ����� ����� = ���
    }

    private void Update() // ���������� ������ ����
    {
        if (_verticalVelocity > 0) // ��������� �� ����������� �� ���� ������������� ������
        {
            _playerTransform.GetComponent<CharacterController>().Move(new Vector3(_horizontalVelocity, _verticalVelocity, 0) * Time.deltaTime); // ���������� ����� Move � CharacterController �� ������, �����
                                                                                                                                                // �������� ��� ���� ���������� �� ���������� (_horizontalVelocity, _verticalVelocity)
                                                                                                                                                // � �������� �� Time.deltaTime ��� ���������
            _verticalVelocity += -9.81f * Time.deltaTime * 2; // ��� ��� �� ������ �����, ��� ���� ���������� ����� -9.81f �� ������ �������� �� ������� ������������ �������� -9.81 ���������� �� ��������� � �� 2, ����� �������������� � ��� ���� �������
            _horizontalVelocity -= Time.deltaTime * 2; // ��� ����� �� ������, ������ ��� ��� ���� ����������, ������ ��� ��� �������������� ��������, ��� ���� ����
        }
        else // ���� ����������� ����
        {
            if (_hasTrail) // ���� �� �����
            {
                _trailObject.transform.SetParent(transform); // ������ ��� �������� �� �����, ����� �� ������ �� �������� �� �������
                Invoke(nameof(DestroyTrail), 2); // ������� ������ ������ �� ����� ����� 2 �������
                _hasTrail = false; // ����� ����� = ����
            }
        }
    }

    private void DestroyTrail() 
    {
        Destroy(_trailObject); // ������� ������ ������
    }
}
