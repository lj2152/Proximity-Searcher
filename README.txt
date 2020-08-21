---------------------README---------------------

SOURCE CODE:
ProximitySearch Solution -> Has 2 projects. 
1) Main Project (ProximitySearch)
2) UnitTests Project

RUNNING INSTRUCTIONS
Program can be run by following the instructions on the Console, which is to enter the params in the below format:
<application_name> <keyword1> <keyword2> <range> <input_filename>
Example: ProximitySearch.exe the canal 6 sampleshort.txt

FILE INFORMATION:
The 2 files included are sampleshort.txt and samplelong.txt. They are in the bin\Debug folder for both the Main Project (ProxmitySearch) and UnitTests Project.

Content of sampleshort.txt:
the man the plan the canal Panama 

Content of samplelong.txt:
the man the plan the canal panama panama canal the plan the man the the man the plan the canal panama   

ERROR HANDLING:
Error handling is done for the below & the appropriate message gets displayed to the user conveying that information:
1) if all required params are entered.
2) if the file exists
3) if the range is atleast 2 since we have 2 keywords.

ALGORITHM:
We loop through each word in the wordsArray (which is words from the file)
When we find the 1st keyword, we try to search for the 2nd keyword within the range provided and we get the count.
When we find the 2nd keyword, we try to search for the 1st keyword within the range provided and we get the count & add it to the previous count.
We repeat the process again for the rest of words in the file.
Finally we return the total count to the user.
Extra details about the 2 functions/methods used to implement this algorithm is provided in the method summary above each of the 2 methods in the code.

COMPLEXITY:
Worst Case Time Complexity: O(N ^ 2) where N is the number of words that are in the file. This is worst case if we are given a range that's the length of the total number of words in the file.
Worst Case Space Complexity: O(N) where N is the number of words that are in the file.


TEST CASES:
Please see the UnitTests project. All methods have test cases implemented to ensure it's working correctly. 
Testcases also include the sample inputs with expected outputs tested to make sure the results returned are correct.


Thanks for using ProximitySearch!