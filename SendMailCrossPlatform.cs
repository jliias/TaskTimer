// --------------------------------------------------------------------
// Cross Platform send mail
// Author: Juha Liias / WestSloth Games
// --------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class SendMailCrossPlatform : MonoBehaviour {

     // <param name="email"> myMail@something.com</param>
     // <param name="subject">my subject</param>
     // <param name="body">my body</param>
     public static void SendEmail (string email,string subject,string body)
     {
         subject = MyEscapeURL(subject);
         body = MyEscapeURL(body);
         Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
     }
     public static string MyEscapeURL (string url)
     {
         return WWW.EscapeURL(url).Replace("+","%20");
     }
 }
