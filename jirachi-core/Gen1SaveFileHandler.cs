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
            saveGame.Inventory = this.ReadInventory();
            saveGame.PCInventory = this.ReadPCInventory();

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

        private List<ItemModel> ReadInventory() {
            // In Generation 1, the bag inventory is stored in 0x2A bytes, starting at 0x25C9
            // The list format consists of a count byte at 0x00; 2 bytes for each item, which
            // represent item index and quantity; and a terminator byte 0xFF

            int itemCount = this.saveFileBytes[0x25C9];
            List<ItemModel> inventory = new List<ItemModel>(itemCount);

            for(int i = 0; i < itemCount; i++) {
                int offset = 0x25C9 + 1 + 2 * i;
                inventory.Add(new ItemModel(this.saveFileBytes[offset], this.saveFileBytes[offset + 1], 1, ItemPocket.ItemPocket));
            }

            return inventory;
        }

        private List<ItemModel> ReadPCInventory() {
            // In Generation 1, the PC inventory is stored in 0x68 bytes, starting at 0x27E6
            // The list format consists of a count byte at 0x00; 2 bytes for each item, which
            // represent item index and quantity; and a terminator byte 0xFF

            int itemCount = this.saveFileBytes[0x27E6];
            List<ItemModel> inventory = new List<ItemModel>(itemCount);

            for(int i = 0; i < itemCount; i++) {
                int offset = 0x27E6 + 1 + 2 * i;
                inventory.Add(new ItemModel(this.saveFileBytes[offset], this.saveFileBytes[offset + 1], 1, ItemPocket.ItemPocket));
            }

            return inventory;
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

        public static ItemModel ReadItemFromBytes(byte[] bytes) {
            if(bytes.Length != 2) {
                throw new ArgumentException("A Generation 1 item is only 2 bytes");
            }

            return new ItemModel(bytes[0], bytes[1], 1, ItemPocket.ItemPocket);
        }

        public static PokemonModel ReadPokemonFromBytes(byte[] bytes, PokemonLocation location, int index) {
            if(location == PokemonLocation.Daycare && index != 0) {
                throw new ArgumentOutOfRangeException("Only one daycare Pokemon in Gen 1");
            }

            // We need to create some holders for all of our properties, since
            // daycare v.s. party/box have different structures.

            byte[] pokemonDataBytes;

            int nationalDexNumber;
            string nickname;
            int level;
            int xp;
            int currentHP;
            List<MoveModel> moveset;
            StatusType status;
            int OTID;

            if(location == PokemonLocation.Daycare) {
                // Daycare data needs to be exactly 55 bytes long
                if(bytes.Length != 55) {
                    throw new ArgumentException("Daycare pokemon should be 55 bytes");
                }

                byte[] nicknameBytes = new byte[11];
                Array.Copy(bytes, nicknameBytes, 11);
                nickname = Gen1SaveFileHandler.PkmnTextDecode(nicknameBytes);

                pokemonDataBytes = new byte[33];
                Array.Copy(bytes, 22, pokemonDataBytes, 0, 33);
            }
            else {
                int dataLength;
                int nicknameStart;
                int dataStart;

                if(location == PokemonLocation.Box) {
                    // Box data needs to be exactly 1,142 bytes long
                    if(bytes.Length != 1142) {
                        throw new ArgumentException("You need to provide the full box data");
                    }
                    dataLength = 33;
                    nicknameStart = 0x386;
                    dataStart = 0x16;
                }
                else {
                    // Party data needs to be exactly 404 bytes long
                    if(bytes.Length != 404) {
                        throw new ArgumentException("You need to provide the full party data");
                    }
                    dataLength = 44;
                    nicknameStart = 0x152;
                    dataStart = 0x8;
                }

                byte[] nicknameBytes = new byte[11];
                Array.Copy(bytes, nicknameStart + (index * 0xB), nicknameBytes, 0, 11);
                nickname = Gen1SaveFileHandler.PkmnTextDecode(nicknameBytes);

                pokemonDataBytes = new byte[dataLength];
                Array.Copy(bytes, dataStart + (index * dataLength), pokemonDataBytes, 0, dataLength);
            }

            // national dex id
            nationalDexNumber = Gen1SaveFileHandler.ReadNationalDexNumberFromPkmnBytes(pokemonDataBytes);

            // current HP


            // return thisPokemon;
            return null;
        }

        public static int ReadNationalDexNumberFromPkmnBytes(byte[] bytes) {
            int gen1Index = bytes[0x0];

            int nationalDexId;
            // lookup... to do...
            return nationalDexId;
        }

        public static int ReadCurrentHPFromPkmnBytes(byte[] bytes) {
            int currentHP = ByteFunctions.ReadBytesToInteger(bytes, 0x1, 2);
            return currentHP;
        }

        public static int ReadLevelFromPkmnBytes(byte[] bytes) {
            int level = bytes[0x3];
            return level;
        }

        public static StatusType ReadStatusFromPkmnBytes(byte[] bytes) {
            int status = bytes[0x4];
            if (status == 0b0) {
                return StatusType.None;
            }
            else if (status == 0b100) {
                return StatusType.Sleep;
            }
            else if (status == 0b1000) {
                return StatusType.Poison;
            }
            else if (status == 0b10000) {
                return StatusType.Burn;
            }
            else if (status == 0b100000) {
                return StatusType.Freeze;
            }
            else if (status == 0b1000000) {
                return StatusType.Paralysis;
            }
            else {
                throw new ArgumentOutOfRangeException("Unknown value for status type");
            }
        }

        public static int ConvertIndexToNationalDex(int index) {
            // source: https://bulbapedia.bulbagarden.net/wiki/List_of_Pok%C3%A9mon_by_index_number_(Generation_I)

            Dictionary<int, int> NationalDexLookup = new Dictionary<int, int>();

            return 0; // stump
        }
    }
}
