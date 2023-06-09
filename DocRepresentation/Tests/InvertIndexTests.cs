using NUnit.Framework;
using System.Collections.Generic;

namespace DocRepresentation.Tests
{
    [TestFixture]
    public class InvertIndexTests
    {
        [Test]
        public void GetMergedIndex_ReturnsEmptyDictionary_WhenNoTokensProvided()
        {
            // Arrange
            List<Token> tokens = new List<Token>();
            InvertIndex invertIndex = new InvertIndex(tokens);

            // Act
            Dictionary<string, List<Token>> mergedIndex = invertIndex.GetMergedIndex();

            // Assert
            Assert.IsEmpty(mergedIndex);
        }

        [Test]
        public void GetMergedIndex_ConstructsInvertedIndex_WithCorrectTermsAndDocumentIDs()
        {
            // Arrange
            Token appleFile1 = new Token(1, "apple", "file1.txt");
            Token appleFile3 = new Token(3, "apple", "file3.txt");
            Token bananaFile2 = new Token(2, "banana", "file2.txt");
            Token bananaFile3 = new Token(3, "banana", "file3.txt");
            Token orangeFile3 = new Token(3, "orange", "file3.txt");

            List<Token> tokens = new List<Token>
            {
                appleFile1,
                bananaFile2,
                orangeFile3,
                appleFile3,
                bananaFile3,
            };

            InvertIndex invertIndex = new InvertIndex(tokens);

            // Act
            Dictionary<string, List<Token>> mergedIndex = invertIndex.GetMergedIndex();

            // Assert
            Assert.AreEqual(3, mergedIndex.Count);

            Assert.IsTrue(mergedIndex.ContainsKey("apple"));
            CollectionAssert.AreEquivalent(new List<Token>
            {
                appleFile3,
                appleFile1,
            }, mergedIndex["apple"]);

            Assert.IsTrue(mergedIndex.ContainsKey("banana"));
            CollectionAssert.AreEquivalent(new List<Token>
            {
                bananaFile2,
                bananaFile3
            }, mergedIndex["banana"]);

            Assert.IsTrue(mergedIndex.ContainsKey("orange"));
            CollectionAssert.AreEquivalent(new List<Token>
            {
                orangeFile3
            }, mergedIndex["orange"]);
        }
    }
}
  
