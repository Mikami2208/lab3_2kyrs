

class Program
{
    static void Main(string[] args)
    {
        string folderPath = "/Users/artemdwightm1/Documents/projects/Новая папка";
        List<string> noFileList = new List<string>();
        List<string> badFileList = new List<string>();
        List<string> overflowList = new List<string>();

        int sumOfProducts = 0;
        int count = 0;

        List<string> fileNames = new List<string>();

        try
        {
            File.WriteAllText(folderPath + "/no_file.txt", string.Empty);
            File.WriteAllText(folderPath + "/bad_file.txt", string.Empty);
            File.WriteAllText(folderPath + "/overflow_file.txt", string.Empty);

            for(int i = 0; i <= 19; i++)
            {
                fileNames.Add(folderPath + $"/{i+10}.txt");
                try
                {
                    string[] numbers = File.ReadAllLines(fileNames[i]);

                    int line1 = int.Parse(numbers[0]);
                    int line2 = int.Parse(numbers[1]);

                    long multiply = line1 * line2;
                    int sumInt = checked((int)multiply);

                    sumOfProducts += sumInt;
                    count++;

                }
                catch (IndexOutOfRangeException)
                {
                    badFileList.Add(fileNames[i]);
                }
                catch (FileNotFoundException)
                {
                    noFileList.Add(fileNames[i]);
                }
                catch (FormatException)
                {
                    badFileList.Add(fileNames[i]);
                }
                catch (OverflowException)
                {
                    overflowList.Add(fileNames[i]);
                }
                
            }
            File.WriteAllLines(folderPath + "/no_file.txt", noFileList);
            File.WriteAllLines(folderPath + "/bad_file.txt", badFileList);
            File.WriteAllLines(folderPath + "/overflow_file.txt", overflowList);
           
            double average = (double)sumOfProducts / count;
            Console.WriteLine(average);
            
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Неможливо створити або оновити файл no_file.txt, bad_data.txt або overflow.txt.");
            return;
        }


    }
}