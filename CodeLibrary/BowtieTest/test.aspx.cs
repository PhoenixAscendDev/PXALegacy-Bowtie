using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BowtieTest
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var key = JB2.Bowtie.Utility.Appkey("vg-102");
            var key = JB2.Bowtie.Utility.ComputeWep40("vg-102");

            var appKey = BitConverter.ToString(key[0]).Replace("-","");

            var cmdCode = "1223344444";

            System.Text.StringBuilder keyAndCmd = new System.Text.StringBuilder();

            for(int i = 0; i < appKey.Length; i++)
            {
                keyAndCmd.Append(appKey[i]);
                keyAndCmd.Append(cmdCode[i]);
            }
            var checksum = JB2.Bowtie.Utility.GetChecksum(keyAndCmd.ToString(), 16);
            //var byte[] test2 = 23;
            

            char[] reverseChecksum = checksum.ToString().ToCharArray();
            Array.Reverse(reverseChecksum);

            string strchecksum = new string(reverseChecksum);


            System.Text.StringBuilder finalCmd = new System.Text.StringBuilder();

            finalCmd.Append(strchecksum.Substring(0, 3));
            finalCmd.Append(keyAndCmd.ToString());
            finalCmd.Append(strchecksum.Substring(3, 2));



        }
    }
}