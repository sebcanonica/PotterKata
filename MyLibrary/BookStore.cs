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
			var price = _books.Count * 8m;

			if(_books.Count == 2 && _books[0] != _books[1])
			{
				price *= 0.95m;
			}

			return price;
        }
    }

	public enum Books
	{
		Book1,
		Book2
	}
}
