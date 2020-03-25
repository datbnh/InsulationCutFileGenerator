using Microsoft.Win32;
using System;
using System.Text;

namespace InsulationCutFileGeneratorMVC
{
    public class Settings
    {
        public const string REGISTRY_KEY_PATH = @"SOFTWARE\Augustine Software\Mega HVAC\";
        public bool IsSingleEntry;
        public bool IsUseFeMaleInstead;
        public string PasswordHash;
        public PittsburghSixMmValidationMode PittsburgSixMmValidationMode;
        public string PredefinedFileName = "STRAIGHT.CUT";
        public string PredefinedPath = @".\";
        public bool UsePredefinedFileName;
        public bool UsePredefinedPath;
        private static Settings instance;
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Settings()
        {
        }

        private Settings()
        {
        }

        public static Settings DefaultSettings
        {
            get => new Settings()
            {
                IsSingleEntry = false,
                IsUseFeMaleInstead = false,
                PittsburgSixMmValidationMode = PittsburghSixMmValidationMode.Enforcing,
                UsePredefinedFileName = true,
                UsePredefinedPath = true,
                PredefinedFileName = "STRAIGHT.CUT",
                PredefinedPath = @".\",
                PasswordHash = ""
            };
        }

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                    LoadSettingsFromRegistry();
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        public static void LoadSettingsFromRegistry()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_PATH);

            instance = DefaultSettings;
            if (key == null)
                return;

            instance = new Settings();
            try
            {
                instance.IsSingleEntry
                    = bool.Parse((string)key.GetValue(nameof(instance.IsSingleEntry)));
            }
            catch (Exception) { }

            try
            {
                instance.IsUseFeMaleInstead
                = bool.Parse((string)key.GetValue(nameof(instance.IsUseFeMaleInstead)));
            }
            catch (Exception) { }

            try
            {
                instance.PittsburgSixMmValidationMode
                    = (PittsburghSixMmValidationMode)Enum.Parse(typeof(PittsburghSixMmValidationMode), (string)key.GetValue(nameof(instance.PittsburgSixMmValidationMode)));
            }
            catch (Exception) { }

            try
            {
                instance.UsePredefinedFileName
                = bool.Parse((string)key.GetValue(nameof(instance.UsePredefinedFileName)));
            }
            catch (Exception) { }

            try
            {
                instance.UsePredefinedPath
                = bool.Parse((string)key.GetValue(nameof(instance.UsePredefinedPath)));
            }
            catch (Exception) { }

            try
            {
                instance.PredefinedFileName
                = (string)key.GetValue(nameof(instance.PredefinedFileName));
            }
            catch (Exception) { }

            try
            {
                instance.PredefinedPath
                = ((string)key.GetValue(nameof(instance.PredefinedPath))).Replace("/", "\\");
            }
            catch (Exception) { }

            try
            {
                instance.PasswordHash
                = ((string)key.GetValue(nameof(instance.PasswordHash)));
            }
            catch (Exception) { }
        }

        public static void SaveSettingsToRegistry()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_PATH);

            key.SetValue(nameof(Instance.IsSingleEntry),
                Instance.IsSingleEntry);
            key.SetValue(nameof(Instance.IsUseFeMaleInstead),
                Instance.IsUseFeMaleInstead);
            key.SetValue(nameof(Instance.PittsburgSixMmValidationMode),
                Instance.PittsburgSixMmValidationMode);
            key.SetValue(nameof(Instance.UsePredefinedFileName),
                Instance.UsePredefinedFileName);
            key.SetValue(nameof(Instance.UsePredefinedPath),
                Instance.UsePredefinedPath);
            key.SetValue(nameof(Instance.PredefinedFileName),
                string.IsNullOrEmpty(Instance.PredefinedFileName) ? "" : Instance.PredefinedFileName);
            key.SetValue(nameof(Instance.PredefinedPath),
                string.IsNullOrEmpty(Instance.PredefinedPath) ? "" : Instance.PredefinedPath.Replace("\\", "/"));
            key.SetValue(nameof(Instance.PasswordHash),
                string.IsNullOrEmpty(Instance.PasswordHash) ? "" : Instance.PasswordHash);
            key.Close();
        }

        public static string GetPasswordHash(string password)
        {
            byte[] data = Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }

        public static void SavePassword(string password)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_PATH);
            Instance.PasswordHash = GetPasswordHash(password);
            key.SetValue(nameof(Instance.PasswordHash),
                string.IsNullOrEmpty(Instance.PasswordHash) ? "" : Instance.PasswordHash);
        }

    }
}