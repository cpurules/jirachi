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
    }

}
