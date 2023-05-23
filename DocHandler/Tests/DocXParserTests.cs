using NUnit.Framework;


namespace DocHandler.Tests
{
    [TestFixture]
    public class DocXParserTests
    {
        private DocXParser parser;
        static string path = @"C:\Users\Julius Alibrown\Desktop\class\Project\new\search-engine\Files\";
        string filePath = path + "DocX_test_file.docx";

        [SetUp]
        public void Setup()
        {
            parser = new DocXParser();
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
            string invalidFilePath = "C:\\path\\to\\invalid.docx";

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
            string result = parser.parseDocument(filePath).Trim();

            // Assert
            Assert.That(result, Is.EqualTo(expected: expectedContent));
        }
    }
}
