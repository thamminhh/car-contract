using CleanArchitecture.Application.Repository;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CarContractVer2.Controllers
{
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly SMSService _sMSService;

        public SmsController(SMSService sMSService)
        {
            _sMSService = sMSService;
        }

        [HttpPost("sendOTP/{phoneNumber}")]
        public async Task<IActionResult> SendOtp(string phoneNumber)
        {
            try
            {
                bool isSent = await _sMSService.SendOtpAsync(phoneNumber);
                if (isSent)
                {
                    return Ok("OTP sent successfully.");
                }
                else
                {
                    return BadRequest("Failed to send OTP.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error sending OTP: " + ex.Message);
            }
        }

        [HttpPost("verifyOTP/{phoneNumber}/{otp}")]
        public IActionResult VerifyOtp(string phoneNumber, string otp)
        {
            bool isOtpValid = _sMSService.checkVerifyOtp(phoneNumber, otp);

            if (isOtpValid)
            {
                return Ok("OTP verified successfully.");
            }
            else
            {
                return BadRequest("Invalid OTP.");
            }
        }
        


        //[HttpPost("sendOTP/{phoneNumber}")]
        //public async Task<IActionResult> SendOtp(string phoneNumber)
        //{
        //    TwilioClient.Init("ACfa7081671b843b3749287265d284f43a", "6804ac27cb1456b5505546b861cd691e");
        //    var otp = GenerateOtp(); // Generate a random OTP code
        //    var message = $"Your OTP code is: {otp}";
        //    phoneNumber = "+84" + phoneNumber;
        //    try
        //    {
        //        var result = await MessageResource.CreateAsync(
        //            body: message,
        //            from: new Twilio.Types.PhoneNumber("+16202691166"),
        //            to: new Twilio.Types.PhoneNumber(phoneNumber)
        //        );
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("sendOTP/{phoneNumber}")]
        //public async Task<IActionResult> SendOtp(string phoneNumber)
        //{
        //    try
        //    {
        //        bool result = await _sMSService.SendOtpAsync(phoneNumber);
        //        if (result)
        //        {
        //            return Ok("OTP sent successfully.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}