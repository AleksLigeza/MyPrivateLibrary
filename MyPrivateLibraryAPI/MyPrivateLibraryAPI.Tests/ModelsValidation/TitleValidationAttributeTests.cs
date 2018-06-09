using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using MyPrivateLibraryAPI.Models;
using Xunit;

namespace MyPrivateLibraryAPI.Tests.ModelsValidation
{
    public class TitleValidationAttributeTests
    {
        [Theory]
        [InlineData("Text to check", 0, 5)]
        [InlineData("1234", 5, 5)]
        public void IsValid_TextIsNotValid_ReturnsFalse(string title, int min, int max)
        {
            // Assert
            var attribute = new TitleValidationAttribute();
            attribute.maxLength = max;
            attribute.minLength = min;

            // Act
            var result = attribute.IsValid(title);

            // Arrange
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("Text to check", 0, 20)]
        [InlineData("12345", 5, 5)]
        public void IsValid_TextIsValid_ReturnsFalse(string title, int min, int max)
        {
            // Assert
            var attribute = new TitleValidationAttribute();
            attribute.maxLength = max;
            attribute.minLength = min;

            // Act
            var result = attribute.IsValid(title);

            // Arrange
            result.Should().BeTrue();
        }
    }
}
