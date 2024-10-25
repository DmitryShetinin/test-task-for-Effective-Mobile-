using System;
using Xunit;

namespace DeliveryService.Tests
{
    public class RequiredFieldsValidatorTests
    {
        private readonly RequiredFieldsValidator _validator = new RequiredFieldsValidator();

        [Fact]
        public void Validate_ValidOrder_ReturnsTrue()
        {
            var order = new Order { Id = "1", DeliveryTime = new DateTime(2025, 11, 25, 12, 30, 0), District = "Central" };
            var result = _validator.Validate(order);
            Assert.True(result);
        }

        [Fact]
        public void Validate_InvalidOrder_ReturnsFalse()
        {
            var order = new Order { Id = null, DeliveryTime = DateTime.Now.AddHours(1), District = "North" };
            var result = _validator.Validate(order);
            Assert.False(result);
        }
    }

    public class DateValidatorTests
    {
        private readonly DateValidator _validator = new DateValidator();

        [Fact]
        public void Validate_ValidDate_ReturnsTrue()
        {
            var order = new Order { Id = "12", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0), District = "Downtown" };
            var result = _validator.Validate(order);
            Assert.True(result);
        }

        [Fact]
        public void Validate_InvalidDate_ReturnsFalse()
        {
            var order = new Order { Id = "123", DeliveryTime = new DateTime(2023, 11, 25, 12, 30, 0), District = "Downtown" };
            var result = _validator.Validate(order);
            Assert.False(result);
        }
    }

    public class InputDataValidatorTests
    {
        private readonly InputDataValidator _validator = new InputDataValidator();

        [Fact]
        public void Validate_ValidInputData_ReturnsTrue()
        {
            string[] inputData = { "District", DateTime.Now.AddHours(1).ToString(), "log/path", "order/path" };
            var result = _validator.Validate(inputData);
            Assert.True(result);
        }

        [Fact]
        public void Validate_InvalidInputData_ReturnsFalse()
        {
            string[] inputData = { "", "invalid_date", "", "" };
            var result = _validator.Validate(inputData);
            Assert.False(result);
        }

        [Fact]
        public void Validate_InsufficientInputData_ReturnsFalse()
        {
            string[] inputData = { "District", "log/path" }; // Less than 3 elements
            var result = _validator.Validate(inputData);
            Assert.False(result);
        }
    }
}
