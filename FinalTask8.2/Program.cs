namespace FinalTask8_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (Directory.Exists(args[0]))
            {
                DirectoryInfo dir = new(args[0]);
                
                Console.WriteLine($"размер каталога {dir.Name} - {ViewSize(DirSize(dir, true))}");
            }
            else
            {
                Console.WriteLine("наименование каталога указано неверно");
            }
        }

        /// <summary>
        /// Подсчет размера каталога
        /// </summary>
        /// <param name="dir"> путь к каталогу</param>
        /// <returns></returns>
        public static long DirSize(DirectoryInfo dir, bool islog)
        {
            long size = 0;

            if (!dir.Exists && islog) Console.WriteLine($"каталог {dir} отсутствует");

            FileInfo[] files = dir.GetFiles();
            foreach (var file in files)
            {                

                try
                {
                    size += file.Length;
                }
                catch (IOException)
                {
                    if (islog) Console.WriteLine($"файл {file} доступен только для чтения или используется");
                }
                catch (UnauthorizedAccessException)
                {
                    if (islog) Console.WriteLine($"у вас отсутствует доступ к файлу {file}");
                }

                if (islog) Console.WriteLine($"{file.Name} - {ViewSize(file.Length)}");
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (var d in dirs)
            {   
                DirSize(d, islog);
            }

            return size;
        }

        /// <summary>
        /// Вывод размера с размерностью
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ViewSize(long size)
        {
            return size switch
            {
                < 4000 => string.Concat(size.ToString(), " байт"),
                >= 4000 and < 4000000 => string.Concat(((double)size / 1024).ToString("0.00"), " Кбайт"),
                >= 4000000 and < 4000000000 => string.Concat(((double)size / 1048576).ToString("0.00"), " Мбайт"),
                >= 4000000000 => string.Concat(((double)size / 1073741824).ToString("0.00"), " Гбайт")
            };
        }
    }
}