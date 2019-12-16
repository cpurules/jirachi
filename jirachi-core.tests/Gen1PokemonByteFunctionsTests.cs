using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jirachi_core;
using Xunit;

namespace jirachi_core.tests {
    public class Gen1PokemonByteFunctionsTests {
        string pathToDemoSave = "C:\\Users\\cpuru\\Downloads\\TEST_BLUE_SAVE.sav";

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadCurrentHP() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            // Box 12 starts at 0x75EA
            byte[] magmarBox12 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x75EA + 0x16, magmarBox12, 0, 33);
            int magmarBox12Expected = 179;
            int magmarBox12Actual = Gen1PokemonByteFunctions.ReadCurrentHPFromPkmnBytes(magmarBox12);

            // Party starts at 0x2F2C
            byte[] mewPartySlot1 = new byte[44];
            Array.Copy(Gen1Save.saveFileBytes, 0x2F2C + 0x8, mewPartySlot1, 0, 44);
            int mewPartySlot1Expected = 260;
            int mewPartySlot1Actual = Gen1PokemonByteFunctions.ReadCurrentHPFromPkmnBytes(mewPartySlot1);

            Assert.Equal(magmarBox12Expected, magmarBox12Actual);
            Assert.Equal(mewPartySlot1Expected, mewPartySlot1Actual);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadLevel() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            // Box 6 starts at 0x55EA
            byte[] persianBox6 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x55EA + 0x16 + (33 * 2), persianBox6, 0, 33);
            int persianExpected = 70;
            int persianActual = Gen1PokemonByteFunctions.ReadLevelFromPkmnBytes(persianBox6);

            Assert.Equal(persianExpected, persianActual);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadStatus() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            // We're going to write these later...
            // PkHex doesn't display the status so will need to load up the game..
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadMoveset() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            byte[] magmarBox12 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x75EA + 0x16, magmarBox12, 0, 33);

            List<MoveModel> magmarBox12Expected = new List<MoveModel>();
            magmarBox12Expected.Add(new MoveModel(52, 25, 0));
            magmarBox12Expected.Add(new MoveModel(126, 5, 0));
            magmarBox12Expected.Add(new MoveModel(53, 15, 0));
            magmarBox12Expected.Add(new MoveModel(7, 15, 0));
            List<MoveModel> magmarBox12Actual = Gen1PokemonByteFunctions.ReadMovesetFromPkmnBytes(magmarBox12);


            byte[] mewPartySlot1 = new byte[44];
            Array.Copy(Gen1Save.saveFileBytes, 0x2F2C + 0x8, mewPartySlot1, 0, 44);

            List<MoveModel> mewPartyExpected = new List<MoveModel>();
            mewPartyExpected.Add(new MoveModel(105, 32, 3));
            mewPartyExpected.Add(new MoveModel(5, 32, 3));
            mewPartyExpected.Add(new MoveModel(94, 16, 3));
            mewPartyExpected.Add(new MoveModel(144, 16, 3));
            List<MoveModel> mewPartyActual = Gen1PokemonByteFunctions.ReadMovesetFromPkmnBytes(mewPartySlot1);

            Assert.Equal(magmarBox12Expected, magmarBox12Actual);
            Assert.Equal(mewPartyExpected, mewPartyActual);


            byte[] magmarDaycare = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x2D0B, magmarDaycare, 0, 33);

            List<MoveModel> magmarDaycareExpected = new List<MoveModel>();
            magmarDaycareExpected.Add(new MoveModel(52, 25, 0));
            List<MoveModel> magmarDaycareActual = Gen1PokemonByteFunctions.ReadMovesetFromPkmnBytes(magmarDaycare);

            Assert.Equal(magmarDaycareExpected, magmarDaycareActual);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadOTID() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            byte[] magmarBox12 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x75EA + 0x16, magmarBox12, 0, 33);
            int magmarBoxExpected = 20893;
            int magmarBoxActual = Gen1PokemonByteFunctions.ReadOTIDFromPkmnBytes(magmarBox12);
            Assert.Equal(magmarBoxExpected, magmarBoxActual);

            byte[] machampBox12 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x75EA + 0x16 + 33, machampBox12, 0, 33);
            int machampBoxExpected = 17178;
            int machampBoxActual = Gen1PokemonByteFunctions.ReadOTIDFromPkmnBytes(machampBox12);
            Assert.Equal(machampBoxExpected, machampBoxActual);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadXP() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            byte[] magmarBox12 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x75EA + 0x16, magmarBox12, 0, 33);
            int magmarBoxExpected = 274925;
            int magmarBoxActual = Gen1PokemonByteFunctions.ReadXPFromPkmnBytes(magmarBox12);
            Assert.Equal(magmarBoxExpected, magmarBoxActual);

            byte[] persianBox6 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x55EA + 0x16 + (33 * 2), persianBox6, 0, 33);
            int persianExpected = 343000;
            int persianActual = Gen1PokemonByteFunctions.ReadXPFromPkmnBytes(persianBox6);
            Assert.Equal(persianExpected, persianActual);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadStats() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            byte[] mewPartySlot1 = new byte[44];
            Array.Copy(Gen1Save.saveFileBytes, 0x2F2C + 0x8, mewPartySlot1, 0, 44);

            StatModel mewHPExpected = new StatModel(StatType.HP, 260, 9, 25648);
            StatModel mewAtkExpected = new StatModel(StatType.Attack, 191, 13, 25648);
            StatModel mewDefExpected = new StatModel(StatType.Defense, 178, 4, 25648);
            StatModel mewSpdExpected = new StatModel(StatType.Speed, 192, 14, 25648);
            StatModel mewSpcExpected = new StatModel(StatType.Special, 188, 11, 25648);

            List<StatModel> mewStatsActual = Gen1PokemonByteFunctions.ReadStatsFromPkmnBytes(mewPartySlot1);

            // Gen 1 stats byte order is HP, Atk, Def, Spd, Spc
            Assert.Equal(mewHPExpected, mewStatsActual[0]);
            Assert.Equal(mewAtkExpected, mewStatsActual[1]);
            Assert.Equal(mewDefExpected, mewStatsActual[2]);
            Assert.Equal(mewSpdExpected, mewStatsActual[3]);
            Assert.Equal(mewSpcExpected, mewStatsActual[4]);
        }
    }
}
