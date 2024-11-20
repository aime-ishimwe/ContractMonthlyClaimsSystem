using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq; // Ensure Moq is imported
using Aime_Prog_POE; // Ensure your main project's namespace is imported

namespace Aime_Prog_POE.Tests
{
    [TestClass]
    public class LecturerDashboardTests
    {
        private LecturerDashboard _dashboard;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize the LecturerDashboard instance before each test method.
            _dashboard = new LecturerDashboard();
        }

        [TestMethod]
        public void CalculateTotalClaim_ShouldReturnCorrectAmount()
        {
            // Arrange
            decimal hoursWorked = 10;
            decimal hourlyRate = 25;

            // Act
            decimal result = _dashboard.CalculateTotalClaim(hoursWorked, hourlyRate);

            // Assert
            Assert.AreEqual(250, result); // 10 * 25 = 250
        }

        
    }
}