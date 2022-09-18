using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GON.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class BytesToHexTest
    {
        [TestMethod]
        public void ToHex_WithByteArray_ReturnsHexString()
        {
            //Arrange
            byte[] data = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            string expected = "00112233445566778899AABBCCDDEEFF";

            //Act
            var res = data.ToHex();

            //Assert
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void ToHex_WithList_ReturnsHexString()
        {
            //Arrange
            List<byte> data = new List<byte>() { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            string expected = "00112233445566778899AABBCCDDEEFF";

            //Act
            var res = data.ToHex();

            //Assert
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void ToHex_WithList_ReturnsEmptyString()
        {
            //Arrange
            List<byte> data = new List<byte>() { };
            string expected = "";

            //Act
            var res = data.ToHex();

            //Assert
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.AreEqual(expected, res);
        }


        [TestMethod]
        public void hex_WithByteArray_ReturnsHexString()
        {
            //Arrange
            byte[] data = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            string expected = "00112233445566778899AABBCCDDEEFF";

            //Act
            var res = data.hex();

            //Assert
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void hex_WithList_ReturnsHexString()
        {
            //Arrange
            List<byte> data = new List<byte>() { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            string expected = "00112233445566778899AABBCCDDEEFF";

            //Act
            var res = data.hex();

            //Assert
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void hex_WithList_ReturnsEmptyString()
        {
            //Arrange
            List<byte> data = new List<byte>() { };
            string expected = "";

            //Act
            var res = data.hex();

            //Assert
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void hex_WithByte_ReturnsHexString()
        {
            //Arrange
            byte data = 0xFA;
            string expected = "FA";

            //Act
            var res = data.hex();

            //Assert
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.AreEqual(expected, res);
        }
    }
}
