using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace GOOS_SampleTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void test_getNowString()
        {
            var expected = "2018/05/23 14:27:10.567";
            var timeProvider = GivenNowIs(expected);
            var target = new GetDateTimeStringService(timeProvider);

            string actual = target.GetNowString();

            Assert.AreEqual(expected, actual);
        }

        private static ITimeProvider GivenNowIs(string timeString)
        {
            ITimeProvider timeProvider = Substitute.For<ITimeProvider>();
            timeProvider.GetCurrentTime().Returns(timeString);
            return timeProvider;
        }
    }

    public interface ITimeProvider
    {
        string GetCurrentTime();
    }

    public class GetDateTimeStringService
    {
        private readonly ITimeProvider _timeProvider;

        public GetDateTimeStringService(ITimeProvider timeProvider)
        {
            this._timeProvider = timeProvider;
        }

        public string GetNowString()
        {
            return _timeProvider.GetCurrentTime();
        }
    }
}
