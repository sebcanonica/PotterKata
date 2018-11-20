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
			return _books.Count * 8m;
        }
    }

	public enum Books
	{
		Book1
	}
}
