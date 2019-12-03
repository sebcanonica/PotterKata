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

		public decimal ComputePrice()
        {
			var bookCounts = _books.GroupBy(book => book)
				.OrderBy(group => group.Count());

			if (bookCounts.Count() == 5)
			{				
				var bundlesOf4 = bookCounts.Skip(1).First().Count();
				var bundlesOf3 = bookCounts.Skip(2).First().Count();
				if (bundlesOf3 > bundlesOf4)
				{
					// Maximize number of bundles of 4
					var priceWithBundlesOf4 = 0m;
					var bundlesOf5 = bookCounts.First().Count();
					var additionalBundlesOf4 = Math.Min(bundlesOf5, bundlesOf3 - bundlesOf4);
					var pricedBundlesOf5 = bundlesOf5 - additionalBundlesOf4;
					var pricedBundlesOf4 = bundlesOf4 + additionalBundlesOf4 - pricedBundlesOf5;
					priceWithBundlesOf4 = BundlePrice(5) * pricedBundlesOf5
						+ BundlePrice(4) * pricedBundlesOf4
						+ BookCountsPrice(bookCounts.Skip(2), bundlesOf4 + additionalBundlesOf4);
					return priceWithBundlesOf4;
				}				
			}

			return BookCountsPrice(bookCounts);
        }

		private decimal BookCountsPrice(IEnumerable<IGrouping<Books,Books>> bookCounts, int booksInstanceAlreadyPriced = 0)
		{
			var price = 0m;
			var maxBundleSize = bookCounts.Count();
			foreach (var bookCount in bookCounts)
			{
				price += BundlePrice(maxBundleSize) * (bookCount.Count() - booksInstanceAlreadyPriced);
				maxBundleSize -= 1;
				booksInstanceAlreadyPriced = bookCount.Count();
			}
			return price;
		}

		private decimal BundlePrice(int booksInBundle)
		{
			decimal[] BUNDLE_PRICES = { 0m, 8m, 15.2m, 21.6m, 25.6m, 30m };
			return BUNDLE_PRICES[booksInBundle];
		}
	}

	public enum Books
	{
		Book1,
		Book2,
		Book3,
		Book4,
		Book5
	}
}
