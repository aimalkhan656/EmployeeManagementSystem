using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS
{
    public class AVLNode
    {      
            public Employee Data;
            public AVLNode Left, Right;
            public int Height;

            public AVLNode(Employee emp)
            {
                Data = emp;
                Height = 1;
            }

    }
}
