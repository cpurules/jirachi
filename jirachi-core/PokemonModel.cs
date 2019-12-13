﻿using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    class PokemonModel {
        /// <summary>
        /// Represents the national dex number of the Pokemon.
        /// </summary>
        public int NationalDexNumber { get; set; }
        /// <summary>
        /// Represents the Pokemon's nickname
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// Represents the Pokemon's current (actual) level
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Represents the Pokemon's current XP earned
        /// </summary>
        public int XP { get; set; }
        /// <summary>
        /// Represents the Pokemon's current HP
        /// </summary>
        public int CurrentHP { get; set; }
        /// <summary>
        /// Represents the Pokemon's moveset
        /// </summary>
        public List<MoveModel> Moveset { get; set; }
        /// <summary>
        /// Represents this Pokemon's held item
        /// </summary>
        public ItemModel HeldItem { get; set; }
    }
}
