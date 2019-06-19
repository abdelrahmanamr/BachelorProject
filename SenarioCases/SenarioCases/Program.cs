using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace SenarioCases
{
    class Program
    {
        public static String getFeedaxisPosition(float tcpSpeed) 
        {
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://localhost:8890/sparql"), "http://localhost:8890/test1");
            String query = "SELECT ?fa WHERE{?s <http://bedrock/hasTCP_Speed_(53)>" + tcpSpeed + " . ?s <http://bedrock/hasFeed_axis_position_(5100)> ?fa .}";
            SparqlResultSet results = endpoint.QueryWithResultSet("SELECT ?fa WHERE{?s <http://bedrock/hasTCP_Speed_(53)> "+ tcpSpeed +" . ?s <http://bedrock/hasFeed_axis_position_(5100)> ?fa .}");
            String res = "";
            //Console.WriteLine( results.Count);
            foreach (SparqlResult result in results)
            {
                //    res += result.ToString().Split('=')[result.ToString().Split('=').Length - 1].Split('/')[result.ToString().Split('=')[result.ToString().Split('=').Length - 1].Split('/').Length - 1].ToString() +"\n";
                res += result.ToString().Split('=')[1] + "\n";
            }
            return res;
        }
        public static String getTcpSpeed(int feedaccess,int programPointer) // First Case Scenario (getting the tcp speed from the feed access position and program pointer)
        {
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://localhost:8890/sparql"), "http://localhost:8890/test1");
            SparqlResultSet results = endpoint.QueryWithResultSet("SELECT ?tcpSpeed WHERE{?s <http://bedrock/hasTCP_Speed_(53)> ?tcpSpeed . ?s <http://bedrock/hasFeed_axis_position_(5100)> ?fa . ?s <http://bedrock/hasProgram_pointer_(5102)> ?pp.FILTER(?fa= "+feedaccess+" && ?pp= "+programPointer+")}");
            String res = "";
            Console.WriteLine(results.Count);
            foreach (SparqlResult result in results)
            {
                //    res += result.ToString().Split('=')[result.ToString().Split('=').Length - 1].Split('/')[result.ToString().Split('=')[result.ToString().Split('=').Length - 1].Split('/').Length - 1].ToString() +"\n";
                res += result.ToString().Split('=')[1] + "\n";
                
            }
            return res;
        }
        public static String getPyrometers(int tcpSpeed, int lampPower) // Second Case Scenario (getting all pyrometers from the tcp speed and the lamp power)
        {
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://localhost:8890/sparql"), "http://localhost:8890/test1");
            SparqlResultSet results = endpoint.QueryWithResultSet("SELECT * WHERE {?s <http://bedrock/has_Lamp_N%EF%BF%BD1_(1001)> ?lamp.?s <http://bedrock/has_TCP_Speed_(53)> ?tcp.?s <http://bedrock/has_Pyrometer_N%EF%BF%BD1_(27)> ?p1.?s <http://bedrock/has_Pyrometer_N%EF%BF%BD2_(29)> ?p2.?s <http://bedrock/has_Pyrometer_N%EF%BF%BD3_(31)> ?p3. ?s <http://bedrock/has_Pyrometer_N%EF%BF%BD4_(33)> ?p4.FILTER(?tcp=0 && ?lamp=100).}");
            String res = "";
            Console.WriteLine(results.Count);
            foreach (SparqlResult result in results)
            {
                res += result.ToString() + "\n";
            }
            return res;
        }
        public static String getErrorsInCertainPlaceOfLane(int feedaccess, int programPointer) // Third case Scenario (getting Errors in certain places of lane giving the feed access position and the program counter for range of date )
        {
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://localhost:8890/sparql"), "http://localhost:8890/test1");
            SparqlResultSet results = endpoint.QueryWithResultSet("SELECT * WHERE {?s <http://bedrock/hasDate> ?date.?s2 <http://bedrock/hasStart_date> ?sd.?s2 <http://bedrock/hasEnd_date> ?ed.?s2 <http://bedrock/hasDefault> ?error.?s <http://bedrock/has_Feed_axis_position_(5100)> ?fa.?s <http://bedrock/has_Program_pointer_(5102)> ?pp.FILTER(xsd:dateTime(?sd)<=xsd:dateTime(?date)&& xsd:dateTime(?date)<=xsd:dateTime(?ed) && ?fa="+feedaccess+" && ?pp="+programPointer+")}");
            String res = "";
            Console.WriteLine(results.Count);
            foreach (SparqlResult result in results)
            {
                //    res += result.ToString().Split('=')[result.ToString().Split('=').Length - 1].Split('/')[result.ToString().Split('=')[result.ToString().Split('=').Length - 1].Split('/').Length - 1].ToString() +"\n";
                res += result.ToString() + "\n";

            }
            return res;
        }
        public static String getErrorsInCertainPlaceOfLaneEnhanced(int feedaccess, int programPointer) // Third case Scenario (getting Errors in certain places of lane giving the feed access position and the program counter for exact date )
        {
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://localhost:8890/sparql"), "http://localhost:8890/test1");
            SparqlResultSet results = endpoint.QueryWithResultSet("SELECT * WHERE {?s <http://bedrock/has_Feed_axis_position_(5100)> ?fa.?s <http://bedrock/has_Program_pointer_(5102)> ?pp. ?s <http://bedrock/hasDate> ?date . ?s1 <http://bedrock/hasStart_date> ?sd.?s1 <http://bedrock/hasDefault> ?error .FILTER(?fa ="+ feedaccess+" && ?pp="+ programPointer + " && xsd:dateTime(?date)= xsd:dateTime(?sd))}");
            String res = "";
            Console.WriteLine(results.Count);
            foreach (SparqlResult result in results)
            {
                //    res += result.ToString().Split('=')[result.ToString().Split('=').Length - 1].Split('/')[result.ToString().Split('=')[result.ToString().Split('=').Length - 1].Split('/').Length - 1].ToString() +"\n";
                res += result.ToString() + "\n" +"\n";

            }
            return res;
        }
        public static void Main(string[] args)
        {
            //Console.WriteLine(getErrorsInCertainPlaceOfLane(2411, 10));
            Console.WriteLine(getErrorsInCertainPlaceOfLaneEnhanced(2411, 10));
            Console.ReadLine();
            //while (true)
            //{


            //    // code for the getting the TCP query
            //    //Console.WriteLine("Please enter The TCP speed: ");
            //    //float grandchildName;
            //    //string input = Console.ReadLine();
            //    //if (float.TryParse(input, out grandchildName))
            //    //{
            //    //    Console.WriteLine(getFeedaxisPosition(grandchildName));
            //    //}
            //    //else
            //    //{
            //    //    Console.WriteLine("incorrect input format");
            //    //}

              
            //}

        }
    }
}

