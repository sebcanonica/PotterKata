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
			var bookCounts = _books
				.GroupBy(book => book)
				.Select(book => book.Count())
				.OrderBy(num => num);

			var bundleCounts = bookCounts
				.Aggregate(new { totalBundleCount = 0, list = new List<int>() },
					(acc, bookCount) =>
					{
						acc.list.Add(bookCount - acc.totalBundleCount);
						return new { totalBundleCount = bookCount, acc.list };
					}).list.ToArray();

			MaximizeBundlesOf4(bundleCounts);

			return BundleCountsPrice(bundleCounts);
		}

		private static void MaximizeBundlesOf4(int[] bundleCounts)
		{
			if (bundleCounts.Count() == 5)
			{
				const int I_BUNDLES_OF_5 = 0;
				const int I_BUNDLES_OF_4 = 1;
				const int I_BUNDLES_OF_3 = 2;
				var bundlesOf3 = bundleCounts[I_BUNDLES_OF_3];
				if (bundlesOf3 > 0)
				{
					var bundlesOf5 = bundleCounts[I_BUNDLES_OF_5];
					var additionalBundlesOf4 = 2 * Math.Min(bundlesOf5, bundlesOf3);
					bundleCounts[I_BUNDLES_OF_5] -= additionalBundlesOf4 / 2;
					bundleCounts[I_BUNDLES_OF_4] += additionalBundlesOf4;
					bundleCounts[I_BUNDLES_OF_3] -= additionalBundlesOf4 / 2;
				}
			}
		}

		private decimal BundleCountsPrice(IEnumerable<int> bundleCounts)
		{
			return bundleCounts
				.Reverse()
				.Select((bundleCount, index) => bundleCount * BundlePrice(index + 1))
				.Sum();
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
