using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GON.Extensions;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ByteArrayFunctionsTest
    {
        [TestMethod]
        public void Cut_WithRangeInTheBegin_ReturnsSubarray()
        {
            //Arrange
            var data = "0102030405060708090a0b0c0d".bin();
            var expected = "010203".bin();

            //Act
            var res = data.Cut(0, 3);

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void Cut_WithRangeInTheMiddle_ReturnsSubarray()
        {
            //Arrange
            var data = "0102030405060708090a0b0c0d".bin();
            var expected = "04050607".bin();

            //Act
            var res = data.Cut(3, 4);

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void Cut_WithRangeInTheEnd_ReturnsSubarray()
        {
            //Arrange
            var data = "0102030405060708090a0b0c0d".bin();
            var expected = "0c0d".bin();

            //Act
            var res = data.Cut(data.Length-2, 2);

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void Combine_WithOneByteArray_ReturnsConcatenetedByteArray()
        {
            //Arrange
            var data1 = "1111".bin();
            var data2 = "2222".bin();
            var expected = "11112222".bin();

            //Act
            var res = data1.Combine(data2);

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void Combine_WithFewByteArray_ReturnsConcatenetedByteArray()
        {
            //Arrange
            var data1 = "1111".bin();
            var data2 = "2222".bin();
            var data3 = "3333".bin();
            var data4 = "FFFF".bin();
            var expected = "111122223333FFFF1111".bin();

            //Act
            var res = data1.Combine(data2, data3, data4, data1);

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void Compare_EqualsByteArrays_ReturnsTrue()
        {
            //Arrange
            var data1 = "112233".bin();
            var data2 = "112233".bin();

            //Act
            var res = data1.Compare(data2);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Compare_DifferentByteArraysWithSameLength_ReturnsFalse()
        {
            //Arrange
            var data1 = "1111".bin();
            var data2 = "2222".bin();

            //Act
            var res = data1.Compare(data2);

            //Assert
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Compare_DifferentByteArraysWithDifferentLength_ReturnsFalse()
        {
            //Arrange
            var data1 = "112233".bin();
            var data2 = "112233445566".bin();

            //Act
            var res = data1.Compare(data2);

            //Assert
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Compare_PartlyEqualsByteArraysWithDifferentLength_ReturnsTrue()
        {
            //Arrange
            var data1 = "112233".bin();
            var data2 = "112233445566".bin();

            //Act
            var res = data1.Compare(data2, false);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void PadLeft_WithRegularPadding_ReturnsPaddedByteArray()
        {
            //Arrange
            var data1 = "1111".bin();
            var expected = "0000001111".bin();

            //Act
            var res = data1.PadLeft(5);

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void PadRight_WithRegularPadding_ReturnsPaddedByteArray()
        {
            //Arrange
            var data1 = "1111".bin();
            var expected = "1111000000".bin();

            //Act
            var res = data1.PadRight(5);

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void SplitToChunks_ArrayWithLengthAliquotToChunks_RetunsSameSizeChunks()
        {
            //Arrange
            var data = "111111222222333333444444".bin();
            var expected = new List<byte[]>(){ "111111".bin(), "222222".bin() , "333333".bin()  , "444444".bin() };

            //Act
            var res = data.SplitToChunks(3);

            //Assert
            Assert.IsInstanceOfType(res, typeof(IEnumerable<IEnumerable<byte>>));
            var i = expected.GetEnumerator();
            foreach (var item in res)
            {
                i.MoveNext();
                Assert.IsTrue(item.SequenceEqual(i.Current));
            }
        }

        [TestMethod]
        public void SplitToChunks_ArrayWithLengthNotAliquotToChunks_RetunsSameSizeChunksAndLastWithDifferentSize()
        {
            //Arrange
            var data = "11111122222233333344".bin();
            var expected = new List<byte[]>() { "111111".bin(), "222222".bin(), "333333".bin(), "44".bin() };

            //Act
            var res = data.SplitToChunks(3);

            //Assert
            Assert.IsInstanceOfType(res, typeof(IEnumerable<IEnumerable<byte>>));
            var i = expected.GetEnumerator();
            foreach (var item in res)
            {
                i.MoveNext();
                Assert.IsTrue(item.SequenceEqual(i.Current));
            }
        }

        [TestMethod]
        public void XOR_ByteArrayAndByteArray_ReturnsXoredByteArray()
        {
            //Arrange
            var data1 = "ABCD".bin();
            var data2 = "FEDC".bin();
            var expected = "5511".bin();

            //Act
            var res = data1.XOR(data2);

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void XOR_ByteArrayAndString_ReturnsXoredByteArray()
        {
            //Arrange
            var data1 = "ABCD".bin();
            var data2 = "FEDC";
            var expected = "5511".bin();

            //Act
            var res = data1.XOR(data2);

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void XOR_StringAndByteArray_ReturnsXoredHexString()
        {
            //Arrange
            var data1 = "ABCD";
            var data2 = "FEDC".bin();
            var expected = "5511";

            //Act
            var res = data1.XOR(data2);

            //Assert
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void XOR_StringAndString_ReturnsXoredHexString()
        {
            //Arrange
            var data1 = "ABCD";
            var data2 = "FEDC";
            var expected = "5511";

            //Act
            var res = data1.XOR(data2);

            //Assert
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void AdjustDESKeyParity_WithDESKey_ReturnsCorrectedKey()
        {
            //Arrange
            var data = "000102030405060708".bin();
            var expected = "010102020404070708".bin();

            //Act
            var res = data.AdjustDESKeyParity();

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void AdjustDESKeyParity_WithDoubleDESKey_ReturnsCorrectedKey()
        {
            //Arrange
            var data = "404142434445464748494a4b4c4d4e4f".bin();
            var expected = "404043434545464649494A4A4C4C4F4F".bin();

            //Act
            var res = data.AdjustDESKeyParity();

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void AdjustDESKeyParity_WithTrippleDESKey_ReturnsCorrectedKey()
        {
            //Arrange
            var data = "234567abdcef098765de0987ab09876d0987fd35b5432560".bin();
            var expected = "234567ABDCEF088664DF0886AB08866D0886FD34B5432561".bin();

            //Act
            var res = data.AdjustDESKeyParity();

            //Assert
            Assert.IsInstanceOfType(res, typeof(byte[]));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void IsHexDigit_WithHexChar_ReturnsTrue()
        {
            //Arrange
            char data = 'C';

            //Act
            var res = data.IsHexDigit();

            //Assert
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void IsHexDigit_WithNonHexChar_ReturnsFalse()
        {
            //Arrange
            char data = 'G';

            //Act
            var res = data.IsHexDigit();

            //Assert
            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void IsHexString_WithHexString_ReturnsTrue()
        {
            //Arrange
            string data = "0102030405060708090a0b0c0d";

            //Act
            var res = data.IsHexString();

            //Assert
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void IsHexString_WithNonHexString_ReturnsFalse()
        {
            //Arrange
            string data = "Hello World!";

            //Act
            var res = data.IsHexString();

            //Assert
            Assert.AreEqual(false, res);
        }
    }
}
