using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_v2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w grze kółko i krzyżyk!");
            //deklaracja zmiennych zawierających rozmiary planszy
            List<string> pola = new List<string>();
            int width = 0;
            int height = 0;
            string obecnyRuch = "X";
            try
            {
                //przypisanie wartości do tych zmiennych
                Console.WriteLine("Podaj szerokość planszy(max 30):");
                width = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj wysokość planszy(max 30):");
                height = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                //restart programu w przypadku napotkania nieprawidłowych wartości
                Console.WriteLine("Podano niewłaściwą wartość!\n");
                Main(args);
            }
            //error prompt
            if (width > 30 || height > 30)
            {
                Console.WriteLine("Wielkość planszy wykracza poza dozwolony rozmiar!\n");
                Main(args);
            }
            int numerPola = 1;
            //ponumeruj pola od 1 do n
            for (int j = 0; j < width*height; j++)
            {
                pola.Add(numerPola.ToString());
                numerPola++;
            }
            CreateBoard(pola, width, height);
            //zamień liczby na liście na puste stringi
            for(int i = 0; i < pola.Count; i++)
            {
                pola[i] = " ";
            }  
            Console.WriteLine("Obecny ruch należy do: " + obecnyRuch);
            MakeAMove(obecnyRuch, pola, width, height, args, 0);
        }
        public static void PopulateBoard(List<string> pola, int wybranePole, int width, int height, string obecnyRuch)
        {
            int indexPola = 0;
            //pętla tworząca nowe wiersze
            for (int i = 0; i < height; i++)
            {
                Console.Write("|");
                //pętla tworząca nowe komórki w wierszu
                for (int j = 0; j < width; j++)
                {
                    if (wybranePole == indexPola)
                        pola[indexPola] = obecnyRuch;
                    Console.Write($" {pola[indexPola]} |");
                    indexPola++;
                }
                Console.WriteLine();
                if (i != height - 1)
                {
                    Console.Write(" ");
                    //pętla tworząca linię oddzielającą wiersze
                    for (int k = 0; k < width; k++)
                    {
                        if (k == width - 1)
                            Console.Write("---");
                        else
                            Console.Write("---+");
                    }
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Tworzy planszę
        /// </summary>
        /// <param name="pola">Wartości pól</param>
        /// <param name="width">Szerokość planszy</param>
        /// <param name="height">Wysokość planszy</param>
        private static void CreateBoard(List<string> pola, int width, int height)
        {
            int indexPola = 0;
            //pętla tworząca nowe wiersze
            for (int i = 0; i < height; i++)
            {
                Console.Write("|");
                //pętla tworząca nowe komórki w wierszu
                for (int j = 0; j < width; j++)
                {
                    //stwórz pole uwzględniając ilość cyfr z jakich składa się liczba
                    if (Math.Floor(Math.Log10(Convert.ToInt32(pola[indexPola])) + 1) == 1)
                        Console.Write($" {pola[indexPola]} |");
                    else if (Math.Floor(Math.Log10(Convert.ToInt32(pola[indexPola])) + 1) == 2)
                        Console.Write($" {pola[indexPola]}|");
                    else if (Math.Floor(Math.Log10(Convert.ToInt32(pola[indexPola])) + 1) == 3)
                        Console.Write($"{pola[indexPola]}|");
                    indexPola++;
                }
                Console.WriteLine();
                if (i != height - 1)
                {
                    Console.Write(" ");
                    //pętla tworząca linię oddzielającą wiersze
                    for (int k = 0; k < width; k++)
                    {
                        if (k == width - 1)
                            Console.Write("---");
                        else
                            Console.Write("---+");
                    }
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Odczytuje numer pola wpisanego przez użytkownika i wpisuje do niego wartość X lub O
        /// </summary>
        /// <param name="obecnyRuch">Gracz do którego należy następny ruch</param>
        /// <param name="pola">Lista zawierająca pola wraz z ich wartościami</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void MakeAMove(string obecnyRuch, List<string> pola, int width, int height, string[] args, int moveCounter)
        {
            if(moveCounter == pola.Count)
            {
                Console.WriteLine("Remis!\n");
                Console.ReadKey();
                Main(args);
            }
            int wybranePole = -1;
            try
            {
                //zapisz wybrane przez użytkownika pole
                Console.WriteLine("Podaj pole:");
                wybranePole = Convert.ToInt32(Console.ReadLine()) - 1;
                if(wybranePole > pola.Count - 1)
                {
                    //w przypadku podania przez użytkownika błędnych danych powtórz pytanie
                    Console.WriteLine("Wybrane pole wykracza poza dostępne pola!\n");
                    MakeAMove(obecnyRuch, pola, width, height, args, moveCounter);
                }
                if(pola[wybranePole] == "X" || pola[wybranePole] == "O")
                {
                    Console.WriteLine("\nTo pole jest już zajęte!");
                    MakeAMove(obecnyRuch, pola, width, height, args, moveCounter);
                }
            }
            catch
            {
                MakeAMove(obecnyRuch, pola, width, height, args, moveCounter);
            }
            pola[wybranePole] = obecnyRuch;
            //wyświetl planszę uwzględniając zaktualizowaną pozycję
            PopulateBoard(pola, wybranePole, width, height, obecnyRuch);
            if (obecnyRuch == "X")
                obecnyRuch = "O";
            else
                obecnyRuch = "X";
            //sprawdź czy jeden z graczy wygrał
            bool krzyzykWygral = ChceckIfGameIsWon(width, height, pola, "X");
            bool kolkoWygralo = ChceckIfGameIsWon(width, height, pola, "O");
            if(krzyzykWygral == true)
            {
                Console.WriteLine("Krzyżyk wygrywa!\n");
                Console.ReadKey();
                Main(args);
            }
            else if(kolkoWygralo == true)
            {
                Console.WriteLine("Kółko wygrywa!\n");
                Main(args);
            }
            //jeżeli nikt nie wygrał, kontynuuj
            Console.WriteLine("Obecny ruch należy do: " + obecnyRuch);
            MakeAMove(obecnyRuch, pola, width, height, args, moveCounter+1);
        }
        /// <summary>
        /// Sprawdza czy został spełniony warunek zwycięstwa
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="pola"></param>
        /// <param name="gracz"></param>
        /// <returns></returns>
        public static bool ChceckIfGameIsWon(int width, int height, List<string> pola, string gracz)
        {
            List<string> wierszDoSprawdzenia = new List<string>();
            List<string> kolumnaDoSprawdzenia = new List<string>();
            //iteruj po wszystkich wierszach
            for(int wiersz = 0; wiersz < height; wiersz++)
            {
                //dodaj poszczególne komórki do listy zaierającej wiersz numer n
                for (int kolumna = 0; kolumna < width; kolumna++)
                {
                    wierszDoSprawdzenia.Add(pola[kolumna + (width * wiersz)]);
                }
                //jeżeli wiersz spełnia warunki wygranej, wyślij stosowną informacje do Main()
                if(SprawdzWiersz(wierszDoSprawdzenia, width, height, pola, gracz) == true)
                    return true;
                //wyczyść listę z wierszem
                wierszDoSprawdzenia.Clear();
            }
            //iteruj po wszystkich kolumnach
            for(int kolumna = 0; kolumna < width; kolumna++)
            {
                //dodaj poszczególne komórki do listy zawierającej kolumnę numer n
                for(int wiersz = 0; wiersz < height; wiersz++)
                {
                    kolumnaDoSprawdzenia.Add(pola[wiersz * width + kolumna]);
                }
                //jeżeli kolumna spełnia warunki wygranej, wyślij stosowną informacje do Main()
                if (SprawdzKolumne(kolumnaDoSprawdzenia, width, height, pola, gracz) == true)
                    return true;
                //wyczyść listę z kolumną
                kolumnaDoSprawdzenia.Clear();
            }
            if(width == height)
            {
                if (SprawdzPierwszaPrzekatna(width, height, pola, gracz) == true)
                    return true;
                if (SprawdzDrugaPrzekatna(width, height, pola, gracz) == true)
                    return true;
            }
            return false;
        }

        private static bool SprawdzDrugaPrzekatna(int width, int height, List<string> pola, string gracz)
        {
            bool wygrana = false;
            List<string> przekatna = new List<string>();
            for (int wiersz = 0; wiersz < width; wiersz++)
            {
                przekatna.Add(pola[(width - 1) * (wiersz + 1)]);
            }
            for (int komorka = 0; komorka < przekatna.Count; komorka++)
            {
                if (przekatna[komorka] != gracz)
                {
                    wygrana = false;
                    return wygrana;
                }
                if (komorka == przekatna.Count - 1)
                {
                    wygrana = true;
                    return wygrana;
                }
            }
            return false;
        }

        private static bool SprawdzPierwszaPrzekatna(int width, int height, List<string> pola, string gracz)
        {
            bool wygrana = false;
            List<string> przekatna = new List<string>();
            for (int wiersz = 0; wiersz < width; wiersz++)
            {
                przekatna.Add(pola[width * wiersz + wiersz]);
            }
            for (int komorka = 0; komorka < przekatna.Count; komorka++)
            {
                if (przekatna[komorka] != gracz)
                {
                    wygrana = false;
                    return wygrana;
                }
                if (komorka == przekatna.Count - 1)
                {
                    wygrana = true;
                    return wygrana;
                }
            }
            return false;
        }

        /// <summary>
        /// Sprawdza czy w danej kolumnie zaszedł warunek zwycięstwa
        /// </summary>
        /// <param name="kolumna"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="pola"></param>
        /// <param name="gracz"></param>
        /// <returns></returns>
        private static bool SprawdzKolumne(List<string> kolumna, int width, int height, List<string> pola, string gracz)
        {
            bool wygrana = false;
            for(int komorka = 0; komorka < kolumna.Count; komorka++)
            {
                if(kolumna[komorka] != gracz)
                {
                    wygrana = false;
                    return wygrana;
                }
                wygrana = true;
                if(komorka == height - 1)
                {
                    wygrana = true;
                    return wygrana;
                }
            }
            return wygrana;
        }
        /// <summary>
        /// Sprawdza czy w danym wierszu zaszedł warunek zwycięstwa
        /// </summary>
        /// <param name="wiersz"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="pola"></param>
        /// <param name="gracz"></param>
        /// <returns></returns>
        private static bool SprawdzWiersz(List<string> wiersz, int width, int height, List<string> pola, string gracz)
        {
            bool wygrana = false;
            for (int komorka = 0; komorka < wiersz.Count; komorka++)
            {
                if (wiersz[komorka] != gracz)
                {
                    wygrana = false;
                    return wygrana;
                }
                wygrana = true;
                if (komorka == width - 1)
                {
                    wygrana = true;
                    return wygrana;
                }
            }
            return wygrana;
        }
    }
}
