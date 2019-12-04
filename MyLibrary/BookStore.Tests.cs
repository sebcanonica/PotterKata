using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static MyLibrary.Books;

namespace MyLibrary
{
    [TestFixture]
    class BookStore_should
    {
        [TestCase(0,
            TestName = "Return_0_for_an_empty_basket")]
        [TestCase(8, Book1,
            TestName = "Return_8_for_a_basket_with_1_book")]
        [TestCase(16, Book1, Book1,
            TestName = "Return_16_for_a_basket_with_the_same_book_twice")]
        [TestCase(8 * 2 * 0.95, Book1, Book2,
            TestName = "Return a 5% discount for 2 different books")]
        [TestCase(8 * 3 * 0.9, Book1, Book2, Book3,
            TestName = "Return a 10% discount for 3 different books")]
        [TestCase(8 * 5 * 0.75, Book1, Book2, Book3, Book4, Book5,
            TestName = "Return a 25% discount for 5 different books")]
        [TestCase(23.2, Book1, Book2, Book1,
            TestName = "Return a 5% discount for 3 books including 2 different books")]
        [TestCase(30.4, Book1, Book2, Book1, Book2,
            TestName = "Return a 5% discount for 2 bundles of 2 different books")]
        [TestCase(8 * 4 * 0.8 + 8 * 2 * 0.95, Book1, Book1, Book2, Book3, Book3, Book4,
            TestName = "1 bundle of 4 and 1 bundle of 2")]
        [TestCase(8 * 5 * 0.75 + 8, Book1, Book2, Book2, Book3, Book4, Book5,
            TestName = "1 bundle of 5 and 1 solo")]
        [TestCase(2 * (8 * 4 * 0.8), Book1, Book1, Book2, Book2, Book3, Book3, Book4, Book5,
            TestName = "2 bundles of 4")]
        [TestCase(3 * (8 * 5 * 0.75) + 2 * (8 * 4 * 0.8),
            Book1, Book1, Book1, Book1, Book1,
            Book2, Book2, Book2, Book2, Book2,
            Book3, Book3, Book3, Book3,
            Book4, Book4, Book4, Book4, Book4,
            Book5, Book5, Book5, Book5,
            TestName = "3 bundles of 5 and 2 bundle of 4")]
        [TestCase(8 * 5 * 0.75 + 8 * 4 * 0.8 + 8 * 2 * 0.95 + 8,//78.8, // 1 2 2 3 4
            Book1, Book1, Book1,
            Book2, Book2,
            Book3, Book3, Book3, Book3,
            Book4, Book4,
            Book5,
            TestName = "1 of 5, 1 of 4, 1 of 2 and 1 solo")]
        [TestCase(3 * 8 * 4 * 0.8 + 8 * 2 * 0.95 + 8, //100, // 1 2 3 4 5
            Book1,
            Book2, Book2,
            Book3, Book3, Book3,
            Book4, Book4, Book4, Book4,
            Book5, Book5, Book5, Book5, Book5,
            TestName = "3 of 4, 2 of 2 and 1 solo")]
        public void TestBookStore(decimal totalPrice, params Books[] books)
        {
            var store = new BookStore(books);
            var actual = store.ComputePrice();
            Check.That(actual).IsEqualTo(totalPrice);
        }

        [TestCase(0)]
        [TestCase(8, Book1)]
        [TestCase(8, Book2)]
        [TestCase(8, Book3)]
        [TestCase(8, Book4)]
        [TestCase(8, Book5)]
        [TestCase(8*3, Book1, Book1, Book1)]
        public void Basics(decimal totalPrice, params Books[] books)
        {
            Check.That(new BookStore(books).ComputePrice()).IsEqualTo(totalPrice);
        }

        [TestCase(8 * 2 * 0.95, Book1, Book2)]
        [TestCase(8 * 3 * 0.9, Book1, Book3, Book5)]
        [TestCase(8 * 4 * 0.8, Book1, Book2, Book3, Book5)]
        [TestCase(8 * 5 * 0.75, Book1, Book2, Book3, Book4, Book5)]
        public void SimpleDiscounts(decimal totalPrice, params Books[] books)
        {
            Check.That(new BookStore(books).ComputePrice()).IsEqualTo(totalPrice);
        }

        [TestCase(8 + (8 * 2 * 0.95), Book1, Book1, Book2)]
        [TestCase(2 * (8 * 2 * 0.95), Book1, Book1, Book2, Book2)]
        [TestCase((8 * 4 * 0.8) + (8 * 2 * 0.95), Book1, Book1, Book2, Book3, Book3, Book4)]
        public void SeveralDiscounts(decimal totalPrice, params Books[] books)
        {
            Check.That(new BookStore(books).ComputePrice()).IsEqualTo(totalPrice);
        }

        [TestCase(2 * (8 * 4 * 0.8), Book1, Book1, Book2, Book2, Book3, Book3, Book4, Book5)]
        [TestCase(3 * (8 * 5 * 0.75) + 2 * (8 * 4 * 0.8),
            Book1, Book1, Book1, Book1, Book1,
            Book2, Book2, Book2, Book2, Book2,
            Book3, Book3, Book3, Book3,
            Book4, Book4, Book4, Book4, Book4,
            Book5, Book5, Book5, Book5)]
        public void EdgeCases(decimal totalPrice, params Books[] books)
        {
            Check.That(new BookStore(books).ComputePrice()).IsEqualTo(totalPrice);
        }
    }
}
