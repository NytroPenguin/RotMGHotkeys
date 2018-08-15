using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMGHotkeys
{
    public static class Config
    {
        public static string f1()
        {
            return Properties.Settings.Default.f1;
        }
        public static string f2()
        {
            return Properties.Settings.Default.f2;
        }
        public static string f3()
        {
            return Properties.Settings.Default.f3;
        }
        public static string f4()
        {
            return Properties.Settings.Default.f4;
        }
        public static string f5()
        {
            return Properties.Settings.Default.f5;
        }
        public static string f6()
        {
            return Properties.Settings.Default.f6;
        }
        public static string f7()
        {
            return Properties.Settings.Default.f7;
        }
        public static string f8()
        {
            return Properties.Settings.Default.f8;
        }
        public static void updateConfig(string f1, string f2, string f3, string f4, string f5, string f6, string f7, string f8)
        {
            Properties.Settings.Default.f1 = f1;
            Properties.Settings.Default.f2 = f2;
            Properties.Settings.Default.f3 = f3;
            Properties.Settings.Default.f4 = f4;
            Properties.Settings.Default.f5 = f5;
            Properties.Settings.Default.f6 = f6;
            Properties.Settings.Default.f7 = f7;
            Properties.Settings.Default.f8 = f8;
            Properties.Settings.Default.Save();
        }
    }
}
