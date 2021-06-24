using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRPApp.View.Setting;
using System;
using System.Linq;

namespace MRPApp.Test
{
    [TestClass]
    public class SettingsTest
    {
        // DB상에 중복된 데이터가 있는지 테스트
        [TestMethod]
        public void IsDuplicateDataTest()
        {
            var expectVal = true;       // 예상값
            var inputCode = "PC010001"; // DB에 있는 값

            var code = Logic.DataAccess.GetSettings().Where(d => d.BasicCode.Contains(inputCode)).FirstOrDefault();
            var realVal = code != null ? true : false;

            Assert.AreNotEqual(expectVal, realVal); // 값이 같으면 Pass, 다르면 Fail
        }

        public void IsCodeSearched()
        {
            var expectVal = 2;
            var inputCode = "설비";

            var realal = Logic.DataAccess.GetSettings().Where(d => d.BasicCode.Contains(inputCode)).FirstOrDefault();
            Assert.AreNotEqual(expectVal, realVal);
        }
    }
}
