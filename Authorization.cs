using Propizdation_AKA_10_practos;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;
using static Propizdation_AKA_10_practos.Authorization;

namespace Propizdation_AKA_10_practos
{
    internal class Authorization
    {
        public static void Main(string[] args)
        {
            Author();
        }

        public static void Author() 
        {
            string login = "", password = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Сексшоп у Кузьмича");
                for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
                Console.WriteLine($"  Логин: {login}");
                Console.Write("  Пароль: ");
                for (int i = 0; i < password.Length; i++) { Console.Write("*"); }
                Console.WriteLine();
                Console.WriteLine("  Авторизоваться");
                int pos = (Menu.Show(2, 2));

                if (pos == (int)position.login)
                {
                    Console.SetCursorPosition(9, pos + 2);
                    ConsoleKeyInfo n;
                    Console.Write(login);
                    do
                    {
                        n = Console.ReadKey();
                        if (n.Key == ConsoleKey.Backspace)
                        {
                            if (login.Length == 0)
                            {
                                break;
                            }
                            login = login.Substring(0, login.Length - 1);
                            Console.Write(' ');
                            Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, pos + 2);
                            continue;
                        }
                        if (n.KeyChar != '\r' && n.KeyChar != '\0')
                        {
                            login += n.KeyChar.ToString();
                        }

                    } while (n.Key != ConsoleKey.DownArrow && n.Key != ConsoleKey.UpArrow && n.Key != ConsoleKey.Enter);
                }
                if (pos == (int)position.password)
                {
                    Console.SetCursorPosition(10, pos + 2);
                    ConsoleKeyInfo n;
                    for (int i = 0; i < password.Length; i++) { Console.Write("*"); }
                    do
                    {
                        n = Console.ReadKey(true);
                        if (n.Key == ConsoleKey.Backspace)
                        {
                            if (password.Length == 0)
                            {
                                break;
                            }
                            password = password.Substring(0, password.Length - 1);
                            Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, pos + 2);
                            Console.Write(' ');
                            Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, pos + 2);
                            continue;
                        }
                        if (n.KeyChar != '\r' && n.KeyChar != '\0')
                        {
                            Console.Write("*");
                            password += n.KeyChar.ToString();
                        }

                    } while (n.Key != ConsoleKey.DownArrow && n.Key != ConsoleKey.UpArrow && n.Key != ConsoleKey.Enter);
                }
                if (pos == (int)position.auth)
                {
                    List<User> users = Reader.Read<List<User>>("users.json");
                    foreach (User user in users)
                    {
                        if (user.password == password && user.login == login && user.post_id == (int)Posts.Admin)
                        {
                            Admin admin = new Admin();
                            MakeMenu(admin);
                            break;
                        }
                        else if (user.password == password && user.login == login && user.post_id == (int)Posts.HR)
                        {
                            HR hr = new HR();
                            MakeMenu(hr);
                        }
                        else if (user.password == password && user.login == login && user.post_id == (int)Posts.Storager)
                        {
                            Storager storager = new Storager();
                            MakeMenu(storager);
                        }
                        else if (user.password == password && user.login == login && user.post_id == (int)Posts.Kassir)
                        {
                            Kassir kassir = new Kassir();
                            MakeMenu(kassir);
                        }
                        else if (user.password == password && user.login == login && user.post_id == (int)Posts.Buh)
                        {
                            Buh buh = new Buh();
                            MakeMenu(buh);
                        }
                    }
                }

            }
        }

        static void MakeMenu(ICRUD cRUD)
        {
            Console.Clear();
            Console.WriteLine($"Вы вошли как {Posts.Admin.ToString()}"); 
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            cRUD.Action();
        }

        internal enum position
        {
            login,
            password,
            auth
        }

    }
}