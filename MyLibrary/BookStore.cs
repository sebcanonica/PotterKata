using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary
{
    public class BookStore


    {
		private List<Books> _books;

		public BookStore(params Books[] books)
		{
			_books = books.ToList();
		}

		public decimal BundlePrice(int booksInBundle)
		{
			decimal [] BUNDLE_PRICES = { 0m, 8m, 15.2m, 21.6m };
			return BUNDLE_PRICES[booksInBundle];
		}

		public decimal ComputePrice()
        {
			var distinct = _books.Distinct();
			if (distinct.Count() == _books.Count)
			{
				return BundlePrice(_books.Count);
			}
			else
			{
				var uniquePrice = 8m *(_books.Count - distinct.Count());
				var bundlePrice = BundlePrice(distinct.Count());
				return uniquePrice + bundlePrice;
			}
        }
    }

	public enum Books
	{
		Book1,
		Book2,
		Book3
	}
}
