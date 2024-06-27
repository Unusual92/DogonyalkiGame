using UnityEngine;

public class RotateObject : MonoBehaviour
{
    void Update()
    {
        // Получить текущий угол поворота объекта вокруг оси Y
        float currentAngle = transform.eulerAngles.y;

        // Вычислить новый угол поворота, чтобы объект был всегда под углом 90 градусов к оси X
        float newAngle = -90 + currentAngle;

        // Установить новый угол поворота объекта вокруг оси X
        transform.eulerAngles = new Vector3(newAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}