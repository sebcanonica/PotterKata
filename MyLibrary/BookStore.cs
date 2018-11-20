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
			if (_books.Count == 0)
				return 0m;
			return 8m;
        }
    }

	public enum Books
	{
		Book1
	}
}
