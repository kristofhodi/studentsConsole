using System.Globalization;
using studentConsole;

var lines = File.ReadAllLines("tanulok.csv");

var students = new List<Student>();

foreach (var line in lines.Skip(1))
{
    var data = line.Split(',');
    var student = new Student
    {
        Id=int.Parse(data[0]),
        Nev=data[1],
        Nem=data[2],
        Magassag_cm=int.Parse(data[3]),
        Suly_kg=float.Parse(data[4].Replace(".", ",")),
        Osztaly=data[5]
    };
    students.Add(student);
}

string choice;
do
{
    Console.WriteLine("0 - Lista, 1 - Create, 2 - Edit,  3 - Delete, 4 - Riport, 9 - Kilépés");
    choice =  Console.ReadLine();

    switch (choice)
    {
        case "0":
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Nev}, {student.Magassag_cm}, {student.Suly_kg}, {student.Osztaly}");
            }
            break;

        case "1":
            Console.WriteLine("Adja meg az új tanuló nevét: ");
            var nev = Console.ReadLine();

            Console.WriteLine("Adja meg a magasságát: ");
            int.TryParse(Console.ReadLine(), out int magassag_cm);

            Console.WriteLine("Adja meg a testsúlyát: ");
            float.TryParse(Console.ReadLine(), out float suly_kg);

            Console.WriteLine("Adja meg az osztályát: ");
            var osztaly = Console.ReadLine();

            students.Add(new Student
            {
                Id = students.Max(s => s.Id) + 1,
                Nev = nev,
                Magassag_cm = magassag_cm,
                Suly_kg = suly_kg,
                Osztaly = osztaly
            });
            break;
        
        case "2":
            Console.WriteLine("Adja meg a tanuló Id-ját, akinek az osztályát szeretné változtatni: ");
            if (int.TryParse(Console.ReadLine(), out int editId))
            {
                var student = students.FirstOrDefault(s => s.Id == editId);
                if (student != null)
                {
                    Console.WriteLine("Adja meg az új osztályát: ");
                    student.Osztaly = Console.ReadLine();
                } else Console.WriteLine("Nincs ilyen tanuló!");
            }
            break;
        
        case "3":
            Console.WriteLine("Adja meg a tanuló Id-ját, aki szeretne törölni: ");
            if (int.TryParse(Console.ReadLine(), out int deleteId))
            {
                var student = students.FirstOrDefault(s => s.Id == deleteId);
                if (student != null) students.Remove(student);
                else Console.WriteLine("Nincs ilyen tanuló!");
            }
            break;
        
        case "4":
            var tallestGirlCm = students.Where(s => s.Nem == "Nő").Max(s => s.Magassag_cm);
            foreach (var student in students.Where(s => s.Nem == "Nő"))
            {
                if (student.Magassag_cm == tallestGirlCm)
                {
                    Console.WriteLine("A legmagasabb lány: " +  student.Nev);
                }
            }
            var biggestBoiKg = students.Where(s => s.Nem == "Férfi").Max(s => s.Suly_kg);
            foreach (var student in students.Where(s => s.Nem == "Férfi"))
            {
                if (student.Suly_kg == biggestBoiKg)
                {
                    Console.WriteLine("A súlyosabb fiú: " +  student.Nev);
                }
            }

            break;
    }
} while (choice != "9");