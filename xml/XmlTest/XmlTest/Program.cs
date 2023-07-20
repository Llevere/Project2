using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace XmlTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../ghg-canada.xml"; 

            // Load the XML file into an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading XML file: {ex.Message}");
                return;
            }
            // Get the region node with name="Alberta"
            XmlNode? regionNode = xmlDoc.SelectSingleNode("//region[@name='Alberta']");

            if (regionNode != null)
            {
                // Loop through the child elements of the region node (source elements)
                foreach (XmlNode? sourceNode in regionNode.ChildNodes)
                {
                    string? description = sourceNode.Attributes["description"]?.Value;

                    // Loop through the emissions elements under each source node
                    foreach (XmlNode emissionsNode in sourceNode.ChildNodes)
                    {
                        int year = int.Parse(emissionsNode.Attributes["year"].Value);
                        double emissions = double.Parse(emissionsNode.InnerText);

                        Console.WriteLine($"Region: Alberta, Source: {description}, Year: {year}, Emissions: {emissions}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Region 'Alberta' not found in the XML.");
            }
        }
    }
}