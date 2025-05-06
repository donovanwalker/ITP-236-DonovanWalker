using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Data.SqlClient;

namespace Project2
{
    public class Program
    {
        // Donovan Walker
        static void CreateParts()
        {
            // Read the Part from the XML data
            XmlDocument doc = new XmlDocument();
            doc.Load("Part.xml");

            // Database connection string
            string connectionString = "Data Source=localhost;Initial Catalog=Sales;Integrated Security=True"; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Select all parts first to avoid duplicate key issues.
                HashSet<int> existingPartIds = new HashSet<int>();
                using (SqlCommand selectCommand = new SqlCommand("SELECT PartId FROM Sales.Part", connection))
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        existingPartIds.Add((int)reader["PartId"]);
                    }
                }

                // Get the list of Part elements.
                XmlNodeList partNodes = doc.SelectNodes("/Parts/Part");
                if (partNodes != null)
                {
                    foreach (XmlNode partNode in partNodes)
                    {
                         //check for null
                        if (partNode.Attributes?["PartId"] == null || partNode.Attributes?["Name"] == null)
                            continue;
                            
                        int partId = int.Parse(partNode.Attributes["PartId"].Value);
                        string name = partNode.Attributes["Name"].Value;

                        // Create a Part object from the XML data that you just read
                        Part part = new Part
                        {
                            PartId = partId,
                            Name = name
                        };

                        // Insert the Part object into the database Sales.Part table
                        if (!existingPartIds.Contains(partId)) //check if partId exists
                        {
                            string insertQuery = "INSERT INTO Sales.Part (PartId, Name) VALUES (@PartId, @Name)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@PartId", part.PartId);
                                insertCommand.Parameters.AddWithValue("@Name", part.Name);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Part with PartId {partId} already exists in the database.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Part elements found in the XML file.");
                }
            }
            Console.WriteLine("Parts have been processed and inserted into the database (if they didn't already exist).");

        }

        static void CreatePartsXml()
        {
            // Donovan Walker
            // Create a new Part.XML file
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create("Part.xml", settings);

            writer.WriteStartDocument();
            writer.WriteComment("Part Data");
            writer.WriteStartElement("Parts");

            // Use a SELECT statement to read the Sales.Part table
            string connectionString = "Data Source=localhost;Initial Catalog=Sales;Integrated Security=True"; // Replace with your actual connection string
            string selectQuery = "SELECT PartId, Name FROM Sales.Part";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create Part Elements with attributes: PartId and Name.
                            writer.WriteStartElement("Part");
                            writer.WriteAttributeString("PartId", reader["PartId"].ToString());
                            writer.WriteAttributeString("Name", reader["Name"].ToString());
                            writer.WriteEndElement(); // Part
                        }
                    }
                }
            }

            writer.WriteEndElement(); // Parts
            writer.WriteEndDocument();

            // Close the Part.XML document
            writer.Close();

            Console.WriteLine("Part.xml has been created.");
        }

        static void Main(string[] args)
        {
             CreatePartsXml(); //generate the xml file
             CreateParts(); //create parts and insert them
        }
    }
}
