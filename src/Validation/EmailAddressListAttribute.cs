using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EmailQueue.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class EmailAddressListAttribute : ValidationAttribute
{
    
    public override bool IsValid(object? value){
        var result = false;
        if (value is not null){
            var raw = value as string;
            
            if (string.IsNullOrEmpty(raw))
                return result;
            
            var list = raw.Split(';');
            
            if(list.Length == 1){
                result = IsValidEmail(list[0]);
            } else {
                foreach(var email in list){
                    var isValidEmail = IsValidEmail(email);
                    if(!isValidEmail){
                        result = false;
                        break;
                    } 
                }
            }
        }
        
        return result;
    }


    internal bool IsValidEmail(string email){
        
        if (string.IsNullOrWhiteSpace(email))
                return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}
