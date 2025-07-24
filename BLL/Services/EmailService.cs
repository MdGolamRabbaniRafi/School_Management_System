using BLL.Common;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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
                Subject = "Your OTP Code for Sign Up",
                IsBodyHtml = true,
                Body = $@"
               <div style='font-family: Arial, sans-serif; background: #f0f2f5; padding: 30px;'>
                 <div style='max-width: 600px; margin: auto; background: #ffffff; padding: 30px; border-radius: 12px; box-shadow: 0 4px 20px rgba(0,0,0,0.1);'>
                       <h2 style='color: #2c3e50; text-align: center;'>🔐 Email Verification</h2>
                           <p style='font-size: 16px; color: #555;'>Hello,</p>
                             <p style='font-size: 16px; color: #555;'>Use the following OTP to verify your email address. This code is valid for <strong>3 minutes only</strong>.</p>

                                    <div style='margin: 20px 0; text-align: center;'>
                                      <span style='display: inline-block; background: #f4f6f8; padding: 16px 28px; font-size: 28px; font-weight: bold; color: #2c3e50; letter-spacing: 4px; border-radius: 8px; border: 1px dashed #ccc;'>{otp}</span>
                                     </div>

                                       <div style='text-align: center;'>
                                          <span style='font-size: 14px; color: #888;'>Copy the OTP and paste it into the verification screen</span>
                       </div>

                     <hr style='margin: 30px 0; border: none; border-top: 1px solid #eee;' />

                     <p style='font-size: 12px; color: #aaa;'>If you did not request this, please ignore this email.</p>
                     <p style='font-size: 12px; color: #aaa;'>© {DateTime.Now.Year} School Management System</p>
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
