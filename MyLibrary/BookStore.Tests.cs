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
		[TestCase(0, TestName= "Return_0_for_an_empty_basket")]
		[TestCase(8, Books.Book1, TestName = "Return_8_for_a_basket_with_1_book")]
		[TestCase(16, Books.Book1, Books.Book1, TestName = "Return_16_for_a_basket_with_the_same_book_twice")]
		public void TestBookStore(decimal totalPrice, params Books[] books)
		{
			var store = new BookStore(books);
			var actual = store.ComputePrice();
			Check.That(actual).IsEqualTo(totalPrice);
		}
    }
}
