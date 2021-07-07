using NUnit.Framework;

namespace Interview
{
    // Если тесты не запускаются через Test Explorer, выполните Clean Solution
    public class Tests
    {
        [TestCase("123456", TestName = "ValidShortConstructionAndSerializationTest")]
        [TestCase("123456123456123456123456123456123456123456", TestName = "ValidLongConstructionAndSerializationTest")]
        public void ValidConstructionAndSerializationTest(string number)
        {
            Assert.AreEqual(number, new BigNumber(number).ToString());
        }

        [TestCase("1", "2", "3", TestName = "EqualLengthWithoutOverflowShortAdditionTest")]
        [TestCase("100", "200", "300", TestName = "EqualLengthWithoutOverflowAdditionTest")]
        [TestCase("700", "800", "1500", TestName = "EqualLengthWithOverflowAdditionTest")]
        [TestCase("123000", "456", "123456", TestName = "DifferentLengthWithoutOverflowAdditionTest")]
        [TestCase("456", "123000", "123456", TestName = "DifferentLengthWithoutOverflowAdditionTest2")]
        [TestCase("999999", "1", "1000000", TestName = "DifferentLengthWithOverflowAdditionTest")]
        [TestCase("1", "999999", "1000000", TestName = "DifferentLengthWithOverflowAdditionTest2")]
        [TestCase("3", "99", "102", TestName = "DifferentLengthWithOverflowAdditionTest3")]
        [TestCase("1111111111111111111111111111111111111111", "2222222222222222222222222222222222222222", "3333333333333333333333333333333333333333", TestName = "AdditionLongerThenUInt64WorksTest")]
        [TestCase("918", "726", "1644", TestName = "ResetOverflowTest")]
        [TestCase("918000000", "726000000", "1644000000", TestName = "OverflowForBigBaseTest")]
        [TestCase("50000000000", "40000000000", "90000000000", TestName = "11DigitsManyZeroesTest")]
        [TestCase("123456789987654321", "100000000100000000", "223456790087654321", TestName = "LongCarryTest")]
        [TestCase("1234567891234567891230", "1", "1234567891234567891231", TestName = "LongAndShortNumberTest")]
        [TestCase("500000000000000000000", "400000000000000000000", "900000000000000000000", TestName = "21DigitsManyZeroesTest")]
        [TestCase("5000000000", "4900000000", "9900000000", TestName = "10DigitsMoreThanIntTest")]
        public void AddTest(string number1, string number2, string expected)
        {
            var bNumber1 = new BigNumber(number1);
            var bNumber2 = new BigNumber(number2);
            var sum = bNumber1 + bNumber2;
            Assert.AreEqual(expected, sum.ToString());
        }

        [TestCase("998", "12", TestName = "ImmutableAdditionTest")]
        public void ImmutableAdditionTest(string number1, string number2)
        {
            var bNumber1 = new BigNumber(number1);
            var bNumber2 = new BigNumber(number2);
            var bNumber3 = bNumber1 + bNumber2; // Неиспользуемая переменная
            Assert.AreEqual(number1, bNumber1.ToString());
            Assert.AreEqual(number2, bNumber2.ToString());
        }

        [Test]
        public void ImmutableToStringTest()
        {
            var number = "12345678910";
            var bNumber = new BigNumber(number);
            Assert.AreEqual(number, bNumber.ToString());
        }
    }
}