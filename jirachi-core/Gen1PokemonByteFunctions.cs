using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public static class Gen1PokemonByteFunctions {
        public static int ReadNationalDexNumberFromPkmnBytes(byte[] bytes) {
            int gen1Index = bytes[0x0];

            int nationalDexId = 0;
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

        public static List<MoveModel> ReadMovesetFromPkmnBytes(byte[] bytes) {
            // Move indexes are stored 0x08-0x0B and PP is stored at 0x1D-0x20

            List<MoveModel> moveset = new List<MoveModel>();
            for (int move = 0; move < 4; move++) {
                int moveIndex = bytes[0x08 + move];
                if (moveIndex == 0) {
                    break;
                }

                int movePP = bytes[0x1D + move];
                int currentPP = movePP & 0b111111;
                int ppUps = (movePP & 0b11000000) >> 6;

                moveset.Add(new MoveModel(moveIndex, currentPP, ppUps));
            }

            return moveset;
        }

        public static int ReadOTIDFromPkmnBytes(byte[] bytes) {
            // OTID is at 0xC-0xD
            int OTID = ByteFunctions.ReadBytesToInteger(bytes, 0xC, 2);
            return OTID;
        }

        public static int ReadXPFromPkmnBytes(byte[] bytes) {
            // XP is at 0x0E-0x10
            int XP = ByteFunctions.ReadBytesToInteger(bytes, 0xE, 3);
            return XP;
        }

        public static int ConvertIndexToNationalDex(int index) {
            // source: https://bulbapedia.bulbagarden.net/wiki/List_of_Pok%C3%A9mon_by_index_number_(Generation_I)

            Dictionary<int, int> NationalDexLookup = new Dictionary<int, int>();

            return 0; // stump
        }
    }
}
