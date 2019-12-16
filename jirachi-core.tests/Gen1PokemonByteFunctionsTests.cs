using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jirachi_core;
using Xunit;

namespace jirachi_core.tests {
    public class Gen1PokemonByteFunctionsTests {
        private byte[] mewPartySlot1 = new byte[44];
        private byte[] persianBox6 = new byte[33];
        private byte[] machampBox12 = new byte[33];
        private byte[] magmarBox12 = new byte[33];
        private byte[] magmarDaycare = new byte[33];
        private string pathToDemoSave = "C:\\Users\\cpuru\\Downloads\\TEST_BLUE_SAVE.sav";

        public Gen1PokemonByteFunctionsTests() {
            string pathToDemoSave = "C:\\Users\\cpuru\\Downloads\\TEST_BLUE_SAVE.sav";
            Gen1SaveFileHandler demoSave = new Gen1SaveFileHandler(pathToDemoSave);
            byte[] demoSaveBytes = demoSave.saveFileBytes;

            Array.Copy(demoSaveBytes, 0x2F2C + 0x08, this.mewPartySlot1, 0, 44); // mew party slot 1
            Array.Copy(demoSaveBytes, 0x55EA + 0x16 + (33 * 2), this.persianBox6, 0, 33); // persian box 6
            Array.Copy(demoSaveBytes, 0x75EA + 0x16 + 33, this.machampBox12, 0, 33); // machamp box 12
            Array.Copy(demoSaveBytes, 0x75EA + 0x16, this.magmarBox12, 0, 33); // magmar box 12
            Array.Copy(demoSaveBytes, 0x2D0B, this.magmarDaycare, 0, 33); // magmar in daycare
        }        

        [Fact]
        public void Gen1PkmnByteFunctions_ShouldReadCurrentHP() {
            // Arrange
            int mewPartySlot1Expected = 260;
            int magmarBox12Expected = 179;

            // Act
            int mewPartySlot1Actual = Gen1PokemonByteFunctions.ReadCurrentHPFromPkmnBytes(this.mewPartySlot1);
            int magmarBox12Actual = Gen1PokemonByteFunctions.ReadCurrentHPFromPkmnBytes(this.magmarBox12);

            // Assert
            Assert.Equal(mewPartySlot1Expected, mewPartySlot1Actual);
            Assert.Equal(magmarBox12Expected, magmarBox12Actual);
        }

        [Fact]
        public void Gen1PkmnByteFunctions_ShouldReadLevel() {
            // Arrange
            int persianBox6Expected = 70;

            // Act
            int persianBox6Actual = Gen1PokemonByteFunctions.ReadLevelFromPkmnBytes(this.persianBox6);

            Assert.Equal(persianBox6Expected, persianBox6Actual);
        }

        [Fact]
        public void Gen1PkmnByteFunctions_ShouldReadStatus() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            // We're going to write these later...
            // PkHex doesn't display the status so will need to load up the game..
        }

        [Fact]
        public void Gen1PkmnByteFunctions_ShouldReadMoveset() {
            // Arrange
            List<MoveModel> magmarBox12Expected = new List<MoveModel>();
            magmarBox12Expected.Add(new MoveModel(52, 25, 0));
            magmarBox12Expected.Add(new MoveModel(126, 5, 0));
            magmarBox12Expected.Add(new MoveModel(53, 15, 0));
            magmarBox12Expected.Add(new MoveModel(7, 15, 0));

            List<MoveModel> mewPartyExpected = new List<MoveModel>();
            mewPartyExpected.Add(new MoveModel(105, 32, 3));
            mewPartyExpected.Add(new MoveModel(5, 32, 3));
            mewPartyExpected.Add(new MoveModel(94, 16, 3));
            mewPartyExpected.Add(new MoveModel(144, 16, 3));

            List<MoveModel> magmarDaycareExpected = new List<MoveModel>();
            magmarDaycareExpected.Add(new MoveModel(52, 25, 0));

            // Act
            List<MoveModel> mewPartyActual = Gen1PokemonByteFunctions.ReadMovesetFromPkmnBytes(this.mewPartySlot1);
            List<MoveModel> magmarBox12Actual = Gen1PokemonByteFunctions.ReadMovesetFromPkmnBytes(this.magmarBox12);
            List<MoveModel> magmarDaycareActual = Gen1PokemonByteFunctions.ReadMovesetFromPkmnBytes(this.magmarDaycare);

            // Assert
            Assert.Equal(magmarBox12Expected, magmarBox12Actual);
            Assert.Equal(mewPartyExpected, mewPartyActual);
            Assert.Equal(magmarDaycareExpected, magmarDaycareActual);
        }

        [Fact]
        public void Gen1PkmnByteFunctions_ShouldReadOTID() {
            // Arrange
            int magmarBox12Expected = 20893;
            int machampBox12Expected = 17178;

            // Act
            int magmarBox12Actual = Gen1PokemonByteFunctions.ReadOTIDFromPkmnBytes(this.magmarBox12);
            int machampBox12Actual = Gen1PokemonByteFunctions.ReadOTIDFromPkmnBytes(this.machampBox12);

            // Assert
            Assert.Equal(magmarBox12Expected, magmarBox12Actual);
            Assert.Equal(machampBox12Expected, machampBox12Actual);
        }

        [Fact]
        public void Gen1PkmnByteFunctions_ShouldReadXP() {
            // Arrange
            int persianBox6Expected = 343000;
            int magmarBox12Expected = 274925;

            // Act
            int persianBox6Actual = Gen1PokemonByteFunctions.ReadXPFromPkmnBytes(this.persianBox6);
            int magmarBox12Actual = Gen1PokemonByteFunctions.ReadXPFromPkmnBytes(this.magmarBox12);

            // Assert
            Assert.Equal(persianBox6Expected, persianBox6Actual);
            Assert.Equal(magmarBox12Expected, magmarBox12Actual);
        }

        [Fact]
        public void Gen1PkmnBytefunctions_ShouldReadStats() {
            // Arrange
            List<StatModel> mewPartySlot1Expected = new List<StatModel>();
            mewPartySlot1Expected.Add(new StatModel(StatType.HP, 260, 9, 25648));
            mewPartySlot1Expected.Add(new StatModel(StatType.Attack, 191, 13, 25648));
            mewPartySlot1Expected.Add(new StatModel(StatType.Defense, 178, 4, 25648));
            mewPartySlot1Expected.Add(new StatModel(StatType.Speed, 192, 14, 25648));
            mewPartySlot1Expected.Add(new StatModel(StatType.Special, 188, 11, 25648));

            List<StatModel> magmarDaycareExpected = new List<StatModel>();
            magmarDaycareExpected.Add(new StatModel(StatType.HP, 11, 0));
            magmarDaycareExpected.Add(new StatModel(StatType.Attack, 1, 0));
            magmarDaycareExpected.Add(new StatModel(StatType.Defense, 0, 0));
            magmarDaycareExpected.Add(new StatModel(StatType.Speed, 15, 0));
            magmarDaycareExpected.Add(new StatModel(StatType.Special, 7, 0));

            // Act
            List<StatModel> mewPartySlot1Actual = Gen1PokemonByteFunctions.ReadStatsFromPkmnBytes(this.mewPartySlot1);
            List<StatModel> magmarDaycareActual = Gen1PokemonByteFunctions.ReadStatsFromPkmnBytes(this.magmarDaycare);

            // Assert
            Assert.Equal(mewPartySlot1Expected, mewPartySlot1Actual);
            Assert.Equal(magmarDaycareExpected, magmarDaycareActual);
        }
    }
}
