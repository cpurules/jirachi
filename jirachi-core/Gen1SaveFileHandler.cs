using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace jirachi_core {
    class Gen1SaveFileHandler : ISaveFileHandler {
        private byte[] saveFileBytes;

        public GameModel ReadSaveFile(string filePath) {
            if(!File.Exists(filePath)) {
                throw new FileNotFoundException("Could not locate file " + filePath);
            }

            // Generation 1 save files are 32KiB, or 2^15 bytes
            long fileLength = new System.IO.FileInfo(filePath).Length;
            if(fileLength != Math.Pow(2,15)) {
                throw new FormatException("File " + filePath + " is not a Gen 1 save file");
            }

            this.saveFileBytes = File.ReadAllBytes(filePath);

            
        }

        private int ReadMoney() {
            // In Generation 1, money is stored in 3 bytes, beginning at 0x25F3 and stored as binary-encoded decimal
            // (source:  https://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_I#Main_Data)
            int money = 0;
            for(int i = 0; i < 3; i++) {
                byte currentByte = this.saveFileBytes[0x25F3 + i];
                money += Convert.ToInt32(currentByte.ToString("X")) * Convert.ToInt32(Math.Pow(100, 2 - i));
            }

            return money;
        }

        private int ReadCoins() {
            // In Generation 1, slot coins are stored in 2 bytes, beginning at 0x2850 and stored as binary-encoded decimal
            // (source: https://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_I#Main_Data)
            int coins = 0;
            for(int i = 0; i < 2; i++) {
                byte currentByte = this.saveFileBytes[0x2850 + i];
                coins += Convert.ToInt32(currentByte.ToString("X")) * Convert.ToInt32(Math.Pow(100, 1 - i));
            }

            return coins;
        }

        public void WriteSaveFile(string filePath) {
            throw new NotImplementedException();
        }
    }
}
