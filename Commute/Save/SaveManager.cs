namespace Commute.Save
{
    using Commute.Platforms;
    using Newtonsoft.Json;
    using System.IO;

    /// <summary>
    /// The save manager.
    /// </summary>
    internal class SaveManager
    {
        /// <summary>
        /// The save file for machine data.
        /// </summary>
        private const string MachineFile = "fish.lcl";

        /// <summary>
        /// The singleton instance of the save manager.
        /// </summary>
        private static SaveManager saveManager;

        /// <summary>
        /// The game save data.
        /// </summary>
        private GameSaveData gameData;

        /// <summary>
        /// The save data for the current machine.
        /// </summary>
        private MachineSaveData machineData;

        /// <summary>
        /// A private constructor.
        /// </summary>
        private SaveManager()
        {
            saveManager = this;

            Load();
        }

        /// <summary>
        /// Initialise the save manager.
        /// </summary>
        public static void Initialise()
        {
            if (saveManager == null)
            {
                new SaveManager();
            }
        }

        /// <summary>
        /// The game data.
        /// </summary>
        public static GameSaveData GameData => saveManager.gameData;

        /// <summary>
        /// The machine data.
        /// </summary>
        public static MachineSaveData MachineData => saveManager.machineData;

        /// <summary>
        /// Save the game.
        /// </summary>
        public static void Save()
        {
            try
            {
                // Serialise the data into strings
                string gameData = JsonConvert.SerializeObject(saveManager.gameData);

                string machineData = JsonConvert.SerializeObject(saveManager.machineData);

                // Save using the platform's save method
                PlatformManager.Platform.SaveData(gameData);

                // Save the machine data
                using (StreamWriter streamWriter = new StreamWriter(MachineFile))
                {
                    streamWriter.Write(machineData);
                }
            }
            catch
            {
                // todo: display message
            }
        }

        /// <summary>
        /// Load the data.
        /// </summary>
        public static void Load()
        {
            // Load using the platform's load method
            saveManager.gameData = PlatformManager.Platform.LoadGameData();

            // If a machine file exists, then load it
            if (File.Exists(MachineFile))
            {
                try
                {
                    using (StreamReader streamReader = new StreamReader(MachineFile))
                    {
                        saveManager.machineData = JsonConvert.DeserializeObject<MachineSaveData>(streamReader.ReadToEnd());
                    }
                }
                catch
                {
                    // todo: display message
                }
            }
            else
            {
                saveManager.machineData = new MachineSaveData();
            }
        }
    }
}
