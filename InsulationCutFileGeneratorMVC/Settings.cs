using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGeneratorMVC
{
    public class Settings
    {
        private static Settings instance;

        public bool IsSingleEntry;
        public bool IsUseFeMaleInstead;

        public PittsburghSixMmValidationMode PittsburgSixMmValidationMode;

        public bool UsePredefinedFileName;
        public bool UsePredefinedPath;

        public string PredefinedFileName = "STRAIGHT.CUT";
        public string PredefinedPath = @".\";


        public const string REGISTRY_KEY_PATH = @"SOFTWARE\Augustine Software\Mega HVAC\";

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Settings()
        {
        }

        private Settings()
        {
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
                PredefinedPath = @".\"
            };
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
                string.IsNullOrEmpty(Instance.PredefinedFileName)?"": Instance.PredefinedFileName);
            key.SetValue(nameof(Instance.PredefinedPath),
                string.IsNullOrEmpty(Instance.PredefinedPath)?"": Instance.PredefinedPath.Replace("\\","/"));

            key.Close();
        }
    }
}
