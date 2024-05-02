using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ser_ver
{
    internal static class Serialization
    {
        public static List<T> Input<T>()
        {
            List<T> list = new List<T>();

            string fs = File.ReadAllText(@"C:\Users\user\Desktop\курсач_итог\курсач\data\user.json");
            if (fs.Length != 0)
            {
                list = JsonSerializer.Deserialize<List<T>>(fs) ?? new List<T>();
            }

            return list;
        }

        public static void Output<T>(List<T> list)
        {

            string us = JsonSerializer.Serialize(list);
            File.WriteAllText(@"C:\Users\user\Desktop\ser_ver\profilUser.json", us);
        }

        public static List<T> UserIn<T>()
        {
            List<T> user = new List<T>();

            string fs = File.ReadAllText(@"C:\Users\user\Desktop\ser_ver\profilUser.json");
            if (fs.Length != 0)
            {
                user = JsonSerializer.Deserialize<List<T>>(fs);
            }

            return user;
        }

        public static void UserOut<T>(List<T> user)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string us = JsonSerializer.Serialize(user);
            File.WriteAllText(@"C:\Users\user\Desktop\ser_ver\profilUser.json", us);
        }

        public static void LogOut<T>(List<T> user)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            using FileStream fs = new FileStream(@"C:\Users\user\Desktop\ser_ver\profilUser.json", FileMode.Create);
            JsonSerializer.SerializeAsync(fs, user, options);
        }

        public static List<T> AdminIn<T>()
        {
            List<T> admin = new List<T>();

            string fs = File.ReadAllText(@"C:\Users\user\Desktop\ser_ver\admin.json");
            if (fs.Length != 0)
            {
                admin = JsonSerializer.Deserialize<List<T>>(fs);
            }

            return admin;
        }

        public static void AdminOut<T>(List<T> admin)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            using FileStream fs = new FileStream(@"C:\Users\user\Desktop\ser_ver\admin.json", FileMode.Create);
            JsonSerializer.SerializeAsync(fs, admin, options);
        }

        public static Dictionary<string, int> PriceOut()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();

            string fs = File.ReadAllText(@"C:\Users\user\Desktop\ser_ver\PriceOfService.json");
            if (fs.Length != 0)
            {
                list = JsonSerializer.Deserialize<Dictionary<string, int>>(fs) ?? new Dictionary<string, int>();
            }

            return list;
        }

        public static void PriceIn(Dictionary<string, int> list)
        {

            string us = JsonSerializer.Serialize(list);
            File.WriteAllText(@"C:\Users\user\Desktop\ser_ver\PriceOfService.json", us);
        }

        public static List<string> AllServicesOut()
        {
            List<string> list = new List<string>();

            string fs = File.ReadAllText(@"C:\Users\user\Desktop\ser_ver\AllServices.json");
            if (fs.Length != 0)
            {
                list = JsonSerializer.Deserialize<List<string>>(fs) ?? new List<string>();
            }

            return list;
        }

        public static void AllServicesIn(List<string> list)
        {
            string us = JsonSerializer.Serialize(list);
            File.WriteAllText(@"C:\Users\user\Desktop\ser_ver\AllServices.json", us);
        }

        public static void AllMassegesIn(Dictionary<string, List< string>> allMasseges)
        {
            string us = JsonSerializer.Serialize(allMasseges);
            File.WriteAllText(@"C:\Users\user\Desktop\ser_ver\AllMasseges.json", us);
        }

        public static Dictionary<string, List<string>> AllMassegesOut()
        {
            Dictionary<string, List<string>> list = new Dictionary<string, List<string>>();

            string fs = File.ReadAllText(@"C:\Users\user\Desktop\ser_ver\AllMasseges.json");
            if (fs.Length != 0)
            {
                list = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(fs) ?? new Dictionary<string, List<string>>();
            }

            return list;
        }
    }
}
