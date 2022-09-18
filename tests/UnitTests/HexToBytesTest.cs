using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GON.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class HexToBytesTest
    {
        [TestMethod]
        public void ToByteArray_WithCleanHexString_ReturnsCorrectByteArray()
        {
            //Arrange
            string hexString = "00112233445566778899AABBCCDDEEFF";
            IEnumerable<byte> expected = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };

            //Act
            var res = hexString.ToByteArray();

            //Assert
            Assert.IsInstanceOfType(res, typeof(IEnumerable<byte>));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Hexadecimal string cannot have an odd number of digits")]
        public void ToByteArray_WithOddHexString_ThrowsException()
        {
            //Arrange
            string hexString = "0112233445566778899AABBCCDDEEFF";

            //Act
            var res = hexString.ToByteArray();
        }

        [TestMethod]
        public void ToByteArray_WithByteArrayStyleHexString_ReturnsCorrectByteArray()
        {
            //Arrange
            string hexString = "0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF";
            IEnumerable<byte> expected = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };

            //Act
            var res = hexString.ToByteArray();

            //Assert
            Assert.IsInstanceOfType(res, typeof(IEnumerable<byte>));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void ToByteArray_WithEmptyString_ReturnsEmptyByteArray()
        {
            //Arrange
            string hexString = "";
            IEnumerable<byte> expected = new byte[] {};

            //Act
            var res = hexString.ToByteArray();

            //Assert
            Assert.IsInstanceOfType(res, typeof(IEnumerable<byte>));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void bin_WithCleanHexString_ReturnsCorrectByteArray()
        {
            //Arrange
            string hexString = "00112233445566778899AABBCCDDEEFF";
            IEnumerable<byte> expected = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };

            //Act
            var res = hexString.bin();

            //Assert
            Assert.IsInstanceOfType(res, typeof(IEnumerable<byte>));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Hexadecimal string cannot have an odd number of digits")]
        public void bin_WithOddHexString_ThrowsException()
        {
            //Arrange
            string hexString = "0112233445566778899AABBCCDDEEFF";

            //Act
            var res = hexString.bin();
        }

        [TestMethod]
        public void bin_WithByteArrayStyleHexString_ReturnsCorrectByteArray()
        {
            //Arrange
            string hexString = "0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF";
            IEnumerable<byte> expected = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };

            //Act
            var res = hexString.bin();

            //Assert
            Assert.IsInstanceOfType(res, typeof(IEnumerable<byte>));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

        [TestMethod]
        public void bin_WithEmptyString_ReturnsEmptyByteArray()
        {
            //Arrange
            string hexString = "";
            IEnumerable<byte> expected = new byte[] { };

            //Act
            var res = hexString.bin();

            //Assert
            Assert.IsInstanceOfType(res, typeof(IEnumerable<byte>));
            Assert.IsTrue(res.SequenceEqual(expected));
        }

    }
}
