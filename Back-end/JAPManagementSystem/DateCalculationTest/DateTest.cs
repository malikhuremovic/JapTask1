using DateCalculation;

namespace DateCalculationTest
{
    [TestFixture]
    public class Tests
    {
        private DateCalculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new DateCalculator();
        }

        [Test]
        public void PositiveTestEndDateCalculated()
        {
            DateTime endDate = new DateTime(2022, 10, 1);
            int expectedHoursToComplete = 48;
            calculator.CalculateTimeDifference(endDate, expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
            Assert.That(newEndDate, Is.EqualTo(new DateTime(2022, 10, 3)));
        }

        [Test]
        public void PositiveTestEndDateCalculatedSecond()
        {
            DateTime endDate = new DateTime(2022, 12, 1);
            int expectedHoursToComplete = 58;
            calculator.CalculateTimeDifference(endDate, expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
            Assert.That(newEndDate, Is.EqualTo(new DateTime(2022, 12, 3, 10, 0, 0)));
        }

        [Test]
        public void NegativeTestEndDateCalculatedFirst()
        {
            DateTime endDate = new DateTime(2022, 12, 5);
            int expectedHoursToComplete = 28;
            calculator.CalculateTimeDifference(endDate, expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
            Assert.That(newEndDate, Is.Not.EqualTo(new DateTime(2022, 12, 10)));
        }

        [Test]
        public void NegativeTestEndDateCalculatedSecond()
        {
            DateTime endDate = new DateTime(2022, 12, 5);
            int expectedHoursToComplete = 2;
            calculator.CalculateTimeDifference(endDate, expectedHoursToComplete, out DateTime newStartDate, out DateTime newEndDate);
            Assert.That(newEndDate, Is.Not.EqualTo(new DateTime(2022, 12, 6)));
        }
    }
 }