using MyLibrary;
using static MyLibrary.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        interface IGenerateStrategy
        {
            void Generate(List<Books> otherBooks, int nbBooks);
            void Reset();
        }

        class AllPermutationGenerator : IGenerateStrategy
        {
            public void Generate(List<Books> otherBooks, int nbBooks)
            {
                CheckWith(Book1, otherBooks, nbBooks - 1);
                CheckWith(Book2, otherBooks, nbBooks - 1);
                CheckWith(Book3, otherBooks, nbBooks - 1);
                CheckWith(Book4, otherBooks, nbBooks - 1);
                CheckWith(Book5, otherBooks, nbBooks - 1);
            }

            public void Reset() { }
        }

        class RandomStrategy : IGenerateStrategy
        {
            private const int ROLLS_PER_STEP = 2;
            private readonly static Random _random = new Random();
            private readonly static Array _values = Enum.GetValues(typeof(Books));

            public void Generate(List<Books> otherBooks, int nbBooks)
            {
                for (var i = 0; i < ROLLS_PER_STEP; i++)
                {
                    if (rolls > 20) return;
                    var book = (Books)_values.GetValue(_random.Next(_values.Length));
                    CheckWith(book, otherBooks, nbBooks - 1);
                    rolls++;                    
                }
            }

            private int rolls = 0;
            public void Reset() {
                rolls = 0;
            }
        }

        static IGenerateStrategy _strategy = new RandomStrategy();

        static void Main(string[] args)
        {
            for (var nbBooks = 1; nbBooks < 20; nbBooks++)
            {
                Console.WriteLine($"--------------- {nbBooks} -----------------");
                var books = new List<Books>();
                _strategy.Generate(books, nbBooks);
                _strategy.Reset();
            }
            Console.WriteLine("DONE");
            Console.ReadLine();
        }

        private static void CheckWith(Books book, List<Books> otherBooks, int nbBooks)
        {
            var clonedBooks = otherBooks.ConvertAll(b => b);
            clonedBooks.Add(book);
            if (nbBooks == 0)
            {
                var cart = clonedBooks.ToArray();
                var combinatorial = new BookStoreCombinatorial(cart).ComputePrice();
                var linear = new BookStore(cart).ComputePrice();
                Console.WriteLine($"{combinatorial - linear}\t|\t{combinatorial}\t-\t{linear}");
                Debug.Assert(combinatorial == linear);
            } 
            else
            {
                _strategy.Generate(clonedBooks, nbBooks);
            }
        }
    }
}
