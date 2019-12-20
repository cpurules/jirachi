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
            int byteVal = 0;
            int bytesRead = 0;

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

                bytesRead += 1;
                if(bytesRead > 4) {
                    if (currentByte != 0) {
                        throw new OverflowException("Integers can only hold 4 bytes");
                    }
                }
                else {
                    byteVal += Convert.ToInt32(Math.Pow(256, byteNum)) * Convert.ToInt32(currentByte);
                }
            }

            return byteVal;
        }

        /// <summary>
        /// Returns an array of bytes representing the provided integer.
        /// </summary>
        /// <param name="value">The integer to convert to bytes</param>
        /// <param name="forcedByteCount">If the integer is fewer bytes than this parameter, the return bytes will be padded to this count</param>
        /// <param name="bigEndian">Whether or not to return the bytes in Big Endian format</param>
        /// <returns></returns>
        public static byte[] ReadIntegerToBytes(int value, int forcedByteCount = 0, bool bigEndian = true) {
            if(value < 0) {
                throw new ArgumentOutOfRangeException("Only positive values can be converted to bytes");
            }

            byte[] returnBytes;
            byte[] integerAsBytes = BitConverter.GetBytes(value); // This will always have a count of 4

            int sigByteCount = Convert.ToInt32(Math.Ceiling(Math.Log(value + 1, 256)));
            int returnByteCount;

            if (forcedByteCount < 0 || forcedByteCount > 4) {
                throw new ArgumentOutOfRangeException("Valid values for forcedByteCount are 0 to 4, inclusive");
            }
            else if (forcedByteCount == 0) {
                if (value == 0) {
                    returnByteCount = 1;
                }
                else {
                    returnByteCount = sigByteCount;
                }
            }
            else if (forcedByteCount < sigByteCount) {
                throw new ArgumentOutOfRangeException(String.Format("You cannot squish {0} bytes into {1} bytes", sigByteCount, forcedByteCount));
            }
            else {
                returnByteCount = forcedByteCount;
            }
            returnBytes = new byte[returnByteCount];

            // If the integer bytes are in Big Endian, we will transform them to Little Endian for ease of copy
            // This way we can just copy the first returnByteCount bytes, since it will ignore 
            if(!BitConverter.IsLittleEndian) {
                Array.Reverse(integerAsBytes);
            }
            Array.Copy(integerAsBytes, 0, returnBytes, 0, returnByteCount);
            
            // Now, if we want the output to be in Big Endian, we have to flip again
            if(bigEndian) {
                Array.Reverse(returnBytes);
            }
            return returnBytes;
        }

        public static int ReadBinaryEncodedDecimal(byte[] bytes, int offset, int length, bool bigEndian = true) {
            int byteVal = 0;
            int bytesRead = 0;

            // We choose to read the smallest bytes first
            for(int byteNum = 0; byteNum < length; byteNum++) {
                byte currentByte;
                if(bigEndian) {
                    currentByte = bytes[offset + length - byteNum - 1];
                }
                else {
                    currentByte = bytes[offset + byteNum];
                }

                bytesRead += 1;
                if(bytesRead > 4) {
                    if (currentByte != 0) {
                        throw new OverflowException("To prevent overflows, you can only read 4 bytes of data.");
                    }
                }
                else {
                    string currentByteHex = currentByte.ToString("X");
                    if (int.TryParse(currentByteHex, out int currentByteValue)) {
                        byteVal += currentByteValue * Convert.ToInt32(Math.Pow(100, byteNum));
                    }
                    else {
                        throw new ArgumentOutOfRangeException("Found non-numeric byte: " + currentByteHex);
                    }
                }
            }

            return byteVal;
        }

        public static byte[] ReadIntegerToBED(int value, int forcedByteCount = 0) {
            if (value < 0) {
                throw new ArgumentOutOfRangeException("Only positive values can be converted to bytes");
            }

            byte[] returnBytes;
            string integerAsString = value.ToString();
            if(integerAsString.Length % 2 == 1) {
                // We have an odd number of digits.  We will need to prepend a zero
                integerAsString = "0" + integerAsString;
            }

            int sigByteCount = integerAsString.Length / 2;
            int returnByteCount;

            if (forcedByteCount < 0) {
                throw new ArgumentOutOfRangeException("Valid values for forcedByteCount are non-negative");
            }
            else if (forcedByteCount == 0) {
                returnByteCount = sigByteCount;
            }
            else if (forcedByteCount < sigByteCount) {
                throw new ArgumentOutOfRangeException(String.Format("You cannot squish {0} bytes into {1} bytes", sigByteCount, forcedByteCount));
            }
            else {
                returnByteCount = forcedByteCount;
                int zerosToAdd = (returnByteCount * 2) - integerAsString.Length;
                integerAsString = new string('0', zerosToAdd) + integerAsString;
            }
            returnBytes = new byte[returnByteCount];

            for(int i = 0; i < returnByteCount; i++) {
                string thisByteHex = integerAsString.Substring(i*2, 2);
                returnBytes[i] = Convert.ToByte(thisByteHex, 16);
            }

            return returnBytes;
        }
    }
}
