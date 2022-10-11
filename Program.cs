// See https://aka.ms/new-console-template for more information
{
    DateTime date1 = DateTime.Now;
    string date2 = (date1.ToString("MMddyy"));
    Console.WriteLine("Date1: {0} ", date1);
    Console.Write("Input the Date: ");
    string date3 = Console.ReadLine();
    if (date3 == "")
    { date3 = date2; }
    string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    string source = myDocuments + @"\ADAREPTs\adarepts.d" + date3 + @".txt";
    string target = myDocuments + @"\ADAREPTs\adarepts.d" + date3 + @".Summary2.txt";
    delold(target);
    //adarept(source);
    adarepAsync(source,target);
    static async Task adarepAsync(string fileName, string target)
    //static void adarept(string fileName)
    {
        string[] lines = System.IO.File.ReadAllLines(fileName);
        bool b = false;
        int dbid = 0;
        string dbids = " ";
        int max = 9;
        // Display the file contents by using a foreach loop.
        Console.WriteLine("Contents of {0}", fileName);
        foreach (string line in lines)
        {
            //    string line2 = line;
            //     if (!((line.StartsWith("*")) || (line.StartsWith("1")) || line.Contains(@"***") || (line.StartsWith("\r\n"))))
            //    {
            //if ((line.ToUpper().Contains(@"* FILE OPTIONS *")) || (line.ToUpper().Contains(@"CONTENTS OF PPT")))
            //{
            //    b = false;
            //}
            if (line.ToUpper().Contains(@"DATA BASE NAME          =")) // || (line.ToUpper().Contains(@"DATA BASE NUMBER        =")))
            { b = false; }
            if (line.ToUpper().Contains(@"DATA BASE NUMBER        ="))
            {
                //Console.WriteLine(line);
                dbids = line.Substring(31, 5);
                //       Console.WriteLine(dbids);
                try
                {
                    dbid = Int32.Parse(dbids);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(line);
                    Console.WriteLine(e.Message);
                }
            }
            if (b && line.Length > 3)
            {
                int n1 = 1, u1 = 1, a1 = 1, ac21 = 1, d1 = 1, mi1 = 1;
                string mi = line.Substring(50, 8);
                try
                {
                    mi1 = Int32.Parse(mi);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(line);
                    Console.WriteLine(e.Message);
                }
                string n = line.Substring(60, 3);
                                try
                {
                    n1 = Int32.Parse(n);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(line);
                    Console.WriteLine(e.Message);
                }
                string u = line.Substring(64, 3);
                try
                {
                    u1 = Int32.Parse(u);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(line);
                    Console.WriteLine(e.Message);
                }
                string a = line.Substring(68, 3);
                try
                {
                    a1 = Int32.Parse(a);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(line);
                    Console.WriteLine(e.Message);
                }
                string ac2 = line.Substring(72, 3);
                try
                {
                    ac21 = Int32.Parse(ac2);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(line);
                    Console.WriteLine(e.Message);
                }
                string d = line.Substring(76, 3);
                try
                {
                    d1 = Int32.Parse(d);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(line);
                    Console.WriteLine(e.Message);
                }
                //Console.WriteLine("INDEXS  {0} ", n);
                //Console.WriteLine("INDEXS  {0} ", u);
                //Console.WriteLine("INDEXS  {0} ", a);
                //Console.WriteLine("INDEXS  {0} ", ac2);
                //Console.WriteLine("INDEXS  {0} ", d);
                //Console.WriteLine("INDEXS  {0} {1} {2} {3} {4} ", n, u, a, ac2, d);
                //Console.WriteLine("INDEXS  {0} {1} {2} {3} {4} ", n1, u1, a1, ac21, d1);
                if ((n1 > max) || (u1 > max) || (a1 > max) || (d1 > max) || (ac21 > max) || ((mi1 > 16777000) && (mi1 < 16777500)))
                {
                    string line1 = @"DB:" + dbids + @" " + line;
                    Console.WriteLine(line1);
                    using StreamWriter file = new(target, append: true);
                    await file.WriteLineAsync(line1);
                }
            }
            //  if (line.ToUpper().Contains(@"CONTENTS OF DATABASE"))
            if (line.ToUpper().Contains(@"N   U   A AC2   D"))
            {
                b = true;
            }
        }
    }
}
static void delold(string target)
{
    // Files to be deleted    
    try
    {
        // Check if file exists with its full path    
        if (File.Exists(target))
        {
            // If file found, delete it    
            File.Delete(target);
            Console.WriteLine("{0} File deleted.", target);
        }
        else Console.WriteLine("{0} File not found", target);
    }
    catch (IOException ioExp)
    {
        Console.WriteLine(ioExp.Message);
    }
}