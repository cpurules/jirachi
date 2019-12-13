using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public static class ByteFunctions {
        /// <summary>
        /// Returns the integer value of the LENGTH bytes that
        /// begin at offset OFFSET
        /// </summary>
        /// <param name="bytes">The byte array to pull from</param>
        /// <param name="offset">The offset of the first byte</param>
        /// <param name="length">The number of bytes to read</param>
        /// <param name="bigEndian">Whether the bytes are in Big- or Little-Endian </param>
        /// <returns>The integer value of the series of bytes</returns>
        public static int ReadBytesToInteger(byte[] bytes, int offset, int length, bool bigEndian = true) {
            if(length > 4) {
                throw new OverflowException("Reading more than 4 bytes will cause an integer overflow")
            }

            int byteVal = 0;

            // We choose to read the smallest bytes first because then we can use the
            // byteNum variable as our multiplier for sequential bytes
            for(int byteNum = 0; byteNum < length; byteNum++) {
                byte currentByte;
                if(bigEndian) {
                    currentByte = bytes[offset + length - byteNum - 1];
                }
                else {
                    currentByte = bytes[offset + byteNum];
                }
                byteVal += 256 * Convert.ToInt32(currentByte);
            }

            return byteVal;
        }

        public static int ReadBinaryEncodedDecimal(byte[] bytes, int offset, int length, bool bigEndian = true) {
            if(length > 4) {
                throw new OverflowException("Reading more than 4 bytes may cause an integer overflow");
            }

            int byteVal = 0;

            // We choose to read the smallest bytes first
            for(int byteNum = 0; byteNum < length; byteNum++) {
                byte currentByte;
                if(bigEndian) {
                    currentByte = bytes[offset + length - byteNum - 1];
                }
                else {
                    currentByte = bytes[offset + byteNum];
                }

                int currentByteValue = Convert.ToInt32(currentByte.ToString("X"));

                byteVal += currentByteValue * Convert.ToInt32(Math.Pow(100, byteNum));
            }

            return byteVal;
        }
    }
}
