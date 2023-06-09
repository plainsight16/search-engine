using NUnit.Framework;


namespace DocHandler.Tests
{
    [TestFixture]
    public class XmlParserTests
    {
        private XmlParser parser;
        static string path = @"C:\Users\Julius Alibrown\Desktop\class\Project\new\search-engine\Files\Documents\";
        string filePath = path + "xml_test_file.xml";

        [SetUp]
        public void Setup()
        {
            parser = new XmlParser();
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
            string invalidFilePath = "C:\\path\\to\\invalid.xml";

            // Act
            bool result = parser.CanParse(invalidFilePath);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ParseDocument_ValidFile_ReturnsContent()
        {
            // Arrange
            string expectedContent = "Hello";

            // Act
            string result = parser.parseDocument(filePath).Trim();

            // Assert
            Assert.That(result, Is.EqualTo(expected: expectedContent));
        }
    }
}
