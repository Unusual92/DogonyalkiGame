using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonuses : MonoBehaviour
{
    public GameObject[] bonusesToSpawn; // Массив объектов бонусов для генерации
    public int numberOfBonusesToSpawn; // Количество бонусов для генерации каждый раз
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SpawnBonusesCoroutine());
    }

    IEnumerator SpawnBonusesCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < numberOfBonusesToSpawn; i++)
            {
                // Выбираем случайный бонус из массива
                GameObject bonusToSpawn = bonusesToSpawn[Random.Range(0, bonusesToSpawn.Length)];

                // Генерируем случайные координаты x, y и z
                float x = Random.Range(0, 30);
                float y = 0.5f;
                float z = Random.Range(0, 30);

                // Проверяем, не находится ли точка в стенах
                while (Physics.CheckSphere(new Vector3(x, y, z), 0.1f))
                {
                    x = Random.Range(0, 30);
                    z = Random.Range(0, 30);
                }

                // Создаем новый объект бонуса в указанной точке
                Instantiate(bonusToSpawn, new Vector3(x, y, z), Quaternion.identity);
            }

            yield return new WaitForSeconds(8); // Ожидаем 8 секунд перед генерацией новых бонусов
        }
    }
}