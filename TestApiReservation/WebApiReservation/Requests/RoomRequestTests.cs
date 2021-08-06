using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiReservation.Requests;

namespace TestApiReservation.WebApiReservation.Requests
{
    [TestClass]
    public class RoomRequestTests
    {
        [TestMethod]
        public void Should_Validate_When_Valid_Room_Request()
        {
            var request = CreateValidRoomRequest();
            var validationContext = new ValidationContext(request);
            var errors = new List<ValidationResult>();
            
            var result = Validator.TryValidateObject(request, validationContext, errors, true);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_Unvalidate_When_Name_More_Than_50()
        {
            var request = CreateValidRoomRequest();
            request.Name = "123456789 123456789 123456789 123456789 123456789 1";
            var validationContext = new ValidationContext(request);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(request, validationContext, errors, true);

            Assert.IsFalse(result);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(RoomRequest.Name));
        }

        private RoomRequest CreateValidRoomRequest() => new RoomRequest
        {
            Name = "Test"
        };
    }
}
