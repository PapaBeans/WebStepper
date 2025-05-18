using System;
using System.Threading.Tasks;
using WebStepper.Core.Application;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;
using Moq;
using NUnit.Framework;

namespace WebStepper.Tests.Core
{
    [TestFixture]
    public class TemplateServiceTests
    {
        private Mock<ITemplateRepository> _mockTemplateRepository;
        private Mock<ILogService> _mockLogService;
        private TemplateService _templateService;

        [SetUp]
        public void Setup()
        {
            _mockTemplateRepository = new Mock<ITemplateRepository>();
            _mockLogService = new Mock<ILogService>();
            _templateService = new TemplateService(_mockTemplateRepository.Object, _mockLogService.Object);
        }

        [Test]
        public async Task LoadTemplateAsync_ValidPath_ReturnsTemplate()
        {
            // Arrange
            string path = "valid/template/path.json";
            var expectedTemplate = new Template { Id = "test-id", Name = "Test Template" };

            _mockTemplateRepository.Setup(r => r.LoadTemplateAsync(path))
                .ReturnsAsync(expectedTemplate);

            // Act
            var result = await _templateService.LoadTemplateAsync(path);

            // Assert
            Assert.AreEqual(expectedTemplate, result);
            _mockLogService.Verify(l => l.LogInfo(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public async Task LoadTemplateAsync_NullTemplate_ThrowsException()
        {
            // Arrange
            string path = "invalid/template/path.json";

            _mockTemplateRepository.Setup(r => r.LoadTemplateAsync(path))
                .ReturnsAsync((Template)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _templateService.LoadTemplateAsync(path));
            Assert.That(ex.Message, Contains.Substring("Failed to load template"));
            _mockLogService.Verify(l => l.LogError(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task SaveTemplateAsync_ValidTemplate_CallsRepository()
        {
            // Arrange
            string path = "valid/save/path.json";
            var template = new Template
            {
                Id = "test-id",
                Name = "Test Template",
                Pages = new System.Collections.Generic.List<Page>
                {
                    new Page
                    {
                        Id = "page1",
                        Name = "Test Page"
                    }
                }
            };

            // Act
            await _templateService.SaveTemplateAsync(path, template);

            // Assert
            _mockTemplateRepository.Verify(r => r.SaveTemplateAsync(path, template), Times.Once);
            _mockLogService.Verify(l => l.LogInfo(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void SaveTemplateAsync_NullTemplate_ThrowsArgumentNullException()
        {
            // Arrange
            string path = "valid/save/path.json";

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _templateService.SaveTemplateAsync(path, null));
        }

        [Test]
        public void ValidateTemplate_ValidTemplate_ReturnsTrue()
        {
            // Arrange
            var template = new Template
            {
                Id = "test-id",
                Name = "Test Template",
                Pages = new System.Collections.Generic.List<Page>
                {
                    new Page
                    {
                        Id = "page1",
                        Name = "Test Page",
                        PageIdentifierSelector = "#test-selector",
                        Steps = new System.Collections.Generic.List<Step>
                        {
                            new Step
                            {
                                Id = "step1",
                                Name = "Test Step",
                                Type = StepType.ClickButton,
                                Selector = "#test-button"
                            }
                        }
                    }
                }
            };

            // Act
            bool result = _templateService.ValidateTemplate(template);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateTemplate_NullTemplate_ReturnsFalse()
        {
            // Act
            bool result = _templateService.ValidateTemplate(null);

            // Assert
            Assert.IsFalse(result);
            _mockLogService.Verify(l => l.LogError("Template is null"), Times.Once);
        }

        [Test]
        public void ValidateTemplate_MissingTemplateId_ReturnsFalse()
        {
            // Arrange
            var template = new Template
            {
                Name = "Test Template",
                Pages = new System.Collections.Generic.List<Page>()
            };

            // Act
            bool result = _templateService.ValidateTemplate(template);

            // Assert
            Assert.IsFalse(result);
            _mockLogService.Verify(l => l.LogError("Template ID is missing"), Times.Once);
        }

        [Test]
        public void CreateNewTemplate_ReturnsValidTemplate()
        {
            // Act
            var template = _templateService.CreateNewTemplate();

            // Assert
            Assert.IsNotNull(template);
            Assert.IsNotNull(template.Id);
            Assert.AreEqual("New Template", template.Name);
            Assert.IsNotNull(template.Pages);
            Assert.IsTrue(template.Pages.Count > 0);
            _mockLogService.Verify(l => l.LogInfo("Creating new template"), Times.Once);
        }
    }
}
