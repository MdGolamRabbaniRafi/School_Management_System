using BLL.Common;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BLL.Services
{
    public class EmailService
    {
        private readonly UserService userService = new();

        public static bool SendRandomOtpEmail(string toEmail)
        {
            var fromEmail = Environment.GetEnvironmentVariable("EMAIL_USER");
            var fromPass = Environment.GetEnvironmentVariable("EMAIL_PASS");
            var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
            var smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");

            if (string.IsNullOrEmpty(fromEmail))
            {
                throw new ArgumentNullException(nameof(fromEmail), "Environment variable 'EMAIL_USER' is not set.");
            }

            var otp = OTPGenaretor.GenerateOtp();
            var redis = RedisHelper.GetDatabase();
            redis.StringSet($"otp:{toEmail}", otp, TimeSpan.FromMinutes(3));

            var client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, fromPass)
            };

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = "🔐 Verify Your Email - School Management System",
                IsBodyHtml = true,
                Body = $@"
<div style='font-family: ""Segoe UI"", Roboto, sans-serif; background: #f0f4ff; padding: 30px;'>
  <div style='max-width: 600px; margin: auto; background: #ffffff; padding: 40px 30px; border-radius: 12px; box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1); transition: all 0.3s ease-in-out;'>

    <!-- Header -->
    <div style='background-color: #3a86ff; padding: 20px; border-radius: 10px; text-align: center; color: #ffffff; transition: background-color 0.3s ease;'>
      <h1 style='margin: 0; font-size: 24px;'>School Management System</h1>
    </div>

    <!-- Title -->
    <h2 style='color: #2d3748; text-align: center; margin-top: 30px;'>Email Verification</h2>

    <p style='font-size: 16px; color: #4a5568; text-align: center;'>Hello,</p>
    <p style='font-size: 16px; color: #4a5568; text-align: center;'>
      Use the OTP below to verify your email. This code is valid for <strong>3 minutes</strong>.
    </p>

    <!-- OTP Box -->
    <div style='margin: 30px auto; text-align: center;'>
      <span style='display: inline-block; background: #ebf8ff; padding: 20px 40px; font-size: 34px; font-weight: bold; color: #2c5282; letter-spacing: 6px; border-radius: 10px; border: 2px dashed #3182ce; transition: transform 0.3s ease;'>
        {otp}
      </span>
    </div>

    <!-- Note -->
    <p style='text-align: center; font-size: 14px; color: #718096;'>Enter this code in the verification field to proceed.</p>

    <hr style='margin: 40px 0; border: none; border-top: 1px solid #e2e8f0;' />

    <!-- Footer -->
    <p style='font-size: 12px; color: #a0aec0; text-align: center;'>If you did not request this code, please ignore this email.</p>
    <p style='font-size: 12px; color: #a0aec0; text-align: center;'>&copy; {DateTime.Now.Year} School Management System</p>

  </div>
</div>"
            };

            message.To.Add(toEmail);

            try
            {
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<UserDTO?> VerifyOtpAsync(string email, string inputOtp)
        {
            var redis = RedisHelper.GetDatabase();
            var key = $"otp:{email}";
            var storedOtp = await redis.StringGetAsync(key);

            if (!storedOtp.HasValue || storedOtp.ToString() != inputOtp)
                return null;

            await redis.KeyDeleteAsync(key);

            var createdUser = await userService.ConfirmUserRegistrationAsync(email);
            return createdUser;
        }



    }
}
