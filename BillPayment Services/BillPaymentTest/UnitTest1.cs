using BillPayment.Models;
using BillPayment.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BillPaymentTest
{
    public class Tests
    {
        List<Payment> item = new List<Payment>();
        IQueryable<Payment> itemdata;
        Mock<DbSet<Payment>> mockSet;
        Mock<PaymentDbContext> itemcontextmock;
        [SetUp]
        public void Setup()
        {
            item = new List<Payment>()
           {
                new Payment{PaymentId = 2, BillId=1234,Amount=130},
           };
            itemdata = item.AsQueryable();
            mockSet = new Mock<DbSet<Payment>>();
            mockSet.As<IQueryable<Payment>>().Setup(m => m.Provider).Returns(itemdata.Provider);
            mockSet.As<IQueryable<Payment>>().Setup(m => m.Expression).Returns(itemdata.Expression);
            mockSet.As<IQueryable<Payment>>().Setup(m => m.ElementType).Returns(itemdata.ElementType);
            mockSet.As<IQueryable<Payment>>().Setup(m => m.GetEnumerator()).Returns(itemdata.GetEnumerator());
            var p = new DbContextOptions<PaymentDbContext>();
            itemcontextmock = new Mock<PaymentDbContext>(p);
            itemcontextmock.Setup(x => x.Payments).Returns(mockSet.Object);



        }


        [Test]
        public void GetAll()
        {
            var obj = new BillServices() { BillNo = 1234, CustomerName = "Manisha", BillAmt = 189 };
            var itemrepo = new PaymentRepository(itemcontextmock.Object);
            var itemlist = itemrepo.MakePayment(obj);
            Assert.NotNull(itemlist);
        }
    }

    }