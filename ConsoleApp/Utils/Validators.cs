using System.Text.RegularExpressions;

namespace ConsoleApp.Utils
{
    public static class Validators
    {
        private static readonly Regex nameRx = new Regex(@"^[A-Za-zА-Яа-яІіЇїЄєҐґ'\-]{1,50}$");
        private static readonly Regex studentIdRx = new Regex(@"^[A-Z]{2}\d{6}$", RegexOptions.IgnoreCase);
        private static readonly Regex residenceRx = new Regex(@"^\d+\.\d+$");
        public static bool ValidName(string s) => !string.IsNullOrWhiteSpace(s) && nameRx.IsMatch(s);
        public static bool ValidStudentId(string s) => !string.IsNullOrWhiteSpace(s) && studentIdRx.IsMatch(s);
        public static bool ValidResidence(string s) => string.IsNullOrWhiteSpace(s) ? true : residenceRx.IsMatch(s);
        public static bool ValidCourse(int c) => c >= 1 && c <= 6;
    }
}
