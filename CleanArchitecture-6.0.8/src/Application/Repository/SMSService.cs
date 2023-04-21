using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CleanArchitecture.Application.Repository;
public class SMSService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _twilioPhoneNumber;
    private readonly TwilioRestClient _twilioClient;

    public SMSService(TwilioRestClient twilioClient)
    {
        _accountSid = "ACfa7081671b843b3749287265d284f43a";
        _authToken = "6804ac27cb1456b5505546b861cd691e";
        _twilioPhoneNumber = "+1 620 445 8446";
        _twilioClient = twilioClient;
    }
    public int GenerateOtp()
    {
        Random random = new Random();
        int otp = random.Next(100000, 999999);
        return otp;
    }
    public async Task<bool> SendOtpAsync(string phoneNumber)
    {
        TwilioClient.Init(_accountSid, _authToken);
        var otp = GenerateOtp(); // Generate a random OTP code
        var message = $"Your OTP code is: {otp}";
        phoneNumber = "+84" + phoneNumber;
        try
        {
            var result = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber(_twilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );
            // If the message was sent successfully, return true
            //if (result.Status == MessageResource.StatusEnum.Sending)
                return true;
            //else
            //    return false;
        }
        catch (Exception ex)
        {
            // Handle exception
            throw new Exception("Error sending OTP: " + ex.Message);
        }
    }
    public bool checkVerifyOtp(string phoneNumber, string otp)
    {
        TwilioClient.Init(_accountSid, _authToken);
        phoneNumber = "+84" + phoneNumber;
        try
        {
            var messages = MessageResource.Read(
                from: new Twilio.Types.PhoneNumber(_twilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber),
                limit: 1);
            //var messages = MessageResource.Read(to: new Twilio.Types.PhoneNumber(phoneNumber));

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


    //public async Task<int> SendOtpAsync(string phoneNumber)
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
    //        return otp;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle exception
    //        throw new Exception("Error sending OTP: " + ex.Message);
    //    }
    //}


}
