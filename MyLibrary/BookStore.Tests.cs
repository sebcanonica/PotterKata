using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary
{
    [TestFixture]
    class BookStore_should
    {
        [Test]
        public void Return_0_for_an_empty_basket()
        {
            var store = new BookStore();
            var actual = store.ComputePrice();
            Check.That(actual).IsEqualTo(0m);
        }

		[Test]
		public void Return_8_for_a_basket_with_1_book()
		{
			var store = new BookStore(Books.Book1);
			var actual = store.ComputePrice();
			Check.That(actual).IsEqualTo(8m);
		}

    }
}
