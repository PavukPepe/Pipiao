using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Propizdation_AKA_10_practos.Menu;

namespace Propizdation_AKA_10_practos
{
    internal class Admin : ICRUD
    {
        List<User> users = Reader.Read<List<User>>("users.json");

        public void Action()
        {
            Console.Clear();
            Console.WriteLine($"Вы вошли как {Posts.Admin.ToString()}");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            int pos = 2;
            foreach (User user in users)
            {
                
                Console.SetCursorPosition(4, pos);
                Console.Write(user.id);
                Console.SetCursorPosition(Console.WindowWidth / 5, pos);
                Console.Write(user.login);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 2, pos);
                Console.Write(user.password);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 3, pos);
                Console.Write(Enum.GetValues(typeof(Posts)).GetValue(user.post_id));
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
            Console.WriteLine("  ID: ");
            Console.WriteLine("  Login: ");
            Console.WriteLine("  Password: ");
            Console.WriteLine("  Post_id: ");
            int p = Menu.Show(2, 3);
            string login = "";
            string password = "";
            string id_str = "";
            string post_id_str = "";
            int id = -1;
            int post_id = -1;
            List<User> sort = new List<User>();
            switch (p)
            {
                case 0:
                    id_str = Addition(p, id_str);
                    if (int.TryParse(id_str, out id))
                    {
                        Console.SetCursorPosition(p, 6);
                        Console.WriteLine("                  ");
                        foreach (User user in users)
                        {
                            if (user.id == id)
                            {
                                sort.Add(user);
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(p, 6);
                        Console.WriteLine("Неверный ввод!");
                    }
                    break;

                case 1:
                    login = Addition(p, login);
                    foreach (User user in users)
                    {
                        if (user.login == login)
                        {
                            sort.Add(user);
                        }
                    }
                    break;
                case 2:
                    password = Addition(p, password);
                    foreach (User user in users)
                    {
                        if (user.password == password)
                        {
                            sort.Add(user);
                        }
                    }
                    break;
                case 3:
                    post_id_str = Addition(p, post_id_str);
                    if (int.TryParse(post_id_str, out post_id))
                    {
                        Console.SetCursorPosition(p, 6);
                        Console.WriteLine("                  ");
                        foreach (User user in users)
                        {
                            if (user.post_id == post_id)
                            {
                                sort.Add(user);
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(p, 6);
                        Console.WriteLine("Неверный ввод!");
                    }
                    break;
            }
            do
            {
                
                Console.Clear();
                Console.WriteLine($"Вы вошли как {Posts.Admin.ToString()}");
                for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
                int pos = 2;
                foreach (User user in sort)
                {
                    Console.SetCursorPosition(4, pos);
                    Console.Write(user.id);
                    Console.SetCursorPosition(Console.WindowWidth / 5, pos);
                    Console.Write(user.login);
                    Console.SetCursorPosition(Console.WindowWidth / 5 * 2, pos);
                    Console.Write(user.password);
                    Console.SetCursorPosition(Console.WindowWidth / 5 * 3, pos);
                    Console.Write(Enum.GetValues(typeof(Posts)).GetValue(user.post_id));
                    pos++;
                }
                p = Menu.Button();
            }while (p!=(int)klavishi.Escape);
            Action();

        }
         
        public void Read(int pol)
        {
            var kek = users[pol];
            Console.Clear();
            Console.WriteLine($"Вы вошли как {Posts.Admin.ToString()}");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            Console.WriteLine($"ID: {kek.id}");
            Console.WriteLine($"Login: {kek.login}");
            Console.WriteLine($"Password: {kek.password}");
            Console.WriteLine($"Post-id: {kek.post_id} {Enum.GetValues(typeof(Posts)).GetValue(kek.post_id)}");
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
            string login = "";
            string password = "";
            string id_str = "";
            string post_id_str = "";
            int id = -1;
            int post_id = -1;
            Console.Clear();
            Console.WriteLine("Сексшоп у Кузьмича");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            Console.WriteLine("  ID: ");
            Console.WriteLine("  Login: ");
            Console.WriteLine("  Password: ");
            Console.WriteLine("  Post_id: ");
            int p;
            do
            {
                p = Menu.Show(2, 3);
                switch (p)
                {
                    case 0:
                        id_str = Addition(p, id_str);
                        if (int.TryParse(id_str, out id))
                        {
                            Console.SetCursorPosition(p, 6);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 6);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                        
                    case 1:
                        login = Addition(p, login);
                        break;
                    case 2:
                        password = Addition(p, password);
                        break;
                    case 3:
                        post_id_str = Addition(p, post_id_str);
                        if (int.TryParse(post_id_str, out post_id))
                        {
                            Console.SetCursorPosition(p, 6);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 6);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                }
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            if (p == (int)klavishi.S && id != -1 && post_id != -1) 
            {
                var user = new User(id, login, password, post_id);
                users.Add(user);
                Reader.Write(users, "users.json");
                Action();
            }
            else 
            {
                Action(); 
            }
        }
        public void Update(int pol)
        {
            string login = users[pol].login;
            string password = users[pol].password;
            int id = users[pol].id;
            int post_id = users[pol].post_id;
            string id_str = id.ToString();
            string post_id_str = post_id.ToString();
            

            Console.Clear();
            Console.WriteLine("Сексшоп у Кузьмича");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            Console.WriteLine($"  ID:       {id_str}");
            Console.WriteLine($"  Login:    {login}");
            Console.WriteLine($"  Password: {password}");
            Console.WriteLine($"  Post_id:  {post_id_str}");
            int p;
            do
            {
                p = Menu.Show(2, 3);
                switch (p)
                {
                    case 0:
                        id_str = Addition(p, id_str);
                        if (int.TryParse(id_str, out id))
                        {
                            Console.SetCursorPosition(p, 6);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 6);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;

                    case 1:
                        login = Addition(p, login);
                        break;
                    case 2:
                        password = Addition(p, password);
                        break;
                    case 3:
                        post_id_str = Addition(p, post_id_str);
                        if (int.TryParse(post_id_str, out post_id))
                        {
                            Console.SetCursorPosition(p, 6);
                            Console.WriteLine("                  ");
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(p, 6);
                            Console.WriteLine("Неверный ввод!");
                        }
                        break;
                }
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            if (p == (int)klavishi.S && id != -1 && post_id != -1)
            {
                users.Remove(users[pol]);
                var user = new User(id, login, password, post_id);
                users.Add(user);
                Reader.Write(users, "users.json");
                Action();
            }
            else
            {
                Action();
            }
        }
        public void Delete(int pol)
        {
            users.Remove(users[pol]);
            Reader.Write(users, "users.json");
            Action();
        }

        static string Addition(int pol, string v)
        {
            Console.SetCursorPosition(12, pol + 2);
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
