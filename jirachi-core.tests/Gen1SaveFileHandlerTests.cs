﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xunit;
using jirachi_core;

namespace jirachi_core.tests {
    public class Gen1SaveFileHandlerTests {
        string pathToDemoSave = "C:\\Users\\cpuru\\Downloads\\TEST_BLUE_SAVE.sav";
        // this demo save will always have the same data

        [Fact]
        public void Gen1SaveFileHandler_ShouldCreate() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldThrowFileNotFound() {
            Assert.Throws<FileNotFoundException>(() => new Gen1SaveFileHandler("THIS_IS_A_FAKE_PATH"));
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldThrowNotSave() {
            string pathToNonSaveFile = "C:\\key.txt";

            Assert.Throws<FormatException>(() => new Gen1SaveFileHandler(pathToNonSaveFile));
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadTrainerID() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            Assert.Equal(20893, Gen1Game.TrainerID);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadMoney() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            Assert.Equal(980399, Gen1Game.Money);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadCoins() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            Assert.Equal(50, Gen1Game.Coins);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadHours() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            Assert.Equal(42, Gen1Game.HoursPlayed);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadMins() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            Assert.Equal(42, Gen1Game.MinsPlayed);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadSecs() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            Assert.Equal(44, Gen1Game.SecsPlayed);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadFrames() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            Assert.Equal(44, Gen1Game.FramesPlayed);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadRivalName() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            Assert.Equal("GARY", Gen1Game.RivalName);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadTrainerName() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            Assert.Equal("ASH", Gen1Game.TrainerName);
        }

        [Theory]
        [InlineData(new byte[] { 0x0A, 0x63 }, 10, 99)]
        [InlineData(new byte[] { 0x01, 0x02 }, 1, 2)]
        [InlineData(new byte[] { 0x00, 0x00 }, 0, 0)]
        public void Gen1SaveFileHandler_ShouldReadItem(byte[] bytes, int expectedId, int expectedQuantity) {
            // Arrange

            // Act
            ItemModel actual = Gen1SaveFileHandler.ReadItemFromBytes(bytes);

            // Assert
            Assert.Equal(expectedId, actual.ItemId);
            Assert.Equal(expectedQuantity, actual.Quantity);

            // The below will be true for every generation 1 item
            Assert.Equal(1, actual.Generation);
            Assert.Equal(ItemPocket.ItemPocket, actual.Pocket);
        }

        [Theory]
        [InlineData(new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x1A, 0xFF, 0x3B })]
        public void Gen1SaveFileHandler_WrongByteCountShouldThrow(byte[] bytes) {
            Assert.Throws<ArgumentException>(() => Gen1SaveFileHandler.ReadItemFromBytes(bytes));
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadInventory() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            List<ItemModel> actual = Gen1Game.Inventory;
            ItemModel expectedFirst = new ItemModel(6, 1, 1, ItemPocket.ItemPocket); // Bicycle at top of inventory
            ItemModel expectedLast = new ItemModel(52, 2, 1, ItemPocket.ItemPocket); // 2 Full Heals at end of inventory
            ItemModel expectedSixth = new ItemModel(40, 99, 1, ItemPocket.ItemPocket); // 99 Rare Candies at position 6

            Assert.Equal(expectedFirst, actual[0]);
            Assert.Equal(expectedLast, actual[actual.Count - 1]);
            Assert.Equal(expectedSixth, actual[5]);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadPCInventory() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);
            GameModel Gen1Game = Gen1Save.ReadSaveFile();

            List<ItemModel> actual = Gen1Game.PCInventory;
            ItemModel expectedFirst = new ItemModel(212, 1, 1, ItemPocket.ItemPocket); // TM12 at top of PC inventory
            ItemModel expectedLast = new ItemModel(72, 1, 1, ItemPocket.ItemPocket); // Silph Scope at end of PC inventory
            ItemModel expected35th = new ItemModel(38, 4, 1, ItemPocket.ItemPocket); // 4 Carbos

            Assert.Equal(expectedFirst, actual[0]);
            Assert.Equal(expectedLast, actual[actual.Count - 1]);
            Assert.Equal(expected35th, actual[34]);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadCurrentHP() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            // Box 12 starts at 0x75EA
            byte[] magmarBox12 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x75EA + 0x16, magmarBox12, 0, 33);
            int magmarBox12Expected = 179;
            int magmarBox12Actual = Gen1SaveFileHandler.ReadCurrentHPFromPkmnBytes(magmarBox12);

            // Party starts at 0x2F2C
            byte[] mewPartySlot1 = new byte[44];
            Array.Copy(Gen1Save.saveFileBytes, 0x2F2C + 0x8, mewPartySlot1, 0, 44);
            int mewPartySlot1Expected = 260;
            int mewPartySlot1Actual = Gen1SaveFileHandler.ReadCurrentHPFromPkmnBytes(mewPartySlot1);

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
            int persianActual = Gen1SaveFileHandler.ReadLevelFromPkmnBytes(persianBox6);

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
            List<MoveModel> magmarBox12Actual = Gen1SaveFileHandler.ReadMovesetFromPkmnBytes(magmarBox12);


            byte[] mewPartySlot1 = new byte[44];
            Array.Copy(Gen1Save.saveFileBytes, 0x2F2C + 0x8, mewPartySlot1, 0, 44);

            List<MoveModel> mewPartyExpected = new List<MoveModel>();
            mewPartyExpected.Add(new MoveModel(105, 32, 3));
            mewPartyExpected.Add(new MoveModel(5, 32, 3));
            mewPartyExpected.Add(new MoveModel(94, 16, 3));
            mewPartyExpected.Add(new MoveModel(144, 16, 3));
            List<MoveModel> mewPartyActual = Gen1SaveFileHandler.ReadMovesetFromPkmnBytes(mewPartySlot1);

            Assert.Equal(magmarBox12Expected, magmarBox12Actual);
            Assert.Equal(mewPartyExpected, mewPartyActual);


            byte[] magmarDaycare = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x2D0B, magmarDaycare, 0, 33);

            List<MoveModel> magmarDaycareExpected = new List<MoveModel>();
            magmarDaycareExpected.Add(new MoveModel(52, 25, 0));
            List<MoveModel> magmarDaycareActual = Gen1SaveFileHandler.ReadMovesetFromPkmnBytes(magmarDaycare);

            Assert.Equal(magmarDaycareExpected, magmarDaycareActual);
        }

        [Fact]
        public void Gen1SaveFileHandler_ShouldReadOTID() {
            Gen1SaveFileHandler Gen1Save = new Gen1SaveFileHandler(this.pathToDemoSave);

            byte[] magmarBox12 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x75EA + 0x16, magmarBox12, 0, 33);
            int magmarBoxExpected = 20893;
            int magmarBoxActual = Gen1SaveFileHandler.ReadOTIDFromPkmnBytes(magmarBox12);
            Assert.Equal(magmarBoxExpected, magmarBoxActual);

            byte[] machampBox12 = new byte[33];
            Array.Copy(Gen1Save.saveFileBytes, 0x75EA + 0x16 + 33, machampBox12, 0, 33);
            int machampBoxExpected = 17178;
            int machampBoxActual = Gen1SaveFileHandler.ReadOTIDFromPkmnBytes(machampBox12);
            Assert.Equal(machampBoxExpected, machampBoxActual);
        }
    }
}
