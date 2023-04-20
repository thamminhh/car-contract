﻿using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CarContractVer2.Controllers
{
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly TwilioRestClient _twilioClient;

        public SmsController(TwilioRestClient twilioClient)
        {
            _twilioClient = twilioClient;
        }
        [NonAction]
        public int GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp;
        }

        [HttpPost("sendOTP/{phoneNumber}")]
        public async Task<IActionResult> SendOtp(string phoneNumber)
        {
            TwilioClient.Init("ACfa7081671b843b3749287265d284f43a", "185bb388d22974460a32b2e13b8903f1");
            var otp = GenerateOtp(); // Generate a random OTP code
            var message = $"Your OTP code is: {otp}";

            try
            {
                var result = await MessageResource.CreateAsync(
                    body: message,
                    from: new Twilio.Types.PhoneNumber("+1 620 269 1166"),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );
                HttpContext.Session.SetString("OtpCode", otp.ToString());
                HttpContext.Session.SetString("PhoneNumber", phoneNumber.ToString());
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exception
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("verifyOTP/{phoneNumber}/{otp}")]
        public IActionResult VerifyOtp(string phoneNumber, string otp)
        {
            //bool isOtpValid = checkVerifyOtp(phoneNumber, otp);
            var storedPhoneNumber = HttpContext.Session.GetString("PhoneNumber");

            var storedOtpCode = HttpContext.Session.GetString("OtpCode");

            if (otp == storedOtpCode)
            {
                return Ok("OTP verified successfully.");
            }
            else
            {
                return BadRequest("Invalid OTP.");
            }
        }
        [NonAction]
        private bool checkVerifyOtp(string phoneNumber, string otp)
        {
            TwilioClient.Init("ACfa7081671b843b3749287265d284f43a", "185bb388d22974460a32b2e13b8903f1");
            try
            {
                //var messages = MessageResource.Read(
                //    to: new Twilio.Types.PhoneNumber(phoneNumber),
                //    limit: 1);
                //phoneNumber = "+84" + phoneNumber;
               var messages = MessageResource.Read(to: new Twilio.Types.PhoneNumber(phoneNumber));

                foreach (var message in messages)
                    if (message.Body.Contains(otp))
                    {
                        return true; // OTP code matches
                    }

                return false; // OTP code not found in latest message
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }
    }
}