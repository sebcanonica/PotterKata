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

    }
}
