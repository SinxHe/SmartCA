using System;

namespace SmartCA.Infrastructure
{
    public static class SecurityHelper
    {
        public static void CheckParameter(string param, bool checkForNull, 
            bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
        {
            if (param == null)
            {
                if (checkForNull)
                {
                    throw new ArgumentNullException(paramName);
                }
            }
            else
            {
                param = param.Trim();
                if (checkIfEmpty && (param.Length < 1))
                {
                    throw new ArgumentException("Parameter cannot be empty", 
                        paramName);
                }
                if ((maxSize > 0) && (param.Length > maxSize))
                {
                    throw new ArgumentException("Parameter too long", 
                        paramName);
                }
                if (checkForCommas && param.Contains(","))
                {
                    throw new ArgumentException("Parameter cannot contain commas", 
                        paramName);
                }
            }
        }

        public static void CheckPasswordParameter(string param, int maxSize, 
            string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
            if (param.Length < 1)
            {
                throw new ArgumentException("Parameter cannot be empty", 
                    paramName);
            }
            if ((maxSize > 0) && (param.Length > maxSize))
            {
                throw new ArgumentException("Parameter too long", 
                    paramName);
            }
        }

        public static bool ValidateParameter(string param, bool checkForNull, 
            bool checkIfEmpty, bool checkForCommas, int maxSize)
        {
            if (param == null)
            {
                return !checkForNull;
            }
            param = param.Trim();
            return (((!checkIfEmpty || (param.Length >= 1)) && 
                ((maxSize <= 0) || (param.Length <= maxSize))) && 
                (!checkForCommas || !param.Contains(",")));
        }
    }
}
