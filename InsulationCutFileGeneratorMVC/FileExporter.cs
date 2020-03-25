using InsulationCutFileGeneratorMVC.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC
{
    public static class FileExporter
    {
        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        public static void Export(string data, string line2OnSuccessMessgaeBox)
        {
            string path;

            if (Settings.Instance.UsePredefinedPath && Settings.Instance.UsePredefinedFileName)
            {
                path = Path.GetFullPath(Path.Combine(Settings.Instance.PredefinedPath, Settings.Instance.PredefinedFileName));
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Cut Files (*.CUT)|*.CUT";
                saveFileDialog.FileName = "STRAIGHT";
                saveFileDialog.InitialDirectory = Path.GetFullPath(Settings.Instance.PredefinedPath);
                saveFileDialog.RestoreDirectory = true;

                if (Settings.Instance.UsePredefinedPath) // && !Settings.Instance.UsePredefinedFileName
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //path = Path.GetFullPath(@".\STRAIGHT.CUT");

                        if (!
                            NormalizePath(Path.GetFullPath(Path.GetDirectoryName(saveFileDialog.FileName))).Equals(
                            NormalizePath(Path.GetFullPath(Settings.Instance.PredefinedPath))))
                        {
                            System.Windows.MessageBox.Show("Cut file must be saved in "
                                + Path.GetFullPath(Settings.Instance.PredefinedPath),
                                "Invalid Directory",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        {
                            path = Path.GetFullPath(saveFileDialog.FileName);
                        }

                    }
                    else
                    {
                        return;
                    }
                }
                else if (Settings.Instance.UsePredefinedFileName)
                {
                    saveFileDialog.Filter = "Directory | directory";
                    saveFileDialog.FileName = "Save To This Directory";
                    saveFileDialog.CheckFileExists = false;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        path = Path.GetFullPath(Path.Combine(
                            Path.GetDirectoryName(saveFileDialog.FileName), Settings.Instance.PredefinedFileName));
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        path = Path.GetFullPath(saveFileDialog.FileName);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            try
            {
                System.IO.File.WriteAllText(path, data);
                TimedMessageBoxLoader.ShowTimedMessageBox(path, line2OnSuccessMessgaeBox, 15);
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show("Error saving file. "
                    + ex.Message
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Ensure the file " + path + " is closed and try again.",
                    "Error Saving File",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
