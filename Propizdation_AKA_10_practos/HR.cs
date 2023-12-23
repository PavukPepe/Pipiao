using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Propizdation_AKA_10_practos.Menu;

namespace Propizdation_AKA_10_practos
{
    internal class HR : ICRUD
    {
        List<Employeer> employeers = Reader.Read<List<Employeer>>("employeers.json");
        List<User> users = Reader.Read<List<User>>("users.json");
        public void Action()
        {
            Console.Clear();
            Console.WriteLine($"Вы вошли как {Posts.HR.ToString()}");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            int pos = 2;
            
            foreach (Employeer employeer in employeers)
            {
                List<User> sort = new List<User>();
                foreach (User user in users)
                {
                    if (user.id == employeer.user_id)
                    {
                        sort.Add(user);
                    }
                }
                Console.SetCursorPosition(4, pos);
                Console.Write(employeer.id);
                Console.SetCursorPosition(Console.WindowWidth / 5, pos);
                Console.Write(employeer.name);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 2, pos);
                Console.Write(employeer.midlename);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 3, pos);
                try
                {
                    Console.Write(Enum.GetValues(typeof(Posts)).GetValue(sort[0].post_id));
                }
                catch
                {
                    Console.WriteLine("НЕТ");
                }
                pos++;
            }
            int pol = Menu.Show(2, pos - 3);

            if (pol == (int)klavishi.F1)
            {
                Create();
            }
            else if (pol == (int)klavishi.F2)
            {
                Search();
            }
            else if (pol == (int)klavishi.Escape)
            {
                Authorization.Author();
            }
            else
            {
                try
                {
                    Read(pol);
                }
                catch
                {
                    Action();
                }
            }

        }
        public void Search()
        {
            Console.Clear();
            Console.WriteLine($"Вы вошли как {Posts.Admin.ToString()}");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            Console.WriteLine($"  ID: ");
            Console.WriteLine($"  Имя:              ");
            Console.WriteLine($"  Фамилия:          ");
            Console.WriteLine($"  Отчество:         ");
            Console.WriteLine($"  Дата рождения:    ");
            Console.WriteLine($"  Пасспорт(сер/ном):");
            Console.WriteLine($"  Зарплата:         ");
            Console.WriteLine($"  ID польователя:   ");
            int p = Menu.Show(2, 7);


            string name = "";
            string surname = "";
            string midlename = "";
            string str_birthday = "";
            string str_id = "";
            string str_passport = "";
            string str_salary = "";
            string str_user_id = "";

            int id = -1; DateTime birthday = DateTime.Now;
            int passport = -1;
            double salary = -1;
            int user_id = -1;

            List<Employeer> sort = new List<Employeer>();

            switch (p)
            {
                case 0:
                    str_id = Addition(p, str_id);
                    if (int.TryParse(str_id, out id))
                    {
                        Console.SetCursorPosition(p, 10);
                        Console.WriteLine("                  ");
                        foreach (Employeer employeer in employeers)
                        {
                            if (id == employeer.id)
                                sort.Add(employeer);
                        }
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(p, 10);
                        Console.WriteLine("Неверный ввод!");
                    }
                    break;

                case 1:
                    name = Addition(p, name);
                    foreach (Employeer employeer in employeers)
                    {
                        if (name == employeer.name)
                            sort.Add(employeer);
                    }
                    break;
                case 2:
                    surname = Addition(p, surname);
                    foreach (Employeer employeer in employeers)
                    {
                        if (surname == employeer.surname)
                            sort.Add(employeer);
                    }
                    break;
                case 3:
                    midlename = Addition(p, midlename);
                    foreach (Employeer employeer in employeers)
                    {
                        if (midlename == employeer.midlename)
                            sort.Add(employeer);
                    }
                    break;
                case 4:
                    str_birthday = Addition(p, str_birthday);
                    try
                    {
                        string[] date = str_birthday.Split(".");
                        var date1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0);
                        birthday = date1;
                        foreach (Employeer employeer in employeers)
                        {
                            if (birthday == employeer.birthday)
                                sort.Add(employeer);
                        }
                        Console.SetCursorPosition(p, 10);
                        Console.WriteLine("                  ");
                    }
                    catch
                    {
                        str_birthday = "";
                        Console.SetCursorPosition(p, 10);
                        Console.WriteLine("Неверный ввод!");
                    }
                    break;
                case 5:
                    str_passport = Addition(p, str_passport);
                    if (int.TryParse(str_passport, out passport))
                    {
                        Console.SetCursorPosition(p, 10);
                        Console.WriteLine("                  ");
                        foreach (Employeer employeer in employeers)
                        {
                            if (passport == employeer.passport)
                                sort.Add(employeer);
                        }
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(p, 10);
                        Console.WriteLine("Неверный ввод!");
                    }
                    
                    break;
                case 6:
                    str_salary = Addition(p, str_salary);
                    try
                    {
                        salary = Convert.ToDouble(str_salary);
                        Console.SetCursorPosition(p, 10);
                        foreach (Employeer employeer in employeers)
                        {
                            if (salary == employeer.salary)
                                sort.Add(employeer);
                        }
                        Console.WriteLine("                  ");

                    }
                    catch
                    {
                        Console.SetCursorPosition(p, 10);
                        Console.WriteLine("Неверный ввод!");
                    }
                    break;
                case 7:
                    str_user_id = Addition(p, str_user_id);
                    if (int.TryParse(str_user_id, out user_id))
                    {
                        Console.SetCursorPosition(p, 10);
                        Console.WriteLine("                  ");
                        foreach (Employeer employeer in employeers)
                        {
                            if (user_id == employeer.user_id)
                                sort.Add(employeer);
                        }
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(p, 10);
                        Console.WriteLine("Неверный ввод!");
                    }
                    break;
            }
            do
            {
                int pos = 2;
                int cl = 0;
                foreach( var employeer in sort)
                {

                    List<User> sort_1 = new List<User>();
                    Console.Clear();
                    foreach (User user in users)
                    {
                        if (user.id == employeer.user_id)
                        {
                            sort_1.Add(user);
                        }
                    }
                    Console.SetCursorPosition(4, pos);
                    Console.Write(employeer.id);
                    Console.SetCursorPosition(Console.WindowWidth / 5, pos);
                    Console.Write(employeer.name);
                    Console.SetCursorPosition(Console.WindowWidth / 5 * 2, pos);
                    Console.Write(employeer.midlename);
                    Console.SetCursorPosition(Console.WindowWidth / 5 * 3, pos);
                    try
                    {
                        Console.Write(Enum.GetValues(typeof(Posts)).GetValue(sort_1[0].post_id));
                    }
                    catch
                    {
                        Console.WriteLine("НЕТ");
                    }
                    cl++;
                    pos++;
                }
                p = Menu.Button();
            } while (p != (int)klavishi.Escape);
            Action();

        }

        public void Read(int pol)
        {
            var kek = employeers[pol];
            List<User> sort = new List<User>();
            foreach (User user in users)
            {
                if (user.id == kek.user_id)
                {
                    sort.Add(user);
                }
            }
            Console.Clear();
            Console.WriteLine($"Вы вошли как {Posts.Admin.ToString()}");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            Console.WriteLine($"ID: {kek.id}");
            Console.WriteLine($"Имя: {kek.name}              ");
            Console.WriteLine($"Фамилия: {kek.surname}          ");
            Console.WriteLine($"Отчество: {kek.midlename}         ");
            Console.WriteLine($"Дата рождения: {kek.birthday.ToLongDateString()}    ");
            Console.WriteLine($"Пасспорт(сер/ном): {kek.passport}");
            try
            {
                Console.WriteLine($"Должность: {Enum.GetValues(typeof(Posts)).GetValue(sort[0].post_id)}");
            }
            catch
            {
                Console.WriteLine("Должность: НЕТ");
            }
            Console.WriteLine($"Зарплата: {kek.salary}         ");
            Console.WriteLine($"ID польователя: {kek.user_id}   ");
            int p = Menu.Button();
            if (p == (int)klavishi.Delete)
            {
                Delete(pol);
                Action();
            }
            if (p == (int)klavishi.F10)
            {
                Update(pol);
            }
            if (p == (int)klavishi.Escape)
            {
                Action();
            }
        }

        public void Create()
        {
            int id = -1;
            string name = "";
            string surname = "";
            string midlename = "";
            DateTime birthday = DateTime.Now;
            int passport = -1;
            double salary = -1;
            int user_id = -1;

            string str_birthday = "";
            string str_id = "";
            string str_passport = "";
            string str_salary = "";
            string str_user_id = "";

            Console.Clear();
            Console.WriteLine("Сексшоп у Кузьмича");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            Console.WriteLine("  ID:                ");
            Console.WriteLine("  Имя:               ");
            Console.WriteLine("  Фамилия:           ");
            Console.WriteLine("  Отчество:          ");
            Console.WriteLine("  Дата рождения:     ");
            Console.WriteLine("  Пасспорт(сер/ном): ");
            Console.WriteLine("  Зарплата:          ");
            Console.WriteLine("  ID польователя:    ");
            int p;
            do
            {
                p = Menu.Show(2, 7);
                switch (p)
                {
                    case 0:
                        str_id = Addition(p, str_id);
                        if (int.TryParse(str_id, out id))
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;

                    case 1:
                        name = Addition(p, name);
                        break;
                    case 2:
                        surname = Addition(p, surname);
                        break;
                    case 3:
                        midlename = Addition(p, midlename);
                        break;
                    case 4:
                        str_birthday = Addition(p, str_birthday);
                        try
                        { 
                            string[] date = str_birthday.Split(".");
                            var date1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0);
                            birthday = date1;
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");
                        }
                        catch
                        {
                            str_birthday = "";
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                    case 5:
                        str_passport = Addition(p, str_passport);
                        if (int.TryParse(str_passport, out passport))
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                    case 6:
                        str_salary = Addition(p, str_salary);
                        try
                        {
                            salary = Convert.ToDouble(str_salary);
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");

                        }
                        catch
                        { 
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                    case 7:
                        str_user_id = Addition(p, str_user_id);
                        if (int.TryParse(str_user_id, out user_id))
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                }
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                var user = new Employeer(id, name, surname, midlename, birthday, passport, salary, user_id);
                employeers.Add(user);
                Reader.Write(employeers, "employeers.json");
                Action();
            }
            catch
            {
                Create();
            }

        }
    
        public void Update(int pol)
        {
            int id = employeers[pol].id;
            string name = employeers[pol].name;
            string surname = employeers[pol].surname;
            string midlename = employeers[pol].midlename;
            DateTime birthday = employeers[pol].birthday;
            int passport = employeers[pol].passport;
            double salary = employeers[pol].salary;
            int user_id = employeers[pol].user_id;

            string str_birthday = birthday.ToString();
            string str_id = id.ToString();
            string str_passport = passport.ToString();
            string str_salary = salary.ToString();
            string str_user_id = user_id.ToString();

            Console.Clear();
            Console.WriteLine("Сексшоп у Кузьмича");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            Console.WriteLine($"  ID:               {id}");
            Console.WriteLine($"  Имя:              {name}");
            Console.WriteLine($"  Фамилия:          {surname}");
            Console.WriteLine($"  Отчество:         {midlename}");
            Console.WriteLine($"  Дата рождения:    {birthday.ToLongDateString()}");
            Console.WriteLine($"  Пасспорт(сер/ном):{passport}");
            Console.WriteLine($"  Зарплата:         {salary}");
            Console.WriteLine($"  ID польователя:   {user_id}");
            int p;
            do
            {
                p = Menu.Show(2, 7);
                switch (p)
                {
                    case 0:
                        str_id = Addition(p, str_id);
                        if (int.TryParse(str_id, out id))
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;

                    case 1:
                        name = Addition(p, name);
                        break;
                    case 2:
                        surname = Addition(p, surname);
                        break;
                    case 3:
                        midlename = Addition(p, midlename);
                        break;
                    case 4:
                        str_birthday = Addition(p, str_birthday);
                        try
                        {
                            string[] date = str_birthday.Split(".");
                            var date1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0);
                            birthday = date1;
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");
                        }
                        catch
                        {
                            str_birthday = "";
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                    case 5:
                        str_passport = Addition(p, str_passport);
                        if (int.TryParse(str_passport, out passport))
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                    case 6:
                        str_salary = Addition(p, str_salary);
                        try
                        {
                            salary = Convert.ToDouble(str_salary);
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");

                        }
                        catch
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                    case 7:
                        str_user_id = Addition(p, str_user_id);
                        if (int.TryParse(str_user_id, out user_id))
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 10);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                }
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                employeers.Remove(employeers[pol]);
                var user = new Employeer(id, name, surname, midlename, birthday, passport, salary, user_id);
                employeers.Add(user);
                Reader.Write(employeers, "employeers.json");
                Action();
            }
            catch
            {
                Create();
            }
        }
        public void Delete(int pol)
        {
            employeers.Remove(employeers[pol]);
            Reader.Write(employeers, "employeers.json");
            Action();
        }

        public string Addition(int pol, string v)
        {
            Console.SetCursorPosition(20, pol + 2);
            ConsoleKeyInfo n;
            Console.Write(v);
            do
            {
                n = Console.ReadKey();
                if (n.Key == ConsoleKey.Backspace)
                {
                    if (v.Length == 0)
                    {
                        break;
                    }
                    v = v.Substring(0, v.Length - 1);
                    Console.Write(' ');
                    Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, pol + 2);
                    continue;
                }
                if (n.KeyChar != '\r')
                {
                    v += n.KeyChar.ToString();
                }

            } while (n.Key != ConsoleKey.DownArrow && n.Key != ConsoleKey.UpArrow && n.Key != ConsoleKey.Enter);
            return v;


        }
    }
}
