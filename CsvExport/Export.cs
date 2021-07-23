using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using static Models.ScraperModels;
using System.Globalization;

namespace CsvExport
{
    public class Export
    {
        public void ToCSV(List<Lead> leads, string filePath)
        {
            //List<string> str = new List<string>();
            //str.Add("info@apchicago.com");
            //str.Add("mshamis@apchicago.com");
            //str.Add("lauri@apchicago.com");
            //str.Add("jessica@apchicago.com");
            //str.Add("mirelle@apchicago.com");
            //str.Add("hermilo@apchicago.com");
            //leads.Add(new Lead
            //{
            //    Website = "test website",
            //    //Email = $"info@apchicago.com {"@\"} nshamis@apchicago.com {"@\"}  mshamis@apchicago.com {"\""}  lauri@apchicago.com {"\""}  jessica@apchicago.com {"\""}  mirelle@apchicago.com {"\""}  hermilo@apchicago.com"
            //    Email = $"info@apchicago.com",
            //    ListEmails = str
            //});
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecord(new Lead {
                    Website = "Website",
                    Email = "Email",
                    Facebook = "Facebook",
                    Twitter = "Twitter",
                    LinkedIn = "LinkedIn",
                    Instagram = "Instagram",
                });
                
                csv.NextRecord();
                foreach (var items in leads)
                {
                    csv.WriteRecord(items);
                    csv.NextRecord();
                    items.ListEmails.Remove(items.Email);
                    foreach (var emails in items.ListEmails)
                    {
                        csv.WriteRecord(new Lead {
                            Email = emails
                        });
                        csv.NextRecord();
                    }

                }
                
            }
        }
        public void DatatableToCsv(DataTable leadsTable, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                //headers    
                for (int i = 0; i < leadsTable.Columns.Count; i++)
                {
                    sw.Write(leadsTable.Columns[i]);
                    if (i < leadsTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (DataRow dr in leadsTable.Rows)
                {
                    for (int i = 0; i < leadsTable.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < leadsTable.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
            }
        }


    }
}
