using APIDiff.DataAccess;
using APIDiff.DataAccess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace APIDiff
{
    public static class JsonUtility
    {
        //Function used to get data from JSON file
        public static DataDiff ReadFromJson()
        {
            using (StreamReader r = new StreamReader(@"..\APIDiff.DataAccess\inputs.json"))
            {
                string json = r.ReadToEnd();

                return JsonConvert.DeserializeObject<DataDiff>(json);
            }
        }

        //Function used to write data to JSON file
        public static void WriteJsonIntoFile(string result)
        {
            File.WriteAllTextAsync(@"..\APIDiff.DataAccess\inputs.json", result);
        }

        //Function for decoding Base64
        //returns array of bytes
        public static byte[] Base64Decode(string base64EncodedData)
        {
            byte[] data = Convert.FromBase64String(base64EncodedData);
            //string decodedString = Encoding.UTF8.GetString(data);

            return data;
        }

        ///Function that calculates offset and length given two binary data
        ///returns array of objects with calculated offset and length
        public static IEnumerable<Diff> FindOffset(byte[] leftDecoded, byte[] rightDecoded)
        {
            List<Diff> diffs = new List<Diff>();

            int offset = 0;
            int length = 0;

            //check marks if previous pair of bytes were not matching
            bool check = false;

            for (int i = 0; i < leftDecoded.Length; i++)
            {
                if (leftDecoded[i] != rightDecoded[i] && !check)
                {
                    offset = i;
                    length++;
                    check = true;
                }
                else if (leftDecoded[i] != rightDecoded[i] && check)
                {
                    length++;
                }
                else
                {
                    if (check)
                    {
                        diffs.Add(new Diff { Offset = offset, Length = length });

                        check = false;
                        length = 0;
                        offset = 0;
                    }
                }
            }
            if (offset != 0)
                diffs.Add(new Diff { Offset = offset, Length = length });

            return diffs;
        }
    }
}
