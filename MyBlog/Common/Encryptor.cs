using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MyBlog.Common
{
    public static class Encryptor
    {

        public static string MD5Hash(string text)  //Nhận vào một chuỗi string
        {
            MD5 md5 = new MD5CryptoServiceProvider(); // tạo biến

            //Tính toán chuyển chuỗi nhận sang Hash
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //Nhận hàm băm
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString(); // Trả về dãy tring sau khi đã qua hàm Hash
        }
    }
}