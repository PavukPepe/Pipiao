using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Propizdation_AKA_10_practos.Menu;

namespace Propizdation_AKA_10_practos
{
    internal class Kassir : ICRUD
    {
        List<Buh_notes> buh_notes = Reader.Read<List<Buh_notes>>("Buh_notes.json");
        List<Product_kass> products = Reader.Read<List<Product_kass>>("products.json");

        

        public void Action()
        {
            Welcome();
            int pos = 2;
            foreach (Product_kass prod in products)
            {
                Product_info(prod, true, true, pos);
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
        {}

        public static void Welcome()
        {
            Console.Clear();
            Console.WriteLine($"Вы вошли как {Posts.Storager.ToString()}");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
        }

        public void Read(int pol)
        {
            var kek = products[pol];
            int p;
            do
            {
                int pos = 2;
                foreach (Product_kass prod in products)
                {
                    Product_info(prod, true, true, pos);
                    pos++;
                }
                p = Menu.Button();
                if (p == (int)klavishi.Plus && kek.kolvo_kass < kek.kolvo)
                {
                    kek.kolvo_kass += 1;
                }
                if (p == (int)klavishi.Minus && kek.kolvo_kass > 0)
                {
                    kek.kolvo_kass -= 1;
                }
            } while (p != (int)klavishi.S);
            if (p == (int)klavishi.S)
            {
                List<Product> prods = new List<Product>();
                foreach(Product_kass prod in products)
                {
                    prods.Add(new Product(prod.id, prod.name, prod.price, prod.kolvo - prod.kolvo_kass));
                    if (prod.kolvo - prod.kolvo_kass < prod.kolvo)
                    {
                        int rnd = new Random().Next(1, 1000);
                        buh_notes.Add(new Buh_notes(rnd, $"{prod.name} x {prod.kolvo_kass}", prod.price * prod.kolvo_kass));
                    }
                }
                Reader.Write(buh_notes, "buh_notes.json");
                Reader.Write(prods, "products.json");
                Action();
            }
        }
        public void Create()
        {}
        public void Update(int pol)
        {}
        public void Delete(int pol)
        {}

        static object Addition(int pol, string v)
        {
            Console.SetCursorPosition(15, pol + 2);
            ConsoleKeyInfo n;
            Console.Write(v);
            do
            {
                n = Console.ReadKey();
                if (n.Key == ConsoleKey.Backspace)
                {
                    if (v.Length == 0)
                        break;
                    v = v.Substring(0, v.Length - 1);
                    Console.Write(' ');
                    Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, pol + 2);
                    continue;
                }
                if (n.KeyChar != '\r' && n.KeyChar != '\0')
                {
                    v += n.KeyChar.ToString();
                }
            } while (n.Key != ConsoleKey.DownArrow && n.Key != ConsoleKey.UpArrow && n.Key != ConsoleKey.Enter);
            return v;
        }
        public void Product_info(Product_kass kek, bool flag, bool table = false, int pos = 0)
        {
            if (table == false)
            {
                Console.WriteLine($"  ID:          {(kek.id > -1 ? kek.id : "")}");
                Console.WriteLine($"  Наименование:{kek.name}");
                Console.WriteLine($"  Цену:        {(kek.price > -1 ? kek.price : "")}");
                Console.WriteLine($"  Количество:  {(kek.kolvo > -1 ? kek.kolvo : "")}");
            }
            else
            {
                Console.SetCursorPosition(4, pos);
                Console.Write(kek.id);
                Console.SetCursorPosition(Console.WindowWidth / 5, pos);
                Console.Write(kek.name);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 2, pos);
                Console.Write(kek.price);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 3, pos);
                Console.Write($"{kek.kolvo}/{kek.kolvo_kass}"); 
            }
        }
    }
}
