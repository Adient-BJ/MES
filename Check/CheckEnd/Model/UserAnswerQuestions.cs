using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckEnd.Model
{
    public class UserAnswerQuestions
    {

        public static string BarCode { get; set; } 

        public static Dictionary<string, string[]> UserAnswer { get; set; }


        public static void AddUserAnswer(string key, string[] value)
        {
            if (UserAnswer == null)
            {
                UserAnswer = new Dictionary<string, string[]>();
                UserAnswer.Add(key, value);
            }

            else
            {             
                if (UserAnswer.ContainsKey(key))
                {

                    string[] v = UserAnswer[key];
                    string val = value[0];

                    List<string> va = new List<string>(v);
                    va.Add(val);

                    string[] valu = va.ToArray();
                    UserAnswer[key] = valu;
                }
                else
                {
                    UserAnswer.Add(key, value);
                }

            }

        }


        public static void RemoveUserAnswer(string key ,string value)
        {
            string[] v = UserAnswer[key];
            List <string> va = new List<string>(v);
            va.Remove(value);

            string[] val = va.ToArray();
            UserAnswer[key] = val;
            //UserAnswer.Remove(key);
        }


        


    }
}
