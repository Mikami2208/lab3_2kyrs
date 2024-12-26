using SixLabors.ImageSharp;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp.Processing;

class Program
{
    static void Main(string[] args)
    {
        string folderPath = "/Users/artemdwightm1/Documents/Новая папка 2";
        Regex regexExtForImage = new Regex(@"^((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$", RegexOptions.IgnoreCase);

        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            string extension = Path.GetExtension(filePath).TrimStart('.').ToLower();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

            try
            {
                if (regexExtForImage.IsMatch(extension) && !fileNameWithoutExtension.EndsWith("-mirrored"))
                {
                    using (Image orgIm = Image.Load(filePath))
                    {
                        orgIm.Mutate(x => x.Flip(FlipMode.Horizontal));
                        string newFile = $"{folderPath}/{fileNameWithoutExtension}-mirrored.gif";
                        orgIm.Save(newFile);
                    }
                }
                else if (fileNameWithoutExtension.EndsWith("-mirrored"))
                {
                    Console.WriteLine($"Файл {filePath} вже відредагований");
                }
                else
                {
                    Console.WriteLine($"Файл {filePath} не є графічним.");
                }
            }
            catch (Exception ex)
            {
                if (regexExtForImage.IsMatch(extension))
                {
                    Console.WriteLine($"Файл {filePath} не містить картинку, хоча має графічне розширення:  {ex.Message}");
                }
                else
                {
                    Console.WriteLine($"Файл {filePath} нне є графічним.");
                }
            }
        }
    }
}
