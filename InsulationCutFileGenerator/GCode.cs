using System;

namespace InsulationCutFileGenerator
{
    public static class GCode
    {
        public static string Init()
        {
            return "H210*" + Environment.NewLine + "G71*";
        }

        public static string LineBlock(int idx)
        {
            return string.Format("N{0:0}*", idx);
        }

        public static string AddText(string text)
        {
            return string.Format("M31*{0}*", text);
        }

        public static string MoveTo(long xMillimeters, long yMillimeters)
        {
            return string.Format("X{0:0}Y{1:0}*", xMillimeters * 10, yMillimeters * 10);
        }

        public static string KnifeDown()
        {
            return "M14*";
        }

        public static string KnifeUp()
        {
            return "M15*";
        }

        public static string Stop()
        {
            return "M0*";
        }
    }
}