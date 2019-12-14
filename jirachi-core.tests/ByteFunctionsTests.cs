using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jirachi_core;
using Xunit;

namespace jirachi_core.tests {
    public class ByteFunctionsTests {
        [Theory]
        [InlineData(new byte[] { 0x01, 0x07, 0x0A, 0x04 }, 0x02, 10)]
        [InlineData(new byte[] { 0x05, 0x1E, 0x01}, 0x01, 30)]
        public void ReadBytesToInteger_SimpleValueShouldRead(byte[] bytes, int offset, int expected) {
            // Arrange

            // Act
            int actual = ByteFunctions.ReadBytesToInteger(bytes, offset, 1);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new byte[] { 0x02, 0x10 }, 0x00, 2, 528)]
        [InlineData(new byte[] { 0x1E, 0x00, 0x03, 0xA4, 0x11 }, 0x01, 3, 932)]
        [InlineData(new byte[] { 0x01, 0x00, 0x4F, 0xFF }, 0x00, 3, 65615)]
        [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x10, 0x00 ,0x00 }, 0x00, 6, 1048576)]
        public void ReadBytesToInteger_LongerValuesShouldRead(byte[] bytes, int offset, int length, int expected) {
            // Arrange

            // Act
            int actual = ByteFunctions.ReadBytesToInteger(bytes, offset, length);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new byte[] { 0x11, 0x01, 0x3E, 0x0F, 0x00 }, 0x01, 2, 15873)]
        [InlineData(new byte[] { 0x00, 0x01, 0x02 }, 0x00, 3, 131328)]
        [InlineData(new byte[] { 0x0A, 0x01, 0x00, 0x00, 0x00, 0x00 }, 0x00, 6, 266)]
        public void ReadBytesToInteger_LittleEndianShouldRead(byte[] bytes, int offset, int length, int expected) {
            // Arrange

            // Act
            int actual = ByteFunctions.ReadBytesToInteger(bytes, offset, length, false);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new byte[] { 0x10, 0x03, 0x00, 0x45, 0x22, 0xA3 }, 0x01, 5)]
        [InlineData(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0x00, 6)]
        public void ReadBytesToInteger_LargeValueShouldOverflow(byte[] bytes, int offset, int length) {
            Assert.Throws<OverflowException>(() => ByteFunctions.ReadBytesToInteger(bytes, offset, length));
        }

        [Theory]
        [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x01 }, 0x00, 5)]
        [InlineData(new byte[] { 0xA1, 0x33, 0x00, 0x03, 0xFB, 0x00, 0x45, 0x00 }, 0x01, 6)]
        public void ReadBytesToInteger_LittleEndianLargeValueShouldOverflow(byte[] bytes, int offset, int length) {
            Assert.Throws<OverflowException>(() => ByteFunctions.ReadBytesToInteger(bytes, offset, length, false));
        }

        [Theory]
        [InlineData(new byte[] { 0x01, 0x02, 0x03, 0x04 }, 0x02, 3)]
        [InlineData(new byte[] { 0x00 }, 0x00, 0)]
        public void ReadBinaryEncodedDecimal_SimpleValueShouldRead(byte[] bytes, int offset, int expected) {
            // Arrange

            // Act
            int actual = ByteFunctions.ReadBinaryEncodedDecimal(bytes, offset, 1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new byte[] { 0x10, 0x99, 0xAA, 0x32 }, 0x00, 2, 1099)]
        [InlineData(new byte[] { 0x45, 0x00, 0x00, 0x21 }, 0x00, 4, 45000021)]
        [InlineData(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x01}, 0x02, 3, 1)]
        [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x01, 0x11 }, 0x00, 6, 111)]
        public void ReadBinaryEncodedDecimal_LongerValueShouldRead(byte[] bytes, int offset, int length, int expected) {
            // Arrange

            // Act
            int actual = ByteFunctions.ReadBinaryEncodedDecimal(bytes, offset, length);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new byte[] { 0x10, 0x0A, 0x03 }, 0x01, 1)]
        [InlineData(new byte[] { 0x00, 0x01, 0x02, 0x3A }, 0x00, 4)]
        public void ReadBinaryEncodedDecimal_NonDigitsShouldThrow(byte[] bytes, int offset, int length) {
            Assert.Throws<ArgumentOutOfRangeException>(() => ByteFunctions.ReadBinaryEncodedDecimal(bytes, offset, length));
        }

        [Theory]
        [InlineData(new byte[] { 0x10, 0x11, 0x12, 0x13, 0x14 }, 0x00, 5)]
        [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x22, 0x00}, 0x01, 8)]
        public void ReadBinaryEncodedDecimal_LargeValueShouldOverflow(byte[] bytes, int offset, int length) {
            Assert.Throws<OverflowException>(() => ByteFunctions.ReadBinaryEncodedDecimal(bytes, offset, length));
        }

        [Theory]
        [InlineData(new byte[] { 0x12, 0x93, 0x22, 0x40, 0x01 }, 0x00, 5)]
        [InlineData(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05 }, 0x01, 5)]
        [InlineData(new byte[] { 0x43, 0x34, 0x00, 0x56, 0x01, 0x00 }, 0x00, 6)]
        public void ReadBinaryEncodedDecimal_LittleEndianLargeValueShouldOverflow(byte[] bytes, int offset, int length) {
            Assert.Throws<OverflowException>(() => ByteFunctions.ReadBinaryEncodedDecimal(bytes, offset, length));
        }
    }
}
