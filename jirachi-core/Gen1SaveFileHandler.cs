using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace jirachi_core {
    public class Gen1SaveFileHandler : ISaveFileHandler {
        private byte[] saveFileBytes;

        public Gen1SaveFileHandler(string filePath) {
            if(!File.Exists(filePath)) {
                throw new FileNotFoundException("Could not locate file " + filePath);
            }

            // Generation 1 save file are 32KiB, or 2^15 bytes
            long fileLength = new System.IO.FileInfo(filePath).Length;
            if(fileLength != Math.Pow(2, 15)) {
                throw new FormatException("File " + filePath + " is not a Gen 1 save file");
            }

            this.saveFileBytes = File.ReadAllBytes(filePath);
        }

        public GameModel ReadSaveFile() {
            GameModel saveGame = new GameModel();

            saveGame.TrainerID = this.ReadTrainerID();
            saveGame.Money = this.ReadMoney();
            saveGame.Coins = this.ReadCoins();
            saveGame.HoursPlayed = this.ReadHoursPlayed();
            saveGame.MinsPlayed = this.ReadMinsPlayed();
            saveGame.SecsPlayed = this.ReadSecsPlayed();
            saveGame.FramesPlayed = this.ReadFramesPlayed();
            saveGame.RivalName = this.ReadRivalName();
            saveGame.TrainerName = this.ReadTrainerName();

            return saveGame;
        }

        private int ReadMoney() {
            // In Generation 1, money is stored in 3 bytes, beginning at 0x25F3 and stored as binary-encoded decimal
            // (source:  https://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_I#Main_Data)
            int money = ByteFunctions.ReadBinaryEncodedDecimal(this.saveFileBytes, 0x25F3, 3, true);

            return money;
        }

        private int ReadCoins() {
            // In Generation 1, slot coins are stored in 2 bytes, beginning at 0x2850 and stored as binary-encoded decimal
            // (source: https://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_I#Main_Data)
            int coins = ByteFunctions.ReadBinaryEncodedDecimal(this.saveFileBytes, 0x2850, 2, true);

            return coins;
        }

        private int ReadTrainerID() {
            // In Generation 1, player ID is stored in 2 bytes, beginning at 0x2605
            // (source: https://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_I#Main_Data)
            int trainerId = ByteFunctions.ReadBytesToInteger(this.saveFileBytes, 0x2605, 2);

            return trainerId;
        }

        private int ReadHoursPlayed() {
            // In Generation 1, hours played is stored in 1 byte at 0x2CED
            int hoursPlayed = ByteFunctions.ReadBytesToInteger(this.saveFileBytes, 0x2CED, 1);

            return hoursPlayed;
        }

        private int ReadMinsPlayed() {
            // In Generation 1, minutes played is stored in 1 byte at 0x2CEF
            int minsPlayed = ByteFunctions.ReadBytesToInteger(this.saveFileBytes, 0x2CEF, 1);

            return minsPlayed;
        }

        private int ReadSecsPlayed() {
            // In Generation 1, seconds played is stored in 1 byte at 0x2CF0
            int secsPlayed = ByteFunctions.ReadBytesToInteger(this.saveFileBytes, 0x2CF0, 1);

            return secsPlayed;
        }

        private int ReadFramesPlayed() {
            // In Generation 1, frames played is stored in 1 byte at 0x2CF1
            int framesPlayed = ByteFunctions.ReadBytesToInteger(this.saveFileBytes, 0x2CF1, 1);

            return framesPlayed;
        }

        private string ReadRivalName() {
            // In Generation 1, rival name is stored in 11 bytes starting at 0x25F6
            byte[] rivalNameBytes = new byte[11];
            Array.Copy(this.saveFileBytes, 0x25F6, rivalNameBytes, 0, 11);

            string rivalName = Gen1SaveFileHandler.PkmnTextDecode(rivalNameBytes);
            return rivalName;
        }

        private string ReadTrainerName() {
            // In Generation 1, trainer name is stored in 11 bytes starting at 0x2598
            byte[] trainerNameBytes = new byte[11];
            Array.Copy(this.saveFileBytes, 0x2598, trainerNameBytes, 0, 11);

            string trainerName = Gen1SaveFileHandler.PkmnTextDecode(trainerNameBytes);
            return trainerName;
        }

        public void WriteSaveFile(string filePath) {
            throw new NotImplementedException();
        }

        public static string PkmnTextDecode(byte[] bytes) {
            // Text is encoded using a propriety format and will require us to convert to English
            // (source: https://bulbapedia.bulbagarden.net/wiki/Character_encoding_in_Generation_I)
            Dictionary<int, string> EncodeLookup = new Dictionary<int, string>();

            // Encoding mappings for A-Z and a-z
            for(int i = 0x80; i < 0x99; i++) {
                EncodeLookup.Add(i, Char.ConvertFromUtf32(i - 63)); // for A-Z mapping
                EncodeLookup.Add(i + 0x20, Char.ConvertFromUtf32(i - 63)); // for a-z mapping
            }

            // Add in the additional special characters
            EncodeLookup.Add(0x9A, "(");
            EncodeLookup.Add(0x9B, ")");
            EncodeLookup.Add(0x9C, ":");
            EncodeLookup.Add(0x9D, ";");
            EncodeLookup.Add(0x9E, "[");
            EncodeLookup.Add(0x9F, "]");
            EncodeLookup.Add(0xE1, "{pk}");
            EncodeLookup.Add(0xE2, "{mn}");
            EncodeLookup.Add(0xE3, "-");
            EncodeLookup.Add(0xE5, "?");
            EncodeLookup.Add(0xE6, "!");
            EncodeLookup.Add(0xEF, "{male}");
            EncodeLookup.Add(0xF5, "{female}");
            EncodeLookup.Add(0xF3, "/");
            EncodeLookup.Add(0xF2, ".");
            EncodeLookup.Add(0xF4, ",");

            byte stringTerminator = 0x50;

            string decodedText = "";
            int curByteNum = 0;

            while(curByteNum < bytes.Length) {
                byte curByte = bytes[curByteNum];
                if(curByte == stringTerminator) {
                    break;
                }

                decodedText += EncodeLookup[Convert.ToInt32(curByte)];
                curByteNum += 1;
            }

            return decodedText;
        }
    }
}
