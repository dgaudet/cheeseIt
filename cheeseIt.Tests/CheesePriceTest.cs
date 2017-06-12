using System;
using cheeseIt.Models;
using Xunit;

namespace cheeseIt.Tests
{
    public class CheesePriceTest
    {
        #region - Standard Cheese Type Tests
        [Fact]
        public void CheesePriceForDay_ShouldReturnNull_GivenNullPrice()
        {
            Cheese cheese = new Cheese { Name = "test" };
            Assert.Null(cheese.PriceForDay(DateTime.Now));
        }

        [Fact]
        public void CheesePriceForDay_ShouldReturnMax_GivenPriceOverMax()
        {
            Cheese cheese = new Cheese { Name = "test", Price = 100M, DateRecieved = DateTime.Now };
            Assert.Equal(CheeseConstants.MAX_PRICE, cheese.PriceForDay(DateTime.Now));
        }

        [Fact]
        public void CheesePriceForDay_ShouldReturnNull_GivenNoDateRecieved()
        {
            Cheese cheese = new Cheese { Name = "test", Price = 100M };
            Assert.Null(cheese.PriceForDay(DateTime.Now));
        }

        [Fact]
        public void StandardCheesePriceForDay_ShouldLoosePercent_GivenOneDayOldAndNullBeforeBestBeforeDateAndNullBeforeDaysToSell()
        {
            var dateRecieved = DateTime.Now;
            var price = 10M;

            Cheese cheese = new Cheese { 
                Name = "test", 
                Price = price, 
                Type = CheeseType.Standard, 
                DateRecieved = dateRecieved 
            };
            var expectedPrice = price + price * CheeseConstants.STANARD_PRICE_DECREASE_RATE;

            Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
        }

		[Fact]
		public void StandardCheesePriceForDay_ShouldLoosePercent_GivenOneDayOldAndBeforeBestBeforeDateAndBeforeDaysToSell()
		{
			var dateRecieved = DateTime.Now;
			var price = 10M;

			Cheese cheese = new Cheese
			{
				Name = "test",
				Price = price,
				Type = CheeseType.Standard,
				DateRecieved = dateRecieved,
                BestBeforeDate = dateRecieved.AddDays(2),
                DaysToSell = 5
			};
			var expectedPrice = price + price * CheeseConstants.STANARD_PRICE_DECREASE_RATE;

			Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
		}

        [Fact]
        public void StandardCheesePriceForDay_ShouldLoosePercent_GivenOneDayOldAndPriceOriginallyOverMax()
        {
            var dateRecieved = DateTime.Now;
            var price = CheeseConstants.MAX_PRICE + 1M;

            Cheese cheese = new Cheese { Name = "test", Price = price, Type = CheeseType.Standard, DateRecieved = dateRecieved };
            var expectedPrice = price + price * CheeseConstants.STANARD_PRICE_DECREASE_RATE;

            Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
        }

        [Fact]
        public void StandardCheesePriceForDay_ShouldLooseTwicePercent_GivenTwoDayOld()
        {
            var dateRecieved = DateTime.Now;
            var price = 10M;

            Cheese cheese = new Cheese { Name = "test", Price = price, Type = CheeseType.Standard, DateRecieved = dateRecieved };
            var expectedPrice = price + price * CheeseConstants.STANARD_PRICE_DECREASE_RATE * 2;

            Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(2)));
        }

        [Fact]
        public void StandardCheesePriceForDay_ShouldBeZero_GivenNegativePrice()
        {
            var dateRecieved = DateTime.Now;
            var price = -10M;

            Cheese cheese = new Cheese { Name = "test", Price = price, Type = CheeseType.Standard, DateRecieved = dateRecieved };
            var expectedPrice = 0M;

            Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(2)));
        }

        [Fact]
        public void StandardCheesePriceForDay_ShouldLooseTwicePercent_GivenPassedBestBeforeDate()
        {
            var dateRecieved = DateTime.Now;
            var price = 10M;

            Cheese cheese = new Cheese
            {
                Name = "test",
                Price = price,
                Type = CheeseType.Standard,
                DateRecieved = dateRecieved,
                BestBeforeDate = dateRecieved
            };
            var expectedPrice = price + price * CheeseConstants.STANARD_PRICE_DECREASE_RATE * 2;

            Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
        }

		[Fact]
		public void StandardCheesePriceForDay_ShouldBeNull_GivenPassedDaysToSell()
		{
            var dateRecieved = DateTime.Now;
			var price = 10M;
            var daysToSell = 2;

			Cheese cheese = new Cheese
			{
				Name = "test",
				Price = price,
				Type = CheeseType.Standard,
                DaysToSell = daysToSell,
				DateRecieved = dateRecieved,
				BestBeforeDate = dateRecieved
			};

            Assert.Null(cheese.PriceForDay(dateRecieved.AddDays(daysToSell+1)));
		}
        #endregion
        #region - Aged Cheese Tests
        [Fact]
        public void AgedCheesePriceForDay_ShouldIncreasePercent_GivenOneDayOld()
        {
            var dateRecieved = DateTime.Now;
            var price = 10M;

            Cheese cheese = new Cheese { Name = "test", Price = price, Type = CheeseType.Aged, DateRecieved = dateRecieved };
            var expectedPrice = price + price * CheeseConstants.AGED_PRICE_INCREASE_RATE;

            Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
        }

        [Fact]
        public void AgedCheesePriceForDay_ShouldIncreasePercent_GivenPassedBestBeforeDate()
        {
            var dateRecieved = DateTime.Now;
            var price = 10M;

            Cheese cheese = new Cheese
            {
                Name = "test",
                Price = price,
                Type = CheeseType.Aged,
                DateRecieved = dateRecieved,
                BestBeforeDate = dateRecieved
            };
            var expectedPrice = price + price * CheeseConstants.AGED_PRICE_INCREASE_RATE;

            Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
        }
		#endregion
		#region - Fresh Cheese Tests
		[Fact]
		public void FreshCheesePriceForDay_ShouldDecreasePercent_GivenOneDayOld()
		{
			var dateRecieved = DateTime.Now;
			var price = 10M;

            Cheese cheese = new Cheese { Name = "test", Price = price, Type = CheeseType.Fresh, DateRecieved = dateRecieved };
            var expectedPrice = price + price * CheeseConstants.FRESH_PRICE_DECREASE_RATE;

			Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
		}

		[Fact]
		public void FreshCheesePriceForDay_ShouldDecreaseTwicePercent_GivenPassedBestBeforeDate()
		{
			var dateRecieved = DateTime.Now;
			var price = 10M;

			Cheese cheese = new Cheese
			{
				Name = "test",
				Price = price,
                Type = CheeseType.Fresh,
				DateRecieved = dateRecieved,
				BestBeforeDate = dateRecieved
			};
            var expectedPrice = price + price * CheeseConstants.FRESH_PRICE_DECREASE_RATE*2;

			Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
		}
		#endregion
		#region Unique Cheese Tests
		[Fact]
		public void UniqueCheesePriceForDay_ShouldNotChange_GivenOneDayOld()
		{
			var dateRecieved = DateTime.Now;
			var price = 10M;

            Cheese cheese = new Cheese { Name = "test", Price = price, Type = CheeseType.Unique, DateRecieved = dateRecieved };
			var expectedPrice = price;

			Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
		}

		[Fact]
		public void UniqueCheesePriceForDay_ShouldNotChange_GivenPassedBestBeforeDate()
		{
			var dateRecieved = DateTime.Now;
			var price = 10M;

			Cheese cheese = new Cheese
			{
				Name = "test",
				Price = price,
                Type = CheeseType.Unique,
				DateRecieved = dateRecieved,
				BestBeforeDate = dateRecieved
			};
			var expectedPrice = price;

			Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
		}

		[Fact]
		public void UniqueCheesePriceForDay_ShouldNotChange_GivenPassedDaysToSell()
		{
			var dateRecieved = DateTime.Now;
			var price = 10M;

			Cheese cheese = new Cheese
			{
				Name = "test",
				Price = price,
				Type = CheeseType.Unique,
				DateRecieved = dateRecieved,
				BestBeforeDate = dateRecieved,
                DaysToSell = 1
			};
			var expectedPrice = price;

			Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(10)));
		}
		#endregion
		#region - Special Cheese Tests
		[Fact]
		public void SpecialCheesePriceForDay_ShouldIncreasePercent_GivenSixDaysToSell()
		{
			var dateRecieved = DateTime.Now;
			var price = 10M;

            Cheese cheese = new Cheese { 
                Name = "test", 
                Price = price, 
                Type = CheeseType.Special, 
                DateRecieved = dateRecieved,
                DaysToSell = 7
            };
            var expectedPrice = price + price * CheeseConstants.SPECIAL_PRICE_INCREASE_RATE;

			Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
		}

		[Fact]
		public void SpecialCheesePriceForDay_ShouldIncreaseTwicePercent_GivenFiveDaysToSell()
		{
			var dateRecieved = DateTime.Now;
			var price = 10M;

			Cheese cheese = new Cheese
			{
				Name = "test",
				Price = price,
				Type = CheeseType.Special,
				DateRecieved = dateRecieved,
				DaysToSell = 5
			};
			var expectedPrice = price + price * CheeseConstants.SPECIAL_PRICE_INCREASE_RATE *2;

			Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(1)));
		}

		//[Fact]
		//public void SpecialCheesePriceForDay_ShouldIncreaseTwicePercent_Given6DaysToSellAndOnlyFourDaysBeforeCheeseIsTooOld()
		//{
		//	var dateRecieved = DateTime.Now;
		//	var price = 10M;

		//	Cheese cheese = new Cheese
		//	{
		//		Name = "test",
		//		Price = price,
		//		Type = CheeseType.Special,
		//		DateRecieved = dateRecieved,
		//		DaysToSell = 6
		//	};
		//	var expectedPrice = price + price * CheeseConstants.SPECIAL_PRICE_INCREASE_RATE;
  //          expectedPrice = expectedPrice + expectedPrice * CheeseConstants.SPECIAL_PRICE_INCREASE_RATE * 2;
  //          expectedPrice = expectedPrice + expectedPrice * CheeseConstants.SPECIAL_PRICE_INCREASE_RATE * 2;

		//	Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(2)));
		//}

		[Fact]
		public void SpecialCheesePriceForDay_ShouldIncreaseTwicePercent_Given7DaysToSellAndPassedBestBeforeDate()
		{
			var dateRecieved = DateTime.Now;
			var price = 10M;

			Cheese cheese = new Cheese
			{
				Name = "test",
				Price = price,
				Type = CheeseType.Special,
				DateRecieved = dateRecieved,
                BestBeforeDate = dateRecieved,
				DaysToSell = 7
			};
			var expectedPrice = price + price * CheeseConstants.SPECIAL_PRICE_INCREASE_RATE * 4;

			Assert.Equal(expectedPrice, cheese.PriceForDay(dateRecieved.AddDays(2)));
		}
		#endregion
	}
}
