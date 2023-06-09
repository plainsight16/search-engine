using NUnit.Framework;
using System.Text;


namespace DocHandler.Tests
{
    [TestFixture]
    public class SpreadsheetParserTests
    {
        private SpreadsheetParser parser;
        static string path = @"C:\Users\Julius Alibrown\Desktop\class\Project\new\search-engine\Files\Documents\";
        string filePath = path + "spreadsheet_test_file.xlsx";

        [SetUp]
        public void Setup()
        {
            parser = new SpreadsheetParser();
        }

        [Test]
        public void CanParse_ValidFile_ReturnsTrue()
        {
            // Act
            bool result = parser.CanParse(filePath);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CanParse_InvalidFile_ReturnsFalse()
        {
            // Arrange
            string invalidFilePath = "C:\\path\\to\\invalid.xlsx";

            // Act
            bool result = parser.CanParse(invalidFilePath);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ParseDocument_ValidFile_ReturnsContent()
        {
            // Arrange
            string expectedContent = "This is the content of the document.";

            // Act
            byte[] bomBytes = Encoding.UTF8.GetBytes("\uFEFF");
            char[] bomChars = Encoding.UTF8.GetString(bomBytes).ToCharArray();
            string result = parser.parseDocument(filePath).Trim().TrimStart(bomChars);

            // Assert
            Assert.That(result, Is.EqualTo(expected: expectedContent));
        }
    }
}
