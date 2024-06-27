using UnityEngine;

public class RotateObject : MonoBehaviour
{
    void Update()
    {
        // �������� ������� ���� �������� ������� ������ ��� Y
        float currentAngle = transform.eulerAngles.y;

        // ��������� ����� ���� ��������, ����� ������ ��� ������ ��� ����� 90 �������� � ��� X
        float newAngle = -90 + currentAngle;

        // ���������� ����� ���� �������� ������� ������ ��� X
        transform.eulerAngles = new Vector3(newAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}