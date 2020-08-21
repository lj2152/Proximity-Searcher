using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProximitySearch
{
    public class ProximitySearcher
    {
        #region PROPERTIES & SUBCLASSES
        public static string MessageToUser { get; set; }

        public class InputParams
        {
            public string[] WordsArray { get; set; }
            public string Keyword1 { get; set; }
            public string Keyword2 { get; set; }
            public int Range { get; set; }            
        }

        #endregion

        public static void Main(string[] args)
        {
            Console.WriteLine("Proximity Search\r\n-Please see the README.txt for detailed information about this program.\r\n-Currently available input files are sampleshort.txt & samplelong.txt\r\n-Minimum Range required is 2 (since we have 2 keywords) \r\n\r\nPlease enter the required params mentioned below with a space in between:\r\n\r\n<application_name> <keyword1> <keyword2> <range> <input_filename>\r\nExample: ProximitySearch.exe the canal 6 sampleshort.txt\r\n");
            
            //Get the inputs from the user based on the instructions given above
            args = Console.ReadLine().Split(' ');

            ProcessInputsFromUser(args);

            while (true)
            {
                Console.WriteLine("\r\nDo you want to try another search? (y/n)");
                var choice = Console.ReadLine();
                if (choice=="y")
                {
                    Console.WriteLine("Please enter search params as mentioned above");
                    args = Console.ReadLine().Split(' ');
                    ProcessInputsFromUser(args);
                }
                else
                {
                    Environment.Exit(0);
                }
            }

        }

        #region MAIN METHODS

        /// ProcessInputsFromUser Method Summary:
        /// If inputs are invalid, the MessageToUser gets set to the appropriate text depending on what makes the input params invalid.
        /// Build the InputParams class with all the valid params that the user has entered.
        /// In the Search method below, the MessageToUser variable will be set to the count calculated and will get displayed to the user below
        public static void ProcessInputsFromUser(string[] args)
        {
            while (!HasValidInputParams(args))
            {
                Console.WriteLine(MessageToUser);
                args = Console.ReadLine().Split(' ');
            }
            
            var inputParams = new InputParams()
            {
                Keyword1 = args[1],
                Keyword2 = args[2],
                Range = Int32.Parse(args[3]),
                WordsArray = GetContentFromFileAsWordsArray(args[4])
            };
            
            Search(inputParams);
            Console.WriteLine(MessageToUser);
        }

        /// Search Method Summary:
        /// Check if file has no content or keyword1 or keyword2 doesn't exist in the file.  If so, return a count of 0 since we don't need to go further.
        /// Loop through each word in the sample file (which we split into a wordsArray by splitting on space).
        /// If currentWord is Keyword1, we need to find Keyword2. If currentWord is Keyword2, we need to find Keyword1. If neither, set to empty string.
        /// If we have a valid KeyWordToSearch, then get the count for that and keep adding that to the existing count.
        /// Return the final count to the user            
        public static int Search(InputParams inputParams)
        {
            string[] wordsArray = inputParams.WordsArray;
            string keyWord1 = inputParams.Keyword1;
            string keyWord2 = inputParams.Keyword2;

            if (wordsArray.Count() == 0 || !wordsArray.Contains(keyWord1) || !wordsArray.Contains(keyWord2))
            {
                MessageToUser = "0";
                return -1; //this is not the value, we're just returning a number (-1) since the return type is an int (for doing proper unit tests)
            }

            int currentWordIndex = 0;
            int count = 0;

            try
            {
                foreach (string word in wordsArray)
                {
                    currentWordIndex++; //currentWordIndex starts at 1

                    var keyWordToSearch = word == keyWord1 ? keyWord2 : word == keyWord2 ? keyWord1 : "";

                    if (!string.IsNullOrWhiteSpace(keyWordToSearch))
                    {
                        count += GetCountForKeyWordToSearch(inputParams, keyWordToSearch, currentWordIndex);
                    }
                }

                MessageToUser = count.ToString();
            }
            catch(Exception ex)
            {
                MessageToUser = $"Please try again. Exception: {ex.Message} , Inner Exception: {ex.InnerException}";
            }          

            return count;
        }

        ///  GetCountForKeyWordToSearch Method Summary:
        ///  We want to limit the actualRange to the length of the WordsArray if the range exceeds it.
        ///  For the case of the inputData.WordsArray.Length-1, we do '-1' because currentWordIndex starts at 1 as seen in the Search Method above        
        public static int GetCountForKeyWordToSearch(InputParams inputData, string keywordToSearch,  int currentWordIndex)
        {
            int count = 0;

            try
            {
                int actualRange = Math.Min(currentWordIndex + inputData.Range, inputData.WordsArray.Length-1);

                for (int wordIndex = currentWordIndex; wordIndex < actualRange; wordIndex++)
                {
                    string currentWordInWordsArray = inputData.WordsArray[wordIndex];

                    if (currentWordInWordsArray == keywordToSearch)
                    {
                        count++;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex; //throwing to the catch of the calling method which will notify the user of the exception details.
                
            }

            return count;
        }

        #endregion

        #region OTHER HELPER METHODS
        public static bool HasValidInputParams(string[] args)
        {
            return HasAllRequiredParameters(args) && FileExists(args) && HasMinimumRange(args);
        }
        public static bool HasAllRequiredParameters(string[] args)
        {
            bool hasRequiredParams = args.Length == 5;

            if (!hasRequiredParams)
            {
                MessageToUser = "Please enter all required parameters with a space in between as mentioned above:";
            }

            return hasRequiredParams;
        }
        public static bool FileExists(string[] args)
        {
            var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), args[4]);
            var fileExists = File.Exists(pathToFile);

            if (!fileExists)
            {
                MessageToUser = $"\r\nFile Not Found -> Please check to make sure the file with the path {pathToFile} exists \r\n";
            }

            return fileExists;
        }
        public static bool HasMinimumRange(string[] args)
        {
            int.TryParse(args[3], out int range);
            bool hasValidRange = range >= 2;

            if (!hasValidRange)
            {
                MessageToUser = "Since there are 2 Keywords, minimum range is 2. Please provide a number greater than or equal to 2 \r\n";
            }

            return hasValidRange;
        }
        public static string[] GetContentFromFileAsWordsArray(string fileName)
        {
           
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var fileContentAsString = File.ReadAllText(filePath, Encoding.UTF8);
            var wordsArray = fileContentAsString.Split(' ');               

            return wordsArray;
        }

        #endregion

    }
}
