using System;
using System.Collections.Generic;

namespace MyLibrary
{
    public class BookStore


    {
		private List<Books> _books = new List<Books>();

		public BookStore()
		{
		}

		public BookStore(Books book)
		{
			this._books.Add(book);
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
