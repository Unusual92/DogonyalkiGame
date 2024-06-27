using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.labirint
{
    public class WallsGenerate : MonoBehaviour
    {
        // Перечислимые типы, содержащие пустые, наземные и стены
        private enum Grid { empty, floor, wall };
        // Может быть сгенерировано все квадратное пространство
        private Grid[,] grid;
        // размер квадратного пространства
        // Вы также можете использовать открытый тип для отладки
        private int width = 30;
        private int height = 30;

        private struct Indicator
        {
            // Координатная позиция индикатора
            public Vector2 pos;
            // Направление индикатора
            public Vector2 dir;
        }
        // Связанный список типа индикатора, он будет содержать несколько индикаторов
        private List<Indicator> inds;
        // Вероятность изменения направления индикатора
        private float chanceToTurn = 0.5f; // Увеличиваем вероятность изменения направления индикатора
        private float chanceToSpawn = 0.02f; // Уменьшаем вероятность генерации нового индикатора
                                             // Вероятность уничтожения индикатора
        private float chanceToDestroy = 0.08f;
        // Максимальное количество индикаторов, которые существуют одновременно
        private int maxAmount = 99999;

        private float fillRatio = 0.5f; // Увеличиваем степень заполнения

        void Init()
        {
            // Инициализируем сетку
            grid = new Grid[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = Grid.empty;
                }
            }
            // Инициализировать индикатор
            inds = new List<Indicator>();
            Indicator ind = new Indicator();
            // случайным образом выбираем начальное направление
            // RandomDirection () случайным образом сгенерирует переменную Vector2, которая будет предоставлена ​​позже
            ind.dir = RandomDirection();
            // Начальная позиция генерации
            ind.pos = new Vector2(Mathf.RoundToInt(width / 2.0f), Mathf.RoundToInt(height / 2.0f));

            inds.Add(ind);
        }
        void FloorGeneration()
        {
            // Цикл поддерживает выполнение до тех пор, пока не будет достигнута определенная степень покрытия, но while (true) может вызвать непредсказуемые последствия
            int counter = 0;

            while (counter < 123456)
            {
                // Генерируем землю
                foreach (Indicator floor in inds)
                {
                    grid[(int)floor.pos.x, (int)floor.pos.y] = Grid.floor;
                }

                // уничтожить индикатор
                int numbers = inds.Count;
                for (int i = 0; i < numbers; i++)
                {
                    if (Random.value < chanceToDestroy && numbers > 1)
                    {
                        inds.RemoveAt(i);
                        break; // Уничтожить только один индикатор за цикл
                    }
                }

                // Индикатор изменения направления
                for (int i = 0; i < inds.Count; i++)
                {
                    if (Random.value < chanceToTurn)
                    {
                        Indicator tempInd = inds[i];
                        tempInd.dir = RandomDirection();
                        inds[i] = tempInd;
                    }
                }

                // Генерируем новый индикатор
                int num = inds.Count;
                for (int i = 0; i < num; i++)
                {
                    // Примечание: вероятность генерации не должна быть слишком высокой
                    if (Random.value < chanceToSpawn && num < maxAmount)
                    {
                        Indicator newInd = new Indicator();
                        newInd.dir = RandomDirection();
                        newInd.pos = inds[i].pos;
                        inds.Add(newInd);
                    }
                }

                // Индикатор перемещения
                for (int i = 0; i < inds.Count; i++)
                {
                    Indicator tempInd = inds[i];
                    // Направление нормализовано, и есть только четыре направления вверх, вниз, влево и вправо, так что вы можете напрямую добавить направление для перемещения
                    tempInd.pos += tempInd.dir;
                    inds[i] = tempInd;
                }

                // Избегайте перемещения индикатора к краю, потому что для стены нужно зарезервировать хотя бы одно пространство
                for (int i = 0; i < inds.Count; i++)
                {
                    Indicator tempInd = inds[i];
                    // Зажим ограничивает значение между заданным минимальным значением и заданным максимальным значением
                    tempInd.pos.x = Mathf.Clamp(tempInd.pos.x, 1, width - 2);
                    tempInd.pos.y = Mathf.Clamp(tempInd.pos.y, 1, height - 2);
                    inds[i] = tempInd;
                }

                // Проверяем условие окончания цикла, т.е. покрытие
                if (NumberOfFloors() / (float)grid.Length > fillRatio)
                {
                    break;
                }
                counter++;
            }
        }

        private int NumberOfFloors()
        {
            int count = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (grid[i, j] == Grid.floor)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private int CountAdjacentWalls(int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int newX = x + i;
                    int newY = y + j;

                    if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                    {
                        if (grid[newX, newY] == Grid.wall)
                            count++;
                    }
                }
            }
            return count;
        }


        void WallGeneration()
        {
            // Создаем периметр из стен вокруг комнат
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if (grid[i, j] == Grid.floor)
                    {
                        int adjacentWalls = CountAdjacentWalls(i, j);
                        if (adjacentWalls < 3) // Если менее 3-х соседних стен, то оставляем клетку пустой
                        {
                            continue;
                        }

                        if (grid[i - 1, j] == Grid.empty)
                            grid[i - 1, j] = Grid.wall;
                        if (grid[i + 1, j] == Grid.empty)
                            grid[i + 1, j] = Grid.wall;
                        if (grid[i, j - 1] == Grid.empty)
                            grid[i, j - 1] = Grid.wall;
                        if (grid[i, j + 1] == Grid.wall)
                            grid[i, j + 1] = Grid.wall;
                    }
                }
            }

            // Создаем периметр из стен вокруг комнат
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if (grid[i, j] == Grid.floor)
                    {
                        if (grid[i - 1, j] == Grid.empty)
                            grid[i - 1, j] = Grid.wall;
                        if (grid[i + 1, j] == Grid.empty)
                            grid[i + 1, j] = Grid.wall;
                        if (grid[i, j - 1] == Grid.empty)
                            grid[i, j - 1] = Grid.wall;
                        if (grid[i, j + 1] == Grid.wall)
                            grid[i, j + 1] = Grid.wall;
                    }
                }
            }



            // Добавляем внешний периметр
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                    {
                        grid[i, j] = Grid.wall;
                    }
                }
            }
            // Генерация пола внутри периметра
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if (grid[i, j] == Grid.empty)
                    {
                        grid[i, j] = Grid.floor;
                    }
                }
            }

        }

        void RemoveSingle()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (grid[i, j] == Grid.wall)
                    {
                        // Предположим, он окружен полом
                        bool all = true;
                        for (int x = -1; x <= 1; x++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                // пограничный контроль
                                if (i + x < 0 || i + x > width - 1 || j + y < 0 || j + y > height - 1)
                                    continue;

                                // Пропустить центр и четыре угла и проверить только четыре направления
                                if (x != 0 && y != 0 || x == 0 && y == 0)
                                    continue;

                                if (grid[i + x, j + y] != Grid.floor)
                                    all = false;
                            }
                        }
                        if (all)
                        {
                            grid[i, j] = Grid.floor;
                        }
                    }
                }
            }
        }
        Vector2 RandomDirection()
        {
            // Генерируем случайное целое число от 0 до 3
            int choice = Mathf.FloorToInt(Random.value * 3.99f);

            switch (choice)
            {
                case 0:
                    return Vector2.down;
                case 1:
                    return Vector2.up;
                case 2:
                    return Vector2.left;
                default:
                    return Vector2.right;
            }
        }
        public GameObject[] floors;
        public GameObject[] walls;

        void Spawn(float x, float y, GameObject go)
        {
            // Сначала убедитесь, что сборный вид сверху представляет собой квадрат
            // Длина здесь - длина стороны вида сверху
            int length = 1;
            // Рассчитать координаты сгенерированной позиции
            Vector2 spawnPos = new Vector2(x, y) * length;
            // Назначаем ориентацию сборному, чтобы сделать карту более разнообразной
            Quaternion orientation = Quaternion.identity;
            orientation.eulerAngles = ObjOrientation();

            GameObject toSpawn = Instantiate(go, new Vector3(spawnPos.x, 0, spawnPos.y), orientation) as GameObject;
        }

        // Создать карту
        void SpawnMap()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    switch (grid[x, y])
                    {
                        case Grid.empty:
                            break;
                        case Grid.floor:
                            Spawn(x, y, floors[Random.Range(0, floors.Length)]);
                            break;
                        case Grid.wall:
                            Spawn(x, y, walls[Random.Range(0, walls.Length)]);
                            break;
                    }
                }
            }
        }

        // случайным образом выбираем направление
        Vector3 ObjOrientation()
        {
            int choice = Mathf.FloorToInt(Random.value * 3.99f);

            switch (choice)
            {
                case 0:
                    return new Vector3(0, 0, 0);
                case 1:
                    return new Vector3(0, 90, 0);
                case 2:
                    return new Vector3(0, -90, 0);
                default:
                    return new Vector3(0, 180, 0);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
            FloorGeneration();
            WallGeneration();
            RemoveSingle();
            SpawnMap();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}