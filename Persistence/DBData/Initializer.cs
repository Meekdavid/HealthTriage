using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.DBContext;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBData
{
    public class Initializer
    {
        public static IConfiguration _configuration;

        public static async Task Init(HealthTriageDbContext context, RoleManager<Role> roleManager, UserManager<AppUser> userManager, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _configuration = configuration;

            await InsertRoles(roleManager, userManager, context);
            await InsertLanguages(context);
            await InsertCountries(context);
        }

        private static async Task InsertRoles(RoleManager<Role> roleManager, UserManager<AppUser> userManager, HealthTriageDbContext context)
        {
            var sql = context.Users.ToQueryString();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new Role("Admin");
                var result = await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Patient"))
            {
                var role = new Role("Patient");
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Practitioner"))
            {
                var role = new Role("Practitioner");
                await roleManager.CreateAsync(role);
            }

            var adminUserExist = await userManager.FindByEmailAsync("admin@healthtriage.com");

            if (adminUserExist == null)
            {
                try
                {
                    var adminUser = new AppUser
                    {
                        UserName = "HealthTriage",
                        FullName = "HealthTriage Limited",
                        Email = "admin@healthtriage.com",
                        EmailConfirmed = true,
                        Id = Ulid.NewUlid().ToString()
                    };

                    string adminPassword = "Admin239074106*";

                    var createAdminUserResult = await userManager.CreateAsync(adminUser, adminPassword);

                    if (createAdminUserResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");

                        Random rand = new Random();

                        Admin admin = new Admin();
                        admin.UserId = adminUser.Id;

                        context.Admins.Add(admin);

                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private static async Task InsertLanguages(HealthTriageDbContext context)
        {
            if (!context.Languages.Any())
            {
                var languages = new List<Language>
                {
                    new Language { LanguageName = "English", ISOCode = "en" },
                    new Language { LanguageName = "Spanish", ISOCode = "es" },
                    new Language { LanguageName = "French", ISOCode = "fr" },
                    new Language { LanguageName = "German", ISOCode = "de" },
                    new Language { LanguageName = "Chinese", ISOCode = "zh" },
                    new Language { LanguageName = "Japanese", ISOCode = "ja" },
                    new Language { LanguageName = "Russian", ISOCode = "ru" },
                    new Language { LanguageName = "Arabic", ISOCode = "ar" },
                    new Language { LanguageName = "Portuguese", ISOCode = "pt" },
                    new Language { LanguageName = "Hindi", ISOCode = "hi" },
                    new Language { LanguageName = "Bengali", ISOCode = "bn" },
                    new Language { LanguageName = "Punjabi", ISOCode = "pa" },
                    new Language { LanguageName = "Italian", ISOCode = "it" },
                    new Language { LanguageName = "Korean", ISOCode = "ko" },
                    new Language { LanguageName = "Vietnamese", ISOCode = "vi" },
                    new Language { LanguageName = "Turkish", ISOCode = "tr" },
                    new Language { LanguageName = "Dutch", ISOCode = "nl" },
                    new Language { LanguageName = "Greek", ISOCode = "el" },
                    new Language { LanguageName = "Swedish", ISOCode = "sv" },
                    new Language { LanguageName = "Thai", ISOCode = "th" },
                    new Language { LanguageName = "Persian", ISOCode = "fa" },
                    new Language { LanguageName = "Hebrew", ISOCode = "he" },
                    new Language { LanguageName = "Ukrainian", ISOCode = "uk" },
                    new Language { LanguageName = "Hungarian", ISOCode = "hu" },
                    new Language { LanguageName = "Polish", ISOCode = "pl" },
                    new Language { LanguageName = "Czech", ISOCode = "cs" },
                    new Language { LanguageName = "Romanian", ISOCode = "ro" },
                    new Language { LanguageName = "Indonesian", ISOCode = "id" },
                    new Language { LanguageName = "Malay", ISOCode = "ms" },
                    new Language { LanguageName = "Filipino", ISOCode = "fil" },
                    new Language { LanguageName = "Swahili", ISOCode = "sw" },
                    new Language { LanguageName = "Finnish", ISOCode = "fi" },
                    new Language { LanguageName = "Norwegian", ISOCode = "no" },
                    new Language { LanguageName = "Danish", ISOCode = "da" },
                    new Language { LanguageName = "Slovak", ISOCode = "sk" },
                    new Language { LanguageName = "Bulgarian", ISOCode = "bg" },
                    new Language { LanguageName = "Serbian", ISOCode = "sr" },
                    new Language { LanguageName = "Croatian", ISOCode = "hr" },
                    new Language { LanguageName = "Bosnian", ISOCode = "bs" },
                    new Language { LanguageName = "Lithuanian", ISOCode = "lt" },
                    new Language { LanguageName = "Latvian", ISOCode = "lv" },
                    new Language { LanguageName = "Estonian", ISOCode = "et" },
                    new Language { LanguageName = "Georgian", ISOCode = "ka" },
                    new Language { LanguageName = "Armenian", ISOCode = "hy" },
                    new Language { LanguageName = "Azerbaijani", ISOCode = "az" },
                    new Language { LanguageName = "Uzbek", ISOCode = "uz" },
                    new Language { LanguageName = "Kazakh", ISOCode = "kk" },
                    new Language { LanguageName = "Mongolian", ISOCode = "mn" },
                    new Language { LanguageName = "Pashto", ISOCode = "ps" },
                    new Language { LanguageName = "Kurdish", ISOCode = "ku" },
                    new Language { LanguageName = "Amharic", ISOCode = "am" },
                    new Language { LanguageName = "Somali", ISOCode = "so" },
                    new Language { LanguageName = "Hausa", ISOCode = "ha" },
                    new Language { LanguageName = "Yoruba", ISOCode = "yo" },
                    new Language { LanguageName = "Afrikaans", ISOCode = "af" },
                    new Language { LanguageName = "Albanian", ISOCode = "sq" },
                    new Language { LanguageName = "Belarusian", ISOCode = "be" },
                    new Language { LanguageName = "Catalan", ISOCode = "ca" },
                    new Language { LanguageName = "Galician", ISOCode = "gl" },
                    new Language { LanguageName = "Icelandic", ISOCode = "is" },
                    new Language { LanguageName = "Irish", ISOCode = "ga" },
                    new Language { LanguageName = "Macedonian", ISOCode = "mk" },
                    new Language { LanguageName = "Maltese", ISOCode = "mt" },
                    new Language { LanguageName = "Marathi", ISOCode = "mr" },
                    new Language { LanguageName = "Nepali", ISOCode = "ne" },
                    new Language { LanguageName = "Sinhala", ISOCode = "si" },
                    new Language { LanguageName = "Slovenian", ISOCode = "sl" },
                    new Language { LanguageName = "Tamil", ISOCode = "ta" },
                    new Language { LanguageName = "Telugu", ISOCode = "te" },
                    new Language { LanguageName = "Urdu", ISOCode = "ur" },
                    new Language { LanguageName = "Welsh", ISOCode = "cy" },
                    new Language { LanguageName = "Yiddish", ISOCode = "yi" },
                    new Language { LanguageName = "Tajik", ISOCode = "tg" },
                    new Language { LanguageName = "Turkmen", ISOCode = "tk" },
                    new Language { LanguageName = "Kyrgyz", ISOCode = "ky" },
                    new Language { LanguageName = "Tatar", ISOCode = "tt" },
                    new Language { LanguageName = "Uyghur", ISOCode = "ug" },
                    new Language { LanguageName = "Bashkir", ISOCode = "ba" },
                    new Language { LanguageName = "Chuvash", ISOCode = "cv" },
                    new Language { LanguageName = "Ossetian", ISOCode = "os" },
                    new Language { LanguageName = "Udmurt", ISOCode = "udm" },
                    new Language { LanguageName = "Yakut", ISOCode = "sah" },
                    new Language { LanguageName = "Khmer", ISOCode = "km" },
                    new Language { LanguageName = "Lao", ISOCode = "lo" },
                    new Language { LanguageName = "Burmese", ISOCode = "my" },
                    new Language { LanguageName = "Tibetan", ISOCode = "bo" },
                    new Language { LanguageName = "Dzongkha", ISOCode = "dz" },
                    new Language { LanguageName = "Maori", ISOCode = "mi" },
                    new Language { LanguageName = "Samoan", ISOCode = "sm" },
                    new Language { LanguageName = "Tongan", ISOCode = "to" },
                    new Language { LanguageName = "Fijian", ISOCode = "fj" },
                    new Language { LanguageName = "Haitian Creole", ISOCode = "ht" },
                    new Language { LanguageName = "Papiamento", ISOCode = "pap" },
                    new Language { LanguageName = "Guarani", ISOCode = "gn" },
                    new Language { LanguageName = "Quechua", ISOCode = "qu" },
                    new Language { LanguageName = "Aymara", ISOCode = "ay" },
                    new Language { LanguageName = "Mapudungun", ISOCode = "arn" },
                    new Language { LanguageName = "Nahuatl", ISOCode = "nah" },
                    new Language { LanguageName = "Mayan", ISOCode = "myn" },
                    new Language { LanguageName = "Inuktitut", ISOCode = "iu" },
                    new Language { LanguageName = "Greenlandic", ISOCode = "kl" },
                    new Language { LanguageName = "Sundanese", ISOCode = "su" },
                    new Language { LanguageName = "Javanese", ISOCode = "jv" },
                    new Language { LanguageName = "Madurese", ISOCode = "mad" },
                    new Language { LanguageName = "Buginese", ISOCode = "bug" },
                    new Language { LanguageName = "Balinese", ISOCode = "ban" },
                    new Language { LanguageName = "Chamorro", ISOCode = "ch" },
                    new Language { LanguageName = "Fula", ISOCode = "ff" },
                    new Language { LanguageName = "Igbo", ISOCode = "ig" },
                    new Language { LanguageName = "Kanuri", ISOCode = "kr" },
                    new Language { LanguageName = "Kinyarwanda", ISOCode = "rw" },
                    new Language { LanguageName = "Lingala", ISOCode = "ln" },
                    new Language { LanguageName = "Luba-Katanga", ISOCode = "lu" },
                    new Language { LanguageName = "Malagasy", ISOCode = "mg" },
                    new Language { LanguageName = "Mandinka", ISOCode = "mnk" },
                    new Language { LanguageName = "Oromo", ISOCode = "om" },
                    new Language { LanguageName = "Sesotho", ISOCode = "st" },
                    new Language { LanguageName = "Shona", ISOCode = "sn" },
                    new Language { LanguageName = "Sotho", ISOCode = "st" },
                    new Language { LanguageName = "Tswana", ISOCode = "tn" },
                    new Language { LanguageName = "Tsonga", ISOCode = "ts" },
                    new Language { LanguageName = "Twi", ISOCode = "tw" },
                    new Language { LanguageName = "Wolof", ISOCode = "wo" },
                    new Language { LanguageName = "Xhosa", ISOCode = "xh" },
                    new Language { LanguageName = "Zulu", ISOCode = "zu" }
                };

                context.Languages.AddRange(languages);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InsertCountries(HealthTriageDbContext context)
        {
            if (!context.Countries.Any())
            {
                var countries = new List<Country>
                {
                    new Country { CountryName = "Afghanistan", ISOCode2 = "AF", ISOCode3 = "AFG", PhoneCode = "+93", Flag = "https://flagsapi.com/AF/flat/64.png" },
                    new Country { CountryName = "Albania", ISOCode2 = "AL", ISOCode3 = "ALB", PhoneCode = "+355", Flag = "https://flagsapi.com/AL/flat/64.png" },
                    new Country { CountryName = "Algeria", ISOCode2 = "DZ", ISOCode3 = "DZA", PhoneCode = "+213", Flag = "https://flagsapi.com/DZ/flat/64.png" },
                    new Country { CountryName = "Andorra", ISOCode2 = "AD", ISOCode3 = "AND", PhoneCode = "+376", Flag = "https://flagsapi.com/AD/flat/64.png" },
                    new Country { CountryName = "Angola", ISOCode2 = "AO", ISOCode3 = "AGO", PhoneCode = "+244", Flag = "https://flagsapi.com/AO/flat/64.png" },
                    new Country { CountryName = "Antigua and Barbuda", ISOCode2 = "AG", ISOCode3 = "ATG", PhoneCode = "+1-268", Flag = "https://flagsapi.com/AG/flat/64.png" },
                    new Country { CountryName = "Argentina", ISOCode2 = "AR", ISOCode3 = "ARG", PhoneCode = "+54", Flag = "https://flagsapi.com/AR/flat/64.png" },
                    new Country { CountryName = "Armenia", ISOCode2 = "AM", ISOCode3 = "ARM", PhoneCode = "+374", Flag = "https://flagsapi.com/AM/flat/64.png" },
                    new Country { CountryName = "Australia", ISOCode2 = "AU", ISOCode3 = "AUS", PhoneCode = "+61", Flag = "https://flagsapi.com/AU/flat/64.png" },
                    new Country { CountryName = "Austria", ISOCode2 = "AT", ISOCode3 = "AUT", PhoneCode = "+43", Flag = "https://flagsapi.com/AT/flat/64.png" },
                    new Country { CountryName = "Azerbaijan", ISOCode2 = "AZ", ISOCode3 = "AZE", PhoneCode = "+994", Flag = "https://flagsapi.com/AZ/flat/64.png" },
                    new Country { CountryName = "Bahamas", ISOCode2 = "BS", ISOCode3 = "BHS", PhoneCode = "+1-242", Flag = "https://flagsapi.com/BS/flat/64.png" },
                    new Country { CountryName = "Bahrain", ISOCode2 = "BH", ISOCode3 = "BHR", PhoneCode = "+973", Flag = "https://flagsapi.com/BH/flat/64.png" },
                    new Country { CountryName = "Bangladesh", ISOCode2 = "BD", ISOCode3 = "BGD", PhoneCode = "+880", Flag = "https://flagsapi.com/BD/flat/64.png" },
                    new Country { CountryName = "Barbados", ISOCode2 = "BB", ISOCode3 = "BRB", PhoneCode = "+1-246", Flag = "https://flagsapi.com/BB/flat/64.png" },
                    new Country { CountryName = "Belarus", ISOCode2 = "BY", ISOCode3 = "BLR", PhoneCode = "+375", Flag = "https://flagsapi.com/BY/flat/64.png" },
                    new Country { CountryName = "Belgium", ISOCode2 = "BE", ISOCode3 = "BEL", PhoneCode = "+32", Flag = "https://flagsapi.com/BE/flat/64.png" },
                    new Country { CountryName = "Belize", ISOCode2 = "BZ", ISOCode3 = "BLZ", PhoneCode = "+501", Flag = "https://flagsapi.com/BZ/flat/64.png" },
                    new Country { CountryName = "Benin", ISOCode2 = "BJ", ISOCode3 = "BEN", PhoneCode = "+229", Flag = "https://flagsapi.com/BJ/flat/64.png" },
                    new Country { CountryName = "Bhutan", ISOCode2 = "BT", ISOCode3 = "BTN", PhoneCode = "+975", Flag = "https://flagsapi.com/BT/flat/64.png" },
                    new Country { CountryName = "Bolivia", ISOCode2 = "BO", ISOCode3 = "BOL", PhoneCode = "+591", Flag = "https://flagsapi.com/BO/flat/64.png" },
                    new Country { CountryName = "Bosnia and Herzegovina", ISOCode2 = "BA", ISOCode3 = "BIH", PhoneCode = "+387", Flag = "https://flagsapi.com/BA/flat/64.png" },
                    new Country { CountryName = "Botswana", ISOCode2 = "BW", ISOCode3 = "BWA", PhoneCode = "+267", Flag = "https://flagsapi.com/BW/flat/64.png" },
                    new Country { CountryName = "Brazil", ISOCode2 = "BR", ISOCode3 = "BRA", PhoneCode = "+55", Flag = "https://flagsapi.com/BR/flat/64.png" },
                    new Country { CountryName = "Brunei", ISOCode2 = "BN", ISOCode3 = "BRN", PhoneCode = "+673", Flag = "https://flagsapi.com/BN/flat/64.png" },
                    new Country { CountryName = "Bulgaria", ISOCode2 = "BG", ISOCode3 = "BGR", PhoneCode = "+359", Flag = "https://flagsapi.com/BG/flat/64.png" },
                    new Country { CountryName = "Burkina Faso", ISOCode2 = "BF", ISOCode3 = "BFA", PhoneCode = "+226", Flag = "https://flagsapi.com/BF/flat/64.png" },
                    new Country { CountryName = "Burundi", ISOCode2 = "BI", ISOCode3 = "BDI", PhoneCode = "+257", Flag = "https://flagsapi.com/BI/flat/64.png" },
                    new Country { CountryName = "Cabo Verde", ISOCode2 = "CV", ISOCode3 = "CPV", PhoneCode = "+238", Flag = "https://flagsapi.com/CV/flat/64.png" },
                    new Country { CountryName = "Cambodia", ISOCode2 = "KH", ISOCode3 = "KHM", PhoneCode = "+855", Flag = "https://flagsapi.com/KH/flat/64.png" },
                    new Country { CountryName = "Cameroon", ISOCode2 = "CM", ISOCode3 = "CMR", PhoneCode = "+237", Flag = "https://flagsapi.com/CM/flat/64.png" },
                    new Country { CountryName = "Central African Republic", ISOCode2 = "CF", ISOCode3 = "CAF", PhoneCode = "+236", Flag = "https://flagsapi.com/CF/flat/64.png" },
                    new Country { CountryName = "Chad", ISOCode2 = "TD", ISOCode3 = "TCD", PhoneCode = "+235", Flag = "https://flagsapi.com/TD/flat/64.png" },
                    new Country { CountryName = "Chile", ISOCode2 = "CL", ISOCode3 = "CHL", PhoneCode = "+56", Flag = "https://flagsapi.com/CL/flat/64.png" },
                    new Country { CountryName = "China", ISOCode2 = "CN", ISOCode3 = "CHN", PhoneCode = "+86", Flag = "https://flagsapi.com/CN/flat/64.png" },
                    new Country { CountryName = "Colombia", ISOCode2 = "CO", ISOCode3 = "COL", PhoneCode = "+57", Flag = "https://flagsapi.com/CO/flat/64.png" },
                    new Country { CountryName = "Comoros", ISOCode2 = "KM", ISOCode3 = "COM", PhoneCode = "+269", Flag = "https://flagsapi.com/KM/flat/64.png" },
                    new Country { CountryName = "Congo (Congo-Brazzaville)", ISOCode2 = "CG", ISOCode3 = "COG", PhoneCode = "+242", Flag = "https://flagsapi.com/CG/flat/64.png" },
                    new Country { CountryName = "Congo (DRC)", ISOCode2 = "CD", ISOCode3 = "COD", PhoneCode = "+243", Flag = "https://flagsapi.com/CD/flat/64.png" },
                    new Country { CountryName = "Costa Rica", ISOCode2 = "CR", ISOCode3 = "CRI", PhoneCode = "+506", Flag = "https://flagsapi.com/CR/flat/64.png" },
                    new Country { CountryName = "Croatia", ISOCode2 = "HR", ISOCode3 = "HRV", PhoneCode = "+385", Flag = "https://flagsapi.com/HR/flat/64.png" },
                    new Country { CountryName = "Cuba", ISOCode2 = "CU", ISOCode3 = "CUB", PhoneCode = "+53", Flag = "https://flagsapi.com/CU/flat/64.png" },
                    new Country { CountryName = "Cyprus", ISOCode2 = "CY", ISOCode3 = "CYP", PhoneCode = "+357", Flag = "https://flagsapi.com/CY/flat/64.png" },
                    new Country { CountryName = "Czechia (Czech Republic)", ISOCode2 = "CZ", ISOCode3 = "CZE", PhoneCode = "+420", Flag = "https://flagsapi.com/CZ/flat/64.png" },
                    new Country { CountryName = "Denmark", ISOCode2 = "DK", ISOCode3 = "DNK", PhoneCode = "+45", Flag = "https://flagsapi.com/DK/flat/64.png" },
                    new Country { CountryName = "Djibouti", ISOCode2 = "DJ", ISOCode3 = "DJI", PhoneCode = "+253", Flag = "https://flagsapi.com/DJ/flat/64.png" },
                    new Country { CountryName = "Dominica", ISOCode2 = "DM", ISOCode3 = "DMA", PhoneCode = "+1-767", Flag = "https://flagsapi.com/DM/flat/64.png" },
                    new Country { CountryName = "Dominican Republic", ISOCode2 = "DO", ISOCode3 = "DOM", PhoneCode = "+1-809", Flag = "https://flagsapi.com/DO/flat/64.png" },
                    new Country { CountryName = "Ecuador", ISOCode2 = "EC", ISOCode3 = "ECU", PhoneCode = "+593", Flag = "https://flagsapi.com/EC/flat/64.png" },
                    new Country { CountryName = "Egypt", ISOCode2 = "EG", ISOCode3 = "EGY", PhoneCode = "+20", Flag = "https://flagsapi.com/EG/flat/64.png" },
                    new Country { CountryName = "El Salvador", ISOCode2 = "SV", ISOCode3 = "SLV", PhoneCode = "+503", Flag = "https://flagsapi.com/SV/flat/64.png" },
                    new Country { CountryName = "Equatorial Guinea", ISOCode2 = "GQ", ISOCode3 = "GNQ", PhoneCode = "+240", Flag = "https://flagsapi.com/GQ/flat/64.png" },
                    new Country { CountryName = "Eritrea", ISOCode2 = "ER", ISOCode3 = "ERI", PhoneCode = "+291", Flag = "https://flagsapi.com/ER/flat/64.png" },
                    new Country { CountryName = "Estonia", ISOCode2 = "EE", ISOCode3 = "EST", PhoneCode = "+372", Flag = "https://flagsapi.com/EE/flat/64.png" },
                    new Country { CountryName = "Eswatini", ISOCode2 = "SZ", ISOCode3 = "SWZ", PhoneCode = "+268", Flag = "https://flagsapi.com/SZ/flat/64.png" },
                    new Country { CountryName = "Ethiopia", ISOCode2 = "ET", ISOCode3 = "ETH", PhoneCode = "+251", Flag = "https://flagsapi.com/ET/flat/64.png" },
                    new Country { CountryName = "Fiji", ISOCode2 = "FJ", ISOCode3 = "FJI", PhoneCode = "+679", Flag = "https://flagsapi.com/FJ/flat/64.png" },
                    new Country { CountryName = "Finland", ISOCode2 = "FI", ISOCode3 = "FIN", PhoneCode = "+358", Flag = "https://flagsapi.com/FI/flat/64.png" },
                    new Country { CountryName = "France", ISOCode2 = "FR", ISOCode3 = "FRA", PhoneCode = "+33", Flag = "https://flagsapi.com/FR/flat/64.png" },
                    new Country { CountryName = "Gabon", ISOCode2 = "GA", ISOCode3 = "GAB", PhoneCode = "+241", Flag = "https://flagsapi.com/GA/flat/64.png" },
                    new Country { CountryName = "Gambia", ISOCode2 = "GM", ISOCode3 = "GMB", PhoneCode = "+220", Flag = "https://flagsapi.com/GM/flat/64.png" },
                    new Country { CountryName = "Georgia", ISOCode2 = "GE", ISOCode3 = "GEO", PhoneCode = "+995", Flag = "https://flagsapi.com/GE/flat/64.png" },
                    new Country { CountryName = "Germany", ISOCode2 = "DE", ISOCode3 = "DEU", PhoneCode = "+49", Flag = "https://flagsapi.com/DE/flat/64.png" },
                    new Country { CountryName = "Ghana", ISOCode2 = "GH", ISOCode3 = "GHA", PhoneCode = "+233", Flag = "https://flagsapi.com/GH/flat/64.png" },
                    new Country { CountryName = "Greece", ISOCode2 = "GR", ISOCode3 = "GRC", PhoneCode = "+30", Flag = "https://flagsapi.com/GR/flat/64.png" },
                    new Country { CountryName = "Grenada", ISOCode2 = "GD", ISOCode3 = "GRD", PhoneCode = "+1-473", Flag = "https://flagsapi.com/GD/flat/64.png" },
                    new Country { CountryName = "Guatemala", ISOCode2 = "GT", ISOCode3 = "GTM", PhoneCode = "+502", Flag = "https://flagsapi.com/GT/flat/64.png" },
                    new Country { CountryName = "Guinea", ISOCode2 = "GN", ISOCode3 = "GIN", PhoneCode = "+224", Flag = "https://flagsapi.com/GN/flat/64.png" },
                    new Country { CountryName = "Guinea-Bissau", ISOCode2 = "GW", ISOCode3 = "GNB", PhoneCode = "+245", Flag = "https://flagsapi.com/GW/flat/64.png" },
                    new Country { CountryName = "Guyana", ISOCode2 = "GY", ISOCode3 = "GUY", PhoneCode = "+592", Flag = "https://flagsapi.com/GY/flat/64.png" },
                    new Country { CountryName = "Haiti", ISOCode2 = "HT", ISOCode3 = "HTI", PhoneCode = "+509", Flag = "https://flagsapi.com/HT/flat/64.png" },
                    new Country { CountryName = "Honduras", ISOCode2 = "HN", ISOCode3 = "HND", PhoneCode = "+504", Flag = "https://flagsapi.com/HN/flat/64.png" },
                    new Country { CountryName = "Hungary", ISOCode2 = "HU", ISOCode3 = "HUN", PhoneCode = "+36", Flag = "https://flagsapi.com/HU/flat/64.png" },
                    new Country { CountryName = "Iceland", ISOCode2 = "IS", ISOCode3 = "ISL", PhoneCode = "+354", Flag = "https://flagsapi.com/IS/flat/64.png" },
                    new Country { CountryName = "India", ISOCode2 = "IN", ISOCode3 = "IND", PhoneCode = "+91", Flag = "https://flagsapi.com/IN/flat/64.png" },
                    new Country { CountryName = "Indonesia", ISOCode2 = "ID", ISOCode3 = "IDN", PhoneCode = "+62", Flag = "https://flagsapi.com/ID/flat/64.png" },
                    new Country { CountryName = "Iran", ISOCode2 = "IR", ISOCode3 = "IRN", PhoneCode = "+98", Flag = "https://flagsapi.com/IR/flat/64.png" },
                    new Country { CountryName = "Iraq", ISOCode2 = "IQ", ISOCode3 = "IRQ", PhoneCode = "+964", Flag = "https://flagsapi.com/IQ/flat/64.png" },
                    new Country { CountryName = "Ireland", ISOCode2 = "IE", ISOCode3 = "IRL", PhoneCode = "+353", Flag = "https://flagsapi.com/IE/flat/64.png" },
                    new Country { CountryName = "Israel", ISOCode2 = "IL", ISOCode3 = "ISR", PhoneCode = "+972", Flag = "https://flagsapi.com/IL/flat/64.png" },
                    new Country { CountryName = "Italy", ISOCode2 = "IT", ISOCode3 = "ITA", PhoneCode = "+39", Flag = "https://flagsapi.com/IT/flat/64.png" },
                    new Country { CountryName = "Jamaica", ISOCode2 = "JM", ISOCode3 = "JAM", PhoneCode = "+1-876", Flag = "https://flagsapi.com/JM/flat/64.png" },
                    new Country { CountryName = "Japan", ISOCode2 = "JP", ISOCode3 = "JPN", PhoneCode = "+81", Flag = "https://flagsapi.com/JP/flat/64.png" },
                    new Country { CountryName = "Jordan", ISOCode2 = "JO", ISOCode3 = "JOR", PhoneCode = "+962", Flag = "https://flagsapi.com/JO/flat/64.png" },
                    new Country { CountryName = "Kazakhstan", ISOCode2 = "KZ", ISOCode3 = "KAZ", PhoneCode = "+7", Flag = "https://flagsapi.com/KZ/flat/64.png" },
                    new Country { CountryName = "Kenya", ISOCode2 = "KE", ISOCode3 = "KEN", PhoneCode = "+254", Flag = "https://flagsapi.com/KE/flat/64.png" },
                    new Country { CountryName = "Kiribati", ISOCode2 = "KI", ISOCode3 = "KIR", PhoneCode = "+686", Flag = "https://flagsapi.com/KI/flat/64.png" },
                    new Country { CountryName = "Kuwait", ISOCode2 = "KW", ISOCode3 = "KWT", PhoneCode = "+965", Flag = "https://flagsapi.com/KW/flat/64.png" },
                    new Country { CountryName = "Kyrgyzstan", ISOCode2 = "KG", ISOCode3 = "KGZ", PhoneCode = "+996", Flag = "https://flagsapi.com/KG/flat/64.png" },
                    new Country { CountryName = "Laos", ISOCode2 = "LA", ISOCode3 = "LAO", PhoneCode = "+856", Flag = "https://flagsapi.com/LA/flat/64.png" },
                    new Country { CountryName = "Latvia", ISOCode2 = "LV", ISOCode3 = "LVA", PhoneCode = "+371", Flag = "https://flagsapi.com/LV/flat/64.png" },
                    new Country { CountryName = "Lebanon", ISOCode2 = "LB", ISOCode3 = "LBN", PhoneCode = "+961", Flag = "https://flagsapi.com/LB/flat/64.png" },
                    new Country { CountryName = "Lesotho", ISOCode2 = "LS", ISOCode3 = "LSO", PhoneCode = "+266", Flag = "https://flagsapi.com/LS/flat/64.png" },
                    new Country { CountryName = "Liberia", ISOCode2 = "LR", ISOCode3 = "LBR", PhoneCode = "+231", Flag = "https://flagsapi.com/LR/flat/64.png" },
                    new Country { CountryName = "Libya", ISOCode2 = "LY", ISOCode3 = "LBY", PhoneCode = "+218", Flag = "https://flagsapi.com/LY/flat/64.png" },
                    new Country { CountryName = "Liechtenstein", ISOCode2 = "LI", ISOCode3 = "LIE", PhoneCode = "+423", Flag = "https://flagsapi.com/LI/flat/64.png" },
                    new Country { CountryName = "Lithuania", ISOCode2 = "LT", ISOCode3 = "LTU", PhoneCode = "+370", Flag = "https://flagsapi.com/LT/flat/64.png" },
                    new Country { CountryName = "Luxembourg", ISOCode2 = "LU", ISOCode3 = "LUX", PhoneCode = "+352", Flag = "https://flagsapi.com/LU/flat/64.png" },
                    new Country { CountryName = "Madagascar", ISOCode2 = "MG", ISOCode3 = "MDG", PhoneCode = "+261", Flag = "https://flagsapi.com/MG/flat/64.png" },
                    new Country { CountryName = "Malawi", ISOCode2 = "MW", ISOCode3 = "MWI", PhoneCode = "+265", Flag = "https://flagsapi.com/MW/flat/64.png" },
                    new Country { CountryName = "Malaysia", ISOCode2 = "MY", ISOCode3 = "MYS", PhoneCode = "+60", Flag = "https://flagsapi.com/MY/flat/64.png" },
                    new Country { CountryName = "Maldives", ISOCode2 = "MV", ISOCode3 = "MDV", PhoneCode = "+960", Flag = "https://flagsapi.com/MV/flat/64.png" },
                    new Country { CountryName = "Mali", ISOCode2 = "ML", ISOCode3 = "MLI", PhoneCode = "+223", Flag = "https://flagsapi.com/ML/flat/64.png" },
                    new Country { CountryName = "Malta", ISOCode2 = "MT", ISOCode3 = "MLT", PhoneCode = "+356", Flag = "https://flagsapi.com/MT/flat/64.png" },
                    new Country { CountryName = "Marshall Islands", ISOCode2 = "MH", ISOCode3 = "MHL", PhoneCode = "+692", Flag = "https://flagsapi.com/MH/flat/64.png" },
                    new Country { CountryName = "Mauritania", ISOCode2 = "MR", ISOCode3 = "MRT", PhoneCode = "+222", Flag = "https://flagsapi.com/MR/flat/64.png" },
                    new Country { CountryName = "Mauritius", ISOCode2 = "MU", ISOCode3 = "MUS", PhoneCode = "+230", Flag = "https://flagsapi.com/MU/flat/64.png" },
                    new Country { CountryName = "Mexico", ISOCode2 = "MX", ISOCode3 = "MEX", PhoneCode = "+52", Flag = "https://flagsapi.com/MX/flat/64.png" },
                    new Country { CountryName = "Micronesia", ISOCode2 = "FM", ISOCode3 = "FSM", PhoneCode = "+691", Flag = "https://flagsapi.com/FM/flat/64.png" },
                    new Country { CountryName = "Moldova", ISOCode2 = "MD", ISOCode3 = "MDA", PhoneCode = "+373", Flag = "https://flagsapi.com/MD/flat/64.png" },
                    new Country { CountryName = "Monaco", ISOCode2 = "MC", ISOCode3 = "MCO", PhoneCode = "+377", Flag = "https://flagsapi.com/MC/flat/64.png" },
                    new Country { CountryName = "Mongolia", ISOCode2 = "MN", ISOCode3 = "MNG", PhoneCode = "+976", Flag = "https://flagsapi.com/MN/flat/64.png" },
                    new Country { CountryName = "Montenegro", ISOCode2 = "ME", ISOCode3 = "MNE", PhoneCode = "+382", Flag = "https://flagsapi.com/ME/flat/64.png" },
                    new Country { CountryName = "Morocco", ISOCode2 = "MA", ISOCode3 = "MAR", PhoneCode = "+212", Flag = "https://flagsapi.com/MA/flat/64.png" },
                    new Country { CountryName = "Mozambique", ISOCode2 = "MZ", ISOCode3 = "MOZ", PhoneCode = "+258", Flag = "https://flagsapi.com/MZ/flat/64.png" },
                    new Country { CountryName = "Myanmar (Burma)", ISOCode2 = "MM", ISOCode3 = "MMR", PhoneCode = "+95", Flag = "https://flagsapi.com/MM/flat/64.png" },
                    new Country { CountryName = "Namibia", ISOCode2 = "NA", ISOCode3 = "NAM", PhoneCode = "+264", Flag = "https://flagsapi.com/NA/flat/64.png" },
                    new Country { CountryName = "Nauru", ISOCode2 = "NR", ISOCode3 = "NRU", PhoneCode = "+674", Flag = "https://flagsapi.com/NR/flat/64.png" },
                    new Country { CountryName = "Nepal", ISOCode2 = "NP", ISOCode3 = "NPL", PhoneCode = "+977", Flag = "https://flagsapi.com/NP/flat/64.png" },
                    new Country { CountryName = "Netherlands", ISOCode2 = "NL", ISOCode3 = "NLD", PhoneCode = "+31", Flag = "https://flagsapi.com/NL/flat/64.png" },
                    new Country { CountryName = "New Zealand", ISOCode2 = "NZ", ISOCode3 = "NZL", PhoneCode = "+64", Flag = "https://flagsapi.com/NZ/flat/64.png" },
                    new Country { CountryName = "Nicaragua", ISOCode2 = "NI", ISOCode3 = "NIC", PhoneCode = "+505", Flag = "https://flagsapi.com/NI/flat/64.png" },
                    new Country { CountryName = "Niger", ISOCode2 = "NE", ISOCode3 = "NER", PhoneCode = "+227", Flag = "https://flagsapi.com/NE/flat/64.png" },
                    new Country { CountryName = "Nigeria", ISOCode2 = "NG", ISOCode3 = "NGA", PhoneCode = "+234", Flag = "https://flagsapi.com/NG/flat/64.png" },
                    new Country { CountryName = "North Macedonia", ISOCode2 = "MK", ISOCode3 = "MKD", PhoneCode = "+389", Flag = "https://flagsapi.com/MK/flat/64.png" },
                    new Country { CountryName = "Norway", ISOCode2 = "NO", ISOCode3 = "NOR", PhoneCode = "+47", Flag = "https://flagsapi.com/NO/flat/64.png" },
                    new Country { CountryName = "Oman", ISOCode2 = "OM", ISOCode3 = "OMN", PhoneCode = "+968", Flag = "https://flagsapi.com/OM/flat/64.png" },
                    new Country { CountryName = "Pakistan", ISOCode2 = "PK", ISOCode3 = "PAK", PhoneCode = "+92", Flag = "https://flagsapi.com/PK/flat/64.png" },
                    new Country { CountryName = "Palau", ISOCode2 = "PW", ISOCode3 = "PLW", PhoneCode = "+680", Flag = "https://flagsapi.com/PW/flat/64.png" },
                    new Country { CountryName = "Panama", ISOCode2 = "PA", ISOCode3 = "PAN", PhoneCode = "+507", Flag = "https://flagsapi.com/PA/flat/64.png" },
                    new Country { CountryName = "Papua New Guinea", ISOCode2 = "PG", ISOCode3 = "PNG", PhoneCode = "+675", Flag = "https://flagsapi.com/PG/flat/64.png" },
                    new Country { CountryName = "Paraguay", ISOCode2 = "PY", ISOCode3 = "PRY", PhoneCode = "+595", Flag = "https://flagsapi.com/PY/flat/64.png" },
                    new Country { CountryName = "Peru", ISOCode2 = "PE", ISOCode3 = "PER", PhoneCode = "+51", Flag = "https://flagsapi.com/PE/flat/64.png" },
                    new Country { CountryName = "Philippines", ISOCode2 = "PH", ISOCode3 = "PHL", PhoneCode = "+63", Flag = "https://flagsapi.com/PH/flat/64.png" },
                    new Country { CountryName = "Poland", ISOCode2 = "PL", ISOCode3 = "POL", PhoneCode = "+48", Flag = "https://flagsapi.com/PL/flat/64.png" },
                    new Country { CountryName = "Portugal", ISOCode2 = "PT", ISOCode3 = "PRT", PhoneCode = "+351", Flag = "https://flagsapi.com/PT/flat/64.png" },
                    new Country { CountryName = "Qatar", ISOCode2 = "QA", ISOCode3 = "QAT", PhoneCode = "+974", Flag = "https://flagsapi.com/QA/flat/64.png" },
                    new Country { CountryName = "Romania", ISOCode2 = "RO", ISOCode3 = "ROU", PhoneCode = "+40", Flag = "https://flagsapi.com/RO/flat/64.png" },
                    new Country { CountryName = "Rwanda", ISOCode2 = "RW", ISOCode3 = "RWA", PhoneCode = "+250", Flag = "https://flagsapi.com/RW/flat/64.png" },
                    new Country { CountryName = "Saint Kitts and Nevis", ISOCode2 = "KN", ISOCode3 = "KNA", PhoneCode = "+1-869", Flag = "https://flagsapi.com/KN/flat/64.png" },
                    new Country { CountryName = "Saint Lucia", ISOCode2 = "LC", ISOCode3 = "LCA", PhoneCode = "+1-758", Flag = "https://flagsapi.com/LC/flat/64.png" },
                    new Country { CountryName = "Saint Vincent and the Grenadines", ISOCode2 = "VC", ISOCode3 = "VCT", PhoneCode = "+1-784", Flag = "https://flagsapi.com/VC/flat/64.png" },
                    new Country { CountryName = "Samoa", ISOCode2 = "WS", ISOCode3 = "WSM", PhoneCode = "+685", Flag = "https://flagsapi.com/WS/flat/64.png" },
                    new Country { CountryName = "San Marino", ISOCode2 = "SM", ISOCode3 = "SMR", PhoneCode = "+378", Flag = "https://flagsapi.com/SM/flat/64.png" },
                    new Country { CountryName = "Sao Tome and Principe", ISOCode2 = "ST", ISOCode3 = "STP", PhoneCode = "+239", Flag = "https://flagsapi.com/ST/flat/64.png" },
                    new Country { CountryName = "Saudi Arabia", ISOCode2 = "SA", ISOCode3 = "SAU", PhoneCode = "+966", Flag = "https://flagsapi.com/SA/flat/64.png" },
                    new Country { CountryName = "Senegal", ISOCode2 = "SN", ISOCode3 = "SEN", PhoneCode = "+221", Flag = "https://flagsapi.com/SN/flat/64.png" },
                    new Country { CountryName = "Serbia", ISOCode2 = "RS", ISOCode3 = "SRB", PhoneCode = "+381", Flag = "https://flagsapi.com/RS/flat/64.png" },
                    new Country { CountryName = "Seychelles", ISOCode2 = "SC", ISOCode3 = "SYC", PhoneCode = "+248", Flag = "https://flagsapi.com/SC/flat/64.png" },
                    new Country { CountryName = "Sierra Leone", ISOCode2 = "SL", ISOCode3 = "SLE", PhoneCode = "+232", Flag = "https://flagsapi.com/SL/flat/64.png" },
                    new Country { CountryName = "Singapore", ISOCode2 = "SG", ISOCode3 = "SGP", PhoneCode = "+65", Flag = "https://flagsapi.com/SG/flat/64.png" },
                    new Country { CountryName = "Slovakia", ISOCode2 = "SK", ISOCode3 = "SVK", PhoneCode = "+421", Flag = "https://flagsapi.com/SK/flat/64.png" },
                    new Country { CountryName = "Slovenia", ISOCode2 = "SI", ISOCode3 = "SVN", PhoneCode = "+386", Flag = "https://flagsapi.com/SI/flat/64.png" },
                    new Country { CountryName = "Solomon Islands", ISOCode2 = "SB", ISOCode3 = "SLB", PhoneCode = "+677", Flag = "https://flagsapi.com/SB/flat/64.png" },
                    new Country { CountryName = "Somalia", ISOCode2 = "SO", ISOCode3 = "SOM", PhoneCode = "+252", Flag = "https://flagsapi.com/SO/flat/64.png" },
                    new Country { CountryName = "South Africa", ISOCode2 = "ZA", ISOCode3 = "ZAF", PhoneCode = "+27", Flag = "https://flagsapi.com/ZA/flat/64.png" },
                    new Country { CountryName = "South Korea", ISOCode2 = "KR", ISOCode3 = "KOR", PhoneCode = "+82", Flag = "https://flagsapi.com/KR/flat/64.png" },
                    new Country { CountryName = "South Sudan", ISOCode2 = "SS", ISOCode3 = "SSD", PhoneCode = "+211", Flag = "https://flagsapi.com/SS/flat/64.png" },
                    new Country { CountryName = "Spain", ISOCode2 = "ES", ISOCode3 = "ESP", PhoneCode = "+34", Flag = "https://flagsapi.com/ES/flat/64.png" },
                    new Country { CountryName = "Sri Lanka", ISOCode2 = "LK", ISOCode3 = "LKA", PhoneCode = "+94", Flag = "https://flagsapi.com/LK/flat/64.png" },
                    new Country { CountryName = "Sudan", ISOCode2 = "SD", ISOCode3 = "SDN", PhoneCode = "+249", Flag = "https://flagsapi.com/SD/flat/64.png" },
                    new Country { CountryName = "Suriname", ISOCode2 = "SR", ISOCode3 = "SUR", PhoneCode = "+597", Flag = "https://flagsapi.com/SR/flat/64.png" },
                    new Country { CountryName = "Sweden", ISOCode2 = "SE", ISOCode3 = "SWE", PhoneCode = "+46", Flag = "https://flagsapi.com/SE/flat/64.png" },
                    new Country { CountryName = "Switzerland", ISOCode2 = "CH", ISOCode3 = "CHE", PhoneCode = "+41", Flag = "https://flagsapi.com/CH/flat/64.png" },
                    new Country { CountryName = "Syria", ISOCode2 = "SY", ISOCode3 = "SYR", PhoneCode = "+963", Flag = "https://flagsapi.com/SY/flat/64.png" },
                    new Country { CountryName = "Tajikistan", ISOCode2 = "TJ", ISOCode3 = "TJK", PhoneCode = "+992", Flag = "https://flagsapi.com/TJ/flat/64.png" },
                    new Country { CountryName = "Tanzania", ISOCode2 = "TZ", ISOCode3 = "TZA", PhoneCode = "+255", Flag = "https://flagsapi.com/TZ/flat/64.png" },
                    new Country { CountryName = "Thailand", ISOCode2 = "TH", ISOCode3 = "THA", PhoneCode = "+66", Flag = "https://flagsapi.com/TH/flat/64.png" },
                    new Country { CountryName = "Timor-Leste", ISOCode2 = "TL", ISOCode3 = "TLS", PhoneCode = "+670", Flag = "https://flagsapi.com/TL/flat/64.png" },
                    new Country { CountryName = "Togo", ISOCode2 = "TG", ISOCode3 = "TGO", PhoneCode = "+228", Flag = "https://flagsapi.com/TG/flat/64.png" },
                    new Country { CountryName = "Tonga", ISOCode2 = "TO", ISOCode3 = "TON", PhoneCode = "+676", Flag = "https://flagsapi.com/TO/flat/64.png" },
                    new Country { CountryName = "Trinidad and Tobago", ISOCode2 = "TT", ISOCode3 = "TTO", PhoneCode = "+1-868", Flag = "https://flagsapi.com/TT/flat/64.png" },
                    new Country { CountryName = "Tunisia", ISOCode2 = "TN", ISOCode3 = "TUN", PhoneCode = "+216", Flag = "https://flagsapi.com/TN/flat/64.png" },
                    new Country { CountryName = "Turkey", ISOCode2 = "TR", ISOCode3 = "TUR", PhoneCode = "+90", Flag = "https://flagsapi.com/TR/flat/64.png" },
                    new Country { CountryName = "Turkmenistan", ISOCode2 = "TM", ISOCode3 = "TKM", PhoneCode = "+993", Flag = "https://flagsapi.com/TM/flat/64.png" },
                    new Country { CountryName = "Tuvalu", ISOCode2 = "TV", ISOCode3 = "TUV", PhoneCode = "+688", Flag = "https://flagsapi.com/TV/flat/64.png" },
                    new Country { CountryName = "Uganda", ISOCode2 = "UG", ISOCode3 = "UGA", PhoneCode = "+256", Flag = "https://flagsapi.com/UG/flat/64.png" },
                    new Country { CountryName = "Ukraine", ISOCode2 = "UA", ISOCode3 = "UKR", PhoneCode = "+380", Flag = "https://flagsapi.com/UA/flat/64.png" },
                    new Country { CountryName = "United Arab Emirates", ISOCode2 = "AE", ISOCode3 = "ARE", PhoneCode = "+971", Flag = "https://flagsapi.com/AE/flat/64.png" },
                    new Country { CountryName = "United Kingdom", ISOCode2 = "GB", ISOCode3 = "GBR", PhoneCode = "+44", Flag = "https://flagsapi.com/GB/flat/64.png" },
                    new Country { CountryName = "United States", ISOCode2 = "US", ISOCode3 = "USA", PhoneCode = "+1", Flag = "https://flagsapi.com/US/flat/64.png" },
                    new Country { CountryName = "Uruguay", ISOCode2 = "UY", ISOCode3 = "URY", PhoneCode = "+598", Flag = "https://flagsapi.com/UY/flat/64.png" },
                    new Country { CountryName = "Uzbekistan", ISOCode2 = "UZ", ISOCode3 = "UZB", PhoneCode = "+998", Flag = "https://flagsapi.com/UZ/flat/64.png" },
                    new Country { CountryName = "Vanuatu", ISOCode2 = "VU", ISOCode3 = "VUT", PhoneCode = "+678", Flag = "https://flagsapi.com/VU/flat/64.png" },
                    new Country { CountryName = "Venezuela", ISOCode2 = "VE", ISOCode3 = "VEN", PhoneCode = "+58", Flag = "https://flagsapi.com/VE/flat/64.png" },
                    new Country { CountryName = "Vietnam", ISOCode2 = "VN", ISOCode3 = "VNM", PhoneCode = "+84", Flag = "https://flagsapi.com/VN/flat/64.png" },
                    new Country { CountryName = "Yemen", ISOCode2 = "YE", ISOCode3 = "YEM", PhoneCode = "+967", Flag = "https://flagsapi.com/YE/flat/64.png" },
                    new Country { CountryName = "Zambia", ISOCode2 = "ZM", ISOCode3 = "ZMB", PhoneCode = "+260", Flag = "https://flagsapi.com/ZM/flat/64.png" },
                    new Country { CountryName = "Zimbabwe", ISOCode2 = "ZW", ISOCode3 = "ZWE", PhoneCode = "+263", Flag = "https://flagsapi.com/ZW/flat/64.png" }
                };

                context.Countries.AddRange(countries);
                await context.SaveChangesAsync();
            }


        }
    }
}
