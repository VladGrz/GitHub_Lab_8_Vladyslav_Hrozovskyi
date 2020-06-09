using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

namespace Lab_8_zavd_1
{
    public class Playlist : IComparable
    {
        private string _name;
        private int _numOfSongs;
        private DateTime _dateOfAdding;
        private string _typeOfMusic;
        private int _rating;
        public Playlist()
        {
            _name = "Not mentioned";
            _numOfSongs = 0;
            _dateOfAdding = DateTime.Now;
            _typeOfMusic = "Not mentioned";
            _rating = 0;
        }
        public Playlist(string name, int num, DateTime date, string type, int rate)
        {
            _name = name;
            _numOfSongs = num;
            _dateOfAdding = date;
            _typeOfMusic = type;
            _rating = rate;
        }
        public int CompareTo(object n)
        {
            Playlist p = (Playlist)n;
            if (this.NumOfSongs < p.NumOfSongs) return 1;
            if (this.NumOfSongs > p.NumOfSongs) return 1;
            return 0;
        }
        public class SortByDate : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Playlist p1 = (Playlist)ob1;
                Playlist p2 = (Playlist)ob2;
                if (p1.DateOfAdding < p2.DateOfAdding) return 1;
                if (p1.DateOfAdding > p2.DateOfAdding) return -1;
                return 0;
            }
        }
        public class SortByRate : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Playlist p1 = (Playlist)ob1;
                Playlist p2 = (Playlist)ob2;
                if (p1.Rating < p2.Rating) return 1;
                if (p1.Rating > p2.Rating) return -1;
                return 0;
            }
        }
        public class SortByNum : IComparer 
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Playlist p1 = (Playlist)ob1;
                Playlist p2 = (Playlist)ob2;
                if (p1.NumOfSongs < p2.NumOfSongs) return 1;
                if (p1.NumOfSongs > p2.NumOfSongs) return -1;
                return 0;
            }
        }
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public int NumOfSongs
        {
            get => _numOfSongs;
            set => _numOfSongs = value;
        }
        public DateTime DateOfAdding
        {
            get => _dateOfAdding;
            set => _dateOfAdding = value;
        }
        public string TypeOfMusic
        {
            get => _typeOfMusic;
            set => _typeOfMusic = value;
        }
        public int Rating
        {
            get => _rating;
            set => _rating = value;
        }
    }
    class Program
    {
        public static List<Playlist> Add(List<Playlist> p)
        {
            Console.Write("Введiть нову назву плейлисту: ");
            string name = Console.ReadLine();
            Console.Write("Введiть нову кiлькiсть пiсень: ");
            int num = int.Parse(Console.ReadLine());
            Console.Write("Введiть нову дату створення: ");
            DateTime data = DateTime.Parse(Console.ReadLine());
            Console.Write("Введiть новий жанр плейлисту: ");
            string typ = Console.ReadLine();
            Console.Write("Введiть новий рейтинг плейлисту: ");
            int rat = int.Parse(Console.ReadLine());
            p.Add(new Playlist(name, num, data, typ, rat));
            return p;
        }
        public static List<Playlist> Edit(List<Playlist> p)
        {
            Console.Write("Вкажiть номер рядка який хочете редагувати: ");
            int rad = int.Parse(Console.ReadLine());
            Console.WriteLine("Вкажiть номер поля яке хочете редагувати. Наприклад, Назва плейлисту(1), Кiлькiсть пiсень(2), Дата створення(3), Жанр музики(4), Рейтинг(5)");
            int pole = int.Parse(Console.ReadLine());
            newSproba:
            try
            {
                switch (pole)
                {
                    case 1:
                        Console.Write("Введiть нову назву плейлисту: ");
                        p[rad - 1].Name = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Введiть нову кiлькiсть пiсень: ");
                        p[rad - 1].NumOfSongs = int.Parse(Console.ReadLine());
                        break;
                    case 3:
                        Console.Write("Введiть нову дату створення: ");
                        p[rad - 1].DateOfAdding = DateTime.Parse(Console.ReadLine());
                        break;
                    case 4:
                        Console.Write("Введiть новий жанр плейлисту: ");
                        p[rad - 1].TypeOfMusic = Console.ReadLine();
                        break;
                    case 5:
                        Console.Write("Введiть новий рейтинг плейлисту: ");
                        p[rad - 1].Rating = int.Parse(Console.ReadLine());
                        break;
                }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Щось пiшло не так, перевiрте правильнiсть введених данних i спробуйте ще раз");
                goto newSproba;
            }
            return p;
        }
        public static List<Playlist> Delete(List<Playlist> p)
        {
            Console.WriteLine("Вкажiть номер рядка який хочете видалити");
            p.RemoveAt(int.Parse(Console.ReadLine())-1);
            return p;
        }
        public static void Show()
        {
            StreamReader file = new StreamReader(@"D:\ООП\Lab_8\Lab_8_zavd_1\Lab_8_zavd_1\TextFile1.txt");
            string show = file.ReadToEnd();
            if (show.Length == 0)
            {
                Console.WriteLine("Упс, файл пустий.");
            }
            else
            {
                Console.WriteLine($"{"Назва плейлисту",-20}{"Кiлькiсть пiсень",20}{"Дата створення",20}{"Жанр музики",20}{"Рейтинг",20}");
                Console.WriteLine(show);
            }
            file.Close();
        }
        public static List<Playlist> Sorting(List<Playlist> p)
        {
        retry:
            Console.WriteLine("Оберiть за чим бажаєте сортувати данi.\nЗа кiлькiстю пiсень: 1;\nЗа датою створення: 2;\nЗа рейтингом: 3.");
            int k = int.Parse(Console.ReadLine());
            Playlist[] temp = p.ToArray();
            List<Playlist> play = new List<Playlist>();
            switch (k)
            {
                case 1:
                    Array.Sort(temp, new Playlist.SortByNum());
                    p = temp.ToList();
                    break;
                case 2:
                    Array.Sort(temp, new Playlist.SortByDate());
                    p = temp.ToList();
                    break;
                case 3:
                    Array.Sort(temp, new Playlist.SortByRate());
                    p = temp.ToList();
                    break;
                default: Console.WriteLine("Упс, такого сортування не iснує, перевiрте правильнiсть вибору та спробуйте ще раз."); goto retry;
            }
            return p;
        }
        public static void UpdateFile(List<Playlist> play)
        {
            StreamWriter file = new StreamWriter(@"D:\ООП\Lab_8\Lab_8_zavd_1\Lab_8_zavd_1\TextFile1.txt");
            int i = 0;
            foreach (Playlist p in play)
            {
                file.Write($"{play[i].Name,-15} {play[i].NumOfSongs,15} {play[i].DateOfAdding.ToShortDateString(),25} {play[i].TypeOfMusic,20} {play[i].Rating,20}");
                file.Write(Environment.NewLine);
                i++;
            }
            file.Close();
        }
        public static List<Playlist> UpdateBasa()
        {
            List<Playlist> p = new List<Playlist>();
            StreamReader file = new StreamReader(@"D:\ООП\Lab_8\Lab_8_zavd_1\Lab_8_zavd_1\TextFile1.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                line = System.Text.RegularExpressions.Regex.Replace(line, @"\s+", " ");
                string[] str = line.Split(' ');
                p.Add(new Playlist(str[0], int.Parse(str[1]), DateTime.Parse(str[2]), str[3], int.Parse(str[4])));
            }
            file.Close();
            return p;
        }
        static void Main(string[] args)
        {
            List<Playlist> playlists = new List<Playlist>();
            playlists = UpdateBasa();
            Console.WriteLine("Меню програми:\nДодавання записiв - a\nРедагування записiв - e\nЗнищення записiв - f\nВиведення iнформацiї з файлу на екран - s\nМеню сортування - m \nВихiд з програми - q");
            char check;
            do
            {
            userCheck:
                Console.Write("\nВведiть команду: ");
                try
                {
                    check = char.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Неправильна команда. Спробуйте ще раз.");
                    goto userCheck;
                }
                switch (check)
                {
                    case 'a':
                        playlists = Add(playlists);
                        UpdateFile(playlists);
                        break;
                    case 'e':
                        playlists = Edit(playlists);
                        UpdateFile(playlists);
                        break;
                    case 'f':
                        playlists = Delete(playlists);
                        UpdateFile(playlists);
                        break;
                    case 's':
                        Show();
                        break;
                    case 'm':
                        playlists = Sorting(playlists);
                        UpdateFile(playlists);
                        Show();
                        break;
                    case 'q':
                        break;
                    default: Console.WriteLine("Такої команди не iснує, спробуйте ще раз."); break;
                }
            } while (check != 'q');
        }
    }
}
