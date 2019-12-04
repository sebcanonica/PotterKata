using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary
{
    public class BookStoreCombinatorial
    {

        private List<Books> _books;

        public BookStoreCombinatorial(params Books[] books)
        {
            _books = books.ToList();
        }

        public decimal ComputePrice()
        {
            var e = GenerateGroupings(new Ensemble(), _books);
            return e.ComputePrice();
        }       

        private Ensemble GenerateGroupings(Ensemble eIn, List<Books> books)
        {
            if (books.Count == 0) return eIn;
            var e = new Ensemble();
            var book = books[0];
            var otherBooks = books.Skip(1).ToList();
            if (eIn.Count == 0)
            {
                var b = new Bundle();
                b.Add(book);
                var gAlone = new Grouping();
                gAlone.Add(b);
                e.Add(gAlone);
                return GenerateGroupings(e, otherBooks);
            } 
            else
            {
                foreach (var grouping in eIn)
                {
                    var nbBundles = grouping.Count();
                    for (var iBundle = 0; iBundle < nbBundles; iBundle++ )
                    {
                        var bundle = grouping[iBundle];
                        if (bundle.CanAccept(book))
                        {
                            var enrichedGrouping = grouping.Clone();
                            var clonedBundle = enrichedGrouping[iBundle];
                            clonedBundle.Add(book);
                            e.Add(enrichedGrouping);
                        }
                    }
                    var clonedGrouping = grouping.Clone();
                    var b = new Bundle();
                    b.Add(book);
                    clonedGrouping.Add(b);
                    e.Add(clonedGrouping);
                }
                //Console.WriteLine(e.Count);
                eIn.Clear();
                return GenerateGroupings(e, otherBooks);
            }
        }

        class Ensemble : IEnumerable<Grouping>
        {
            private List<Grouping> _groupings = new List<Grouping>();

            public int Count => _groupings.Count;

            public IEnumerator<Grouping> GetEnumerator()
            {
                return ((IEnumerable<Grouping>)_groupings).GetEnumerator();
            }

            internal void Add(Grouping g)
            {
                _groupings.Add(g);
            }

            internal void Clear()
            {
                _groupings = null;
            }

            internal decimal ComputePrice()
            {
                if (_groupings.Count == 0) return 0m;
                return _groupings.Min(g => g.ComputePrice());
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<Grouping>)_groupings).GetEnumerator();
            }
        }

        class Grouping : IEnumerable<Bundle>
        {
            private List<Bundle> _bundles = new List<Bundle>();

            public Grouping Clone()
            {
                var g = new Grouping();
                g._bundles = _bundles.ConvertAll(b => b.Clone());
                return g;
            }

            public IEnumerator<Bundle> GetEnumerator()
            {
                return ((IEnumerable<Bundle>)_bundles).GetEnumerator();
            }

            internal void Add(Bundle b)
            {
                _bundles.Add(b);
            }

            internal decimal ComputePrice()
            {
                return _bundles.Sum(b => b.ComputePrice());
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<Bundle>)_bundles).GetEnumerator();
            }
            internal Bundle this[int index]    // Indexer declaration  
            {
                get
                {
                    return _bundles[index];
                }
            }
        }

        class Bundle
        {
            private static readonly decimal[] BUNDLE_PRICES = { 0m, 8m, 15.2m, 21.6m, 25.6m, 30m };
            private List<Books> _books = new List<Books>();

            internal void Add(Books book)
            {
                _books.Add(book);
            }

            internal bool CanAccept(Books book)
            {
                return ! _books.Contains(book);
            }

            internal Bundle Clone()
            {
                var clonedBundle = new Bundle();
                clonedBundle._books = _books.ConvertAll(book => book);
                return clonedBundle;
            }

            internal decimal ComputePrice()
            {                
                return BUNDLE_PRICES[_books.Count];
            }
        }
    }
}
