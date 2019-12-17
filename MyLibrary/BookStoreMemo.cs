using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary
{
    public class BookStoreMemo
    {
        private Books[] _books;

        public BookStoreMemo(Books[] books)
        {
            _books = books;
        }

        public decimal ComputePrice()
        {
            var bookCounts = _books
                .GroupBy(book => book)
                .Select(book => book.Count())
                .OrderBy(num => num)
                .ToArray();
            return PriceFor(bookCounts);
        }

        private static Dictionary<string, decimal> _priceForCounts = new Dictionary<string, decimal>();

        private decimal PriceFor(int[] bookCounts)
        {
            if (bookCounts.Length == 0) return 0m;
            if (bookCounts.Length == 1) return bookCounts[0] * 8m;

            var hash = BookCountsHash(bookCounts);
            if (_priceForCounts.ContainsKey(hash))
                return _priceForCounts[hash];

            List<decimal> prices = new List<decimal>();
            var allCombi = GetCombinaisonFor(bookCounts.Length);
            
            foreach (var combi in allCombi)
            {
                var bookCountsWithoutCombi = RemoveCombi(bookCounts, combi);
                var nbInBundle = combi.Count(b => b);
                prices.Add(BundlePrice(nbInBundle) + PriceFor(bookCountsWithoutCombi));
            }
            var price = prices.Min();
            _priceForCounts[hash] = price;
            return price;            
        }

        private string BookCountsHash(int[] bookCounts)
        {
            return string.Join("|", bookCounts);
        }

        private static Dictionary<int, List<bool[]>> _memoCombiForNbDifferent = new Dictionary<int, List<bool[]>>();

        private List<bool[]> GetCombinaisonFor(int nbDifferent)
        {
            if (_memoCombiForNbDifferent.ContainsKey(nbDifferent))
                return _memoCombiForNbDifferent[nbDifferent];
            var allCombi = new List<bool[]>();
            var building = new bool[nbDifferent];
            building[0] = true;
            FillCombinaison(allCombi, building, nbDifferent - 1);
            _memoCombiForNbDifferent[nbDifferent] = allCombi;
            return allCombi;
        }
        
        private List<bool[]> FillCombinaison(List<bool[]> combi, bool[] building, int nbDifferent)
        {
            var without = (bool[])building.Clone();
            without[without.Length - nbDifferent] = false;
            var with = building;
            with[with.Length - nbDifferent] = true;

            if (nbDifferent == 1)
            {
                combi.Add(without);
                combi.Add(with);
            }
            else
            {
                FillCombinaison(combi, without, nbDifferent - 1);
                FillCombinaison(combi, with, nbDifferent - 1);
            }
            return combi;
        }

        private int[] RemoveCombi(int[] bookCounts, bool[] combi)
        {
            var newBookCounts = (int[])bookCounts.Clone();
            for (var i=0; i<newBookCounts.Length; i++)
            {
                if (combi[i])
                    newBookCounts[i]--;
            }
            newBookCounts = newBookCounts.Where(nb => nb != 0).ToArray();
            return newBookCounts;
        }

        private static readonly decimal[] BUNDLE_PRICES = { 0m, 8m, 15.2m, 21.6m, 25.6m, 30m };
        private decimal BundlePrice(int booksInBundle)
        {            
            return BUNDLE_PRICES[booksInBundle];
        }
    }
}