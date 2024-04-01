[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/5ZerStQK)

**Назва роботи: Використання механізму віртуальних методів
Мета роботи: Ознайомитися з особливостями використання віртуальних методів.** 

**Теоретичні відомості**
Мова С# включає таку властивість, як поліморфізм – можливість для об’єктів різних класів, зв’язаних з допомогою наслідування, реагувати різними способами при виклику однієї функції-елементу. До найважливіших форм поліморфізму можна віднести:
перевантаження методів та операцій;
віртуальні методи (функції);
Перевантаження методів та операцій називаються статичним поліморфізмом, тому що він підтримується на етапі компіляції (тобто до запуску програми). Віртуальні методи відносяться до динамічного поліморфізму, тому що він реалізується при виконанні програм.
При використанні віртуального методу запит здійснюється з допомогою вказівника базового класу (або посилання), тобто середовище виконання С# вибирає правильно перевизначений метод у відповідному похідному класі, який зв’язаний з цим об’єктом.
Іноді метод визначається в базовому класі як віртуальний, але просто перевизначений в похідному класі. Якщо такий метод викликається через вказівник похідного класу, то використовується версія похідного класу. Це не поліморфна поведінка.
Завдяки використанню поліморфних методів та поліморфізму виклик функції-елемента може привести до різних дій, які залежать від типу об’єкту, який викликається.
Завдання
Написати програму, описавши батьківський та похідний класи і використавши віртуальні методи. За основу візьміть попередню лабораторну і додайте віртуальні методи. Використайте динамічне створення об’єктів та показчики на екземпляр класу. 
Продемонструвати роботу віртуальних методів на практичному прикладі коли наперед невідомо який об’єкт буде створено (невідомо на етапі компіляції):
  

      //Вибрати режим роботи-запитати в користувача, змінна userChoose
        if (userChoose=='1'){
            //Працюємо з одним об'єктом
            //Створюємо, ініціалізуємо ітд
    
        }
            else{
            //Працюємо з іншим об'єктом
            //Створюємо, ініціалізуємо ітд
        }
           //Виклик віртуальної функції через вказівник/показчик на базовий клас

Змініть віртуальні методи на звичайні та перевірте роботу програми. Проаналізуйте зміни.


Приклад програми:


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR05
{
    struct Coords
    {
        public double x;
        public double y;
        public double z;
        private static readonly Random random = new Random();

        public double GetDistance(Coords coord)
        {
            return Math.Sqrt(Math.Pow(coord.x - x, 2) + Math.Pow(coord.y - y, 2) + Math.Pow(coord.z - z, 2));
        }

        public Coords(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public void SetCoords()
        {
            string xStr, yStr, zStr;
            bool isFirst = true;
            do
            {
                if (!isFirst)
                {
                    Console.WriteLine("Incorrect coords, try again");
                }

                Console.Write("Enter x: ");
                xStr = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Enter y: ");
                yStr = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Enter z: ");
                zStr = Console.ReadLine();
                Console.WriteLine("\n");

                isFirst = false;
            } while (!double.TryParse(xStr, out x) &
                     !double.TryParse(yStr, out y) &
                     !double.TryParse(zStr, out z));
        }
        public void GenerateCoord(double min, double max)
        {
            x = random.NextDouble() * (max - min) + min;
            y = random.NextDouble() * (max - min) + min;
            z = random.NextDouble() * (max - min) + min;
        }

        public override string ToString()
        {
            return $"({x.ToString("0.00"),5};{y.ToString("0.00"),5};{z.ToString("0.00"),5})";
        }
    }
    class Triangle
    {
        protected int counterOfVertices = 3;
        protected Coords[] vertices;
        public Triangle()
        {
            vertices = new Coords[counterOfVertices];

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].SetCoords();
            }
        }

        public Triangle(double from, double to)
        {
            vertices = new Coords[counterOfVertices];
            GeneratePoints(from, to);
        }

        public void GeneratePoints(double from, double to)
        {
            for (int i = 0; i < vertices.Length; i++)
                vertices[i].GenerateCoord(from, to);
        }
        protected double GetAreaByCoords(Coords coord1, Coords coord2, Coords coord3)
        {
            if (vertices.Length == counterOfVertices)
            {
                double a = coord1.GetDistance(coord2);
                double b = coord2.GetDistance(coord3);
                double c = coord3.GetDistance(coord1);

                double p = (a + b + c) / 2;

                return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            }

            return 0;
        }
        public virtual double GetArea()
        {
            return GetAreaByCoords(vertices[0], vertices[1], vertices[2]);
        }

        public override string ToString()
        {
            return $"A: {vertices[0]}, B: {vertices[1]}, C: {vertices[2]}";
        }
    }

    class Tetraeder : Triangle
    {
        private Coords S;

        public Tetraeder() : base()
        {
            S.SetCoords();
        }
        public Tetraeder(double from, double to) : base(from, to)
        {
            S.GenerateCoord(from, to);
        }
        public override double GetArea()
        {
            double res = base.GetArea() +
            GetAreaByCoords(vertices[0], vertices[1], S) +
            GetAreaByCoords(vertices[1], vertices[2], S) +
            GetAreaByCoords(vertices[2], vertices[0], S);

            return res;
        }
        public double GetValue()
        {
            if (counterOfVertices == 3)
            {
                Coords[] vectors = new Coords[counterOfVertices];

                for (int i = 0; i < counterOfVertices; i++)
                {
                    vectors[i].x = vertices[i].x - S.x;
                    vectors[i].y = vertices[i].y - S.y;
                    vectors[i].z = vertices[i].z - S.z;
                }

                return (vectors[0].x * vectors[1].y * vectors[2].z +
                       vectors[0].y * vectors[1].z * vectors[2].x +
                       vectors[0].z * vectors[1].y * vectors[2].x -
                       vectors[0].z * vectors[1].y * vectors[2].x -
                       vectors[0].y * vectors[1].x * vectors[2].z -
                       vectors[0].x * vectors[1].z * vectors[2].y) / 6;
            }

            return 0;
        }

        public override string ToString()
        {
            return $"S: {S}, { base.ToString() }";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int code;
            do
            {
                Console.WriteLine("1. Triangle");
                Console.WriteLine("2. Tetraeder");
                Console.Write("Enter mode: ");                
            } while (!int.TryParse(Console.ReadLine(), out code) || code < 1 || code > 2);

            Triangle pObj = null;

            if (code == 1)
            {
                Triangle triangle = new Triangle();
                pObj = triangle;
            } 
            else
            {
                Tetraeder tetraeder = new Tetraeder();
                pObj = tetraeder;
            }

            Console.WriteLine(pObj.GetArea());
        }
    }
}


Варіанти завдань 
Згідно попередньої роботи.

**Хід виконання**
1.	Розробити алгоритми створення класів згідно варіантів завдань.
2.	Написати відповідну програму на мові програмування С#.
3.	Закомітити код у репозиторій 
4.		Проаналізувати роботу програми з віртуальними методами та звичайними. Тобто потрібно дослідити механізм динамічного поліморфізму. Зверніть увагу на приклад коду, зокрема на механізм створення об'єкту одного з класів. На етапі компіляції програми невідомо який вибір зробить користувач під час виконання програми. 
5.	Порівняти результати виконання програми з віртуальними методами та без віртуальних методів.
6.	В README файл додати скріншоти роботи програми з віртуальними методами і без віртуальних. Описати різницю. 
