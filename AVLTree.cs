using System;
using System.Collections.Generic;

namespace EMS
{
    public class AVLTree
    {
        public AVLNode Root;

        int Height(AVLNode n)
        {
            return n == null ? 0 : n.Height;
        }

        int GetBalance(AVLNode n)
        {
            return n == null ? 0 : Height(n.Left) - Height(n.Right);
        }

        AVLNode RightRotate(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        AVLNode LeftRotate(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public AVLNode Insert(AVLNode node, Employee emp)
        {
            if (node == null)
                return new AVLNode(emp);

            if (emp.EmployeeCode < node.Data.EmployeeCode)
                node.Left = Insert(node.Left, emp);

            else if (emp.EmployeeCode > node.Data.EmployeeCode)
                node.Right = Insert(node.Right, emp);

            else
                return node; // duplicate not allowed

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            // LL
            if (balance > 1 && emp.EmployeeCode < node.Left.Data.EmployeeCode)
                return RightRotate(node);

            // RR
            if (balance < -1 && emp.EmployeeCode > node.Right.Data.EmployeeCode)
                return LeftRotate(node);

            // LR
            if (balance > 1 && emp.EmployeeCode > node.Left.Data.EmployeeCode)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // RL
            if (balance < -1 && emp.EmployeeCode < node.Right.Data.EmployeeCode)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        public void InOrder(AVLNode node, List<Employee> list)
        {
            if (node != null)
            {
                InOrder(node.Left, list);
                list.Add(node.Data);
                InOrder(node.Right, list);
            }
        }
    }
}