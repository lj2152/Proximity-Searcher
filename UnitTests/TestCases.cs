using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProximitySearch;

namespace UnitTests
{
    [TestClass]
    public class TestCases
    {
        [TestMethod]
        public void Test_HasAllRequiredParameters()
        {
            var args = new string[] { "ProximitySearch.exe", "the",  "canal" ,  "6",  "sampleshort.txt" };
            var hasAllRequiredParams = ProximitySearcher.HasAllRequiredParameters(args);

            Assert.AreEqual(hasAllRequiredParams, true);
        }


        [TestMethod]
        public void Test_FileExists()
        {
            var args = new string[] { "ProximitySearch.exe", "the", "canal", "6", "sampleshort.txt" };
            var fileExists = ProximitySearcher.FileExists(args);

            Assert.AreEqual(fileExists, true);
        }

        [TestMethod]
        public void Test_HasMinimumRange()
        {
            var args = new string[] { "ProximitySearch.exe", "the", "canal", "1", "sample.txt" };
            var fileExists = ProximitySearcher.HasMinimumRange(args);

            //we're passing 1 for the range which is less than our minimum range which is 2. So we expect this method to return false
            Assert.AreEqual(fileExists, false);
        }

        [TestMethod]
        public void Test_GetContentFromFileAsWordsArray()
        {
            var fileName = "sampleshort.txt";  

            //we know the fileName that gets passed to this method would be valid because we wouldn't call this method in our program if we didn't have a valid file 
            var hasWords = ProximitySearcher.GetContentFromFileAsWordsArray(fileName).Count() > 0;

            Assert.AreEqual(hasWords, true);
        }

        [TestMethod]
        public void Test_GetCountForKeyWordToSearch()
        {
            var inputParams = new ProximitySearcher.InputParams
            {
                Keyword1 = "the",
                Keyword2 = "canal",
                Range = Int32.Parse("6"),
                WordsArray = ProximitySearcher.GetContentFromFileAsWordsArray("sampleshort.txt")
            };

            //sampleshort.txt has this content->  the man the plan the canal Panama             
            // We run with this > ProxmitySearch.exe the canal 6 sampleshort.txt
            // If we find Keyword1 (the),  then we're trying to find Keyword2 (canal)
            //  we start at 1 as the currentWordIndex
            //  we should find just 1 occurence (count) the first time, which is what the below test does

            int count = ProximitySearcher.GetCountForKeyWordToSearch(inputParams, "canal", 1);

            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void Test_Search_WithSample1A()
        {
            var inputParams = new ProximitySearcher.InputParams
            {
                Keyword1 = "the",
                Keyword2 = "canal",
                Range = Int32.Parse("6"),
                WordsArray = ProximitySearcher.GetContentFromFileAsWordsArray("sampleshort.txt")
            };

            //sampleshort.txt has this content->  the man the plan the canal Panama           
            // If we run with this > ProxmitySearch.exe the canal 6 sampleshort.txt
            // Our expected count is 3

            int count = ProximitySearcher.Search(inputParams);

            Assert.AreEqual(count, 3);
        }

        [TestMethod]
        public void Test_Search_WithSample1B()
        {
            var inputParams = new ProximitySearcher.InputParams
            {
                Keyword1 = "the",
                Keyword2 = "canal",
                Range = Int32.Parse("3"),
                WordsArray = ProximitySearcher.GetContentFromFileAsWordsArray("sampleshort.txt")
            };

            //sampleshort.txt has this content->  the man the plan the canal Panama           
            // If we run with this > ProxmitySearch.exe the canal 3 sampleshort.txt
            // Our expected count is 1

            int count = ProximitySearcher.Search(inputParams);

            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void Test_Search_WithSample2()
        {
            var inputParams = new ProximitySearcher.InputParams
            {
                Keyword1 = "the",
                Keyword2 = "canal",
                Range = Int32.Parse("6"),
                WordsArray = ProximitySearcher.GetContentFromFileAsWordsArray("samplelong.txt")
            };

            //samplelong.txt has this content->  the man the plan the canal panama panama canal the plan the man the the man the plan the canal panama
            // If we run with this > ProxmitySearch.exe the canal 6 samplelong.txt
            // Our expected count is 11

            int count = ProximitySearcher.Search(inputParams);

            Assert.AreEqual(count, 11);
        }

    }
}
