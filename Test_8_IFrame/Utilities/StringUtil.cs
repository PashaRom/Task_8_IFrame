using System;
using System.Linq;
namespace Test.Utilities
{
    public static class StringUtil
    {
        public static string GeneraterText(int lenght)
        {
            var random = new Random();
            return new String(Enumerable.Range(0,lenght).Select(n => (Char)(random.Next(65, 90))).ToArray());            
        }
    }
}
