using SmartDealer.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SmartDealer.Service.Services
{
    public class BaseService
    {
        public BaseService()
        {
        }

        protected CustomError CreateCustomError(Exception ex)
        {
            return new CustomError()
            {
                ErrorCode = "100",
                ErrorMessage = "There was an error processing your request",
                ExceptionObject = ex
            };
        }

        protected T DeserializeFromXmlString<T>(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xmlString))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        protected string ValidateString(object str)
        {
            string ReString = "";

            if (str != null)
                ReString = str.ToString().TrimEnd();

            return ReString;
        }
    }
}
