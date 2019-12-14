using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public class GameModel {
        /// <summary>
        /// Represents this game's trainer name
        /// </summary>
        public string TrainerName { get; set; }
        /// <summary>
        /// Represents this game's trainer gender
        /// </summary>
        public int TrainerGender { get; set; }
        /// <summary>
        /// Represents this game's trainer ID
        /// </summary>
        public int TrainerID { get; set; }
        /// <summary>
        /// Represents this game's rival name
        /// </summary>
        public string RivalName { get; set; }
        /// <summary>
        /// Represents the hours played in this game
        /// </summary>
        public int HoursPlayed { get; set; }
        /// <summary>
        /// Represents the minutes played in this game
        /// </summary>
        public int MinsPlayed { get; set; }
        /// <summary>
        /// Represents the seconds played in this game
        /// </summary>
        public int SecsPlayed { get; set; }
        /// <summary>
        /// Represents the frames played in this game
        /// </summary>
        public int FramesPlayed { get; set; }
        /// <summary>
        /// Represents the secret key for this game
        /// </summary>
        public int SecretKey { get; set; }
        /// <summary>
        /// Represents the number of PokeDollars in this game
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// Represents the number of slot coins in this game
        /// </summary>
        public int Coins { get; set; }
        /// <summary>
        /// Represents the Pokedex entries for this game
        /// </summary>
        public List<PokedexEntryModel> Pokedex { get; set; }
        /// <summary>
        /// Represents the player's bag inventory for this game
        /// </summary>
        public List<ItemModel> Inventory { get; set; }
        /// <summary>
        /// Represents the player's PC inventory for this game
        /// </summary>
        public List<ItemModel> PCInventory { get; set; }
        /// <summary>
        /// Represents the player's party Pokemon for this game
        /// </summary>
        public List<PokemonModel> PartyPokemon { get; set; }

    }
}
