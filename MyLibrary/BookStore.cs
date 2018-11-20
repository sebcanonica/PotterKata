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
			var price = booksInBundle * 8m;
			if(booksInBundle == 3)
			{
				price *= 0.9m;
			}
			else if(booksInBundle == 2)
			{
				price *= 0.95m;
			}
			return price;
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
