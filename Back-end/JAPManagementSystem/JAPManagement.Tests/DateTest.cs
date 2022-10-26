using JAPManagenent.Utils.Util;

namespace JAPManagement.Tests
{
    [TestFixture]
    public class Tests
    {
        private IDateCalculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new DateCalculator();
        }

        [Test]
        public void PositiveTestEndDateCalculated()
        {
            DateTime endDate = new DateTime(2022, 10, 1);
            int expectedHoursToComplete = 16;
            calculator.CalculateTimeDifferenceWithWorkingHours(endDate, expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
            Assert.That(newEndDate, Is.EqualTo(new DateTime(2022, 10, 3)));
        }

        [Test]
        public void PositiveTestEndDateCalculatedSecond()
        {
            DateTime endDate = new DateTime(2022, 12, 1);
            int expectedHoursToComplete = 10;
            calculator.CalculateTimeDifferenceWithWorkingHours(endDate, expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
            Assert.That(newEndDate, Is.EqualTo(new DateTime(2022, 12, 2, 2, 0, 0)));
        }

        [Test]
        public void NegativeTestEndDateCalculatedFirst()
        {
            DateTime endDate = new DateTime(2022, 12, 5);
            int expectedHoursToComplete = 28;
            calculator.CalculateTimeDifferenceWithWorkingHours(endDate, expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
            Assert.That(newEndDate, Is.Not.EqualTo(new DateTime(2022, 12, 10)));
        }

        [Test]
        public void NegativeTestEndDateCalculatedSecond()
        {
            DateTime endDate = new DateTime(2022, 12, 5);
            int expectedHoursToComplete = 2;
            calculator.CalculateTimeDifferenceWithWorkingHours(endDate, expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
            Assert.That(newEndDate, Is.Not.EqualTo(new DateTime(2022, 12, 6)));
        }
    }
}