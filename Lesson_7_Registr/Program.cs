using Microsoft.Win32;

namespace Lesson_7_Registr
{
    internal class Program
    {
        static void Main(string[] args)
        {

            RegistryKey currentUser = Registry.CurrentUser;
            foreach (string subKey in currentUser.GetSubKeyNames())
            {
                Console.WriteLine(subKey);
            }

            RegistryKey? exampleKey = currentUser.OpenSubKey("Example");
            if (exampleKey == null)
            {
                Console.WriteLine("Раздел в реестре не найден");
            }
            else
            {
                object? found = exampleKey.GetValue("EXAMPLE");
                if (found == null)
                {
                    Console.WriteLine("Значение в разделе не найдено");
                }
                else
                {
                    string value = found.ToString();
                    Console.WriteLine("Example.EXAMPLE = " + value);
                }
            }

        }
    }
}
