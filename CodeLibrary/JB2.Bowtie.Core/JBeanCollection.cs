using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class JBeanCollection : JB2.Common.BaseCollection<JbeanTreasuryNote>
    {
        public static implicit operator JBeanBag(JBeanCollection c)
        {
            int total = 0;
            foreach(var token in c)
            {
                total = total + (int)token.Amount;
            }

            return (JBeanBag)total;
        }

        public static implicit operator double(JBeanCollection c)
        {
            var bag = (JBeanBag)c;

            return Convert.ToDouble((int)bag);

        }
    }
}
