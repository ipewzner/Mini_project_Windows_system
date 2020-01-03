using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Tools
    {
        //globel ToString func
        public static String TostringProperties<T>(this T t)
        {
            string result = "";
            foreach (PropertyInfo p in t.GetType().GetProperties())
            {
                result += String.Format("{0,-25} , {1}\n", p.Name, p.GetValue(t, null));
            }
            return result;
        }


         /*        
        public static T clone<T>(T info)
        {

            (T)Activator.CreateInstance(typeof(T), info);
            typeof(info) h;
            T t= new (info.GetType()) ;
            foreach (PropertyInfo p in info.GetType().GetProperties())
            {
               // p.GetValue(info, null);
              
                    p.SetValue(info, Convert.ChangeType(input, p.PropertyType));
                PropertyInfo t = info.GetType().GetProperties();

            }
            return info;

        }

           */


        //https://stackoverflow.com/questions/553824/serializing-an-array-of-integers-using-xmlserializer

        //2D matrix to 1D array
        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            T[] arrFlattened = new T[rows * columns];
            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    //var test = arr[i, j];
                    arrFlattened[i + j * rows] = arr[i, j];
                }
            }
            return arrFlattened;
        }

        //1D array to 2D matrix 
        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0);
            int columns = length / rows;
            T[,] arrExpanded = new T[rows, columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    arrExpanded[i, j] = arr[i + j * rows];
                }
            }
            return arrExpanded;
        }

    }
}
