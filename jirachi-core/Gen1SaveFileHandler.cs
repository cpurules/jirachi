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

        public void WriteSaveFile(string filePath) {
            throw new NotImplementedException();
        }
    }
}
